
using FileUtility.Tests.Derivations;
using FileUtility.Tests.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.TextFileManagerTests
{
    /*
     * Tests for TextFileManager.WriteToFileTest(string)
     * 
     * Assert that trying to write to a file that does not exist throws an InvalidOperationException
     * Assert that the file is written to correctly with each newline charachter in the string getting its own line
     */
    public class WriteToFileTest
    {
        [Fact]
        public void WriteToFile_FileDoesNotExist_ThrowInvalidOperationException()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = TextFileManager_Test.TESTING_WRITE_DIRECTORY + testID;
            string baseFilename = TextFileManager_Test.TESTING_WRITE_FILENAME;
            string extension = TextFileManager_Test.EXTENSION;

            TextFileManager_Test testManager = new(baseFilename, directoryPath);
            File.Delete(Path.Combine(directoryPath, baseFilename + extension));

            Assert.Throws<InvalidOperationException>
                (
                    () => testManager.WriteToFile("")
                );

            TestServices.RemoveTestDirectory(directoryPath);
        }
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("a\nb\nc")]
        public void WriteToFile_NonNullInput_FileWrittenTo(string testInput)
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = TextFileManager_Test.TESTING_WRITE_DIRECTORY + testID;
            string baseFilename = TextFileManager_Test.TESTING_WRITE_FILENAME;

            TextFileManager_Test testManager = new(baseFilename, directoryPath);
            testManager.WriteToFile(testInput);

            string? testOutput = TestServices.ReadClosedTextFile(testManager);
            bool result = testOutput == testInput;

            TestServices.RemoveTestDirectory(directoryPath);

            Assert.True(result);
        }
    }
}
