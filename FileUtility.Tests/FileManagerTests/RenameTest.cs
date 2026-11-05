using FileUtility.Tests.Derivations;
using FileUtility.Tests.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.FileManagerTests
{
    /*
     * Tests for FileManager.Rename(string)
     * 
     * Assert that passing in an invalid base filename throws an ArgumentException
     * Assert that passing in a base filename identical to the file's current name throws an ArgumentException
     * Assert that passing in a base finename identical to the basename of a file in the same directory throws an ArgumentException
     * Assert that calling rename on an open file throws an InvalidOperationException
     * Assert that the old file does not exist after calling rename
     * Assert that the new file exists after calling rename
     */
    public class RenameTest
    {
        [Fact]
        public void Rename_InvalidBaseFilename_ThrowArgumentException()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string baseFilename = "a?";

            FileManager_Test testManager = new(directoryPath);

            Assert.Throws<ArgumentException>
                (
                    () => testManager.Rename(baseFilename)
                );

            TestServices.RunTestCleanup(testManager, directoryPath);
        }
        [Fact]
        public void Rename_IdenticalFilenames_ThrowArgumentException()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string baseFilename = FileManager_Test.TESTING_TEMP_FILENAME;

            FileManager_Test testManager = new(directoryPath);

            Assert.Throws<ArgumentException>
                (
                    () => testManager.Rename(baseFilename)
                );

            TestServices.RunTestCleanup(testManager, directoryPath);
        }
        [Fact]
        public void Rename_IdenticalNameToFileInDirectory_ThrowArgumentException()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string baseFilename = FileManager_Test.TESTING_DUPLICATE_FILENAME;
            string extension = FileManager_Test.TESTING_EXTENSION;

            FileManager_Test testManager = new(directoryPath);
            TestServices.CreateThreeClosedTestFiles(directoryPath, extension);

            Assert.Throws<ArgumentException>
                (
                    () => testManager.Rename(baseFilename)
                );

            TestServices.RunTestCleanup(testManager, directoryPath);
        }
        [Fact]
        public void Rename_FileIsOpen_ThrowInvalidOperationException()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            FileAccess fileAccess = FileAccess.ReadWrite;

            FileManager_Test testManager = new(directoryPath, fileAccess);

            Assert.Throws<InvalidOperationException>
                (
                    () => testManager.Rename("test")
                );

            TestServices.RunTestCleanup(testManager, directoryPath);
        }
        [Fact]
        public void Rename_ValidState_OldFileDoesNotExist()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string oldFilename = FileManager_Test.TESTING_TEMP_FILENAME;
            string newFilename = oldFilename + "(1)";
            string extension = FileManager_Test.TESTING_EXTENSION;

            FileManager_Test testManager = new(directoryPath);
            testManager.Rename(newFilename);
            bool result = !File.Exists(Path.Combine(directoryPath, oldFilename + extension));
            TestServices.RunTestCleanup(testManager, directoryPath);

            Assert.True(result);
        }
        [Fact]
        public void Rename_ValidState_NewFileExists()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string newFilename = FileManager_Test.TESTING_TEMP_FILENAME + "(1)";
            string extension = FileManager_Test.TESTING_EXTENSION;

            FileManager_Test testManager = new(directoryPath);
            testManager.Rename(newFilename);
            bool result = File.Exists(Path.Combine(directoryPath, newFilename + extension));
            TestServices.RunTestCleanup(testManager, directoryPath);

            Assert.True(result);
        }
    }
}
