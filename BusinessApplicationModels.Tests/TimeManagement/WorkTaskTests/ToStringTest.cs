using BusinessApplicationModels.TimeManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessApplicationModels.Tests.TimeManagement.WorkTaskTests
{
    /*
     * Tests for Models.WorkTask.ToString()
     * 
     * Assert that calling ToString with no null properties returns the following string:
     * Task start:  MM/dd/yyyy hh:mm:ss tt
     * {task description}
     * Task end:    MM/dd/yyyy hh:mm:ss tt
     * 
     * Assert that calling ToString on a task with a null start time returns the following string:
     * Task start:  --/--/---- --:--:-- --
     * {task description}
     * Task end:    MM/dd/yyyy hh:mm:ss tt
     * 
     * Assert that calling ToString on a task with a null description returns the following string:
     * Task start:  MM/dd/yyyy hh:mm:ss tt
     * ---
     * Task end:    MM/dd/yyyy hh:mm:ss tt
     * 
     * Assert that calling ToString on a task with a null end time returns the following string:
     * Task start:  MM/dd/yyyy hh:mm:ss tt
     * {task description}
     * Task end:    --/--/---- --:--:-- --
     */
    public class ToStringTest
    {
        public static readonly DateTime startTime = new(2026, 4, 18, 11, 30, 0);
        public static readonly DateTime endTime = new(2026, 4, 18, 11, 45, 0);
        public static readonly string description = "Fucking around and testing";

        [Fact]
        public void ToString_NoNull_ProperFormat()
        {
            string expectedOutput =
                $"Task start:  {startTime:MM/dd/yyyy hh:mm:ss tt}\n" +
                $"{description}\n" +
                $"Task end:    {endTime:MM/dd/yyyy hh:mm:ss tt}";

            WorkTask testTask = new(startTime, endTime, description);
            string output = testTask.ToString();

            bool result = output == expectedOutput;

            Assert.True(result);
        }
        [Fact]
        public void ToString_NullStartTime_ProperFormat()
        {
            string expectedOutput =
                "Task start:  --/--/---- --:--:-- --\n" +
                $"{description}\n" +
                $"Task end:    {endTime:MM/dd/yyyy hh:mm:ss tt}";

            WorkTask testTask = new(null, endTime, description);
            string output = testTask.ToString();

            bool result = output == expectedOutput;

            Assert.True(result);
        }
        [Fact]
        public void ToString_NullEndTime_ProperFormat()
        {
            string expectedOutput =
                $"Task start:  {startTime:MM/dd/yyyy hh:mm:ss tt}\n" +
                $"{description}\n" +
                "Task end:    --/--/---- --:--:-- --";

            WorkTask testTask = new(startTime, null, description);
            string output = testTask.ToString();

            bool result = output == expectedOutput;

            Assert.True(result);
        }
        [Fact]
        public void ToString_NullDescription_ProperFormat()
        {
            string expectedOutput =
                $"Task start:  {startTime:MM/dd/yyyy hh:mm:ss tt}\n" +
                "---\n" +
                $"Task end:    {endTime:MM/dd/yyyy hh:mm:ss tt}";

            WorkTask testTask = new(startTime, endTime, null);
            string output = testTask.ToString();

            bool result = output == expectedOutput;

            Assert.True(result);
        }
    }
}
