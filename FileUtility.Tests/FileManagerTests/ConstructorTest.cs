using FileUtility.Tests.Derivations;
using FileUtility.Tests.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.FileManagerTests
{
    /*
     * Tests for the FileManager constructor
     * 
     * Assert that passing in an invalid filepath component throws an ArgumentException
     * Assert that a directory is created if the directory does not already exist
     * Assert that a file is created in the temp testing directory
     * Assert that the file is closed after creation
     */
    public class ConstructorTest
    {
        [Fact]
        public void Constructor_InvalidFilepath_ThrowArgumentException()
        {
            string baseFilename = "test";
            string directoryPath = null!;

            Assert.Throws<ArgumentException>
                (
                    () => new FileManager_Test(baseFilename, directoryPath)
                );
        }
        [Fact]
        public void Constructor_ValidInput_DirectoryCreated()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;

            FileManager_Test testManager = new(directoryPath);

            bool result = Directory.Exists(directoryPath);

            TestServices.RemoveTestDirectory(directoryPath);

            Assert.True(result);
        }
        [Fact]
        public void Constructor_ValidInput_FileCreated()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string filename = FileManager_Test.TESTING_TEMP_FILENAME + FileManager_Test.TESTING_EXTENSION;

            FileManager_Test testManager = new(directoryPath);

            bool result = File.Exists(Path.Combine(directoryPath, filename));

            TestServices.RemoveTestDirectory(directoryPath);

            Assert.True(result);
        }
        [Fact]
        public void Constructor_ValidInput_FileIsClosed()
        {
            string testID = Guid.NewGuid().ToString();
            string directoryPath = FileManager_Test.TESTING_TEMP_DIRECTORY + testID;
            string filename = FileManager_Test.TESTING_TEMP_FILENAME + FileManager_Test.TESTING_EXTENSION;

            FileManager_Test testManager = new(directoryPath);

            bool result = true;
            try
            {
                FileStream fs = File.Open(Path.Combine(directoryPath, filename), FileMode.Open);
                fs.Close();
            }
            catch (IOException)
            {
                result = false;
            }

            TestServices.RemoveTestDirectory(directoryPath);

            Assert.True(result);
        }
    }
}
