using BusinessManagementLibrary.Data;
using BusinessManagementLibrary.TimeManagement;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Sdk;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimesheetEntryTests
{
    // TODO UPDATE TO REFLECT CHANGES TO CONSTRUCTOR

    /* 
     * Test for TimesheetEntry.TimesheetEntry(int, TimesheetEntryData)
     * 
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
            bool        isBreak     = true;
            int         entryID     = 0;
            DateTime    startTime   = new(2026, 7, 11, 12, 00, 0);
            DateTime    endTime     = new(2026, 7, 11, 13, 30, 0);
            string      description = "Test";

            TimesheetEntryData data = new(startTime, endTime, description, isBreak);
            TimesheetEntry entry = new(entryID, data);

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
            EntryType   entryType   = EntryType.Task;
            bool        isBreak     = false;
            int         entryID     = 0;
            DateTime?   startTime   = null;
            DateTime?   endTime     = null;
            string?     description = null;

            TimesheetEntryData  data    = new(startTime, endTime, description, isBreak);
            TimesheetEntry      entry   = new(entryID, data);

            bool result =
                entry.EntryType     == entryType    &&
                entry.EntryID       == entryID      &&
                entry.StartTime     == startTime    &&
                entry.EndTime       == endTime      &&
                entry.Description   == description;

            Assert.True(result);
        }
        [Fact]
        public void Ctor_NegativeEntryID_ThrowArgumentException()
        {
            int entryID = -1;

            Assert.Throws<ArgumentException>(() => new TimesheetEntry(entryID, new(null, null, null, true)));
        }
        [Fact]
        public void Ctor_StartTimeGreaterThanEndTime_ThrowArgumentException()
        {
            DateTime startTime  = new(2026, 7, 11, 12, 30, 0);
            DateTime endTime    = new(2026, 7, 11, 12, 0, 0);

            Assert.Throws<ArgumentException>(() => new TimesheetEntry(0, new(startTime, endTime, null, true)));
        }
        [Fact]
        public void Ctor_StartTimeEqualToEndTime_ThrowArgumentException()
        {
            DateTime startTime = new(2026, 7, 11, 12, 0, 0);
            DateTime endTime = new(2026, 7, 11, 12, 0, 0);

            Assert.Throws<ArgumentException>(() => new TimesheetEntry(0, new (startTime, endTime, null, true)));
        }
    }
}
