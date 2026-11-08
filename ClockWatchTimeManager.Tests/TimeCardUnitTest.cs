using ClockWatchTimeManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWatchTimeManager.Test
{
    public class TimeCardUnitTest
    {
        /*
         * Assert that the ToString() method returns a string of the format:
         * 
         * Clock in:  <ClockInTime.ToString()>
         * Clock out: <ClockOutTIme.ToString()>
         * 
         * If either ClockInTime or ClockOutTime has a null value,
         * they should be represented with 20 hyphens.
         */
        [Fact]
        public void ToStringTest()
        {
            // card with null values for ClockInTime and ClockOutTime
            TimeCard testCard1 = new();
            string expectedOutput1 = "Clock in:  --------------------\n" +
                                     "Clock out: --------------------";

            // card with a null value for ClockOutTime
            DateTime testCard2ClockIn = new(2022, 5, 10, 15, 26, 17);
            TimeCard testCard2 = new() { ClockInDateTime = testCard2ClockIn };
            string expectedOutput2 = "Clock in:  " + testCard2ClockIn.ToString() + "\n" +
                                     "Clock out: --------------------";

            // card with a null value for ClockInTime
            DateTime testCard3ClockOut = new(2024, 7, 23, 1, 39, 0);
            TimeCard testCard3 = new() { ClockOutDateTime = testCard3ClockOut };
            string expectedOutput3 = "Clock in:  --------------------\n" +
                                     "Clock out: " + testCard3ClockOut.ToString();

            // card with no null values
            DateTime testCard4ClockIn = new(1969, 6, 13, 9, 0, 0);
            DateTime testCard4ClockOut = new(1969, 6, 13, 5, 0, 0);
            TimeCard testCard4 = new() { ClockInDateTime = testCard4ClockIn, ClockOutDateTime = testCard4ClockOut };
            string expectedOutput4 = "Clock in:  " + testCard4ClockIn.ToString() + "\n" +
                                     "Clock out: " + testCard4ClockOut.ToString();

            Assert.Equal(expectedOutput1, testCard1.ToString());
            Assert.Equal(expectedOutput2, testCard2.ToString());
            Assert.Equal(expectedOutput3, testCard3.ToString());
            Assert.Equal(expectedOutput4, testCard4.ToString());
        }
    }
}
