using BusinessManagementLibrary.TimeManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimesheetEntryDataTests
{
    /*
     * Test for TimesheetEntryData.TimesheetEntryData(DateTime?, DateTime?, string?, bool)
     * 
     * Assert that passing in a start time that is greater than the end time throws an ArgumentException
     * Assert that passing in a start time that is equal to the end time throwa an ArgumentException
     * Assert that the constructed container holds the values that were passed in to the constructor when the arguments are valid
     */
    public class ConstructorTest
    {
        public static readonly DateTime? endTime = new(2026, 7, 11, 12, 0, 0);
        public static readonly DateTime? startTimeGreaterThan = new(2026, 7, 11, 12, 30, 0);
        public static readonly DateTime? startTimeEqualTo = endTime;

        [Fact]
        public void Ctor_StartTimeGreaterThanEndTime_ThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new TimesheetEntryData(startTimeGreaterThan, endTime, "Test", true));
        }
        [Fact]
        public void Ctor_StartTimeEqualToEndTime_ThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new TimesheetEntryData(startTimeEqualTo, endTime, "Test", true));
        }
        [Fact]
        public void Ctor_ValidInput_PropertiesAreEqualToInputs()
        {
            DateTime?   startTime   = new(2026, 7, 11, 11, 30, 0);
            string?     description = "Test";
            bool        isBreak     = true;

            TimesheetEntryData entryData = new(startTime, endTime, description, isBreak);

            bool result =
                entryData.StartTime     == startTime    &&
                entryData.EndTime       == endTime      &&
                entryData.Description   == description  &&
                entryData.IsBreak       == isBreak;

            Assert.True(result);
        }
    }
}
