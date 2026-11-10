using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimesheetEntryDataTests
{
    /*
     * Test for TimesheetEntryData.TimesheetEntryData(DateTime?, DateTime?, string?, bool)
     * 
     * Assert that passing in a start time that is greater than the end time throws an ArgumentException
     * Assert that passing in a start time that is equal to the end time throwa an ArgumentException
     * Assert that the constructed container holds the values that were passed in to the constructor when the arguments are valid
     */
    public class ConstructorTest
    {
        public static readonly DateTime? endTime = new(2026, 7, 11, 12, 0, 0);
        public static readonly DateTime? startTimeGreaterThan = new(2026, 7, 11, 12, 30, 0);
        public static readonly DateTime? startTimeEqualTo = endTime;

        
    }
}
