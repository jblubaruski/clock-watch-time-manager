using BusinessManagementLibrary.TimeManagement;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace ClockWatchTimeManager.ViewModels;

/*
 * View model for the create time card page
 */
partial class CreateTimeCardViewModel : ObservableObject
{
    /*
     * CONSTANTS
     */

    // time and date shown when the time of an item in the time card is null
    public const string DEFAULT_DATETIME = "--:--:-- -- -- --------- --- --, ----";
    // duration shown when the user is not currently clocked in
    public const string DEFAULT_TIME_DURATION = "--:--:--";
    // duration showing a time span of zero hours, minutes and seconds
    public const string ZERO_TIME_DURATION = "00:00:00";
    // format for displaying the time and date
    public const string DEFAULT_DATETIME_FORMAT = "{0:h:mm:ss tt -- dddd MMM dd, yyyy}";
    // the amount of time between clock ticks
    public static readonly TimeSpan TimeclockTickInterval = new(0, 0, 1);

    /*
     * BINDABLE PROPERTIES
     */

    // whether or not the time card saved notification is visible
    private bool _timeCardSavedNotifVisible = false;
    public bool TimeCardSavedNotifVisible
    {
        get => _timeCardSavedNotifVisible;
        set => SetProperty(ref _timeCardSavedNotifVisible, value);
    }

    // whether or not the clock in button and command is enabled
    private bool _clockInEnabled = true;
    public bool ClockInEnabled
    {
        get => _clockInEnabled;
        set => SetProperty(ref _clockInEnabled, value);
    }

    // whether or not the clock out button and command is enabled
    private bool _clockOutEnabled = false;
    public bool ClockOutEnabled
    {
        get => _clockOutEnabled;
        set => SetProperty(ref _clockOutEnabled, value);
    }

    // the time of the start of the time card's work period
    private string _clockInDateTime = DEFAULT_DATETIME;
    public string ClockInDateTime
    {
        get => _clockInDateTime;
        set => SetProperty(ref _clockInDateTime, value);
    }

    // the time since the user clocked in
    private string _timeSinceClockIn = DEFAULT_TIME_DURATION;
    public string TimeSinceClockIn
    {
        get => _timeSinceClockIn;
        set => SetProperty(ref _timeSinceClockIn, value);
    }

    /*
     * PROPERTIES
     */

    // the ID of the current timecard (is -1 if theres no current timecard)
    private int CurrentTimecardID { get; set; } = -1;
    // the dispatcher used by the program to act as a clock
    private IDispatcher? Dispatcher { get; set; } = null;
    // whether or not the clock should be ticking
    internal bool ShouldClockTick { get; set; } = false;

    /*
     * CONSTRUCTORS
     */
    public CreateTimeCardViewModel()
    {
        SetupDispatcher();
    }

    /*
     * COMMANDS
     */
    
    /* 
     * The command that runs when the user clicks the clock in button.
     * Sets up a new timecard data container with the clock in time and adds the card to the catalog, storing the returned ID in CurrentTimecardID.
     * Updates the clock in time display to the clock in time.
     * Starts the timeclock.
     */
    [RelayCommand(CanExecute = nameof(ClockInCanExecute))]
    void ClockIn()
    {
        ClockInEnabled = false;
        ClockOutEnabled = true;
        TimeCardSavedNotifVisible = false;

        DateTime clockInTime = DateTime.Now;
        TimecardData newCardData = new(clockInTime, null);
        CurrentTimecardID = App.catalog.AddEntry(newCardData);

        App.logger.LogEntry($"Clocked in at {clockInTime}");

        ClockInDateTime = string.Format(DEFAULT_DATETIME_FORMAT, clockInTime);
        
        StartTimeClock();
    }
    bool ClockInCanExecute()
    {
        return ClockInEnabled;
    }

    /*
     * The command that runs when the user clicks the clock out button.
     * Sets the current time card from the catalog and sets its clock out time.
     * Resets the clock in time display to its default state.
     * Stops the timeclock.
     * 
     * Only updates the timecard if a timecard with CurrentTimecardID is stored in the catalog
     */
    [RelayCommand(CanExecute = nameof(ClockOutCanExecute))]
    void ClockOut()
    {
        ClockInEnabled = true;
        ClockOutEnabled = false;
        TimeCardSavedNotifVisible = true;

        Timecard? currentTimecard = App.catalog.GetEntry(CurrentTimecardID);

        if (currentTimecard != null)
        {
            DateTime clockOutTime = DateTime.Now;
            try
            {
                currentTimecard.ClockOutTime = clockOutTime;
                App.logger.LogEntry($"Logged Time card:\n{currentTimecard}");
            }
            catch (ArgumentException)
            {
                App.logger.LogEntry($"clockOutTime ({clockOutTime}) is less than or equal to the current timecard's ClockInTime ({currentTimecard.ClockInTime})");
            }
        }
        else
        {
            App.logger.LogEntry($"The timecard corresponding to timecardID {CurrentTimecardID} does not exist in the catalog");
        }

        ClockInDateTime = DEFAULT_DATETIME;

        StopTimeClock();
    }
    bool ClockOutCanExecute()
    {
        return ClockOutEnabled;
    }

    /*
     * METHODS
     */

    /*
     * Resets the page to its base state
     */
    private void ResetPage()
    {
        ClockInEnabled = true;
        ClockOutEnabled = false;
        TimeCardSavedNotifVisible = false;

        ClockInDateTime = DEFAULT_DATETIME;
        TimeSinceClockIn = DEFAULT_TIME_DURATION;
    }

    /*
     * Returns true if a current timecard is assigned to the page
     */
    public bool IsTimecardAssignesAsCurrent() => CurrentTimecardID > -1;

    /*
     * Sets up the dispatcher used to dispatch events to the UI thread
     */
    public void SetupDispatcher()
    {
        DispatcherProvider dispatcherProvider = new();
        Dispatcher = dispatcherProvider.GetForCurrentThread();
    }

    /*
     * Starts the time clock
     */
    public void StartTimeClock()
    {
        if (Dispatcher != null)
        {
            ShouldClockTick = true;
            TimeSinceClockIn = ZERO_TIME_DURATION;
            Dispatcher.StartTimer(TimeclockTickInterval, TickTimeClock);
        }
        else
        {
            App.logger.LogEntry("The dispatcher for the page is null and the timeclock could not be started");
        }
    }

    /*
     * Stops the time clock
     */
    public void StopTimeClock()
    {
        TimeSinceClockIn = DEFAULT_TIME_DURATION;
        ShouldClockTick = false;
    }

    /*
     * Calculates the timespan between the current time and the current time card's clock in time.
     * Updates the time since clock in display to show how much time has passed since the user clocked in.
     * Returns false when the dispatcher should stop calling this method.
     * Only runs internal logic when ShouldClockTick is true
     */
    public bool TickTimeClock()
    {
        if (ShouldClockTick)
        {
            Timecard? currentTimecard = App.catalog.GetEntry(CurrentTimecardID);

            if (currentTimecard != null)
            {
                DateTime currentTime = DateTime.Now;
                DateTime? clockInTime = currentTimecard.ClockInTime;

                if (clockInTime != null)
                {
                    TimeSpan timeSinceClockIn = currentTime.Subtract((DateTime)clockInTime);
                    TimeSinceClockIn = timeSinceClockIn.ToString(@"hh\:mm\:ss");
                }
            }
            // if the current timecard doesnt exist in the catalog
            else
            {
                App.logger.LogEntry($"the timecard with the ID {CurrentTimecardID} does not exist in the catalog and the timeclock has stopped");
                ResetPage();
                ShouldClockTick = false;
            }
        }

        return ShouldClockTick;
    }
}
