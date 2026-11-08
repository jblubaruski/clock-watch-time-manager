using ClockWatchTimeManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWatchTimeManager.Tests.Models.BreakTests
{
    /*
     * Tests for Models.Break.ToString()
     * 
     * Assert that calling ToString with no null properties returns the following string:
     * Break start: MM/dd/yyyy hh:mm:ss tt
     * {break description}
     * Break end:   MM/dd/yyyy hh:mm:ss tt
     * 
     * Assert that calling ToString on a break with a null start time returns the following string:
     * Break start: --/--/---- --:--:-- --
     * {break description}
     * Break end:   MM/dd/yyyy hh:mm:ss tt
     * 
     * Assert that calling ToString on a break with a null description returns the following string:
     * Break start: MM/dd/yyyy hh:mm:ss tt
     * ---
     * Break end:   MM/dd/yyyy hh:mm:ss tt
     * 
     * Assert that calling ToString on a break with a null end time returns the following string:
     * Break start: MM/dd/yyyy hh:mm:ss tt
     * {break description}
     * Break end:   --/--/---- --:--:-- --
     */
    public class ToStringTest
    {
        public static readonly DateTime startTime = new(2026, 4, 18, 11, 30, 0);
        public static readonly DateTime endTime = new(2026, 4, 18, 11, 45, 0);
        public static readonly string description = "Morning break";

        [Fact]
        public void ToString_NoNull_ProperFormat()
        {
            string expectedOutput =
                $"Break start: {startTime:MM/dd/yyyy hh:mm:ss tt}\n" +
                $"{description}\n" +
                $"Break end:   {endTime:MM/dd/yyyy hh:mm:ss tt}";

            Break testBreak = new(startTime, endTime, description);
            string output = testBreak.ToString();

            bool result = output == expectedOutput;

            Assert.True(result);
        }
        [Fact]
        public void ToString_NullStartTime_ProperFormat()
        {
            string expectedOutput =
                "Break start: --/--/---- --:--:-- --\n" +
                $"{description}\n" +
                $"Break end:   {endTime:MM/dd/yyyy hh:mm:ss tt}";

            Break testBreak = new(null, endTime, description);
            string output = testBreak.ToString();

            bool result = output == expectedOutput;

            Assert.True(result);
        }
        [Fact]
        public void ToString_NullEndTime_ProperFormat()
        {
            string expectedOutput =
                $"Break start: {startTime:MM/dd/yyyy hh:mm:ss tt}\n" + 
                $"{description}\n" + 
                "Break end:   --/--/---- --:--:-- --";

            Break testBreak = new(startTime, null, description);
            string output = testBreak.ToString();

            bool result = output == expectedOutput;

            Assert.True(result);
        }
        [Fact]
        public void ToString_NullDescription_ProperFormat()
        {
            string expectedOutput =
                $"Break start: {startTime:MM/dd/yyyy hh:mm:ss tt}\n" +
                "---\n" +
                $"Break end:   {endTime:MM/dd/yyyy hh:mm:ss tt}";

            Break testBreak = new(startTime, endTime, null);
            string output = testBreak.ToString();

            bool result = output == expectedOutput;

            Assert.True(result);
        }
    }
}
