using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWatchTimeManager.Models
{
    /*
     * Represents a collection containing all the breaks and tasks taken and carried out during the work period.
     */
    internal class TimeSheet
    {
        private List<Break> BreakList { get; } = [];
        private List<WorkTask> TaskList { get; } = [];
    }
}
