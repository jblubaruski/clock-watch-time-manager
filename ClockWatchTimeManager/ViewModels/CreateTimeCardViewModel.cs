using BusinessManagementLibrary.TimeManagement;
using BusinessManagementLibrary.Data;
using ClockWatchTimeManager.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using System.Xml.Serialization;

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

    // the description of the current break
    private string _currentBreakDescription = DEFAULT_BREAK_DESCRIPTION;
    public string CurrentBreakDescription
    {
        get => _currentBreakDescription;
        set => SetProperty(ref _currentBreakDescription, value);
    }

    /*
     * PROPERTIES
     */

    // the ID of the current timecard (is -1 if theres no current timecard)
    private int CurrentTimecardID { get; set; } = -1;
    // the ID of the current break (is -1 if theres no current break
    private int CurrentBreakID { get; set; } = -1;
    // the timeclock used to update the clock in duration display
    private Timeclock? Timeclock { get; } = null;
    // the reciever that gets notifications from the timeclock whenever it ticks
    private NotificationReciever<TimeclockDisplayInfo>? TickReciever { get; } = null;

    /*
     * EVENTS
     */

    internal event Action? AddBreakEntryBoxRequested;
    internal event Action<string, string>? AddBreakDisplayBoxRequested;
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
}
