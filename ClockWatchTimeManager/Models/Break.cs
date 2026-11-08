using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWatchTimeManager.Models
{
    /*
     * Represents a single break period taken during the work period.
     */
    internal class Break(DateTime? startTime, DateTime? endTime, string? description)
    {
        public DateTime? BreakStartDateTime { get; set; } = startTime;
        public DateTime? BreakEndDateTime { get; set; } = endTime;
        public string? BreakDescription { get; set; } = description;

        /*
         * Returns a string representation of this break
         * 
         * The representation is formatted as:
         * Break start: MM/dd/yyyy hh:mm:ss tt
         * {break description}
         * Break end:   MM/dd/yyyy hh:mm:ss tt
         * 
         * Each line is shown as the following if null:
         * Break start: --/--/---- --:--:-- --
         * ---
         * Break end:   --/--/---- --:--:-- --
         */
        public override string ToString()
        {
            string breakString = "";

            if (BreakStartDateTime != null)
                breakString += $"Break start: {BreakStartDateTime:MM/dd/yyyy hh:mm:ss tt}\n";
            else
                breakString += "Break start: --/--/---- --:--:-- --\n";

            if (BreakDescription != null)
                breakString += $"{BreakDescription}\n";
            else
                breakString += "---\n";

            if (BreakEndDateTime != null)
                breakString += $"Break end:   {BreakEndDateTime:MM/dd/yyyy hh:mm:ss tt}";
            else
                breakString += "Break end:   --/--/---- --:--:-- --";

            return breakString;
        }
    }
}
