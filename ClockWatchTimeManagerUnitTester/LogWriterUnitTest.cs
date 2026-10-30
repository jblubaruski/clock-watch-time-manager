using Clock_Watch_Time_Manager.Models.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWatchTimeManagerUnitTester
{
    public class LogWriterUnitTest
    {
        /*
         * The constructor generates a file with the name log_<programname>.txt in the directory %MyDocuments%\PlaypenLogs\<ProgramName>.
         * 
         * The passed in string should not be more than 247 characters.
         * It cannot be an empty string or contain all whitespace.
         * It cant be any of the following reserved windows file names:
         * CON, PRN, AUX, NUL, COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9, COM¹, COM², COM³, LPT1, LPT2, LPT3, LPT4, LPT5, LPT6, LPT7, LPT8, LPT9, LPT¹, LPT², LPT³
         * It should not have any of the following characters:
         * at start of the string: ' ', '-', '_', '.'
         * anywhere in the string: '#', '%', '&', '{', '}', '\', '<', '>', '*', '?', '/', '$', '!', ''', '"', ':', '@', '+', '`', '|', '=', ' '
         * 
         * The directory <ProgramName> should be the passed in string converted to PascalCase and should have all whitespaces removed.
         * <programname> should be the passed in string converted to lowercase and should have all invalid characters and whitespaces removed.
         * 
         * The constructor skips validation and generates a text file with the name logwriter-test-log.txt in the directory %MyDocuments%\PlaypenLogs\Testing if the second argument is set to true
         * The file should be closed after the constructor creates the file.
         */
        [Fact]
        public void Constructor_NullString_ThrowArgumentNullException() => Assert.Throws<ArgumentNullException>(() => new LogWriter(null!, false));
        [Fact]
        public void Constructor_EmptyString_ThrowArgumentException() => Assert.Throws<ArgumentException>(() => new LogWriter("", false));
        [Fact]
        public void Constructor_WhitespaceString_ThrowArgumentException() => Assert.Throws<ArgumentException>(() => new LogWriter("".PadLeft(200, ' '), false));
        [Fact]
        public void Constructor_248CharString_ThrowArgumentException() => Assert.Throws<ArgumentException>(() => new LogWriter("".PadLeft(248, 'a'), false));
        [Theory]
        [InlineData("CON")]  [InlineData("PRN")]  [InlineData("AUX")]  [InlineData("COM1")]
        [InlineData("COM2")] [InlineData("COM3")] [InlineData("COM4")] [InlineData("COM5")]
        [InlineData("COM6")] [InlineData("COM7")] [InlineData("COM8")] [InlineData("COM9")]
        [InlineData("COM¹")] [InlineData("COM²")] [InlineData("COM³")] [InlineData("LPT1")]
        [InlineData("LPT2")] [InlineData("LPT3")] [InlineData("LPT4")] [InlineData("LPT5")]
        [InlineData("LPT6")] [InlineData("LPT7")] [InlineData("LPT8")] [InlineData("LPT9")]
        [InlineData("LPT¹")] [InlineData("LPT²")] [InlineData("LPT³")]
        public void Constructor_ReservedNameUpper_ThrowArgumentException(string input) => Assert.Throws<ArgumentException>(() => new LogWriter(input, false));
        [Theory]
        [InlineData("con")]  [InlineData("prn")]  [InlineData("aux")]  [InlineData("com1")]
        [InlineData("com2")] [InlineData("com3")] [InlineData("com4")] [InlineData("com5")]
        [InlineData("com6")] [InlineData("com7")] [InlineData("com8")] [InlineData("com9")]
        [InlineData("com¹")] [InlineData("com²")] [InlineData("com³")] [InlineData("lpt1")]
        [InlineData("lpt2")] [InlineData("lpt3")] [InlineData("lpt4")] [InlineData("lpt5")]
        [InlineData("lpt6")] [InlineData("lpt7")] [InlineData("lpt8")] [InlineData("lpt9")]
        [InlineData("lpt¹")] [InlineData("lpt²")] [InlineData("lpt³")]
        public void Constructor_ReservedNameLower_ThrowArgumentException(string input) => Assert.Throws<ArgumentException>(() => new LogWriter(input, false));
        [Theory]
        [InlineData(' ')]
        [InlineData('-')]
        [InlineData('_')]
        [InlineData('.')]
        public void Constructor_StringHasInvalidFirstChar_ThrowArgumentException(char input)
        {
            string testName = input + "a";
            Assert.Throws<ArgumentException>(() => new LogWriter(testName, false));
        }
        [Theory]
        [InlineData('|')] [InlineData('#')] [InlineData('%')]  [InlineData('&')]
        [InlineData('{')] [InlineData('}')] [InlineData('\\')] [InlineData('<')]
        [InlineData('>')] [InlineData('*')] [InlineData('?')]  [InlineData('/')]
        [InlineData('$')] [InlineData('!')] [InlineData('\'')] [InlineData('"')]
        [InlineData(':')] [InlineData('@')] [InlineData('+')]  [InlineData('`')]
        [InlineData('=')]
        public void Constructor_StringHasInvalidChar_ThrowArgumentException(char input)
        {
            string testName = "a" + input + "b";
            Assert.Throws<ArgumentException>(() => new LogWriter(testName, false));
        }
        [Theory]
        [InlineData("program", "log_program.txt", "Program")]
        [InlineData("PROGRAM", "log_program.txt", "Program")]
        [InlineData("test pRogram", "log_testprogram.txt", "TestProgram")]
        public void Constructor_ValidStringInput_CreateDirectoryAndFileWithCorrectNames(string input, string expectedFileName, string expectedDirectoryName)
        {
            const string COMPANY_LOGS_DIRECTORY = "PlaypenLogs";

            string expectedLogDirectory = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), COMPANY_LOGS_DIRECTORY), expectedDirectoryName);
            LogWriter testWriter = new(input, false);
            bool testFileExists = File.Exists(Path.Combine(expectedLogDirectory, expectedFileName));

            Assert.True(testFileExists);
            
            if (testFileExists)
            {
                File.Delete(Path.Combine(expectedLogDirectory, expectedFileName));
                Directory.Delete(expectedLogDirectory);
            }
        }
        [Fact]
        public void Constructor_TestMode_CreateDirectoryAndFileWithCorrectNames()
        {
            const string COMPANY_LOGS_DIRECTORY = "PlaypenLogs";
            string expectedDirectoryName = "Testing";
            string expectedFileName = "logwriter-test-log.txt";

            string expectedLogDirectory = Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), COMPANY_LOGS_DIRECTORY), expectedDirectoryName);
            LogWriter testWriter = new("test", true);
            bool testFileExists = File.Exists(Path.Combine(expectedLogDirectory, expectedFileName));
            
            Assert.True(testFileExists);
            
            if (testFileExists)
                File.Delete(Path.Combine(expectedLogDirectory, expectedFileName));
        }

        /*
         * LogMessage(string message) appends the passed in string to the file referenced in LogWriter's properties without overwriting other messages.
         * 
         * Passing in a null string throws an ArgumentNullException.
         * The file should still exist after use.
         * The file should be closed after use.
         */
        [Fact]
        public void LogMessage_NullString_ThrowArgumentNullException()
        {
            LogWriter testWriter = new("test", true);
            Assert.Throws<ArgumentNullException>(() => testWriter.LogMessage(null!));
        }
    }
}
