using BusinessManagementLibrary.TimeManagement;
using ClockWatchTimeManager.Models;
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
    // default break description shown when no description is present
    public const string DEFAULT_BREAK_DESCRIPTION = "Break description...";

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

    // whether or not the start break button is enabled
    private bool _startBreakEnabled = false;
    public bool StartBreakEnabled
    {
        get => _startBreakEnabled;
        set => SetProperty(ref _startBreakEnabled, value);
    }

    // whether or not the end break button is enabled
    private bool _endBreakEnabled = false;
    public bool EndBreakEnabled
    {
        get => _endBreakEnabled;
        set => SetProperty(ref _endBreakEnabled, value);
    }

    // whether or not the break saved notification is visible
    private bool _breakSavedNotifVisible = false;
    public bool BreakSavedNotifVisible
    {
        get => _breakSavedNotifVisible;
        set => SetProperty(ref _breakSavedNotifVisible, value);
    }

    // the start time of the current break
    private string _currentBreakStartTime = DEFAULT_DATETIME;
    public string CurrentBreakStartTime
    {
        get => _currentBreakStartTime;
        set => SetProperty(ref _currentBreakStartTime, value);
    }

    // the end time of the current break
    private string _currentBreakEndTime = DEFAULT_DATETIME;
    public string CurrentBreakEndTime
    {
        get => _currentBreakEndTime;
        set => SetProperty(ref _currentBreakEndTime, value);
    }

    /*
     * PROPERTIES
     */

    // the ID of the current timecard (is -1 if theres no current timecard)
    private int CurrentTimecardID { get; set; } = -1;
    // the timeclock used to update the clock in duration display
    private Timeclock? Timeclock { get; } = null;
    // the reciever that gets notifications from the timeclock whenever it ticks
    private NotificationReciever<TimeclockDisplayInfo>? TickReciever { get; } = null;

    /*
     * EVENTS
     */

    internal event Action? AddBreakEntryBoxRequested;
    internal event Action? ClearBreakDisplayRequested;

    /*
     * CONSTRUCTOR
     */
    public CreateTimeCardViewModel()
    {
        // set up the timeclock
        DispatcherProvider dispatcherProvider = new();
        IDispatcher? dispatcher = dispatcherProvider.GetForCurrentThread();

        if (dispatcher != null)
        {
            TimeSpan timeclockTickInterval = new(0, 0, 1);
            Timeclock = new(timeclockTickInterval, dispatcher);
            TickReciever = new(UpdateDurationDisplay, TimeclockClosed, LogError);
        }
        else
        {
            App.logger.LogEntry
                (
                    $"In {nameof(CreateTimeCardViewModel)}:\n",
                    "The DispatchProvider could not get an instance of IDispatcher for the current thread"
                );
        }
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

        StartBreakEnabled = true;

        DateTime clockInTime = DateTime.Now;
        TimecardData newCardData = new(clockInTime, null);
        CurrentTimecardID = App.catalog.AddEntry(newCardData);

        App.logger.LogEntry($"Clocked in at {clockInTime}");

        ClockInDateTime = string.Format(DEFAULT_DATETIME_FORMAT, clockInTime);
        TimeSinceClockIn = ZERO_TIME_DURATION;

        if (TickReciever != null && Timeclock != null)
        {
            TickReciever.Subscribe(Timeclock.monitor);
            Timeclock.StartTimeClock(clockInTime);
        }
        else
        {
            App.logger.LogEntry
                (
                    $"In {nameof(ClockIn)}:\n",
                    "Timeclock or its monitor does not exist in the view model and the timeclock could not be started"
                );
        }
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

        StartBreakEnabled = false;
        EndBreakEnabled = false;
        BreakSavedNotifVisible = false;

        CurrentBreakStartTime = DEFAULT_DATETIME;
        CurrentBreakEndTime = DEFAULT_DATETIME;

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

        if (Timeclock != null)
        {
            Timeclock.StopTimeClock();
        }
        else
        {
            App.logger.LogEntry
                (
                    $"In {nameof(ClockOut)}:\n",
                    "Timeclock does not exist in the view model and could not be stopped"
                );
        }

        ClearBreakDisplayRequested?.Invoke();

    }
    bool ClockOutCanExecute()
    {
        return ClockOutEnabled;
    }

    /*
     * The command that runs when the user clicks the start break button
     */
    [RelayCommand(CanExecute = nameof(StartBreakCanExecute))]
    void StartBreak()
    {
        StartBreakEnabled = false;
        EndBreakEnabled = true;
        BreakSavedNotifVisible = false;

        AddBreakEntryBoxRequested?.Invoke();
    }
    bool StartBreakCanExecute()
    {
        return StartBreakEnabled;
    }

    /*
     * The command that runs when the user clicks the end break button
     */
    [RelayCommand(CanExecute = nameof(EndBreakCanExecute))]
    void EndBreak()
    {
        StartBreakEnabled = true;
        EndBreakEnabled = false;
        BreakSavedNotifVisible = true;
    }
    bool EndBreakCanExecute()
    {
        return EndBreakEnabled;
    }

    /*
     * METHODS
     */

    /*
     * Returns true if a current timecard is assigned to the page
     */
    public bool IsTimecardAssignesAsCurrent() => CurrentTimecardID > -1;

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

        StartBreakEnabled = false;
        EndBreakEnabled = false;
        BreakSavedNotifVisible = false;

        CurrentBreakStartTime = DEFAULT_DATETIME;
        CurrentBreakEndTime = DEFAULT_DATETIME;
    }

    /*
     * Updates the text in the duration display with the passed in time
     */
    private void UpdateDurationDisplay(TimeclockDisplayInfo data)
    {
        TimeSinceClockIn = data.TimeDuration.ToString(@"hh\:mm\:ss");
    }

    /*
     * Logs the recieved error in the application log
     */
    private void LogError(Exception ex)
    {
        App.logger.LogEntry(ex.Message);
    }

    /* 
     * Resets the duration display to the default string
     */
    private void TimeclockClosed()
    {
        TimeSinceClockIn = DEFAULT_TIME_DURATION;
    }
}
