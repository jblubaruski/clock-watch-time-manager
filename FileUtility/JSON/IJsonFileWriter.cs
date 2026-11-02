using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.JSON
{
    /*
     * Defines behavior for a class that writes to a json file
     */
    public interface IJsonFileWriter
    {
        public void WriteToFile(object[] toWrite);
        public void AppendToFile(object toAppend);
    }
}
