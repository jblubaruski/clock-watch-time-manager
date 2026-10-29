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
        public DateTime? ClockInDateTime { get; set; } = null;
        public DateTime? ClockOutDateTime { get; set; } = null;

        public override string ToString()
        {
            string str = "";

            str += $"Clock in: {ClockInDateTime}\n";
            str += $"Clock out: {ClockOutDateTime}";

            return str;
        }
    }
}
