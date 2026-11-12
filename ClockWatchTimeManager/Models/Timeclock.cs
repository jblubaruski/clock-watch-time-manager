using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWatchTimeManager.Models
{
    /*
     * A clock that sends a push notification to observers with the amount of time that has passed since it was started on a regular interval
     */
    internal class Timeclock : IObservable<TimeclockDisplayInfo>, IDisposable
    {
        // the time this timeclock was activated
        private DateTime? ClockStartTime { get; set; } = null;
        // whether or not the clock should be ticking
        public bool ClockTicking { get; private set; } = false;
        // the dispatcher that manages the timer that calls the clocks tick logic
        private IDispatcher Dispatcher { get; set; }
        // the amount of time between clock ticks
        private TimeSpan _timeclockTickInterval;
        public TimeSpan TimeclockTickInterval 
        { 
            get => _timeclockTickInterval;
            // Throws an InvalidOperationException if TimeclockTickInterval is set whole ClockTicking is true
            set
            {
                if (ClockTicking)
                    throw new InvalidOperationException("TimeclockTickInterval cannot change while ClockTicking is true");
                _timeclockTickInterval = value;
            }
        }

        /*
         * Constructs the timeclock from the passed in TimeSpan and IDispatcher
         */
        public Timeclock(TimeSpan timeclockTickInterval, IDispatcher dispatcher)
        {
            TimeclockTickInterval = timeclockTickInterval;
            Dispatcher = dispatcher;
        }

        /*
         * Starts the time clock
         */
        public void StartTimeClock(DateTime clockStartTime)
        {
            ClockTicking = true;
            ClockStartTime = clockStartTime;
            Dispatcher.StartTimer(TimeclockTickInterval, TickTimeClock);
        }

        /*
         * Stops the time clock
         */
        public void StopTimeClock()
        {
            ClockTicking = false;
            ClockStartTime = null;
        }

        /*
         * Calculates the timespan between the current time and the clock's start time.
         * Only runs internal logic when ClockTicking is true
         * 
         * Throws an InvalidOperationException when this method is called while ClockStartTime is null
         */
        public bool TickTimeClock()
        {
            if (ClockTicking)
            {
                if (ClockStartTime != null)
                {
                    DateTime currentTime = DateTime.Now;
                    TimeSpan timeSinceClockIn = currentTime.Subtract((DateTime)ClockStartTime);
                    TimeSinceClockIn = timeSinceClockIn.ToString(@"hh\:mm\:ss");
                }
                else
                {
                    StopTimeClock();
                    throw new InvalidOperationException($"The timeclock could not process {nameof(TickTimeClock)} because the ClockStartTime is null");
                }
            }

            return ClockTicking;
        }
    }

    /*
     * Container for the time duration data provided to the observer.
     */
    public class TimeclockDisplayInfo(TimeSpan timeDuration)
    {
        public TimeSpan TimeDuration { get; } = timeDuration;
    }
}
