using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.FileValidatorTests
{
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
    public class BaseFilenameIsValidTest
    {
        [Fact]
        public void BaseFilenameIsValid_NullPointer_ReturnFalse()
        {
            string input = null!;
            bool result = FileValidator.BaseFilenameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void BaseFilenameIsValid_EmptyString_ReturnFalse()
        {
            string input = "";
            bool result = FileValidator.BaseFilenameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void BaseFilenameIsValid_WhitespaceString_ReturnFalse()
        {
            string input = " ";
            bool result = FileValidator.BaseFilenameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Theory]
        [InlineData("-a")]
        [InlineData(" a")]
        [InlineData("_a")]
        [InlineData(".a")]
        public void BaseFilenameIsValid_NameHasInvalidStart_ReturnFalse(string input)
        {
            bool result = FileValidator.BaseFilenameIsValid(input, out string errorMessage);
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
            bool result = FileValidator.BaseFilenameIsValid(input, out string errorMessage);
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
            bool result = FileValidator.BaseFilenameIsValid(input, out string errorMessage);
            Assert.False(result);
        }
        [Fact]
        public void BaseFilenameIsValid_ValidName_ReturnTrue()
        {
            string input = "a";
            bool result = FileValidator.BaseFilenameIsValid(input, out string errorMessage);
            Assert.True(result);
        }
    }
}
