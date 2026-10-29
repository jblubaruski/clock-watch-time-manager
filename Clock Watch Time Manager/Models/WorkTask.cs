using System;
using System.Collections.Generic;
using System.Text;

namespace Clock_Watch_Time_Manager.Models
{
    /*
     * Represents a single task or group of tasks carried out at once during the work period.
     */
    internal class WorkTask
    {
        public DateTime? WorkTaskStartDateTime { get; set; } = null;
        public DateTime? WorkTaskEndDateTime { get; set; } = null;
        public string? WorkTaskDescription { get; private set; } = null;
    }
}
