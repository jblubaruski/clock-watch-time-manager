using BusinessManagementLibrary.TimeManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimecardTests
{
    /*
     * Test for ClockOutTime.Set
     * 
     * Assert that passing in a non-null clock out time that is less than the existing non-null clock in time throws an ArgumentException
     */
    public class ClockOutTimeSetTest
    {
        [Fact]
        public void Set_ClockOutTimeGreaterThanClockInTime_ThrowArgumentException()
        {
            DateTime? clockInTime = new(2026, 7, 11, 12, 0, 0);
            DateTime? clockOutTime = new(2026, 7, 11, 11, 0, 0);

            TimecardData data = new(clockInTime, null);
            Timecard timecard = new(0, data);

            Assert.Throws<ArgumentException>(() => timecard.ClockOutTime = clockOutTime);
        }
    }
}
