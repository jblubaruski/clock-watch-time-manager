using Clock_Watch_Time_Manager.Models.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWatchTimeManager.Test
{
    public class LogWriterUnitTest
    {
        /*
         * The constructor generates a file with the name log_<programname>.txt in the directory %MyDocuments%\PlaypenLogs\<ProgramName>.
         * 
         * The directory <ProgramName> should be the passed in string converted to PascalCase and should have all whitespaces removed.
         * <programname> should be the passed in string converted to lowercase and should have all invalid characters and whitespaces removed.
         * 
         * The passed in string should not be more than 247 characters.
         * It cannot be a null reference, an empty string or contain all whitespace.
         * It should not have any of the following characters:
         * at start of the string: ' ', '-', '_', '.'
         * anywhere in the string: '#', '%', '&', '{', '}', '\', '<', '>', '*', '?', '/', '$', '!', ''', '"', ':', '@', '+', '`', '|', '=', ' '
         * 
         * The file should be closed after the constructor creates the file.
         */
        [Fact]
        public void Constructor_NullString_ThrowArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new LogWriter(null!));
        [Fact]
        public void Constructor_EmptyString_ThrowArgumentException() => Assert.Throws<ArgumentException>(() => new LogWriter(""));
        [Fact]
        public void Constructor_WhitespaceString_ThrowArgumentException() => Assert.Throws<ArgumentException>(() => new LogWriter(" "));
        [Fact]
        public void Constructor_248CharString_ThrowArgumentException() => Assert.Throws<ArgumentException>(() => new LogWriter("".PadLeft(248, 'a')));
        [Theory]
        [InlineData(' ')]
        [InlineData('-')]
        [InlineData('_')]
        [InlineData('.')]
        public void Constructor_StringHasInvalidFirstChar_ThrowArgumentException(char input)
        {
            string testName = input + "a";
            Assert.Throws<ArgumentException>(() => new LogWriter(testName));
        }
        [Theory]
        [InlineData('|')]
        [InlineData('#')]
        [InlineData('%')]
        [InlineData('&')]
        [InlineData('{')]
        [InlineData('}')]
        [InlineData('\\')]
        [InlineData('<')]
        [InlineData('>')]
        [InlineData('*')]
        [InlineData('?')]
        [InlineData('/')]
        [InlineData('$')]
        [InlineData('!')]
        [InlineData('\'')]
        [InlineData('"')]
        [InlineData(':')]
        [InlineData('@')]
        [InlineData('+')]
        [InlineData('`')]
        [InlineData('=')]
        public void Constructor_StringHasInvalidChar_ThrowArgumentException(char input)
        {
            string testName = "a" + input + "b";
            Assert.Throws<ArgumentException>(() => new LogWriter(testName));
        }
        [Theory]
        [InlineData("program", "log_program.txt", "Program")]
        [InlineData("PROGRAM", "log_program.txt", "Program")]
        [InlineData("test pRogram", "log_testprogram.txt", "TestProgram")]
        public void Constructor_ValidStringInput_CreateDirectoryAndFileWithCorrectNames(string input, string expectedFileName, string expectedDirectoryName)
        {
            const string COMPANY_LOGS_DIRECTORY = "PlaypenLogs";

            string expectedLogDirectory = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), COMPANY_LOGS_DIRECTORY), expectedDirectoryName);
            LogWriter testWriter = new(input);
            bool testFileExists = File.Exists(Path.Combine(expectedLogDirectory, expectedFileName));

            Assert.True(testFileExists);

            if (testFileExists)
            {
                File.Delete(Path.Combine(expectedLogDirectory, expectedFileName));
                Directory.Delete(expectedLogDirectory);
            }
        }

        /*
         * LogMessage(string message) appends the passed in string to the file referenced in LogWriter's properties without overwriting other messages.
         * 
         * Passing in a null string throws an ArgumentNullException.
         * The file should still exist after use.
         * The file should be closed after use.
         */
        
    }
}
