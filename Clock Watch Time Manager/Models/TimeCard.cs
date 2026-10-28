using System;
using System.Collections.Generic;
using System.Text;

namespace Clock_Watch_Time_Manager.Models
{
    /*
     * Represents a single work period.
     * Includes the start and stop time and date of the work period as well as 
     * the timesheet containing the breaks taken and tasks worked during the work period.
     */
    internal class TimeCard
    {
        private TimeSheet TimeSheet { get; } = new();
        public TimeOnly? ClockInTime { get; private set; } = null;
        public TimeOnly? ClockOutTime { get; private set; } = null;
        public DateOnly? ClockInDate { get; private set; } = null;
        public DateOnly? ClockOutDate { get; private set; } = null;
    }
}
