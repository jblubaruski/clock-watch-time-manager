using BusinessManagementLibrary.TimeManagement;
using BusinessManagementLibrary.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimesheetEntryTests
{
    /*
     * Test for TimesheetEntry.StartTime setter
     * 
     * Assert that passing in a start time that is greater than or equal to the entry's current end time throws an ArgumentException
     * Assert that passing in a start time to an entry with a null end time sets the start time
     */
    public class StartTimeSetTest
    {
        private static readonly DateTime endTime = new(2026, 7, 11, 13, 30, 27);
        private static readonly DateTime startTimeGreaterThan = new(2026, 7, 11, 14, 00, 00);
        private static readonly DateTime startTimeEqualTo = endTime;

        [Fact]
        public void StartTimeSet_ValueGreaterThan_ThrowArgumentException()
        {
            TimesheetEntryData data = new(null, endTime, "Test", true);
            TimesheetEntry entry = new(0, data);

            Assert.Throws<ArgumentException>(() => entry.StartTime = startTimeGreaterThan);
        }
        [Fact]
        public void StartTimeSet_ValueEqualTo_ThrowArgumentException()
        {
            TimesheetEntryData data = new(null, endTime, "Test", true);
            TimesheetEntry entry = new(0, data);

            Assert.Throws<ArgumentException>(() => entry.StartTime = startTimeEqualTo);
        }
        [Fact]
        public void StartTimeSet_NullEndTime_SetProperty()
        {
            TimesheetEntryData data = new(null, null, "Test", true);
            TimesheetEntry entry = new(0, data);

            entry.StartTime = startTimeEqualTo;
            bool result = entry.StartTime == startTimeEqualTo;

            Assert.True(result);
        }
    }
}
