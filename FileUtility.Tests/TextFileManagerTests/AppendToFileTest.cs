using FileUtility.Tests.Derivations;
using FileUtility.Tests.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.TextFileManagerTests
{
    /*
     * Tests for TextFileManager.AppendToFile(string)
     * 
     * Assert that trying to append to a file that does not exist throws an InvalidOperationException
     * Assert that appending a line to the file adds the line on to the end of the file
     */
    public class AppendToFileTest
    {
        [Fact]
        public void AppendToFile_FileDoesNotExist_ThrowInvalidOperationException()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = TextFileManager_Test.TESTING_WRITE_DIRECTORY + testID;
            string baseFilename = TextFileManager_Test.TESTING_WRITE_FILENAME;
            string extension = TextFileManager_Test.EXTENSION;

            TextFileManager_Test testManager = new(baseFilename, directoryPath);
            File.Delete(Path.Combine(directoryPath, baseFilename + extension));

            Assert.Throws<InvalidOperationException>
                (
                    () => testManager.AppendToFile("")
                );

            TestServices.RemoveTestDirectory(directoryPath);
        }
        [Theory]
        [InlineData("", "a", "\na")]
        [InlineData("a", "b", "a\nb")]
        [InlineData("a", "b\nc", "a\nb\nc")]
        public void AppendToFile_NonNullInput_FileAppendedTo(string originalContent, string testInput, string expectedOutput)
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = TextFileManager_Test.TESTING_WRITE_DIRECTORY + testID;
            string baseFilename = TextFileManager_Test.TESTING_WRITE_FILENAME;
            string[] fileContent = originalContent.Split("\n");

            TextFileManager_Test testManager = new(baseFilename, directoryPath);
            TestServices.FillClosedTextFileWithContentStringArray(testManager, fileContent);
            testManager.AppendToFile(testInput);

            string? testOutput = TestServices.ReadClosedTextFile(testManager);
            bool result = testOutput == expectedOutput;

            TestServices.RemoveTestDirectory(directoryPath);

            Assert.True(result);
        }
    }
}
