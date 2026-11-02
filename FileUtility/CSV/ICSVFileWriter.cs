using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.CSV
{
    /*
     * Defines behavior for a class that writes to a csv file
     */
    public interface ICSVFileWriter
    {
        public void WriteToFile(string[,] toWrite);
        public void AppendToFile(string[] toAppend);
    }
}
