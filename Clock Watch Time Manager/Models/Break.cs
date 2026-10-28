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
        public TimeOnly? BreakStartTime { get; private set; } = null;
        public TimeOnly? BreakEndTime { get; private set; } = null;
        public DateOnly? BreakStartDate { get; private set; } = null;
        public DateOnly? BreakEndDate { get; private set; } = null;
        public string? BreakDescription { get; private set; } = null;
    }
}
