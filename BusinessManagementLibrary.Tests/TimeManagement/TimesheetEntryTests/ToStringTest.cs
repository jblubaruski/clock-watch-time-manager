using BusinessManagementLibrary.Data;
using BusinessManagementLibrary.TimeManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TimesheetEntryTests
{
    /*
     * Test for TimesheetEntry.ToString()
     * 
     * Assert that the string representation of an entry with no null properties matches the following format:
     * {entry type} start:{20 - previous characters length # of spaces}{StartTime:MM/dd/yyyy hh:mm:ss tt}
     * {break description}
     * {entry type} end:{20 - previous characters length # of spaces}{EndTime:MM/dd/yyyy hh:mm:ss tt}
     * 
     * Assert that the string representation of an entry with a null start time matches the following format:
     * {entry type} start:{20 - previous characters length # of spaces}--/--/---- --:--:-- --
     * {break description}
     * {entry type} end:{20 - previous characters length # of spaces}{EndTime:MM/dd/yyyy hh:mm:ss tt}
     * 
     * Assert that the string representation of an entry with a null end time matches the following format:
     * {entry type} start:{20 - previous characters length # of spaces}{StartTime:MM/dd/yyyy hh:mm:ss tt}
     * {break description}
     * {entry type} end:{20 - previous characters length # of spaces}--/--/---- --:--:-- --
     * 
     * Assert that the string representation of an entry with a null description matches the following format:
     * {entry type} start:{20 - previous characters length # of spaces}{StartTime:MM/dd/yyyy hh:mm:ss tt}
     * ---
     * {entry type} end:{20 - previous characters length # of spaces}{EndTime:MM/dd/yyyy hh:mm:ss tt}
     */
    public class ToStringTest
    {
        [Fact]
        public void ToString_NoNullProperties_CorrectStringRepresentation()
        {
            EntryType   entryType   = EntryType.Break;
            int         entryID     = 0;
            DateTime    startTime   = new(2026, 7, 11, 12, 0, 0);
            DateTime    endTime     = new(2026, 7, 11, 12, 30, 0);
            string      description = "Lunch Break";

            string expectedOutput =
                $"{entryType} start:".PadRight(20) + $"{startTime:MM/dd/yyyy hh:mm:ss tt}\n" +
                $"{description}\n" +
                $"{entryType} end:".PadRight(20) + $"{endTime:MM/dd/yyyy hh:mm:ss tt}";

            TimesheetEntry  entry           = new(entryType, entryID, startTime, endTime, description);
            string          actualOutput    = entry.ToString();
            bool            result          = actualOutput == expectedOutput;

            Assert.True(result);
        }
        [Fact]
        public void ToString_NullStartTime_CorrectStringRepresentation()
        {
            EntryType   entryType   = EntryType.Break;
            int         entryID     = 0;
            DateTime?   startTime   = null;
            DateTime    endTime     = new(2026, 7, 11, 12, 30, 0);
            string      description = "Lunch Break";

            string expectedOutput =
                $"{entryType} start:".PadRight(20) + "--/--/---- --:--:-- --\n" +
                $"{description}\n" +
                $"{entryType} end:".PadRight(20) + $"{endTime:MM/dd/yyyy hh:mm:ss tt}";

            TimesheetEntry  entry           = new(entryType, entryID, startTime, endTime, description);
            string          actualOutput    = entry.ToString();
            bool            result          = actualOutput == expectedOutput;

            Assert.True(result);
        }
        [Fact]
        public void ToString_NullEndTime_CorrectStringRepresentation()
        {
            EntryType   entryType   = EntryType.Break;
            int         entryID     = 0;
            DateTime    startTime   = new(2026, 7, 11, 12, 0, 0);
            DateTime?   endTime     = null;
            string      description = "Lunch Break";

            string expectedOutput =
                $"{entryType} start:".PadRight(20) + $"{startTime:MM/dd/yyyy hh:mm:ss tt}\n" +
                $"{description}\n" +
                $"{entryType} end:".PadRight(20) + "--/--/---- --:--:-- --";

            TimesheetEntry  entry           = new(entryType, entryID, startTime, endTime, description);
            string          actualOutput    = entry.ToString();
            bool            result          = actualOutput == expectedOutput;

            Assert.True(result);
        }
        [Fact]
        public void ToString_NullDescription_CorrectStringRepresentation()
        {
            EntryType   entryType   = EntryType.Break;
            int         entryID     = 0;
            DateTime    startTime   = new(2026, 7, 11, 12, 0, 0);
            DateTime    endTime     = new(2026, 7, 11, 12, 30, 0);
            string?     description = null;

            string expectedOutput =
                $"{entryType} start:".PadRight(20) + $"{startTime:MM/dd/yyyy hh:mm:ss tt}\n" +
                 "---\n" +
                $"{entryType} end:".PadRight(20) + $"{endTime:MM/dd/yyyy hh:mm:ss tt}";

            TimesheetEntry  entry           = new(entryType, entryID, startTime, endTime, description);
            string          actualOutput    = entry.ToString();
            bool            result          = actualOutput == expectedOutput;

            Assert.True(result);
        }
    }
}
