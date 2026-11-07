using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace FileUtility.JSON
{
    /*
     * A file manager that can carry out read and write operations on a json file.
     */
    public class JsonFileManager : FileManager, IJsonFileReader, IJsonFileWriter
    {
        public const string EXTENSION = ".json";

        // constructs a file at the directory at directory path with a base filename of baseFilename and an extension of EXTENSION
        public JsonFileManager(string baseFilename, string directoryPath) : base(baseFilename, EXTENSION, directoryPath) { }

        /*
         * Reads the file and returns the file's contents as an array of deserialized objects.
         * 
         * If the file is empty, a null value is returned.
         * 
         * Throws an InvalidOperationException if the file is closed
         * Throws an InvalidOperationException if the file does not exist
         * Throws an InvalidOperationException if the file is write-only
         */
        public object[]? ReadFile()
        {
            return null;
        }

        /*
         * Reads the next entry from the file and returns it as a deserialized object.
         * 
         * If there are no more entries to read from the file, a null value is returned.
         * 
         * Throws an InvalidOperationException if the file is closed
         * Throws an InvalidOperationException if the file does not exist
         * Throws an InvalidOperationException if the file is write-only
         */
        public object? ReadEntry()
        {
            return null;
        }

        /*
         * Overwrites a file with the content of the passed in array of objects.
         * 
         * Throws an ArgumentException if the passed in array is null
         * Throws an InvalidOperationException if the file is closed
         * Throws an InvalidOperationException if the file does not exist
         * Throws an InvalidOperationException if the file is read-only
         */
        public void WriteToFile(object[] toWrite)
        {

        }

        /*
         * Appends the passed in object to the end of the file as a new entry.
         * 
         * Throws an ArgumentException if the passed in object is null
         * Throws an InvalidOperationException if the file is closed
         * Throws an InvalidOperationException if the file does not exist
         * Throws an InvalidOperationException if the file is read-only
         */
        public void AppendToFile(object toAppend)
        {

        }
    }
}
