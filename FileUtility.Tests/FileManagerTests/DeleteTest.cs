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
     * Assert that a file does not exist after delete has been called
     */
    public class DeleteTest
    {
        [Fact]
        public void Delete_FileIsClosed_FileDoesNotExist()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string filename = FileManager_Test.TESTING_TEMP_FILENAME + FileManager_Test.TESTING_EXTENSION;

            FileManager_Test testManager = new(directoryPath);
            testManager.Delete();

            bool result = !File.Exists(Path.Combine(directoryPath, filename));

            TestServices.RemoveTestDirectory(directoryPath);

            Assert.True(result);
        }
    }
}
