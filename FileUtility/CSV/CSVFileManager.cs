using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.CSV
{
    /*
     * A file manager that can carry out read and write operations on a csv file.
     */
    public class CSVFileManager : FileManager, ICSVFileReader, ICSVFileWriter
    {
        public const string EXTENSION = ".csv";

        // constructs a file at the directory at directory path with a base filename of baseFilename and an extension of EXTENSION
        public CSVFileManager(string baseFilename, string directoryPath) : base(baseFilename, EXTENSION, directoryPath) { }

        /*
         * Reads the file and returns the file's contents as a 2D array of strings.
         * 
         * firstLineIsHeader determines whether the first line read from the file is a header
         * If the file is empty, a null value is returned.
         * 
         * Throws an InvalidOperationException if the file is closed
         * Throws an InvalidOperationException if the file does not exist
         * Throws an InvalidOperationException if the file is write-only
         */
        public string[,]? ReadFile(bool firstLineIsHeader)
        {
            return null;
        }

        /*
         * Reads the next entry from the file and returns it as an array of strings.
         * 
         * If there is no next line to read from the file, a null value is returned.
         * 
         * Throws an InvalidOperationException if the file is closed
         * Throws an InvalidOperationException if the file does not exist
         * Throws an InvalidOperationException if the file is write-only
         */
        public string[]? ReadEntry()
        {
            return null;
        }

        /*
         * Overwrites a file with the content of the passed in 2D string array.
         * 
         * Throws an ArgumentException if the passed in string is null
         * Throws an InvalidOperationException if the file is closed
         * Throws an InvalidOperationException if the file does not exist
         * Throws an InvalidOperationException if the file is read-only
         */
        public void WriteToFile(string[,] toWrite)
        {

        }

        /*
         * Appends the passed in string array to the end of the file as a new entry.
         * 
         * Throws an ArgumentException if the passed in string is null
         * Throws an InvalidOperationException if the file is closed
         * Throws an InvalidOperationException if the file does not exist
         * Throws an InvalidOperationException if the file is read-only
         */
        public void AppendToFile(string[] toAppend)
        {

        }
    }
}
