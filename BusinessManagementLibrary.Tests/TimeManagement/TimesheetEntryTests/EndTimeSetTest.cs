using BusinessManagementLibrary.Data;
using BusinessManagementLibrary.TimeManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimesheetEntryTests
{
    /*
     * Test for TimesheetEntry.EndTime setter
     * 
     * Assert that passing in an end time that is less than or equal to the entry's current start time throws an ArgumentException
     * Assert that passing in an end time to an entry with a null start time sets the end time
     */
    public class EndTimeSetTest
    {
        private static readonly DateTime startTime = new(2026, 7, 11, 13, 30, 27);
        private static readonly DateTime endTimeLessThan = new(2026, 7, 11, 12, 00, 00);
        private static readonly DateTime endTimeEqualTo = startTime;

        [Fact]
        public void EndTimeSet_ValueGreaterThan_ThrowArgumentException()
        {
            TimesheetEntryData data = new(startTime, null, "Test", true);
            TimesheetEntry entry = new(0, data);

            Assert.Throws<ArgumentException>(() => entry.EndTime = endTimeLessThan);
        }
        [Fact]
        public void EndTimeSet_ValueEqualTo_ThrowArgumentException()
        {
            TimesheetEntryData data = new(startTime, null, "Test", true);
            TimesheetEntry entry = new(0, data);

            Assert.Throws<ArgumentException>(() => entry.EndTime = endTimeEqualTo);
        }
        [Fact]
        public void EndTimeSet_NullEndTime_SetProperty()
        {
            TimesheetEntryData data = new(null, null, "Test", true);
            TimesheetEntry entry = new(0, data);

            entry.EndTime = endTimeEqualTo;
            bool result = entry.EndTime == endTimeEqualTo;

            Assert.True(result);
        }
    }
}
