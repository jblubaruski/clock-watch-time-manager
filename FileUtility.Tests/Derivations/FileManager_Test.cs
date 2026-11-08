using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Tests.Derivations
{
    /*
     * Class used to test FileManager.
     * 
     * Exposes the streamreader and streamwriter from the encapsulated file.
     * Contains testing filepaths.
     */
    internal class FileManager_Test : FileManager
    {
        public const string TESTING_TEMP_DIRECTORY = "D:\\PlaypenTesting\\FileUtility\\FileManagerTests\\TestTempDir";
        public const string TESTING_DUPLICATEFILENAME_DIRECTORY = "D:\\PlaypenTesting\\FileUtility\\FileManagerTests\\TestDuplicateFilenameDir";
        public const string TESTING_MOVETO_DIRECTORY = "D:\\PlaypenTesting\\FileUtility\\FileManagerTests\\TestMoveToDir";
        public const string TESTING_COPYTO_DIRECTORY = "D:\\PlaypenTesting\\FileUtility\\FileManagerTests\\TestCopyToDir";
        public const string TESTING_DUPLICATE_FILENAME = "a";
        public const string TESTING_NONDUPLICATE_FILENAME = "d";
        public const string TESTING_TEMP_FILENAME = "test_temp";
        public const string TESTING_EXTENSION = ".txt";

        // constructor that creates a file named test_temp.txt in the passed in directory
        public FileManager_Test(string directoryPath) : base(TESTING_TEMP_FILENAME, TESTING_EXTENSION, directoryPath) { }

        // constructor that creates a file named {baseFilename}.txt in the passed in directory
        public FileManager_Test(string baseFilename, string directoryPath) : base(baseFilename, TESTING_EXTENSION, directoryPath) { }
    }
}
