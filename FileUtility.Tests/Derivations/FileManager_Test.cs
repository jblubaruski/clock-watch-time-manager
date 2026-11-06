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
    internal class FileManager_Test : FileManager
    {
        public const string TESTING_TEMP_DIRECTORY = "D:\\PlaypenTesting\\FileUtility\\FileManagerTests\\TestTempDir";
        public const string TESTING_READ_DIRECTORY = "D:\\PlaypenTesting\\FileUtility\\FileManagerTests\\TestReadDir";
        public const string TESTING_WRITE_DIRECTORY = "D:\\PlaypenTesting\\FileUtility\\FileManagerTests\\TestWriteDir";
        public const string TESTING_DUPLICATEFILENAME_DIRECTORY = "D:\\PlaypenTesting\\FileUtility\\FileManagerTests\\TestDuplicateFilenameDir";
        public const string TESTING_MOVETO_DIRECTORY = "D:\\PlaypenTesting\\FileUtility\\FileManagerTests\\TestMoveToDir";
        public const string TESTING_COPYTO_DIRECTORY = "D:\\PlaypenTesting\\FileUtility\\FileManagerTests\\TestCopyToDir";
        public const string TESTING_DUPLICATE_FILENAME = "a";
        public const string TESTING_NONDUPLICATE_FILENAME = "c";
        public const string TESTING_TEMP_FILENAME = "test_temp";
        public const string TESTING_EXTENSION = ".txt";

        public StreamReader? ExposedReader { get; private set; }
        public StreamWriter? ExposedWriter { get; private set; }

        // constructor that creates a file named test_temp.txt in the passed in directory
        public FileManager_Test(string directoryPath) : base(TESTING_TEMP_FILENAME, TESTING_EXTENSION, directoryPath)
        {
            ExposedReader = Reader;
            ExposedWriter = Writer;
        }

        // constructor that creates a file named test_temp.txt in the passed in directory and reopens the file with the passed in FileAccess
        public FileManager_Test(string directoryPath, FileAccess fileAccess) : base(TESTING_TEMP_FILENAME, TESTING_EXTENSION, directoryPath)
        {
            FileStream fs = File.Open(Filepath, FileMode.Open, fileAccess);

            if (fileAccess == FileAccess.Read)
            {
                Reader = new(fs);
            }
            else if (fileAccess == FileAccess.Write)
            {
                Writer = new(fs);
            }
            else
            {
                Reader = new(fs);
                Writer = new(fs);
            }

            ExposedReader = Reader;
            ExposedWriter = Writer;
        }

        // constructor that creates a file named {baseFilename}.txt in the passed in directory
        public FileManager_Test(string baseFilename, string directoryPath) : base(baseFilename, TESTING_EXTENSION, directoryPath)
        {
            ExposedReader = Reader;
            ExposedWriter = Writer;
        }

        public void Open_Test(FileAccess fileAccess = FileAccess.ReadWrite, FileShare fileShare = FileShare.None)
        {
            Open(fileAccess, fileShare);

            ExposedReader = Reader;
            ExposedWriter = Writer;
        }
    }
}
