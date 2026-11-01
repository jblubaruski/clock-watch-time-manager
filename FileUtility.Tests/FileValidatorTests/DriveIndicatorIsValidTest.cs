using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.FileValidatorTests
{
    /*
     * Tests for DriveIndicatorIsValid
     * 
     * Assert that passing in a null pointer returns false
     * Assert that passing in a string with more or less than two characters returns false
     * Assert that passing in a string that starts with a character that isnt an ascii letter returns false
     * Assert that passing in a string where the second character isnt a volume separator returns false
     * Assert that passing in a valid drive indicator returns true
     */
    public class DriveIndicatorIsValidTest
    {
        [Fact]
        public void DriveIndicatorIsValid_NullPointer_ReturnFalse()
        {
            string input = null!;
            bool result = FileValidator.DriveIndicatorIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Theory]
        [InlineData("AB")]
        [InlineData("")]
        public void DriveIndicatorIsValid_MoreThanTwoCharacters_ReturnFalse(string input)
        {
            input = input + Path.VolumeSeparatorChar;
            bool result = FileValidator.DriveIndicatorIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void DriveIndicatorIsValid_InvalidFirstCharacter_ReturnFalse()
        {
            string input = "1" + Path.VolumeSeparatorChar;
            bool result = FileValidator.DriveIndicatorIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void DriveIndicatorIsValid_InvalidSecondCharacter_ReturnFalse()
        {
            string input = "AB";
            bool result = FileValidator.DriveIndicatorIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void DriveIndicatorIsValid_ValidDriveIndicator_ReturnTrue()
        {
            string input = "A" + Path.VolumeSeparatorChar;
            bool result = FileValidator.DriveIndicatorIsValid(input, out string errorMessage);
            Assert.True(result);
        }
    }
}
