using BusinessManagementLibrary.TimeManagement;
using BusinessManagementLibrary.Tests.TimeManagement.TestingWrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimecardTests
{
    /*
     * Tests for Timecard.AddEntry(TimesheetEntryData)
     * 
     * Assert that passing in a container with times that overlap with another entries non-null start and end times throws an ArgumentException
     * Assert that passing in a container with a non-null start time that comes before the non-null clock in time throws an ArgumentException
     * Assert that passing in a container with a non-null end time that comes after the non-null clock out time throws an ArgumentException
     * Assert that passing in a container with valid data to a timecard with no entries returns 0
     * Assert that passing in a container with valid data to a timecard with an entry that has a entryID of 2 returns 3
     * Assert that passing in a container with valid data to a timecard with no entries adds a TimesheetEntry with an entryID of 0
     */
    public class AddEntryTest
    {
        [Fact]
        public void AddEntry_ContainerTimesOverlapWithOtherEntry_ThrowArgumentExcpetion()
        {
            DateTime? clockInTime       = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime      = new(2026, 7, 11, 13, 0, 0);
            DateTime? entry1StartTime   = new(2026, 7, 11, 10, 0, 0);
            DateTime? entry1EndTime     = new(2026, 7, 11, 10, 30, 0);
            DateTime? entry2StartTime   = new(2026, 7, 11, 10, 15, 0);
            DateTime? entry2EndTime     = new(2026, 7, 11, 10, 45, 0);

            TimecardData timecardData = new(clockInTime, clockOutTime);
            TimesheetEntryData entry1Data = new(entry1StartTime, entry1EndTime, "Entry1", true);
            TimesheetEntryData entry2Data = new(entry2StartTime, entry2EndTime, "Entry2", true);

            Timecard_Wrapper timecard = new(0, timecardData);
            TimesheetEntry entry1 = new(0, entry1Data);

            timecard.ExposedEntries.Add(entry1);

            Assert.Throws<ArgumentException>(() => timecard.AddEntry(entry2Data));
        }
        [Fact]
        public void AddEntry_ContainerStartTimeComesBeforeClockInTime_ThrowArgumentException()
        {
            DateTime? clockInTime       = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime      = new(2026, 7, 11, 13, 0, 0);
            DateTime? entryStartTime    = new(2026, 7, 11, 8, 45, 0);
            DateTime? entryEndTime      = new(2026, 7, 11, 9, 15, 0);

            TimecardData timecardData = new(clockInTime, clockOutTime);
            TimesheetEntryData entryData = new(entryStartTime, entryEndTime, "Entry", true);
            Timecard_Wrapper timecard = new(0, timecardData);

            Assert.Throws<ArgumentException>(() => timecard.AddEntry(entryData));
        }
        [Fact]
        public void AddEntry_ContainerEndTimeComesAfterClockOutTime_ThrowArgumentException()
        {
            DateTime? clockInTime       = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime      = new(2026, 7, 11, 13, 0, 0);
            DateTime? entryStartTime    = new(2026, 7, 11, 12, 45, 0);
            DateTime? entryEndTime      = new(2026, 7, 11, 13, 15, 0);

            TimecardData timecardData = new(clockInTime, clockOutTime);
            TimesheetEntryData entryData = new(entryStartTime, entryEndTime, "Entry", true);
            Timecard_Wrapper timecard = new(0, timecardData);

            Assert.Throws<ArgumentException>(() => timecard.AddEntry(entryData));
        }
        [Fact]
        public void AddEntry_ValidEntryDataNoEntries_ReturnZero()
        {
            DateTime? clockInTime       = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime      = new(2026, 7, 11, 13, 0, 0);
            DateTime? entryStartTime    = new(2026, 7, 11, 10, 0, 0);
            DateTime? entryEndTime      = new(2026, 7, 11, 10, 30, 0);

            TimecardData timecardData = new(clockInTime, clockOutTime);
            TimesheetEntryData entryData = new(entryStartTime, entryEndTime, "Entry", true);
            Timecard_Wrapper timecard = new(0, timecardData);

            int expectedResult = 0;
            int result = timecard.AddEntry(entryData);

            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void AddEntry_ValidEntryDataOneEntry_ReturnThree()
        {
            DateTime? clockInTime       = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime      = new(2026, 7, 11, 13, 0, 0);
            DateTime? entry1StartTime   = new(2026, 7, 11, 10, 0, 0);
            DateTime? entry1EndTime     = new(2026, 7, 11, 10, 30, 0);
            DateTime? entry2StartTime   = new(2026, 7, 11, 11, 0, 0);
            DateTime? entry2EndTime     = new(2026, 7, 11, 11, 30, 0);

            TimecardData timecardData = new(clockInTime, clockOutTime);
            TimesheetEntryData entry1Data = new(entry1StartTime, entry1EndTime, "Entry1", true);
            TimesheetEntryData entry2Data = new(entry2StartTime, entry2EndTime, "Entry2", true);

            Timecard_Wrapper timecard = new(0, timecardData);
            TimesheetEntry entry1 = new(2, entry1Data);

            timecard.ExposedEntries.Add(entry1);

            int expectedResult = 3;
            int result = timecard.AddEntry(entry2Data);

            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void AddEntry_ValidEntryNoEntries_EntryAddedWithIDofZero()
        {
            DateTime? clockInTime       = new(2026, 7, 11, 9, 0, 0);
            DateTime? clockOutTime      = new(2026, 7, 11, 13, 0, 0);
            DateTime? entryStartTime    = new(2026, 7, 11, 10, 0, 0);
            DateTime? entryEndTime      = new(2026, 7, 11, 10, 30, 0);

            TimecardData timecardData = new(clockInTime, clockOutTime);
            TimesheetEntryData entryData = new(entryStartTime, entryEndTime, "Entry", true);
            Timecard_Wrapper timecard = new(0, timecardData);

            timecard.AddEntry(entryData);

            int expectedResult = 0;
            int result = timecard.ExposedEntries[0].EntryID;

            Assert.Equal(expectedResult, result);
        }
    }
}
