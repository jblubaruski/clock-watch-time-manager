using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.FileValidatorTests
{
    /*
     * Tests for DirectoryPathIsValid(string, out string)
     * 
     * Assert that passing in a path with an invalid directory name returns false
     * Assert that passing in a path with an invalid drive indicator returns false
     * Assert that passing in a null pointer returns false
     * Assert that passing in an empty string returns false
     * Assert that passing in a string that is entirely whitespace returns false
     * Assert that passing in a valid path that starts with a valid root drive indicator returns true
     * Assert that passing in a valid path that starts with a directory separator returns true
     * Assert that passing in a valid path that starts with a directory name returns true
     */
    public class DirectoryPathIsValidTest
    {
        [Fact]
        public void DirectoryPathIsValid_InvalidDirectoryName_ReturnFalse()
        {
            string input = "...";
            bool result = FileValidator.DirectoryPathIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryPathIsValid_InvalidDriveIndicator_ReturnFalse()
        {
            string input = "1" + Path.VolumeSeparatorChar + Path.DirectorySeparatorChar;
            bool result = FileValidator.DirectoryPathIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryPathIsValid_NullPointer_ReturnFalse()
        {
            string input = null!;
            bool result = FileValidator.DirectoryPathIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryPathIsValid_EmptyString_ReturnFalse()
        {
            string input = "";
            bool result = FileValidator.DirectoryPathIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryPathIsValid_WhitespaceString_ReturnFalse()
        {
            string input = " ";
            bool result = FileValidator.DirectoryPathIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryPathIsValid_ValidPathStartsWithValidDriveIndicator_ReturnTrue()
        {
            string input = "A:" + Path.DirectorySeparatorChar + "a";
            bool result = FileValidator.DirectoryPathIsValid(input, out string errorMessage);
            Assert.True(result);
        }
        [Fact]
        public void DirectoryPathIsValid_ValidPathStartsWithDirectorySeparator_ReturnTrue()
        {
            string input = Path.DirectorySeparatorChar + "a";
            bool result = FileValidator.DirectoryPathIsValid(input, out string errorMessage);
            Assert.True(result);
        }
        [Fact]
        public void DirectoryPathIsValid_ValidPathStartsWithDirectory_ReturnTrue()
        {
            string input = "a" + Path.DirectorySeparatorChar + "b";
            bool result = FileValidator.DirectoryPathIsValid(input, out string errorMessage);
            Assert.True(result);
        }
    }
}
