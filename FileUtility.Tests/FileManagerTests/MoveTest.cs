using FileUtility.Tests.Derivations;
using FileUtility.Tests.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.FileManagerTests
{
    /*
     * Tests for Move(string, bool)
     * 
     * Assert that passing in an invalid directoryPath throws an ArgumentException
     * Assert that passing in a duplicate filename while overwrite is set to false throws an ArgumentException
     * Assert that trying to move a file that doesnt exist on the file system throws an InvalidOperationException
     * Assert that trying to move an open file throws an InvalidOperationException
     * Assert that the file exists in the new directory after calling move
     * Assert that moving a file to a directory with a duplicate filename with overwrite set to true replaces that file with this file
     */
    public class MoveTest
    {
        [Fact]
        public void Move_InvalidDirectoryPath_ThrowArgumentException()
        {
            string testID = Guid.NewGuid().ToString();
            string originalDirectoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string newDirectoryPath = FileManager_Test.TESTING_DUPLICATEFILENAME_DIRECTORY + testID + ".";
            bool overwrite = false;

            FileManager_Test testManager = new(originalDirectoryPath);
            Directory.CreateDirectory(newDirectoryPath);

            Assert.Throws<ArgumentException>
                (
                    () => testManager.Move(newDirectoryPath, overwrite)
                );

            TestServices.RunTestCleanup(testManager, originalDirectoryPath);
            TestServices.RunTestCleanup(testManager, newDirectoryPath);
        }
        [Fact]
        public void Move_DuplicateFileOverwriteFalse_ThrowArgumentException()
        {
            string testID = Guid.NewGuid().ToString();
            string originalDirectoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string newDirectoryPath = FileManager_Test.TESTING_DUPLICATEFILENAME_DIRECTORY + testID;
            string filename = FileManager_Test.TESTING_DUPLICATE_FILENAME;
            string extension = FileManager_Test.TESTING_EXTENSION;
            bool overwrite = false;

            FileManager_Test testManager = new(filename, originalDirectoryPath);
            Directory.CreateDirectory(newDirectoryPath);
            TestServices.CreateThreeClosedTestFiles(newDirectoryPath, extension);

            Assert.Throws<ArgumentException>
                (
                    () => testManager.Move(newDirectoryPath, overwrite)
                );

            TestServices.RunTestCleanup(testManager, originalDirectoryPath);
            TestServices.RunTestCleanup(testManager, newDirectoryPath);
        }
        [Fact]
        public void Move_FileDoesNotExist_ThrowInvalidOperationException()
        {
            string testID = Guid.NewGuid().ToString();
            string originalDirectoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string newDirectoryPath = FileManager_Test.TESTING_MOVETO_DIRECTORY + testID;
            string filename = FileManager_Test.TESTING_NONDUPLICATE_FILENAME;
            string extension = FileManager_Test.TESTING_EXTENSION;
            bool overwrite = false;

            FileManager_Test testManager = new(filename, originalDirectoryPath);
            Directory.CreateDirectory(newDirectoryPath);
            File.Delete(Path.Combine(originalDirectoryPath, filename + extension));

            Assert.Throws<InvalidOperationException>
                (
                    () => testManager.Move(newDirectoryPath, overwrite)
                );

            TestServices.RunTestCleanup(testManager, originalDirectoryPath);
            TestServices.RunTestCleanup(testManager, newDirectoryPath);
        }
        [Fact]
        public void Move_FileIsOpen_ThrowInvalidOperationException()
        {
            string testID = Guid.NewGuid().ToString();
            string originalDirectoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string newDirectoryPath = FileManager_Test.TESTING_MOVETO_DIRECTORY + testID;
            FileAccess fileAccess = FileAccess.ReadWrite;
            bool overwrite = false;

            FileManager_Test testManager = new(originalDirectoryPath, fileAccess);
            Directory.CreateDirectory(newDirectoryPath);

            Assert.Throws<InvalidOperationException>
                (
                    () => testManager.Move(newDirectoryPath, overwrite)
                );

            TestServices.RunTestCleanup(testManager, originalDirectoryPath);
            TestServices.RunTestCleanup(testManager, newDirectoryPath);
        }
    }
}
