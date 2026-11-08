using FileUtility.Tests.Derivations;
using FileUtility.Tests.Services;
using FileUtility.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.TextFileManagerTests
{
    /*
     * Tests for TextFileManager.ReadFile()
     * 
     * Assert that reading from a file that doesnt exist throws an InvalidOperationException
     * Assert that reading from an empty file in a valid state returns a null pointer
     * Assert that reading from a file in a valid state and with content returns a string equal to that content
     * Assert that reading from a file in a valid state with multiple lines of content returns a string equal to that content with new lines separating the strings
     */
    public class ReadFileTest
    {
        [Fact]
        public void ReadFile_FileDoesNotExist_ThrowInvalidOperationException()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = TextFileManager_Test.TESTING_READ_DIRECTORY + testID;
            string baseFilename = TextFileManager_Test.TESTING_READ_FILENAME;
            string extension = TextFileManager.EXTENSION;

            TextFileManager_Test testManager = new(baseFilename, directoryPath);
            File.Delete(Path.Combine(directoryPath, baseFilename + extension));

            Assert.Throws<InvalidOperationException>
                (
                    testManager.ReadFile
                );

            TestServices.RemoveTestDirectory(directoryPath);
        }
        [Fact]
        public void ReadFile_EmptyFile_ReturnNull()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = TextFileManager_Test.TESTING_READ_DIRECTORY + testID;
            string baseFilename = TextFileManager_Test.TESTING_READ_FILENAME;

            TextFileManager_Test testManager = new(baseFilename, directoryPath);
            string? testFileContent = testManager.ReadFile();

            bool result = testFileContent == null;

            TestServices.RemoveTestDirectory(directoryPath);

            Assert.True(result);
        }
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("a\nb\nc")]
        public void ReadFile_FileHasContent_ReturnLinesSeparatedWithNewLines(string content)
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = TextFileManager_Test.TESTING_READ_DIRECTORY + testID;
            string baseFilename = TextFileManager_Test.TESTING_READ_FILENAME;
            string[] testInput = content.Split("\n");

            TextFileManager_Test testManager = new(baseFilename, directoryPath);
            TestServices.FillClosedTextFileWithContentStringArray(testManager, testInput);

            string? testOutput = testManager.ReadFile();
            bool result = testOutput == content;

            TestServices.RemoveTestDirectory(directoryPath);

            Assert.True(result);
        }
    }
}
