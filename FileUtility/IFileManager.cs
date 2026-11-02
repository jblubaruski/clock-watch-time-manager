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
        public IFileManager Copy(string baseFilename);
        public IFileManager Copy(string baseFilename, string directoryPath);
        public void Move(string directoryPath);
        public void Delete();
        public bool FileExists();
        public bool IsOpen();
        public string ReadFile();
        public bool ReadLine(out string output);
        public void WriteToFile(string toWrite);
        public void AppendToFile(string toAppend);
    }
}
