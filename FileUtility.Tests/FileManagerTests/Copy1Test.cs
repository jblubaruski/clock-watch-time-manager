using FileUtility.Tests.Derivations;
using FileUtility.Tests.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.FileManagerTests
{
    /*
     * Tests for FileManager.Copy(string, bool)
     *
     * Assert that passing in an invalid base filename throws an ArgumentException
     * Assert that passing in a filename that is the same as the original file's filename while overwrite is true throws an ArgumentException
     * Assert that passing in a filename that is the same as a file in the original file's directory while overwrite is false throws an ArgumentException
     * Assert that trying to copy a file that does not exist on the file system throws an InvalidOperationException
     * Assert that trying to copy an open file throws an InvalidOperationException
     * Assert that copying a file creates a new file in the original file's directory with the passed in filename
     */
    public class Copy1Test
    {
        [Fact]
        public void Copy1_InvalidBaseFilename_ThrowArgumentException()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string originalFilename = FileManager_Test.TESTING_TEMP_FILENAME;
            string newFilename = originalFilename + "?";
            bool overwrite = false;

            FileManager_Test testManager = new(directoryPath);

            Assert.Throws<ArgumentException>
                (
                    () => testManager.Copy(newFilename, overwrite)
                );

            TestServices.RunTestCleanup(testManager, directoryPath);
        }
        [Fact]
        public void Copy1_FilenameSameAsOriginalOverwriteTrue_ThrowArgumentException()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string originalFilename = FileManager_Test.TESTING_TEMP_FILENAME;
            string newFilename = originalFilename;
            bool overwrite = true;

            FileManager_Test testManager = new(directoryPath);

            Assert.Throws<ArgumentException>
                (
                    () => testManager.Copy(newFilename, overwrite)
                );

            TestServices.RunTestCleanup(testManager, directoryPath);
        }
        [Fact]
        public void Copy1_DuplicateFilenameOverwriteFalse_ThrowArgumentException()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string newFilename = FileManager_Test.TESTING_DUPLICATE_FILENAME;
            string extension = FileManager_Test.TESTING_EXTENSION;
            bool overwrite = false;

            FileManager_Test testManager = new(directoryPath);
            TestServices.CreateThreeClosedTestFiles(directoryPath, extension);

            Assert.Throws<ArgumentException>
                (
                    () => testManager.Copy(newFilename, overwrite)
                );

            TestServices.RunTestCleanup(testManager, directoryPath);
        }
        [Fact]
        public void Copy1_FileDoesNotExist_ThrowInvalidOperationException()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string originalFilename = FileManager_Test.TESTING_TEMP_FILENAME;
            string newFilename = FileManager_Test.TESTING_DUPLICATE_FILENAME;
            string extension = FileManager_Test.TESTING_EXTENSION;
            bool overwrite = false;

            FileManager_Test testManager = new(directoryPath);
            File.Delete(Path.Combine(directoryPath, originalFilename + extension));

            Assert.Throws<InvalidOperationException>
                (
                    () => testManager.Copy(newFilename, overwrite)
                );

            TestServices.RunTestCleanup(testManager, directoryPath);
        }
        [Fact]
        public void Copy1_FileIsAlreadyOpen_ThrowInvalidOperationException()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string newFilename = FileManager_Test.TESTING_DUPLICATE_FILENAME;
            FileAccess fileAccess = FileAccess.ReadWrite;
            bool overwrite = false;

            FileManager_Test testManager = new(directoryPath, fileAccess);

            Assert.Throws<InvalidOperationException>
                (
                    () => testManager.Copy(newFilename, overwrite)
                );

            TestServices.RunTestCleanup(testManager, directoryPath);
        }
        [Fact]
        public void Copy1_ValidState_CopyReturnsFileManagerOfCopy()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string newFilename = FileManager_Test.TESTING_DUPLICATE_FILENAME;
            bool overwrite = false;

            FileManager_Test testManager = new(directoryPath);
            IFileManager copy = testManager.Copy(newFilename, overwrite);
            bool result = copy != null;
            TestServices.RunTestCleanup(testManager, directoryPath);

            Assert.True(result);
        }
    }
}
