using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Text
{
    /*
     * Writes to an open file
     */
    internal class TextFileWriter : IFileWriter
    {
        private StreamWriter Stream { get; }

        /*
         * Creates and returns an IFileWriter
         */
        public static IFileWriter CreateWriter(FileStream fs) => new TextFileWriter(fs);

        /*
         * Constructs and sets the internal StreamWriter using the passed in FileStream
         */
        private TextFileWriter(FileStream fs)
        {
            ArgumentNullException.ThrowIfNull(fs);
            Stream = new(fs);
        }

        /*
         * Overwrites the file with the passed in string
         */
        public void WriteToFile(string toWrite)
        {
            ArgumentNullException.ThrowIfNull(toWrite);

            // TODO write write logic
        }

        /*
         * Appends the passed in string to the end of the file
         */
        public void AppendToFile(string toAppend)
        {
            ArgumentNullException.ThrowIfNull(toAppend);

            // TODO write append logic
        }
    }
}
