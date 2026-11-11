using BusinessManagementLibrary.TimeManagement;
using BusinessManagementLibrary.Tests.TimeManagement.TestingWrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimecardCatalogTests
{
    /*
     * Test for TimecardCatalog.GetEntry(int)
     * 
     * Assert that retrieving a timecard that doesnt exist in the catalog returns null
     * Assert thay retrieving a timecard that exists in the catalof returns that timecard
     */
    public class GetEntryTest
    {
        [Fact]
        public void GetEntry_TimecardExists_ReturnTimecard()
        {
            DateTime? clockInTime = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime = new(2026, 7, 11, 13, 0, 0);

            TimecardData timecardData = new(clockInTime, clockOutTime);
            
            int entryID = 0;
            Timecard timecard = new(entryID, timecardData);
            TimecardCatalog_Wrapper catalog = new();

            catalog.ExposedCatalog.Add(timecard);
            Timecard? returnedTimecard = catalog.GetEntry(entryID);

            Assert.Equal(timecard, returnedTimecard);
        }
        [Fact]
        public void GetEntry_TimecardDoesNotExist_ReturnNull()
        {
            int entryID = 0;

            TimecardCatalog_Wrapper catalog = new();

            Timecard? returnedEntry = catalog.GetEntry(entryID);

            Assert.Null(returnedEntry);
        }
    }
}
