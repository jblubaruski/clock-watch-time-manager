using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.FileValidatorTests
{
    /*
     * Tests for FilenameIsValid(string, string, out string)
     * 
     * Assert that passing in an invalid base filename returns false
     * Assert that passing in an invalid extension returns false
     * Assert that passing in strings with a combined length that is greater than 255 characters returns false
     * Assert that passing in a valid filename returns true
     */
    public class FilenameIsValidTest
    {
        [Fact]
        public void FilenameIsValid_InvalidBaseFilename_ReturnFalse()
        {
            string baseFilename = "-a";
            string extension = ".a";

            bool result = FileValidator.FilenameIsValid(baseFilename, extension, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void FilenameIsValid_InvalidExtension_ReturnFalse()
        {
            string baseFilename = "a";
            string extension = ".?";
            bool result = FileValidator.FilenameIsValid(baseFilename, extension!, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void FilenameIsValid_InvalidNameLength_ReturnFalse()
        {
            const int MAX_FILENAME_LENGTH = 255;
            string extension = ".a";
            string baseFilename = "".PadLeft(MAX_FILENAME_LENGTH + 1 - extension.Length);

            bool result = FileValidator.FilenameIsValid(baseFilename, extension, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void FilenameIsValid_ValidName_ReturnTrue()
        {
            string baseFilename = "a";
            string extension = ".a";

            bool result = FileValidator.FilenameIsValid(baseFilename, extension, out string errorMessage);
            Assert.True(result);
        }
    }
}
