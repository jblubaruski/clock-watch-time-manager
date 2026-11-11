using BusinessManagementLibrary.TimeManagement;
using BusinessManagementLibrary.Tests.TimeManagement.TestingWrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimecardTests
{
    /*
     * Test for Timecard.ContainsEntry(int)
     * 
     * Assert adding an entry and checking for its entryID returns true
     * Assert that checking for an entryID in a timecard with no entries returns false
     * Assert that adding an entry and checking for another entryID returns false
     */
    public class ContainsEntryTest
    {
        [Fact]
        public void ContainsEntry_EntryExists_ReturnTrue()
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

            bool result = timecard.ContainsEntry(entryID);

            Assert.True(result);
        }
        [Fact]
        public void ContainsEntry_EntryDoesNotExist_ReturnFalse()
        {
            DateTime? clockInTime = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime = new(2026, 7, 11, 13, 0, 0);

            TimecardData timecardData = new(clockInTime, clockOutTime);
            Timecard_Wrapper timecard = new(0, timecardData);

            bool result = timecard.ContainsEntry(0);

            Assert.False(result);
        }
        [Fact]
        public void ContainsEntry_EntryWithOtherIDExists_ReturnFalse()
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

            bool result = timecard.ContainsEntry(entryID + 1);

            Assert.False(result);
        }
    }
}
