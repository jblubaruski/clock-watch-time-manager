using System;
using System.Collections.Generic;
using System.Text;

namespace Clock_Watch_Time_Manager.Models
{
    /*
     * Represents a single break period taken during the work period.
     */
    internal class Break
    {
        public DateTime? BreakStartDateTime { get; set; } = null;
        public DateTime? BreakEndDateTime { get; set; } = null;
        public string? BreakDescription { get; private set; } = null;
    }
}
