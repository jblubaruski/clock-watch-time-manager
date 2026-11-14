using BusinessManagementLibrary.TimeManagement;
using ClockWatchTimeManager.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWatchTimeManager.ViewModels
{
    partial class CreateTimeCardViewModel : ObservableObject
    {
        /*
         * Returns true if a current timecard is assigned to the page
         */
        public bool IsTimecardAssignesAsCurrent() => CurrentTimecardID > -1;

        /*
         * Creates a new timecard in the catalog and stores its ID in CurrentTimecardID
         */
        private void CreateNewCurrentTimecard(DateTime currentTime)
        {
            TimecardData newCardData = new(currentTime, null);
            CurrentTimecardID = App.catalog.AddEntry(newCardData);
            App.logger.LogEntry($"Clocked in at {currentTime}");
        }

        /*
         * Logs the current timecard in the catalog
         */
        private void LogCurrentTimecard(DateTime currentTime)
        {
            Timecard? currentTimecard = App.catalog.GetEntry(CurrentTimecardID);
            if (currentTimecard != null)
            {
                try
                {
                    currentTimecard.ClockOutTime = currentTime;
                    App.logger.LogEntry($"Logged Time card:\n{currentTimecard}");
                }
                catch (ArgumentException)
                {
                    App.logger.LogEntry
                        (
                            $"In {nameof(ClockOut)}:\n",
                            $"clockOutTime ({currentTime}) is less than or equal to the current timecard's ClockInTime ({currentTimecard.ClockInTime})"
                        );
                }
            }
            else
            {
                App.logger.LogEntry
                    (
                        $"In {nameof(ClockOut)}:\n",
                        $"The timecard corresponding to timecardID {CurrentTimecardID} does not exist in the catalog"
                    );
            }
        }

        /*
         * Creates a new break in the current timecard and stores its ID in CurrentBreakID
         */
        private void CreateNewCurrentBreak(Timecard currentTimecard, DateTime currentTime)
        {
            TimesheetEntryData breakData = new(currentTime, null, null, true);
            CurrentBreakID = currentTimecard.AddEntry(breakData);
            App.logger.LogEntry($"Break started at {currentTime}");
        }

        /*
         * Logs a new break in the current timecard
         */
        private void LogCurrentBreak(Timecard currentTimecard, DateTime currentTime)
        {
            TimesheetEntry? currentBreak = currentTimecard.GetEntry(CurrentBreakID);
            if (currentBreak != null)
            {
                try
                {
                    currentBreak.EndTime = currentTime;
                    currentBreak.Description = CurrentBreakDescription;
                }
                catch (ArgumentException)
                {
                    App.logger.LogEntry
                        (
                            $"In {nameof(EndBreak)}:\n",
                            $"EndTime ({currentTime}) is less than or equal to the current breaks's StartTime ({currentBreak.StartTime})"
                        );
                }
            }
            else
            {
                App.logger.LogEntry
                    (
                        $"In {nameof(EndBreak)}:\n",
                        $"The break corresponding to breakID {CurrentBreakID} does not exist in the current timecard"
                    );
            }
        }

        /*
         * Starts the timeclock and subscribes to its notification provider
         */
        private void StartTimeclock(DateTime currentTime)
        {
            if (TickReciever != null && Timeclock != null)
            {
                TickReciever.Subscribe(Timeclock.monitor);
                Timeclock.StartTimeClock(currentTime);
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

        /*
         * Stops the timeclock
         */
        private void StopTimeclock()
        {
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
        }

        /*
         * Displays all the breaks in the current timecard in the break display
         */
        private void DisplayBreaksInTimecard(Timecard timecard)
        {
            List<TimesheetEntryData> breaks = timecard.GetEitherBreaksOrTasks(true);
            foreach (TimesheetEntryData breakData in breaks)
            {
                AddBreakDisplayBoxRequested?.Invoke(string.Format(DEFAULT_DATETIME_FORMAT, breakData.StartTime), string.Format(DEFAULT_DATETIME_FORMAT, breakData.EndTime));
            }
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
}
