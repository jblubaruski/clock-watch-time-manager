using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Text
{
    /*
     * Defines behavior for a class that writes to a text file
     */
    public interface ITextFileWriter
    {
        public void WriteToFile(string toWrite);
        public void AppendToFile(string toAppend);
    }
}
