using FileUtility.Tests.Derivations;
using FileUtility.Tests.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.TextFileManagerTests
{
    /*
     * Tests for TextFileManager.Close()
     * 
     * Assert that an InvalidOperationException is thrown when Close() is called while the encapsulated file is already closed
     * Assert that an InvalidOperationException is thrown when Close() is called when the encapsulated file does not exist on the file system
     * Assert that the encapsulated file is closed after Close() is called while it is open
     */
    public class CloseTest
    {
        [Fact]
        public void Close_FileClosed_ThrowInvalidOperationException()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;

            FileManager_Test testManager = new(directoryPath);

            Assert.Throws<InvalidOperationException>(testManager.Close);

            TestServices.RunTestCleanup(testManager, directoryPath);
        }
        [Fact]
        public void Close_FileDoesNotExist_ThrowInvalidOperationException()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string filename = FileManager_Test.TESTING_TEMP_FILENAME + FileManager_Test.TESTING_EXTENSION;
            FileAccess fileAccess = FileAccess.ReadWrite;

            FileManager_Test testManager = new(directoryPath, fileAccess);
            File.Delete(Path.Combine(directoryPath, filename));

            Assert.Throws<InvalidOperationException>(testManager.Close);
            
            TestServices.RunTestCleanup(testManager, directoryPath);
        }
        [Fact]
        public void Close_FileLeftOpen_FileIsClosed()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string filename = FileManager_Test.TESTING_TEMP_FILENAME + FileManager_Test.TESTING_EXTENSION;
            FileAccess fileAccess = FileAccess.ReadWrite;

            FileManager_Test testManager = new(directoryPath, fileAccess);
            bool result = true;
            try
            {
                FileStream fs = File.Open(Path.Combine(directoryPath, filename), FileMode.Open);
                fs.Close();
                TestServices.RunTestCleanup(testManager, directoryPath);
            }
            catch (Exception)
            {
                result = false;
            }

            Assert.True(result);
        }
    }
}
