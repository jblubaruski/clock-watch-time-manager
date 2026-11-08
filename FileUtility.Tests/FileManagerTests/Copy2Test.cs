using FileUtility.Tests.Derivations;
using FileUtility.Tests.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.FileManagerTests
{
    /*
     * Tests for FileManager.Copy(string, string, bool)
     * 
     * Assert that passing in an invalid base filename throws an ArgumentException
     * Assert that passing in a filename that is the same as a file in the passed in directory while overwrite is false throws an ArgumentException
     * Assert that trying to copy a file that does not exist on the file system throws an InvalidOperationException
     * Assert that copying a file creates a new file in the passed in directory with the passed in filename
     */
    public class Copy2Test
    {
        [Fact]
        public void Copy2_InvalidBaseFilename_ThrowArgumentException()
        {
            string testID = Guid.NewGuid().ToString();
            string originalDirectoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string newDirectoryPath = FileManager_Test.TESTING_COPYTO_DIRECTORY + testID;
            string originalFilename = FileManager_Test.TESTING_TEMP_FILENAME;
            string newFilename = originalFilename + "?";
            bool overwrite = false;

            FileManager_Test testManager = new(originalDirectoryPath);
            Directory.CreateDirectory(newDirectoryPath);

            Assert.Throws<ArgumentException>
                (
                    () => testManager.Copy(newFilename, newDirectoryPath, overwrite)
                );

            TestServices.RemoveTestDirectory(originalDirectoryPath);
            TestServices.RemoveTestDirectory(newDirectoryPath);
        }
        [Fact]
        public void Copy2_DuplicateFilename_ThrowArgumentException()
        {
            string testID = Guid.NewGuid().ToString();
            string originalDirectoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string newDirectoryPath = FileManager_Test.TESTING_COPYTO_DIRECTORY + testID;
            string newFilename = FileManager_Test.TESTING_DUPLICATE_FILENAME;
            string extension = FileManager_Test.TESTING_EXTENSION;
            bool overwrite = false;

            FileManager_Test testManager = new(originalDirectoryPath);
            Directory.CreateDirectory(newDirectoryPath);
            TestServices.CreateClosedEmptyTestFile("a", extension, newDirectoryPath);
            TestServices.CreateClosedEmptyTestFile("b", extension, newDirectoryPath);
            TestServices.CreateClosedEmptyTestFile("c", extension, newDirectoryPath);

            Assert.Throws<ArgumentException>
                (
                    () => testManager.Copy(newFilename, newDirectoryPath, overwrite)
                );

            TestServices.RemoveTestDirectory(originalDirectoryPath);
            TestServices.RemoveTestDirectory(newDirectoryPath);
        }
        [Fact]
        public void Copy2_FileDoesNotExist_ThrowInvalidOperationException()
        {
            string testID = Guid.NewGuid().ToString();
            string originalDirectoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string newDirectoryPath = FileManager_Test.TESTING_COPYTO_DIRECTORY + testID;
            string originalFilename = FileManager_Test.TESTING_TEMP_FILENAME;
            string newFilename = FileManager_Test.TESTING_DUPLICATE_FILENAME;
            string extension = FileManager_Test.TESTING_EXTENSION;
            bool overwrite = false;

            FileManager_Test testManager = new(originalDirectoryPath);
            Directory.CreateDirectory(newDirectoryPath);
            File.Delete(Path.Combine(originalDirectoryPath, originalFilename + extension));

            Assert.Throws<InvalidOperationException>
                (
                    () => testManager.Copy(newFilename, newDirectoryPath, overwrite)
                );

            TestServices.RemoveTestDirectory(originalDirectoryPath);
            TestServices.RemoveTestDirectory(newDirectoryPath);
        }
        [Fact]
        public void Copy2_ValidState_CopyReturnsFileManagerOfCopy()
        {
            string testID = Guid.NewGuid().ToString();
            string originalDirectoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string newDirectoryPath = FileManager_Test.TESTING_COPYTO_DIRECTORY + testID;
            string newFilename = FileManager_Test.TESTING_DUPLICATE_FILENAME;
            bool overwrite = false;

            FileManager_Test testManager = new(originalDirectoryPath);
            Directory.CreateDirectory(newDirectoryPath);
            IFileManager copy = testManager.Copy(newFilename, newDirectoryPath, overwrite);

            bool result = copy != null;

            TestServices.RemoveTestDirectory(originalDirectoryPath);
            TestServices.RemoveTestDirectory(newDirectoryPath);

            Assert.True(result);
        }
    }
}
