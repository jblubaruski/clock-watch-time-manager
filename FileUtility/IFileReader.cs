using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility
{
    /*
     * Defines the behavior of a class that reads from a file
     */
    public interface IFileReader
    {
        public string ReadFile();
        public string ReadLine();
    }
}
