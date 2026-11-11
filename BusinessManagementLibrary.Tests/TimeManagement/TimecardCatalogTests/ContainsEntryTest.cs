using BusinessManagementLibrary.TimeManagement;
using BusinessManagementLibrary.Tests.TimeManagement.TestingWrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimecardCatalogTests
{
    /*
     * Test for TimecardCatalog.ContainsEntry(int)
     * 
     * Assert that adding a timecard and checking for its entryID returns true
     * Assert that checking for an entryID in a catalog with no entries returns false
     * Assert that adding a timecard and checking for another entryID returns false
     */
    public class ContainsEntryTest
    {
        [Fact]
        public void ContainsEntry_TimecardExists_ReturnTrue()
        {
            DateTime? clockInTime = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime = new(2026, 7, 11, 13, 0, 0);

            TimecardData timecardData = new(clockInTime, clockOutTime);

            int entryID = 0;
            Timecard timecard = new(entryID, timecardData);
            TimecardCatalog_Wrapper catalog = new();

            catalog.ExposedCatalog.Add(timecard);

            bool result = catalog.ContainsEntry(entryID);

            Assert.True(result);
        }
        [Fact]
        public void ContainsEntry_TimecardDoesNotExist_ReturnFalse()
        {
            TimecardCatalog_Wrapper timecard = new();

            bool result = timecard.ContainsEntry(0);

            Assert.False(result);
        }
        [Fact]
        public void ContainsEntry_TimecardWithOtherIDExists_ReturnFalse()
        {
            DateTime? clockInTime = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime = new(2026, 7, 11, 13, 0, 0);

            TimecardData timecardData = new(clockInTime, clockOutTime);

            int entryID = 0;
            Timecard timecard = new(entryID, timecardData);
            TimecardCatalog_Wrapper catalog = new();

            catalog.ExposedCatalog.Add(timecard);

            bool result = catalog.ContainsEntry(entryID + 1);

            Assert.False(result);
        }
    }
}
