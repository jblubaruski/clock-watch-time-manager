using BusinessManagementLibrary.TimeManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimecardDataTests
{
    /*
     * Tests for TimesheetEntryData.ContainedTImesOverlapWithEntry(TimesheetEntry)
     * 
     * Assert that a container with a clock in time that occurs between the clock in and out time of the passed in timecard returns true
     * Assert that a container with a clock out time that occurs between the clock in and out time of the passed in entry returns true
     * Assert that passing in an entry with a null clock in time returns false
     * Assert that passing in an entry with a null clock out time returns false
     * Assert that a container with a clock in and out time that occurs outside of the clock in and out time of the passed in entry returns false
     */
    public class ContainedTimesOverlapWithTimecardTest
    {
        [Fact]
        public void ContainedTimesOverlapWithTimecardTest_OverlappingStartTime_ReturnTrue()
        {
            DateTime? entryClockInTime      = new(2026, 7, 11, 12, 0, 0);
            DateTime? entryClockOutTime     = new(2026, 7, 11, 12, 30, 0);
            DateTime? containerClockInTime  = new(2026, 7, 11, 12, 15, 0);
            DateTime? containerClockOutTime = new(2026, 7, 11, 12, 45, 0);

            Timecard entry          = new(0, new(entryClockInTime, entryClockOutTime));
            TimecardData container  = new(containerClockInTime, containerClockOutTime);

            bool result = container.ContainedTimesOverlapWithTimecard(entry);

            Assert.True(result);
        }
        [Fact]
        public void ContainedTimesOverlapWithTimecardTest_OverlappingEndTime_ReturnTrue()
        {
            DateTime? entryClockInTime      = new(2026, 7, 11, 12, 0, 0);
            DateTime? entryClockOutTime     = new(2026, 7, 11, 12, 30, 0);
            DateTime? containerClockInTime  = new(2026, 7, 11, 11, 45, 0);
            DateTime? containerClockOutTime = new(2026, 7, 11, 12, 15, 0);

            Timecard entry          = new(0, new(entryClockInTime, entryClockOutTime));
            TimecardData container  = new(containerClockInTime, containerClockOutTime);

            bool result = container.ContainedTimesOverlapWithTimecard(entry);

            Assert.True(result);
        }
        [Fact]
        public void ContainedTimesOverlapWithTimecardTest_NullEntryStartTime_ReturnFalse()
        {
            DateTime? entryClockInTime      = null;
            DateTime? entryClockOutTime     = new(2026, 7, 11, 12, 30, 0);
            DateTime? containerClockInTime  = new(2026, 7, 11, 12, 15, 0);
            DateTime? containerClockOutTime = new(2026, 7, 11, 12, 45, 0);

            Timecard entry          = new(0, new(entryClockInTime, entryClockOutTime));
            TimecardData container  = new(containerClockInTime, containerClockOutTime);

            bool result = container.ContainedTimesOverlapWithTimecard(entry);

            Assert.False(result);
        }
        [Fact]
        public void ContainedTimesOverlapWithTimecardTest_NullEntryEndTime_ReturnFalse()
        {
            DateTime? entryClockInTime      = new(2026, 7, 11, 12, 0, 0);
            DateTime? entryClockOutTime     = null;
            DateTime? containerClockInTime  = new(2026, 7, 11, 12, 15, 0);
            DateTime? containerClockOutTime = new(2026, 7, 11, 12, 45, 0);

            Timecard entry          = new(0, new(entryClockInTime, entryClockOutTime));
            TimecardData container  = new(containerClockInTime, containerClockOutTime);

            bool result = container.ContainedTimesOverlapWithTimecard(entry);

            Assert.False(result);
        }
        [Fact]
        public void ContainedTimesOverlapWithTimecardTest_SeparateTimes_ReturnFalse()
        {
            DateTime? entryClockInTime      = new(2026, 7, 11, 12, 0, 0);
            DateTime? entryClockOutTime     = new(2026, 7, 11, 12, 30, 0);
            DateTime? containerStartTime    = new(2026, 7, 11, 1, 0, 0);
            DateTime? containerClockOutTime = new(2026, 7, 11, 1, 30, 0);

            Timecard entry          = new(0, new(entryClockInTime, entryClockOutTime));
            TimecardData container  = new(containerStartTime, containerClockOutTime);

            bool result = container.ContainedTimesOverlapWithTimecard(entry);

            Assert.False(result);
        }
    }
}
