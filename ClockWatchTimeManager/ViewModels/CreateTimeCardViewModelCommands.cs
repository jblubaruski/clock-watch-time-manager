using BusinessManagementLibrary.TimeManagement;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWatchTimeManager.ViewModels
{
    partial class CreateTimeCardViewModel : ObservableObject
    {
        /* 
         * The command that runs when the user clicks the clock in button.
         * Sets up a new timecard data container with the clock in time and adds the card to the catalog, storing the returned ID in CurrentTimecardID.
         * Updates the clock in time display to the clock in time.
         * Starts the timeclock.
         */
        [RelayCommand(CanExecute = nameof(ClockInCanExecute))]
        void ClockIn()
        {
            ToAfterClockInState();

            DateTime currentTime = DateTime.Now;

            CreateNewCurrentTimecard(currentTime);
            ClockInDateTime = string.Format(DEFAULT_DATETIME_FORMAT, currentTime);

            StartTimeclock(currentTime);
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
            ToAfterClockOutState();

            DateTime currentTime = DateTime.Now;

            LogCurrentTimecard(currentTime);
            StopTimeclock();

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
            ToAfterStartBreakState();

            ClearBreakDisplayRequested?.Invoke();

            Timecard? currentTimecard = App.catalog.GetEntry(CurrentTimecardID);
            if (currentTimecard != null)
            {
                DisplayBreaksInTimecard(currentTimecard);

                DateTime currentTime = DateTime.Now;

                CreateNewCurrentBreak(currentTimecard, currentTime);
                CurrentBreakStartTime = string.Format(DEFAULT_DATETIME_FORMAT, currentTime);
            }
            else
            {
                App.logger.LogEntry
                    (
                        $"In {nameof(StartBreak)}:\n",
                        $"The timecard corresponding to timecardID {CurrentTimecardID} does not exist in the catalog"
                    );
            }

            // Add the current break entry box onto the end of the break display
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
            ToAfterEndBreakState();

            ClearBreakDisplayRequested?.Invoke();

            Timecard? currentTimecard = App.catalog.GetEntry(CurrentTimecardID);
            if (currentTimecard != null)
            {
                DateTime currentTime = DateTime.Now;

                LogCurrentBreak(currentTimecard, currentTime);
                DisplayBreaksInTimecard(currentTimecard);
            }
            else
            {
                App.logger.LogEntry
                    (
                        $"In {nameof(EndBreak)}:\n",
                        $"The timecard corresponding to timecardID {CurrentTimecardID} does not exist in the catalog"
                    );
            }
        }
        bool EndBreakCanExecute()
        {
            return EndBreakEnabled;
        }
    }
}
