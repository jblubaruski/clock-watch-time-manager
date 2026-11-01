using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.FileValidatorTests
{
    /*
     * Tests for ExtensionIsValid(string, out string)
     * 
     * Assert that passing in a null pointer returns false
     * Assert that passing in an empty string returns false
     * Assert that passing in a string that is entirely whitespace returns false
     * Assert that passing in a string that doesn't start with a period returns false
     * Assert that passing in a string with only a period returns false
     * Assert that passing in a string with multiple periods returns false
     * Assert that passing in a string that contains any of the characters below returns false
     * '?', ' ', '/', '\', '%', '*', ':', '|', '"', '<', '>', ',', ';', '='
     * Assert that passing in a valid extension returns true
     */
    public class ExtensionIsValidTest
    {
        [Fact]
        public void ExtensionIsValid_NullPointer_ReturnFalse()
        {
            string input = null!;
            bool result = FileValidator.ExtensionIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void ExtensionIsValid_EmptyString_ReturnFalse()
        {
            string input = "";
            bool result = FileValidator.ExtensionIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void ExtensionIsValid_WhitespaceString_ReturnFalse()
        {
            string input = " ";
            bool result = FileValidator.ExtensionIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void ExtensionIsValid_ExtensionDoesntStartWithPeriod_ReturnFalse()
        {
            string input = "a";
            bool result = FileValidator.ExtensionIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void ExtensionIsValid_ExtensionOnlyHasPeriod_ReturnFalse()
        {
            string input = ".";
            bool result = FileValidator.ExtensionIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void ExtensionIsValid_ExtensionHasMultiplePeriods_ReturnFalse()
        {
            string input = "..a";
            bool result = FileValidator.ExtensionIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Theory]
        [InlineData(".?")]
        [InlineData(". ")]
        [InlineData("./")]
        [InlineData(".\\")]
        [InlineData(".%")]
        [InlineData(".*")]
        [InlineData(".:")]
        [InlineData(".|")]
        [InlineData(".\"")]
        [InlineData(".<")]
        [InlineData(".>")]
        [InlineData(".,")]
        [InlineData(".;")]
        [InlineData(".=")]
        public void ExtensionIsValid_ExtensionHasInvalidCharacter_ReturnFalse(string input)
        {
            bool result = FileValidator.ExtensionIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void ExtensionIsValid_ValidExtension_ReturnTrue()
        {
            string input = ".a";
            bool result = FileValidator.ExtensionIsValid(input, out string errorMessage);
            Assert.True(result);
        }
    }
}
