using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility.JSON
{
    /*
     * Defines behavior for a class that reads from a json file
     */
    public interface IJsonFileReader
    {
        public object[]? ReadFile();
        public object? ReadEntry();
    }
}
