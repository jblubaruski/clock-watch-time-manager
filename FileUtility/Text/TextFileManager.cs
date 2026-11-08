using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace FileUtility.Text
{
    /*
     * A file manager that can carry out read and write operations on a text file.
     */
    public class TextFileManager : FileManager, ITextFileReader, ITextFileWriter
    {
        public const string EXTENSION = ".txt";

        // constructs a file at the directory at directory path with a base filename of baseFilename and an extension of EXTENSION
        public TextFileManager(string baseFilename, string directoryPath) : base(baseFilename, EXTENSION, directoryPath) { }

        /*
         * Reads the file and returns the file's contents as a string.
         * 
         * Lines are separated with newline characters.
         * If the file is empty, a null value is returned.
         * 
         * Throws an InvalidOperationException if the file does not exist
         */
        public string? ReadFile()
        {
            if (!FileExists())
                throw new InvalidOperationException($"cannot read from {Filepath} because it does not exist on the file system");

            using StreamReader reader = new(Filepath);

            string? line = reader.ReadLine();
            if (line == null)
            {
                return null;
            }
            else
            {
                string content = line + "\n";
                while ((line = reader.ReadLine()) != null)
                {
                    content += line + "\n";
                }

                return content[..^1];
            }
        }

        /*
         * Reads the next line from the file and returns it as a string.
         * 
         * If there is no next line to read from the file, a null value is returned.
         * 
         * Throws an InvalidOperationException if the file does not exist
         */
        public string? ReadLine()
        {
            return null;
        }

        /*
         * Overwrites a file with the content of the passed in string.
         * 
         * Throws an ArgumentException if the passed in string is null
         * Throws an InvalidOperationException if the file does not exist
         */
        public void WriteToFile(string toWrite)
        {

        }

        /*
         * Appends the passed in string to the end of the file as a new line.
         * 
         * Throws an ArgumentException if the passed in string is null
         * Throws an InvalidOperationException if the file does not exist
         */
        public void AppendToFile(string toAppend)
        {

        }
    }
}
