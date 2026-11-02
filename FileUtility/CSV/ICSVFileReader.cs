using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.CSV
{
    /*
     * Defines behavior for a class that reads from a csv file
     */
    public interface ICSVFileReader
    {
        public string[,] ReadFile(bool firstLineIsHeader);
        public bool ReadEntry(out string[] output);
    }
}
