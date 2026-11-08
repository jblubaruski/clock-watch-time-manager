using FileUtility.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.Derivations
{
    /*
     * Class used to test TextFileManager.
     * 
     * Exposes the streamreader and streamwriter from the encapsulated file.
     * Contains testing filepaths.
     */
    internal class TextFileManager_Test : TextFileManager
    {
        public const string TESTING_READ_DIRECTORY = "D:\\PlaypenTesting\\FileUtility\\FileManagerTests\\TestReadDir";
        public const string TESTING_WRITE_DIRECTORY = "D:\\PlaypenTesting\\FileUtility\\FileManagerTests\\TestWriteDir";
        public const string TESTING_READ_FILENAME = "test_read";
        public const string TESTING_WRITE_FILENAME = "test_write";

        public string ExposedFilepath { get; private set; }
        public string ExposedExtension { get; private set; }
        public string ExposedDirectoryPath { get; private set; }

        // constructor that creates a file named {baseFilename}.txt in the passed in directory
        public TextFileManager_Test(string baseFilename, string directoryPath) : base(baseFilename, directoryPath)
        {
            ExposedFilepath = Filepath;
            ExposedExtension = Extension;
            ExposedDirectoryPath = DirectoryPath;
        }
    }
}
