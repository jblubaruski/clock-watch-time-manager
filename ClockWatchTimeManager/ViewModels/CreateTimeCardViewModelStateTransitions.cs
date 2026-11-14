using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWatchTimeManager.ViewModels
{
    partial class CreateTimeCardViewModel : ObservableObject
    {
        /*
         * Sets the page to its base state
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
        }

        /*
         * Sets the page to the after clock in state
         */
        private void ToAfterClockInState()
        {
            ClockInEnabled = false;
            ClockOutEnabled = true;
            TimeCardSavedNotifVisible = false;

            StartBreakEnabled = true;

            TimeSinceClockIn = ZERO_TIME_DURATION;
        }

        /*
         * Sets the page to the after clock out state
         */
        private void ToAfterClockOutState()
        {
            ClockInEnabled = true;
            ClockOutEnabled = false;
            TimeCardSavedNotifVisible = true;

            StartBreakEnabled = false;
            EndBreakEnabled = false;
            BreakSavedNotifVisible = false;

            ClockInDateTime = DEFAULT_DATETIME;
            CurrentBreakStartTime = DEFAULT_DATETIME;
        }

        /*
         * Sets the page to the after start break state
         */
        private void ToAfterStartBreakState()
        {
            StartBreakEnabled = false;
            EndBreakEnabled = true;
            BreakSavedNotifVisible = false;
        }

        /*
         * Sets the page to the after end break state
         */
        private void ToAfterEndBreakState()
        {
            StartBreakEnabled = true;
            EndBreakEnabled = false;
            BreakSavedNotifVisible = true;
        }
    }
}
