using BusinessManagementLibrary.TimeManagement;
using BusinessManagementLibrary.Tests.TimeManagement.TestingWrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimecardTests
{
    /*
     * Test for Timecard.RemoveEntry(int)
     * 
     * Assert that passing in an integer that equals the entryID of the only entry in the timecard empties the timecard of entries
     */
    public class RemoveEntryTest
    {
        [Fact]
        public void RemoveEntry_OneEntryInTimecard_NoEntriesInTimecard()
        {
            DateTime? clockInTime = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime = new(2026, 7, 11, 13, 0, 0);
            DateTime? entryStartTime = new(2026, 7, 11, 11, 0, 0);
            DateTime? entryEndTime = new(2026, 7, 11, 11, 30, 0);

            TimecardData timecardData = new(clockInTime, clockOutTime);
            TimesheetEntryData entryData = new(entryStartTime, entryEndTime, "Test", true);

            Timecard_Wrapper timecard = new(0, timecardData);
            int entryID = 0;
            TimesheetEntry entry = new(entryID, entryData);

            timecard.ExposedEntries.Add(entry);
            timecard.RemoveEntry(entryID);

            int expectedResult = 0;
            int result = timecard.ExposedEntries.Count;

            Assert.Equal(expectedResult, result);
        }
    }
}
