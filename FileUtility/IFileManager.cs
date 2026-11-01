using System;
using System.Collections.Generic;
using System.Text;

namespace FileUtility
{
    /*
     * Defines the behavior of a class that manages a specific filepath.
     */
    public interface IFileManager
    {
        abstract static IFileManager CreateFile(string filepath, bool isOpenOnSetup);
        public void Open(FileMode fileMode, FileAccess fileAccess, FileShare fileShare);
        public void Close();
        public void Copy(string newFilename, string newDirectoryPath);
        public void Move(string newDirectoryPath);
        public void Delete();
        public bool FileExists();
        public bool IsOpen();
    }
}
