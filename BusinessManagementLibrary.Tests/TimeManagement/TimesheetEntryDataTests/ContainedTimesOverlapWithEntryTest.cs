using BusinessManagementLibrary.TimeManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimesheetEntryTests
{
    /*
     * Tests for TimesheetEntryData.ContainedTImesOverlapWithEntry(TimesheetEntry)
     * 
     * Assert that a container with a start time that occurs between the start and end time of the passed in entry returns true
     * Assert that a container with an end time that occurs between the start and end time of the passed in entry returns true
     * Assert that passing in an entry with a null start time returns false
     * Assert that passing in an entry with a null end time returns false
     * Assert that a container with a start and end time that occurs outside of the start and end time of the passed in entry returns false
     */
    public class ContainedTimesOverlapWithEntryTest
    {
        [Fact]
        public void ContainedTimesOverlapWithEntryTest_OverlappingStartTime_ReturnTrue()
        {
            DateTime? entryStartTime        = new(2026, 7, 11, 12, 0, 0);
            DateTime? entryEndTime          = new(2026, 7, 11, 12, 30, 0);
            DateTime? containerStartTime    = new(2026, 7, 11, 12, 15, 0);
            DateTime? containerEndTime      = new(2026, 7, 11, 12, 45, 0);

            TimesheetEntry      entry       = new(0, new(entryStartTime, entryEndTime, "Entry", true));
            TimesheetEntryData  container   = new(containerStartTime, containerEndTime, "Container", true);

            bool result = container.ContainedTimesOverlapWithEntry(entry);

            Assert.True(result);
        }
        [Fact]
        public void ContainedTimesOverlapWithEntryTest_OverlappingEndTime_ReturnTrue()
        {
            DateTime? entryStartTime        = new(2026, 7, 11, 12, 0, 0);
            DateTime? entryEndTime          = new(2026, 7, 11, 12, 30, 0);
            DateTime? containerStartTime    = new(2026, 7, 11, 11, 45, 0);
            DateTime? containerEndTime      = new(2026, 7, 11, 12, 15, 0);

            TimesheetEntry entry            = new(0, new(entryStartTime, entryEndTime, "Entry", true));
            TimesheetEntryData container    = new(containerStartTime, containerEndTime, "Container", true);

            bool result = container.ContainedTimesOverlapWithEntry(entry);

            Assert.True(result);
        }
        [Fact]
        public void ContainedTimesOverlapWithEntryTest_NullEntryStartTime_ReturnFalse()
        {
            DateTime? entryStartTime        = null;
            DateTime? entryEndTime          = new(2026, 7, 11, 12, 30, 0);
            DateTime? containerStartTime    = new(2026, 7, 11, 12, 15, 0);
            DateTime? containerEndTime      = new(2026, 7, 11, 12, 45, 0);

            TimesheetEntry entry            = new(0, new(entryStartTime, entryEndTime, "Entry", true));
            TimesheetEntryData container    = new(containerStartTime, containerEndTime, "Container", true);

            bool result = container.ContainedTimesOverlapWithEntry(entry);

            Assert.False(result);
        }
        [Fact]
        public void ContainedTimesOverlapWithEntryTest_NullEntryEndTime_ReturnFalse()
        {
            DateTime? entryStartTime        = new(2026, 7, 11, 12, 0, 0);
            DateTime? entryEndTime          = null;
            DateTime? containerStartTime    = new(2026, 7, 11, 12, 15, 0);
            DateTime? containerEndTime      = new(2026, 7, 11, 12, 45, 0);

            TimesheetEntry entry            = new(0, new(entryStartTime, entryEndTime, "Entry", true));
            TimesheetEntryData container    = new(containerStartTime, containerEndTime, "Container", true);

            bool result = container.ContainedTimesOverlapWithEntry(entry);

            Assert.False(result);
        }
        [Fact]
        public void ContainedTimesOverlapWithEntryTest_SeparateTimes_ReturnFalse()
        {
            DateTime? entryStartTime = new(2026, 7, 11, 12, 0, 0);
            DateTime? entryEndTime = new(2026, 7, 11, 12, 30, 0);
            DateTime? containerStartTime = new(2026, 7, 11, 1, 0, 0);
            DateTime? containerEndTime = new(2026, 7, 11, 1, 30, 0);

            TimesheetEntry entry = new(0, new(entryStartTime, entryEndTime, "Entry", true));
            TimesheetEntryData container = new(containerStartTime, containerEndTime, "Container", true);

            bool result = container.ContainedTimesOverlapWithEntry(entry);

            Assert.False(result);
        }
    }
}
