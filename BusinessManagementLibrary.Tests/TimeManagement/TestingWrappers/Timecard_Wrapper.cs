using BusinessManagementLibrary.TimeManagement;
using BusinessManagementLibrary.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TestingWrappers
{
    /*
     * Exposes the list of timesheet entries for testing purposes
     */
    internal class Timecard_Wrapper : Timecard
    {
        public List<TimesheetEntry> ExposedEntries { get; }
        public Timecard_Wrapper(int timecardID, TimecardData data) : base(timecardID, data)
        {
            ExposedEntries = TimesheetEntries;
        }
    }
}
