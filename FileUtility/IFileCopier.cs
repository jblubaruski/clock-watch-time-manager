using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility
{
    public interface IFileCopier
    {
        public FileManager Copy(string baseFilename, bool overwrite);
        public FileManager Copy(string baseFilename, string directoryPath, bool overwrite);
    }
}
