using FileUtility.Tests.Derivations;
using FileUtility.Tests.Services;
using FileUtility.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.TextFileManagerTests
{
    /*
     * Reads the specified line from the file and returns it as a string.
     * 
     * Assert that reading from a file that does not exist throws an InvalidOperationException
     * Assert that reading from a file with a line corresponding to the passed in line number returns that line
     * Assert that reading from a file without a line corresponding to the passed in line number returns a null pointer
     */
    public class ReadLineTest
    {
        [Fact]
        public void ReadLine_FileDoesNotExist_ThrowInvalidOperationException()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = TextFileManager_Test.TESTING_READ_DIRECTORY + testID;
            string baseFilename = TextFileManager_Test.TESTING_READ_FILENAME;
            string extension = TextFileManager.EXTENSION;

            TextFileManager_Test testManager = new(baseFilename, directoryPath);
            File.Delete(Path.Combine(directoryPath, baseFilename + extension));

            Assert.Throws<InvalidOperationException>
                (
                    () => testManager.ReadLine(0)
                );

            TestServices.RemoveTestDirectory(directoryPath);
        }
        [Theory]
        [InlineData("", 0, "")]
        [InlineData("a", 0, "a")]
        [InlineData("a\nb\nc", 1, "b")]
        public void ReadLine_LineNumberExistsInFile_ReturnDesiredLine(string content, int lineNumber, string expectedOutput)
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = TextFileManager_Test.TESTING_READ_DIRECTORY + testID;
            string baseFilename = TextFileManager_Test.TESTING_READ_FILENAME;
            string[] testInput = content.Split("\n");

            TextFileManager_Test testManager = new(baseFilename, directoryPath);
            TestServices.FillClosedTextFileWithContentStringArray(testManager, testInput);

            string? testOutput = testManager.ReadLine(lineNumber);
            bool result = testOutput == expectedOutput;

            TestServices.RemoveTestDirectory(directoryPath);

            Assert.True(result);
        }
        [Theory]
        [InlineData(null, 0)]
        [InlineData("a", -1)]
        [InlineData("a\nb\nc", 3)]
        public void ReadLine_LineNumberDoesntExistInFile_ReturnNull(string? content, int lineNumber)
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = TextFileManager_Test.TESTING_READ_DIRECTORY + testID;
            string baseFilename = TextFileManager_Test.TESTING_READ_FILENAME;
            string[]? testInput = null;
            if (content != null)
                testInput = content.Split("\n");

            TextFileManager_Test testManager = new(baseFilename, directoryPath);
            if (testInput != null)
                TestServices.FillClosedTextFileWithContentStringArray(testManager, testInput);

            string? testOutput = testManager.ReadLine(lineNumber);
            bool result = testOutput == null;

            TestServices.RemoveTestDirectory(directoryPath);

            Assert.True(result);
        }
    }
}
