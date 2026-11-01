using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility
{
    /*
     * Defines the properties and behavior of a class that reads from a file
     */
    public interface IFileReader
    {
        public StreamReader Stream { get; }

        public string ReadFile();
        public string ReadLine();
    }
}
