using BusinessManagementLibrary.Data;
using BusinessManagementLibrary.TimeManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimecardTests
{
    /*
     * Test for Timecard.Timecard(int, TimecardData)
     * 
     * Assert that passing in a negative entryID throws an ArgumentException
     * Assert that passing in valid values creates a Timecard with the passed in data and an EntryType of EntryType.Timecard
     */
    public class ConstructorTest
    {
        [Fact]
        public void Ctor_NegativeEntryID_ThrowArgumentException()
        {
            int         entryID         = -1;
            DateTime?   clockInTime     = new(2026, 7, 11, 12, 0, 0);
            DateTime?   clockOutTime    = new(2026, 7, 11, 12, 30, 0);
            
            TimecardData data = new(clockInTime, clockOutTime);

            Assert.Throws<ArgumentException>(() => new Timecard(entryID, data));
        }
        [Fact]
        public void Ctor_ValidData_ConstructTimecard()
        {
            EntryType   entryType       = EntryType.Timecard;
            int         entryID         = 0;
            DateTime?   clockInTime     = new(2026, 7, 11, 12, 0, 0);
            DateTime?   clockOutTime    = new(2026, 7, 11, 12, 30, 0);
            
            TimecardData    data        = new(clockInTime, clockOutTime);
            Timecard        timecard    = new(entryID, data);

            bool result =
                timecard.EntryType      == entryType &&
                timecard.EntryID        == entryID &&
                timecard.ClockInTime    == clockInTime &&
                timecard.ClockOutTime   == clockOutTime;

            Assert.True(result);
        }
    }
}
