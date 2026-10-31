using ClockWatchTimeManager.Models;

namespace ClockWatchTimeManagerUnitTester
{
    public class TimeCardCatalogUnitTest
    {
        /*
         * Assert that passing a null argument throws an ArgumentNullException
         */
        [Fact]
        public void AddTimeCardTest()
        {
            TimeCardCatalog testCatalog = new();
            Assert.Throws<ArgumentNullException>(() => testCatalog.AddTimeCard(null!));
        }

        /*
         * Assert that the ToString() method returns a string of the format:
         * 
         * Time Cards:
         * ********************
         * <Time Card 1>
         * --------------------
         * <Time Card 2>
         * --------------------
         * ...
         * --------------------
         * <Time Card N>
         */
        [Fact]
        public void ToStringTest()
        {
            TimeCardCatalog testCatalog = new();
            TimeCard testCard1 = new() { ClockInDateTime = new DateTime(2025, 5, 16, 9, 0, 0), ClockOutDateTime = new DateTime(2025, 5, 16, 17, 0, 0) };
            TimeCard testCard2 = new() { ClockInDateTime = new DateTime(2023, 7, 11, 5, 0, 0), ClockOutDateTime = new DateTime(2023, 7, 11, 13, 30, 0) };
            string expectedOutput1 = "Time Cards:\n" +
                                     "********************";
            string expectedOutput2 = "Time Cards:\n" +
                                     "********************\n" +
                                     testCard1.ToString();
            string expectedOutput3 = "Time Cards:\n" +
                                     "********************\n" +
                                     testCard1.ToString() + "\n" +
                                     "--------------------\n" +
                                     testCard2.ToString();

            Assert.Equal(expectedOutput1, testCatalog.ToString());
            testCatalog.AddTimeCard(testCard1);
            Assert.Equal(expectedOutput2, testCatalog.ToString());
            testCatalog.AddTimeCard(testCard2);
            Assert.Equal(expectedOutput3, testCatalog.ToString());
        }
    }
}
