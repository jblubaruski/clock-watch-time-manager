using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility
{
    /*
     * Defines the behavior of a class that writes to a file
     */
    public interface IFileWriter
    {
        public void WriteToFile(string toWrite);
        public void AppendToFile(string toAppend);
    }
}
