using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility
{
    /*
     * Defines the properties and behavior of a class that writes to a file
     */
    public interface IFileWriter
    {
        public StreamWriter Stream { get; }

        public void WriteToFile(string toWrite);
        public void AppendToFile(string toAppend);
    }
}
