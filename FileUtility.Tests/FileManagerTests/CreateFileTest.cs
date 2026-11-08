using FileUtility.Tests.Derivations;
using FileUtility.Tests.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.FileManagerTests
{
    /*
     * Tests for FileManager.CreateFile()
     * 
     * Assert that the the file exists on the file system after calling CreateFile() on a deleted file
     */
    public class CreateFileTest
    {
        [Fact]
        public void CreateFile_DeletedFile_FileExists()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string baseFilename = FileManager_Test.TESTING_TEMP_FILENAME;
            string extension = FileManager_Test.TESTING_EXTENSION;

            FileManager_Test testManager = new(baseFilename, directoryPath);
            File.Delete(Path.Combine(directoryPath, baseFilename + extension));
            testManager.CreateFile();

            bool result = File.Exists(Path.Combine(directoryPath, baseFilename + extension));

            TestServices.RemoveTestDirectory(directoryPath);

            Assert.True(result);
        }
    }
}
