using BusinessManagementLibrary.Data;
using BusinessManagementLibrary.TimeManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimesheetEntryTests
{
    /*
     * Test for TimesheetEntry.TimesheetEntry(EntryType, int, DateTime?, DateTime?, string?)
     * 
     * Assert that passing in a default entry type to entryType throws an ArgumentException
     * Assert that passing in a negative integer to entryID throws an ArgumentException
     * Assert that passing in a start time that is greater than the end time throws an ArgumentException
     * Assert that passing in a start time that is equal to the end time throws an ArgumentException
     * Assert that constructing a TimesheetEntry with valid arguments creates an entry with its properties equal to the arguments
     * Assert that constructing a TimesheetEntry with a null start time, end time and description creates an entry with its properties equal to the arguments
     */
    public class ConstructorTest
    {
        [Fact]
        public void Ctor_ValidNonNullArguments_ConstructTimesheetEntry()
        {
            EntryType   entryType   = EntryType.Break;
            int         entryID     = 0;
            DateTime    startTime   = new(2026, 7, 11, 12, 00, 0);
            DateTime    endTime     = new(2026, 7, 11, 13, 30, 0);
            string      description = "Test";

            TimesheetEntry entry = new(entryType, entryID, startTime, endTime, description);

            bool result =
                entry.EntryType     == entryType    &&
                entry.EntryID       == entryID      &&
                entry.StartTime     == startTime    &&
                entry.EndTime       == endTime      &&
                entry.Description   == description;

            Assert.True(result);
        }
        [Fact]
        public void Ctor_ValidNullArguments_ConstructTimesheetEntry()
        {
            EntryType   entryType   = EntryType.Break;
            int         entryID     = 0;
            DateTime?   startTime   = null;
            DateTime?   endTime     = null;
            string?     description = null;

            TimesheetEntry entry = new(entryType, entryID, startTime, endTime, description);

            bool result =
                entry.EntryType     == entryType    &&
                entry.EntryID       == entryID      &&
                entry.StartTime     == startTime    &&
                entry.EndTime       == endTime      &&
                entry.Description   == description;

            Assert.True(result);
        }
        [Fact]
        public void Ctor_DefaultEntryType_ThrowArgumentException()
        {
            EntryType entryType = EntryType.Default;

            Assert.Throws<ArgumentException>(() => new TimesheetEntry(entryType, 0, null, null, null));
        }
        [Fact]
        public void Ctor_NegativeEntryID_ThrowArgumentException()
        {
            int entryID = -1;

            Assert.Throws<ArgumentException>(() => new TimesheetEntry(EntryType.Break, entryID, null, null, null));
        }
        [Fact]
        public void Ctor_StartTimeGreaterThanEndTime_ThrowArgumentException()
        {
            DateTime startTime  = new(2026, 7, 11, 12, 30, 0);
            DateTime endTime    = new(2026, 7, 11, 12, 0, 0);

            Assert.Throws<ArgumentException>(() => new TimesheetEntry(EntryType.Break, 0, startTime, endTime, null));
        }
        [Fact]
        public void Ctor_StartTimeEqualToEndTime_ThrowArgumentException()
        {
            DateTime startTime = new(2026, 7, 11, 12, 0, 0);
            DateTime endTime = new(2026, 7, 11, 12, 0, 0);

            Assert.Throws<ArgumentException>(() => new TimesheetEntry(EntryType.Break, 0, startTime, endTime, null));
        }
    }
}
