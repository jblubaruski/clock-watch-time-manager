using BusinessManagementLibrary.TimeManagement;
using BusinessManagementLibrary.Tests.TimeManagement.TestingWrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimecardTests
{
    /*
     * Test for Timecard.GetEntry(int)
     * 
     * Assert that retrieving an entry that doesnt exist in the timecard returns null
     * Assert thay retrieving an entry that exists in the timecard returns that entry
     */
    public class GetEntryTest
    {
        [Fact]
        public void GetEntry_EntryExists_ReturnEntry()
        {
            DateTime? clockInTime = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime = new(2026, 7, 11, 13, 0, 0);
            DateTime? entryStartTime = new(2026, 7, 11, 10, 0, 0);
            DateTime? entryEndTime = new(2026, 7, 11, 10, 30, 0);

            TimecardData timecardData = new(clockInTime, clockOutTime);
            TimesheetEntryData entryData = new(entryStartTime, entryEndTime, "Test", true);

            Timecard_Wrapper timecard = new(0, timecardData);
            int entryID = 0;
            TimesheetEntry entry = new(entryID, entryData);

            timecard.ExposedEntries.Add(entry);
            TimesheetEntry? returnedEntry = timecard.GetEntry(entryID);

            Assert.Equal(entry, returnedEntry);
        }
        [Fact]
        public void GetEntry_EntryDoesNotExist_ReturnNull()
        {
            DateTime? clockInTime = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime = new(2026, 7, 11, 13, 0, 0);
            int entryID = 0;

            TimecardData timecardData = new(clockInTime, clockOutTime);
            Timecard_Wrapper timecard = new(0, timecardData);

            TimesheetEntry? returnedEntry = timecard.GetEntry(entryID);

            Assert.Null(returnedEntry);
        }
    }
}
