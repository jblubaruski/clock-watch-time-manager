using BusinessManagementLibrary.TimeManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimecardDataTests
{
    /*
     * Test for TimecardData.TimecardData(DateTime?, DateTime?)
     * 
     * Assert that passing in a clock in time that is greater than the clock out time throws an ArgumentException
     * Assert that passing in a clock in time that is equal to the clock out time throwa an ArgumentException
     * Assert that the constructed container holds the values that were passed in to the constructor when the arguments are valid
     */
    public class ConstructorTest
    {
        public static readonly DateTime? clockOutTime = new(2026, 7, 11, 12, 0, 0);
        public static readonly DateTime? clockInTimeGreaterThan = new(2026, 7, 11, 12, 30, 0);
        public static readonly DateTime? clockInTimeEqualTo = clockOutTime;

        [Fact]
        public void Ctor_ClockInTimeGreaterThanClockOutTime_ThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new TimecardData(clockInTimeGreaterThan, clockOutTime));
        }
        [Fact]
        public void Ctor_ClockInTimeEqualToClockOutTime_ThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new TimecardData(clockInTimeEqualTo, clockOutTime));
        }
        [Fact]
        public void Ctor_ValidInput_PropertiesAreEqualToInputs()
        {
            DateTime?   clockInTime = new(2026, 7, 11, 11, 30, 0);
            string?     description = "Test";
            bool        isBreak     = true;

            TimesheetEntryData entryData = new(clockInTime, clockOutTime, description, isBreak);

            bool result =
                entryData.StartTime     == clockInTime &&
                entryData.EndTime       == clockOutTime &&
                entryData.Description   == description  &&
                entryData.IsBreak       == isBreak;

            Assert.True(result);
        }
    }
}
