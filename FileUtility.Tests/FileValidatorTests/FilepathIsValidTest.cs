using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.FileValidatorTests
{
    /*
     * Tests for FilepathIsValid(string, string, string, out string)
     * 
     * Assert that passing in an invalid base filename returns false
     * Assert that passing in an invalid extension returns false
     * Assert that passing in an invalid directory path returns false
     * Assert that passing in strings with a combined length that is greater than 32,766 characters returns false
     * Assert that passing in a valid filepath returns true
     */
    public class FilepathIsValidTest
    {
        [Fact]
        public void FilepathIsValid_InvalidBaseFilename_ReturnFalse()
        {
            string baseFilename = "-a";
            string extension = ".a";
            string directoryPath = Path.DirectorySeparatorChar + "a";
            bool result = FileValidator.FilepathIsValid(baseFilename!, extension, directoryPath, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void FilepathIsValid_InvalidExtension_ReturnFalse()
        {
            string baseFilename = "a";
            string extension = "a";
            string directoryPath = Path.DirectorySeparatorChar + "a";
            bool result = FileValidator.FilepathIsValid(baseFilename, extension!, directoryPath, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void FilepathIsValid_InvalidDirectoryPath_ReturnFalse()
        {
            string baseFilename = "a";
            string extension = "a";
            string directoryPath = Path.DirectorySeparatorChar + "a?";
            bool result = FileValidator.FilepathIsValid(baseFilename, extension, directoryPath, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void FilepathIsValid_DirectoryPathIsNull_ReturnFalse()
        {
            string baseFilename = "a";
            string extension = ".a";
            string directoryPath = null!;

            bool result = FileValidator.FilepathIsValid(baseFilename, extension, directoryPath, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void FilepathIsValid_DirectoryPathHasAltSeparator_ReturnFalse()
        {
            string baseFilename = "a";
            string extension = ".a";
            string directoryPath = Path.DirectorySeparatorChar + "a" + Path.AltDirectorySeparatorChar;

            bool result = FileValidator.FilepathIsValid(baseFilename, extension, directoryPath, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void FilepathIsValid_InvalidPathLength_ReturnFalse()
        {
            const int MAX_PATH_LENGTH = 32766;
            string directoryPath = Path.DirectorySeparatorChar + "a" + Path.AltDirectorySeparatorChar;
            string extension = ".a";
            string baseFilename = "".PadLeft(MAX_PATH_LENGTH + 1 - directoryPath.Length - extension.Length);

            bool result = FileValidator.FilepathIsValid(baseFilename, extension, directoryPath, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void FilepathIsValid_ValidPath_ReturnTrue()
        {
            string baseFilename = "a";
            string extension = ".a";
            string directoryPath = "a" + Path.DirectorySeparatorChar;

            bool result = FileValidator.FilepathIsValid(baseFilename, extension, directoryPath, out string errorMessage);
            Assert.True(result);
        }
    }
}
