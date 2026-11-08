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
         * Reads the specified line from the file and returns it as a string.
         * 
         * lineNumber is the number of the line to be read (starts at zero)
         * A null pointer is returned if the file is empty or there no line corresponding to the passed in line number
         * 
         * Throws an InvalidOperationException if the file does not exist
         */
        public string? ReadLine(int lineNumber)
        {
            if (!FileExists())
                throw new InvalidOperationException($"cannot read from {Filepath} because it does not exist on the file system");

            using StreamReader reader = new(Filepath);

            string? line = reader.ReadLine();

            // the first line contains content and is the desired line
            if (line != null && lineNumber == 0)
                return line;

            // iterate through rest the file until either the end of the file or desired line number is reached
            int currentLine = 0;
            while ((line = reader.ReadLine()) != null)
            {
                currentLine += 1;

                if (currentLine == lineNumber)
                    return line;
            }

            // the end of the file was reached before reaching the desired line number
            return null;
        }

        /*
         * Overwrites a file with the content of the passed in string.
         * 
         * Does nothing if the passed in string is null
         * 
         * Throws an InvalidOperationException if the file does not exist
         */
        public void WriteToFile(string toWrite)
        {
            if (!FileExists())
                throw new InvalidOperationException($"cannot read from {Filepath} because it does not exist on the file system");

            if (toWrite != null)
            {
                using StreamWriter writer = new(Filepath, false);
                writer.WriteLine(toWrite);
            }
        }

        /*
         * Appends the passed in string to the end of the file as a new line.
         * 
         * Does nothing if the passed in string is null
         * A passed in string with new line characters should append multiple lines with the new line splitting up the lines
         * 
         * Throws an InvalidOperationException if the file does not exist
         */
        public void AppendToFile(string toAppend)
        {
            if (!FileExists())
                throw new InvalidOperationException($"cannot read from {Filepath} because it does not exist on the file system");

            if (toAppend != null)
            {
                using StreamWriter writer = new(Filepath, true);
                writer.WriteLine(toAppend);
            }
        }
    }
}
