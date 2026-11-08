using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWatchTimeManager.Models
{
    /*
     * Represents a single task or group of tasks carried out at once during the work period.
     */
    internal class WorkTask(DateTime? startTime, DateTime? endTime, string? description)
    {
        public DateTime? WorkTaskStartDateTime { get; set; } = startTime;
        public DateTime? WorkTaskEndDateTime { get; set; } = endTime;
        public string? WorkTaskDescription { get; private set; } = description;

        /*
         * Returns a string representation of this task
         * 
         * The representation is formatted as:
         * Task start: MM/dd/yyyy hh:mm:ss tt
         * {break description}
         * Task end:   MM/dd/yyyy hh:mm:ss tt
         * 
         * Each line is shown as the following if null:
         * Task start: --/--/---- --:--:-- --
         * ---
         * Task end:   --/--/---- --:--:-- --
         */
        public override string ToString()
        {
            string taskString = "";

            if (WorkTaskStartDateTime != null)
                taskString += $"Task start: {WorkTaskStartDateTime:MM/dd/yyyy hh:mm:ss tt}\n";
            else
                taskString += "Task start: --/--/---- --:--:-- --\n";

            if (WorkTaskDescription != null)
                taskString += $"{WorkTaskDescription}\n";
            else
                taskString += "---\n";

            if (WorkTaskEndDateTime != null)
                taskString += $"Task end:   {WorkTaskEndDateTime:MM/dd/yyyy hh:mm:ss tt}";
            else
                taskString += "Task end:   --/--/---- --:--:-- --";

            return taskString;
        }
    }
}
