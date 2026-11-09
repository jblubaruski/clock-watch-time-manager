using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessApplicationModels.TimeManagement
{
    /*
     * Represents a single work period.
     * Includes the start and stop time and date of the work period as well as 
     * the timesheet containing the breaks taken and tasks worked during the work period.
     */
    public class TimeCard
    {
        private TimeSheet TimeSheet { get; } = new();
        public DateTime? ClockInDateTime { get; set; } = null;
        public DateTime? ClockOutDateTime { get; set; } = null;

        /*
         * Returns a string representation of this time card in the format:
         * 
         * Clock in:      {MM/dd/yyyy hh:mm:ss tt}
         * {TimeSheet}
         * Clock out:     {MM/dd/yyyy hh:mm:ss tt}
         * 
         * if either ClockInDateTime or ClockOutDateTime are null they are represented with the following:
         * Clock in:      --/--/---- --:--:-- --
         * Clock out:     --/--/---- --:--:-- --
         */
        public override string ToString()
        {
            string str = "";

            if (ClockInDateTime != null)
                str += $"Clock in:      {ClockInDateTime:MM/dd/yyyy hh:mm:ss tt}\n";
            else
                str += "Clock in:      --/--/---- --:--:-- --\n";

            str += TimeSheet.ToString() + "\n";

            if (ClockOutDateTime != null)
                str += $"Clock out:     {ClockOutDateTime:MM/dd/yyyy hh:mm:ss tt}";
            else
                str += "Clock out:     --/--/---- --:--:-- --";

            return str;
        }
    }
}
