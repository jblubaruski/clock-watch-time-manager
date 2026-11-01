using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.FileValidatorTests
{
    /*
     * Tests for DirectoryNameIsValid
     * 
     * Assert that passing in a null pointer returns false
     * Assert that passing in an empty string returns false
     * Assert that passing in a string that is entirely whitespace returns false
     * Assert that passing in a string that contains any of the characters below returns false
     * '?', ' ', '%', '*', ':', '|', '"', '<', '>', ',', ';', '='
     * Assert that passing in a string containing the alt directory separator character returns false
     * Assert that passing in a string name that is more than two periods in a row returns false
     * Assert that passing in a string (not "." or "..") starting with '-', ' ', '_', and '.' returns false
     * Assert that passing in a string with a directory name (not "." or "..") that ends with a period returns false
     * Assert that passing in a valid directory name returns true
     */
    public class DirectoryNameIsValidTest
    {
        [Fact]
        public void DirectoryNameIsValid_NullPointer_ReturnFalse()
        {
            string input = null!;
            bool result = FileValidator.DirectoryNameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryNameIsValid_EmptyString_ReturnFalse()
        {
            string input = "";
            bool result = FileValidator.DirectoryNameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryNameIsValid_WhitespaceString_ReturnFalse()
        {
            string input = " ";
            bool result = FileValidator.DirectoryNameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Theory]
        [InlineData("a?")]
        [InlineData("a ")]
        [InlineData("a%")]
        [InlineData("a*")]
        [InlineData("a:")]
        [InlineData("a|")]
        [InlineData("a\"")]
        [InlineData("a<")]
        [InlineData("a>")]
        [InlineData("a,")]
        [InlineData("a;")]
        [InlineData("a=")]
        public void DirectoryNameIsValid_NameContainsInvalidCharacter_ReturnFalse(string input)
        {
            bool result = FileValidator.DirectoryNameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryNameIsValid_NameContainsAltDirectorySeparator_ReturnFalse()
        {
            string input = "a" + Path.AltDirectorySeparatorChar;
            bool result = FileValidator.DirectoryNameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryNameIsValid_NameContainsMoreThanTwoPeriods_ReturnFalse()
        {
            string input = "...";
            bool result = FileValidator.DirectoryNameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Theory]
        [InlineData("_a")]
        [InlineData(" a")]
        [InlineData("-a")]
        [InlineData(".a")]
        public void DirectoryNameIsValid_NameHasInvalidStart_ReturnFalse(string input)
        {
            bool result = FileValidator.DirectoryNameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryNameIsValid_NameEndsWithPeriod_ReturnFalse()
        {
            string input = "a.";
            bool result = FileValidator.DirectoryNameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryNameIsValid_ValidName_ReturnTrue()
        {
            string input = "a";
            bool result = FileValidator.DirectoryNameIsValid(input, out string errorMessage);
            Assert.True(result);
        }
    }
}
