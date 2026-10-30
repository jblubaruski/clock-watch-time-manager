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

        /*
         * Returns a string representation of this time card in the format:
         * 
         * Clock in:  <ClockInDateTime.ToString()>
         * Clock out: <ClockOutDateTime.ToString()>
         * 
         * if either ClockInDateTime or ClockOutDateTime are null,
         * they should be represented with 20 hyphens.
         */
        public override string ToString()
        {
            string str = "";

            if (ClockInDateTime == null)
                str += $"Clock in:  {"".PadLeft(20, '-')}\n";
            else
                str += $"Clock in:  {ClockInDateTime}\n";

            if (ClockOutDateTime == null)
                str += $"Clock out: {"".PadLeft(20, '-')}";
            else
                str += $"Clock out: {ClockOutDateTime}";

            return str;
        }
    }
}
