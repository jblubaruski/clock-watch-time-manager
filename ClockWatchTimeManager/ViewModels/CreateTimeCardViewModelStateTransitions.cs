using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWatchTimeManager.ViewModels
{
    partial class CreateTimeCardViewModel : ObservableObject
    {
        /*
         * Transitions the page to its base state
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
            CurrentBreakDescEntryBoxEnabled = false;

            CurrentBreakStartTime = DEFAULT_DATETIME;
            CurrentBreakDescription = DEFAULT_BREAK_DESCRIPTION;
        }

        /*
         * Transitions the page to the after clock in state
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
         * Transitions the page to the after clock out state
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
         * Transitions the page to the after start break state
         */
        private void ToAfterStartBreakState()
        {
            StartBreakEnabled = false;
            EndBreakEnabled = true;
            BreakSavedNotifVisible = false;
        }

        /*
         * Transitions the page to the after end break state
         */
        private void ToAfterEndBreakState()
        {
            StartBreakEnabled = true;
            EndBreakEnabled = false;
            BreakSavedNotifVisible = true;
        }

        /*
         * Transitions the page to the edit break description state
         */
        private void ToEditBreakDescriptionState()
        {
            CurrentBreakDescEntryBoxEnabled = true;
            ClockOutEnabled = false;
            EndBreakEnabled = false;
        }

        /*
         * Transitions the page to the enter break description state
         */
        private void ToEnterBreakDescriptionState()
        {
            CurrentBreakDescEntryBoxEnabled = false;
            ClockOutEnabled = true;
            EndBreakEnabled = true;
        }
    }
}
