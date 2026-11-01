using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Text
{
    /*
     * Reads from an open file
     */
    internal class TextFileReader : IFileReader
    {
        private StreamReader Stream { get; }

        /*
         * Creates and returns an IFileReader
         */
        public static IFileReader CreateReader(FileStream fs) => new TextFileReader(fs);

        /*
         * Constructs and sets the internal StreamReader using the passed in FileStream
         */
        private TextFileReader(FileStream fs)
        {
            ArgumentNullException.ThrowIfNull(fs);
            Stream = new(fs);
        }

        /*
         * Reads and returns the entire file
         */
        public string ReadFile()
        {
            // TODO Write read logic

            return "";
        }

        /*
         * Reads and returns a single line from the file
         */
        public string ReadLine()
        {
            // TODO Write read logic

            return "";
        }
    }
}
