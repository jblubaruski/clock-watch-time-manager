using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility
{
    /*
     * Defines the behavior of a class that manages and carries out operations on a file at a specific filepath.
     */
    internal interface IFileManager
    {
        public void Open(FileAccess fileAccess, FileShare fileShare);
        public void Close();
        public void Rename(string baseFilename);
        public void Move(string directoryPath, bool overwrite);
        public void Delete();
        public bool FileExists();
        public bool IsOpen();
    }
}
