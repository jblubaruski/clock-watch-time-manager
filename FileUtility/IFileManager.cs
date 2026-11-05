using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility
{
    /*
     * Defines the behavior of a class that manages and carries out operations on a file at a specific filepath.
     */
    public interface IFileManager
    {
        public void Open(FileAccess fileAccess, FileShare fileShare);
        public void Close();
        public void Rename(string baseFilename);
        public IFileManager Copy(string baseFilename, bool overwrite);
        public IFileManager Copy(string baseFilename, string directoryPath, bool overwrite);
        public void Move(string directoryPath, bool overwrite);
        public void Delete();
        public bool FileExists();
        public bool IsOpen();
    }
}
