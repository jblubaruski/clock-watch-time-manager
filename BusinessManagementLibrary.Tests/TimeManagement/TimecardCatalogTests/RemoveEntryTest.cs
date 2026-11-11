using BusinessManagementLibrary.TimeManagement;
using BusinessManagementLibrary.Tests.TimeManagement.TestingWrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimecardCatalogTests
{
    /*
     * Test for TimecardCatalog.RemoveEntry(int)
     * 
     * Assert that passing in an integer that equals the entryID of the only timecard in the catalog empties the catalog of timecards
     */
    public class RemoveEntryTest
    {
        [Fact]
        public void RemoveEntry_OneTimecardInCatalog_NoTimecardsInCatalog()
        {
            DateTime? clockInTime = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime = new(2026, 7, 11, 13, 0, 0);

            TimecardData timecardData = new(clockInTime, clockOutTime);

            int entryID = 0;
            Timecard timecard = new(entryID, timecardData);
            TimecardCatalog_Wrapper catalog = new();

            catalog.ExposedCatalog.Add(timecard);
            catalog.RemoveEntry(entryID);

            int expectedResult = 0;
            int result = catalog.ExposedCatalog.Count;

            Assert.Equal(expectedResult, result);
        }
    }
}
