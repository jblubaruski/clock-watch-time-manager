using BusinessManagementLibrary.TimeManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimecardTests
{
    /*
     * Test for ClockInTime.Set
     * 
     * Assert that passing in a non-null clock in time that is greater than the existing non-null clock out time throws an ArgumentException
     */
    public class ClockInTimeSetTest
    {
        [Fact]
        public void Set_ClockInTimeGreaterThanClockOutTime_ThrowArgumentException()
        {
            DateTime? clockOutTime = new(2026, 7, 11, 12, 0, 0);
            DateTime? clockInTime = new(2026, 7, 11, 13, 0, 0);

            TimecardData data = new(null, clockOutTime);
            Timecard timecard = new(0, data);

            Assert.Throws<ArgumentException>(() => timecard.ClockInTime = clockInTime);
        }
    }
}
