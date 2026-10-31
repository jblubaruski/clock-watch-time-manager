using Clock_Watch_Time_Manager.Services;

using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWatchTimeManager.Tests
{
    public class FileHelperUnitTest
    {
        /*
         * Tests for DirectoryPathIsValid(string, out string)
         * 
         * Assert that passing in a null pointer returns false
         * Assert that passing in an empty string returns false
         * Assert that passing in a string that is entirely whitespace returns false
         * Assert that passing in a string with a directory name that contains any of the characters below returns false
         * '?', ' ', '%', '*', ':', '|', '"', '<', '>', ',', ';', '='
         * Assert that passing in a string containing the alt directory separator character returns false
         * Assert that passing in a string with a directory name that contains more than two periods returns false
         * Assert that passing in a string with a directory name (not "." or "..") starting with '-', ' ', '_', and '.' returns false
         * Assert that passing in a string with a directory name (not "." or "..") that ends with a period returns false
         * Assert that passing in a string with a root drive indicator pointing to a drive labelled with a non-alphabetical character returns false
         * Assert that passing in a valid path that starts with a valid root drive indicator returns true
         * Assert that passing in a valid path that starts with a directory separator returns true
         * Assert that passing in a valid path that starts with a directory name returns true
         */
        [Fact]
        public void DirectoryPathIsValid_NullPointer_ReturnFalse()
        {
            string input = null!;
            bool result = FileHelper.DirectoryPathIsValid(input, out string message);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryPathIsValid_EmptyString_ReturnFalse()
        {
            string input = "";
            bool result = FileHelper.DirectoryPathIsValid(input!, out string message);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryPathIsValid_WhitespaceString_ReturnFalse()
        {
            string input = " ";
            bool result = FileHelper.DirectoryPathIsValid(input!, out string message);
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
        public void DirectoryPathIsValid_DirectoryContainsInvalidCharacter_ReturnFalse(string input)
        {
            input = Path.DirectorySeparatorChar + input;
            bool result = FileHelper.DirectoryPathIsValid(input, out string message);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryPathIsValid_DirectoryContainsAltDirectorySeparator_ReturnFalse()
        {
            string input = Path.DirectorySeparatorChar + "a" + Path.AltDirectorySeparatorChar;
            bool result = FileHelper.DirectoryPathIsValid(input, out string message);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryPathIsValid_DirectoryContainsMoreThanTwoPeriods_ReturnFalse()
        {
            string input = Path.DirectorySeparatorChar + "...";
            bool result = FileHelper.DirectoryPathIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Theory]
        [InlineData("_a")]
        [InlineData(" a")]
        [InlineData("-a")]
        [InlineData(".a")]
        public void DirectoryPathIsValid_DirectoryHasInvalidStart_ReturnFalse(string input)
        {
            input = Path.DirectorySeparatorChar + input;
            bool result = FileHelper.DirectoryPathIsValid(input, out string message);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryPathIsValid_DirectoryEndsWithPeriod_ReturnFalse()
        {
            string input = Path.DirectorySeparatorChar + "a.";
            bool result = FileHelper.DirectoryPathIsValid(input, out string message);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryPathIsValid_InvalidDriveIndicator_ReturnFalse()
        {
            string input = "1:" + Path.DirectorySeparatorChar;
            bool result = FileHelper.DirectoryPathIsValid(input, out string message);
            Assert.False(result);
        }
        [Fact]
        public void DirectoryPathIsValid_ValidPathStartsWithValidDriveIndicator_ReturnTrue()
        {
            string input = "A:" + Path.DirectorySeparatorChar + "a";
            bool result = FileHelper.DirectoryPathIsValid(input, out string message);
            Assert.True(result);
        }
        [Fact]
        public void DirectoryPathIsValid_ValidPathStartsWithDirectorySeparator_ReturnTrue()
        {
            string input = Path.DirectorySeparatorChar + "a";
            bool result = FileHelper.DirectoryPathIsValid(input, out string message);
            Assert.True(result);
        }
        [Fact]
        public void DirectoryPathIsValid_ValidPathStartsWithDirector_ReturnTrue()
        {
            string input = "a" + Path.DirectorySeparatorChar + "b";
            bool result = FileHelper.DirectoryPathIsValid(input, out string message);
            Assert.True(result);
        }

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
        [Fact]
        public void ExtensionIsValid_NullPointer_ReturnFalse()
        {
            string input = null!;
            bool result = FileHelper.ExtensionIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void ExtensionIsValid_EmptyString_ReturnFalse()
        {
            string input = "";
            bool result = FileHelper.ExtensionIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void ExtensionIsValid_WhitespaceString_ReturnFalse()
        {
            string input = " ";
            bool result = FileHelper.ExtensionIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void ExtensionIsValid_ExtensionDoesntStartWithPeriod_ReturnFalse()
        {
            string input = "a";
            bool result = FileHelper.ExtensionIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void ExtensionIsValid_ExtensionOnlyHasPeriod_ReturnFalse()
        {
            string input = ".";
            bool result = FileHelper.ExtensionIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void ExtensionIsValid_ExtensionHasMultiplePeriods_ReturnFalse()
        {
            string input = "..a";
            bool result = FileHelper.ExtensionIsValid(input, out string errorMessage);
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
            bool result = FileHelper.ExtensionIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void ExtensionIsValid_ValidExtension_ReturnTrue()
        {
            string input = ".a";
            bool result = FileHelper.ExtensionIsValid(input, out string errorMessage);
            Assert.True(result);
        }

        /*
         * Tests for BaseFilenameIsValid(string, out string)
         * 
         * Assert that passing in a null pointer returns false
         * Assert that passing in an empty string returns false
         * Assert that passing in a string that is entirely whitespace returns false
         * Assert that passing in a string that starts with '-', ' ', '_', or '.' returns false
         * Assert that passing in a string that contains any of the characters below returns false
         * '?', ' ', '/', '\', '%', '*', ':', '|', '"', '<', '>', ',', ';', '='
         * Assert that passing in a string that matches any of the below reserved filenames returns false
         * CON, CONIN$, CONOUT$, PRN, AUX, CLOCK$, NUL, COM0, COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9[9],
         * LPT0, LPT1, LPT2, LPT3, LPT4, LPT5, LPT6, LPT7, LPT8, LPT9[9], LST, KEYBD$, SCREEN$, $IDLE$, CONFIG$,
         * $Mft, $MftMirr, $LogFile, $Volume, $AttrDef, $Bitmap, $Boot, $BadClus, $Secure, $Upcase, $Extend, $Quota, $ObjId, $Reparse
         * Assert that passing in a valid base filename returns true
         */
        [Fact]
        public void BaseFilenameIsValid_NullPointer_ReturnFalse()
        {
            string input = null!;
            bool result = FileHelper.BaseFilenameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void BaseFilenameIsValid_EmptyString_ReturnFalse()
        {
            string input = "";
            bool result = FileHelper.BaseFilenameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void BaseFilenameIsValid_WhitespaceString_ReturnFalse()
        {
            string input = " ";
            bool result = FileHelper.BaseFilenameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Theory]
        [InlineData("-a")]
        [InlineData(" a")]
        [InlineData("_a")]
        [InlineData(".a")]
        public void BaseFilenameIsValid_NameHasInvalidStart_ReturnFalse(string input)
        {
            bool result = FileHelper.BaseFilenameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Theory]
        [InlineData("a?")]
        [InlineData("a ")]
        [InlineData("a/")]
        [InlineData("a\\")]
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
        public void BaseFilenameIsValid_NameContainsInvalidCharacters_ReturnFalse(string input)
        {
            bool result = FileHelper.BaseFilenameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Theory]
        [InlineData("CON")]
        [InlineData("CONIN$")]
        [InlineData("CONOUT$")]
        [InlineData("PRN")]
        [InlineData("AUX")]
        [InlineData("CLOCK$")]
        [InlineData("NUL")]
        [InlineData("COM0")]
        [InlineData("COM1")]
        [InlineData("COM2")]
        [InlineData("COM3")]
        [InlineData("COM4")]
        [InlineData("COM5")]
        [InlineData("COM6")]
        [InlineData("COM7")]
        [InlineData("COM8")]
        [InlineData("COM9[9]")]
        [InlineData("LPT0")]
        [InlineData("LPT1")]
        [InlineData("LPT2")]
        [InlineData("LPT3")]
        [InlineData("LPT4")]
        [InlineData("LPT5")]
        [InlineData("LPT6")]
        [InlineData("LPT7")]
        [InlineData("LPT8")]
        [InlineData("LPT9[9]")]
        [InlineData("LST")]
        [InlineData("KEYBD$")]
        [InlineData("SCREEN$")]
        [InlineData("$IDLE$")]
        [InlineData("CONFIG$")]
        [InlineData("$Mft")]
        [InlineData("$MftMirr")]
        [InlineData("$LogFile")]
        [InlineData("$Volume")]
        [InlineData("$AttrDef")]
        [InlineData("$Bitmap")]
        [InlineData("$Boot")]
        [InlineData("$BadClus")]
        [InlineData("$Secure")]
        [InlineData("$Upcase")]
        [InlineData("$Extend")]
        [InlineData("$Quota")]
        [InlineData("$ObjId")]
        [InlineData("$Reparse")]
        public void BaseFilenameIsValid_NameMatchesReservedName_ReturnFalse(string input)
        {
            bool result = FileHelper.BaseFilenameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void BaseFilenameIsValid_ValidName_ReturnTrue()
        {
            string input = "a";
            bool result = FileHelper.BaseFilenameIsValid(input, out string errorMessage);
            Assert.True(result);
        }

        /*
         * Tests for FilenameIsValid(string, string, out string)
         * 
         * Assert that passing in an invalid base filename returns false
         * Assert that passing in an invalid extension returns false
         * Assert that passing in strings with a combined length that is greater than 255 characters returns false
         * Assert that passing in a valid filename returns true
         */
        [Theory]
        [InlineData(null, ".a")]
        [InlineData("", ".a")]
        [InlineData(" ", ".a")]
        [InlineData("-a", ".a")]
        [InlineData("a?", ".a")]
        [InlineData("CON", ".a")]
        public void FilenameIsValid_InvalidBaseFilename_ReturnFalse(string? inputBaseFilename, string inputExtension)
        {
            bool result = FileHelper.FilenameIsValid(inputBaseFilename!, inputExtension, out string errorMessage);
            Assert.False(result);
        }
        [Theory]
        [InlineData("a", null)]
        [InlineData("a", "")]
        [InlineData("a", " ")]
        [InlineData("a", "a")]
        [InlineData("a", "..a")]
        [InlineData("a", ".a.")]
        [InlineData("a", ".?")]
        public void FilenameIsValid_InvalidExtension_ReturnFalse(string inputBaseFilename, string? inputExtension)
        {
            bool result = FileHelper.FilenameIsValid(inputBaseFilename, inputExtension!, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void FilenameIsValid_InvalidNameLength_ReturnFalse()
        {
            const int MAX_FILENAME_LENGTH = 255;
            string extension = ".a";
            string baseFilename = "".PadLeft(MAX_FILENAME_LENGTH + 1 - extension.Length);

            bool result = FileHelper.FilenameIsValid(baseFilename, extension, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void FilenameIsValid_ValidName_ReturnTrue()
        {
            string baseFilename = "a";
            string extension = ".a";

            bool result = FileHelper.FilenameIsValid(baseFilename, extension, out string errorMessage);
            Assert.True(result);
        }

        /*
         * Tests for FilepathIsValid(string, string, string, out string)
         * 
         * Assert that passing in an invalid base filename returns false
         * Assert that passing in an invalid extension returns false
         * Assert that passing in an invalid directory path returns false
         * Assert that passing in strings with a combined length that is greater than 32,766 characters returns false
         * Assert that passing in a valid filepath returns true
         */
        [Theory]
        [InlineData(null, ".a", "a")]
        [InlineData("", ".a", "a")]
        [InlineData(" ", ".a", "a")]
        [InlineData("-a", ".a", "a")]
        [InlineData("a?", ".a", "a")]
        [InlineData("CON", ".a", "a")]
        public void FilepathIsValid_InvalidBaseFilename_ReturnFalse(string? inputBaseFilename, string inputExtension, string inputDirectoryPath)
        {
            inputDirectoryPath = Path.DirectorySeparatorChar + inputDirectoryPath;
            bool result = FileHelper.FilepathIsValid(inputBaseFilename!, inputExtension, inputDirectoryPath, out string errorMessage);
            Assert.False(result);
        }
        [Theory]
        [InlineData("a", null, "a")]
        [InlineData("a", "", "a")]
        [InlineData("a", " ", "a")]
        [InlineData("a", "a", "a")]
        [InlineData("a", "..a", "a")]
        [InlineData("a", ".a.", "a")]
        [InlineData("a", ".?", "a")]
        public void FilepathIsValid_InvalidExtension_ReturnFalse(string inputBaseFilename, string? inputExtension, string inputDirectoryPath)
        {
            inputDirectoryPath = Path.DirectorySeparatorChar + inputDirectoryPath;
            bool result = FileHelper.FilepathIsValid(inputBaseFilename, inputExtension!, inputDirectoryPath, out string errorMessage);
            Assert.False(result);
        }
        [Theory]
        [InlineData("a", ".a", "")]
        [InlineData("a", ".a", " ")]
        [InlineData("a", ".a", "a?")]
        [InlineData("a", ".a", "...")]
        [InlineData("a", ".a", "-a")]
        [InlineData("a", ".a", "a.")]
        [InlineData("a", ".a", "1:")]
        public void FilepathIsValid_InvalidDirectoryPath_ReturnFalse(string inputBaseFilename, string inputExtension, string inputDirectoryPath)
        {
            inputDirectoryPath = Path.DirectorySeparatorChar + inputDirectoryPath;
            bool result = FileHelper.FilepathIsValid(inputBaseFilename, inputExtension, inputDirectoryPath, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void FilepathIsValid_DirectoryPathIsNull_ReturnFalse()
        {
            string baseFilename = "a";
            string extension = ".a";
            string directoryPath = null!;

            bool result = FileHelper.FilepathIsValid(baseFilename, extension, directoryPath, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void FilepathIsValid_DirectoryPathHasAltSeparator_ReturnFalse()
        {
            string baseFilename = "a";
            string extension = ".a";
            string directoryPath = Path.DirectorySeparatorChar + "a" + Path.AltDirectorySeparatorChar;

            bool result = FileHelper.FilepathIsValid(baseFilename, extension, directoryPath, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void FilepathIsValid_InvalidPathLength_ReturnFalse()
        {
            const int MAX_PATH_LENGTH = 32766;
            string directoryPath = Path.DirectorySeparatorChar + "a" + Path.AltDirectorySeparatorChar;
            string extension = ".a";
            string baseFilename = "".PadLeft(MAX_PATH_LENGTH + 1 - directoryPath.Length - extension.Length);

            bool result = FileHelper.FilepathIsValid(baseFilename, extension, directoryPath, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void FilepathIsValid_ValidPath_ReturnTrue()
        {
            string baseFilename = "a";
            string extension = ".a";
            string directoryPath = "a" + Path.DirectorySeparatorChar;

            bool result = FileHelper.FilepathIsValid(baseFilename, extension, directoryPath, out string errorMessage);
            Assert.True(result);
        }
    }
}
