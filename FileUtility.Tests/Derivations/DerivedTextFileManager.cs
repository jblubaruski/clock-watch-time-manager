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
    internal class DerivedTextFileManager : TextFileManager
    {
        public const string TESTING_TEMP_DIRECTORY = "D:\\PlaypenTesting\\FileUtility\\ConstructorTest\\TestTempDir";
        public const string TESTING_READ_DIRECTORY = "D:\\PlaypenTesting\\FileUtility\\ConstructorTest\\TestReadDir";
        public const string TESTING_WRITE_DIRECTORY = "D:\\PlaypenTesting\\FileUtility\\ConstructorTest\\TestWriteDir";
        public const string TESTING_TEMP_FILENAME = "test_temp";
        public const string TESTING_EXTENSION = ".txt";

        public StreamReader? ExposedReader { get; }
        public StreamWriter? ExposedWriter { get; }

        // constructor that creates a file named test_temp.txt in the passed in directory
        public DerivedTextFileManager(string directoryPath) : base(TESTING_TEMP_FILENAME, directoryPath)
        {
            ExposedReader = Reader;
            ExposedWriter = Writer;
        }

        // constructor that creates a file named test_temp.txt in the passed in directory and reopens the file with the passed in FileAccess
        public DerivedTextFileManager(string directoryPath, FileAccess fileAccess) : base(TESTING_TEMP_FILENAME, directoryPath)
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
        public DerivedTextFileManager(string baseFilename, string directoryPath) : base(baseFilename, directoryPath)
        {
            ExposedReader = Reader;
            ExposedWriter = Writer;
        }
    }
}
