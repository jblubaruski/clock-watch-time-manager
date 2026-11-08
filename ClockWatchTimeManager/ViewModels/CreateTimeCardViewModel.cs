using BusinessApplicationModels.TimeManagement;
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

    // time and date shown when the date time of an item in the time card is null
    public const string DEFAULT_DATETIME = "--:--:-- -- -- --------- --- --, ----";
    // format for displaying the time and date
    public const string DEFAULT_DATETIME_FORMAT = "{0:h:mm:ss tt -- dddd MMM dd, yyyy}";

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

    /*
     * PROPERTIES
     */

    // the current time card
    private TimeCard Timecard { get; set; } = new();

    /*
     * COMMANDS
     */
    
    /* 
     * The command that runs when the user clicks the clock in button.
     * Sets the time card's clock in time to the current system time and updates the ClockInDateTime property.
     */
    [RelayCommand(CanExecute = nameof(ClockInCanExecute))]
    void ClockIn()
    {
        ClockInEnabled = false;
        ClockOutEnabled = true;
        TimeCardSavedNotifVisible = false;

        Timecard = new();
        
        DateTime currentDateTime = DateTime.Now;
        Timecard.ClockInDateTime = currentDateTime;

        App.logger.LogEntry($"Clocked in at {Timecard.ClockInDateTime}");

        ClockInDateTime = string.Format(DEFAULT_DATETIME_FORMAT, Timecard.ClockInDateTime);
    }
    bool ClockInCanExecute()
    {
        return ClockInEnabled;
    }

    /*
     * The command that runs when the user clicks the clock out button.
     * Sets the time card's clock out time to the current system time.
     */
    [RelayCommand(CanExecute = nameof(ClockOutCanExecute))]
    void ClockOut()
    {
        ClockInEnabled = true;
        ClockOutEnabled = false;
        TimeCardSavedNotifVisible = true;

        DateTime currentDateTime = DateTime.Now;
        Timecard.ClockOutDateTime = currentDateTime;

        App.catalog.AddTimeCard(Timecard);

        ClockInDateTime = DEFAULT_DATETIME;

        App.logger.LogEntry
            (
                $"Logged Time card:\n{Timecard}",
                App.catalog.ToString()
            );
    }
    bool ClockOutCanExecute()
    {
        return ClockOutEnabled;
    }

    /*
     * METHODS
     */

    /*
     * Resets the view model to its base state
     */
    public void Reset()
    {
        TimeCardSavedNotifVisible = false;
        ClockInEnabled = true;
        ClockOutEnabled = false;
        ClockInDateTime = DEFAULT_DATETIME;
        Timecard = new();
    }

    /*
     * Returns whether or not the time card on the page is empty
     */
}
