using FileUtility.Tests.Derivations;
using FileUtility.Tests.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.FileManagerTests
{
    /*
     * Tests for FileManager.Delete()
     * 
     * Assert that an InvalidOperationException if delete is called while a file is open
     * Assert that a file does not exist after delete has been called
     */
    public class DeleteTest
    {
        [Fact]
        public void Delete_FileIsOpen_ThrowInvalidOperationException()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            FileAccess fileAccess = FileAccess.ReadWrite;

            FileManager_Test testManager = new(directoryPath, fileAccess);

            Assert.Throws<InvalidOperationException>
                (
                    () => testManager.Delete()
                );

            TestServices.RunTestCleanup(testManager, directoryPath);
        }
        [Fact]
        public void Delete_FileIsClosed_FileDoesNotExist()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string filename = FileManager_Test.TESTING_TEMP_FILENAME + FileManager_Test.TESTING_EXTENSION;

            FileManager_Test testManager = new(directoryPath);
            testManager.Delete();
            bool result = !File.Exists(Path.Combine(directoryPath, filename));
            TestServices.RunTestCleanup(testManager, directoryPath);

            Assert.True(result);
        }
    }
}
