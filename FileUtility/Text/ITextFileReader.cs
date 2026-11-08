using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.Text
{
    /*
     * Defines behavior for a class that reads from a text file
     */
    public interface ITextFileReader
    {
        public string? ReadFile();
        public string? ReadLine(int lineNumber);
    }
}
