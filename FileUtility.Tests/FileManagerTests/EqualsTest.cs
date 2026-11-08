using FileUtility.Tests.Derivations;
using FileUtility.Tests.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.FileManagerTests
{
    /*
     * Tests for FileManager.Equals(FileManager?)
     * 
     * Assert that comparing two file managers with the same filepath evaluates to true
     * Assert that comparing two file managers with different filepaths evaluates to false
     */
    public class EqualsTest
    {
        [Fact]
        public void Equals_SameFilepaths_ReturnTrue()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string baseFilename1 = FileManager_Test.TESTING_DUPLICATE_FILENAME;
            string baseFilename2 = FileManager_Test.TESTING_DUPLICATE_FILENAME;

            FileManager_Test testManager1 = new(baseFilename1, directoryPath);
            FileManager_Test testManager2 = new(baseFilename2, directoryPath);

            bool result = testManager1.Equals(testManager2);

            TestServices.RemoveTestDirectory(directoryPath);

            Assert.True(result);
        }
        [Fact]
        public void Equals_DifferentFilepaths_ReturnFalse()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string baseFilename1 = FileManager_Test.TESTING_DUPLICATE_FILENAME;
            string baseFilename2 = FileManager_Test.TESTING_NONDUPLICATE_FILENAME;

            FileManager_Test testManager1 = new(baseFilename1, directoryPath);
            FileManager_Test testManager2 = new(baseFilename2, directoryPath);

            bool result = testManager1.Equals(testManager2);

            TestServices.RemoveTestDirectory(directoryPath);

            Assert.False(result);
        }
    }
}
