using BusinessManagementLibrary.TimeManagement;
using BusinessManagementLibrary.Tests.TimeManagement.TestingWrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimecardCatalogTests
{
    /*
     * Tests for TimecardCatalog.AddEntry(TimecardData)
     * 
     * Assert that passing in a container with times that overlap with another timecard's non-null clock in and out times throws an ArgumentException
     * Assert that passing in a container with valid data to a catalog with no entries returns 0
     * Assert that passing in a container with valid data to a catalog with an entry that has a entryID of 2 returns 3
     * Assert that passing in a container with valid data to a catalog with no entries adds a TimesheetEntry with an entryID of 0
     */
    public class AddEntryTest
    {
        [Fact]
        public void AddEntry_ContainerTimesOverlapWithOtherTimecard_ThrowArgumentExcpetion()
        {
            DateTime? clockInTime1 = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime1 = new(2026, 7, 11, 13, 0, 0);
            DateTime? clockInTime2 = new(2026, 7, 11, 12, 0, 0);
            DateTime? clockOutTime2 = new(2026, 7, 11, 16, 0, 0);

            TimecardData timecard1Data = new(clockInTime1, clockOutTime1);
            TimecardData timecard2Data = new(clockInTime2, clockOutTime2);

            TimecardCatalog_Wrapper catalog = new();
            Timecard timecard1 = new(0, timecard1Data);

            catalog.ExposedCatalog.Add(timecard1);

            Assert.Throws<ArgumentException>(() => catalog.AddEntry(timecard2Data));
        }
        [Fact]
        public void AddEntry_ValidTimecardDataNoTimecards_ReturnZero()
        {
            DateTime? clockInTime = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime = new(2026, 7, 11, 13, 0, 0);

            TimecardData timecardData = new(clockInTime, clockOutTime);
            TimecardCatalog_Wrapper catalog = new();

            int expectedResult = 0;
            int result = catalog.AddEntry(timecardData);

            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void AddEntry_ValidTimecardDataOneTimecard_ReturnThree()
        {
            DateTime? clockInTime1 = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime1 = new(2026, 7, 11, 13, 0, 0);
            DateTime? clockInTime2 = new(2026, 7, 11, 14, 0, 0);
            DateTime? clockOutTime2 = new(2026, 7, 11, 18, 0, 0);

            TimecardData timecard1Data = new(clockInTime1, clockOutTime1);
            TimecardData timecard2Data = new(clockInTime2, clockOutTime2);

            int entryID = 2;
            Timecard timecard1 = new(entryID, timecard1Data);
            TimecardCatalog_Wrapper catalog = new();

            catalog.ExposedCatalog.Add(timecard1);

            int expectedResult = entryID + 1;
            int result = catalog.AddEntry(timecard2Data);

            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void AddEntry_ValidTimecardDataNoTimecards_TimecardAddedWithIDofZero()
        {
            DateTime? clockInTime = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime = new(2026, 7, 11, 13, 0, 0);

            TimecardData timecardData = new(clockInTime, clockOutTime);
            TimecardCatalog_Wrapper catalog = new();

            catalog.AddEntry(timecardData);

            int expectedResult = 0;
            int result = catalog.ExposedCatalog[0].EntryID;

            Assert.Equal(expectedResult, result);
        }
    }
}
