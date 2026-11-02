using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace FileUtility.Text
{
    /*
     * Encapsulates a filepath representing the location of a text file while carrying out operations on and managing the file's state.
     */
    public class TextFileManager : IFileManager
    {
        protected const string EXTENSION = ".txt";
        protected string Filepath { get; set; }
        protected string DirectoryPath { get; set; }
        protected StreamReader? Reader { get; set; }
        protected StreamWriter? Writer { get; set; }

        /*
         * Validates the passed in filepath and creates a file at that location while creating a directory if needed.
         * 
         * baseFilename is the base filename of the file
         * directoryPath is the path to the directory where the file is located
         * if open is true, the filestream isnt closed after creation and a reader and writer is constructed.
         */
        public TextFileManager(string baseFilename, string directoryPath)
        {
            if (!FileValidator.FilepathIsValid(baseFilename, EXTENSION, directoryPath, out string errorMessage))
                throw new ArgumentException(errorMessage);

            Filepath = Path.Combine(directoryPath, baseFilename) + EXTENSION;
            DirectoryPath = directoryPath;

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            FileStream fs = File.Create(Filepath);

            fs.Close();
            Reader = null;
            Writer = null;
        }

        /*
         * Returns true if this file exists on the file system
         */
        public bool FileExists() => File.Exists(Filepath);

        /*
         * Returns true if this file has open filestreams associated with it
         */
        public bool IsOpen()
        {
            bool isOpen = Reader != null || Writer != null;

            if (Reader != null)
                isOpen = Reader.BaseStream.CanRead || Reader.BaseStream.CanWrite;
            if (Writer != null)
                isOpen = Writer.BaseStream.CanRead || Writer.BaseStream.CanWrite;

            return isOpen;
        }

        /*
         * Opens the file using the passed in options.
         * Opens a read-write file that declines file sharing by default.
         * 
         * fileAccess determines whether the file is read-only, write-only or read-write
         * fileShare determines how much access other operations have to this file
         * 
         * Throws an InvalidOperationException if the file is currently open
         * Throws an InvalidOperationException if this file does not exist on the file system
         */
        public void Open(FileAccess fileAccess = FileAccess.ReadWrite, FileShare fileShare = FileShare.None)
        {
            // TODO write file open logic
        }

        /*
         * Closes the file.
         * 
         * Throws an InvalidOperationException if the file is already closed
         * Throws an InvalidOperationException if this file does not exist on the file system
         */
        public void Close()
        {
            // TODO write close logic
            if (!IsOpen())
                throw new InvalidOperationException("the file cannot be closed when this method is called");
            else if (!FileExists())
                throw new InvalidOperationException("the file must exist on the file system in order to close it");

            Reader?.Close();
            Writer?.Close();

            Reader = null;
            Writer = null;
        }

        /*
         * Renames the file to a new filename.
         * 
         * baseFilename is the base name of the new filename
         * 
         * Throws an ArgumentException if the baseFilename is an invalid base filename
         */
        public void Rename(string baseFilename)
        {
            // TODO write rename logic
        }

        /*
         * Copies the contents of this file to a new file in this file's directory.
         * 
         * baseFilename is the filename of the new file
         * 
         * Throws an ArgumentException if the baseFilename is an invalid base filename
         * Throws an InvalidOperationException if this file does not exist on the file system
         */
        public IFileManager Copy(string baseFilename)
        {
            // TODO write copy logic

            return null!;
        }

        /*
         * Copies the contents of this file to a new file at a new location.
         * 
         * baseFilename is the filename of the new file
         * directoryPath is the location of the new file
         * 
         * Throws an ArgumentException if the filepath formed by the passed in strings is invalid
         * Throws an InvalidOperationException if this file does not exist on the file system
         */
        public IFileManager Copy(string baseFilename, string directoryPath)
        {
            // TODO write copy logic

            return null!;
        }

        /*
         * Moves this file to a new location
         * 
         * directoryPath is the new location of this file
         * 
         * Throws an ArgumentException if directoryPath is an invalid directory path
         * Throws an InvalidOperationException if this file does not exist on the file system
         */
        public void Move(string directoryPath)
        {
            // TODO write move logic
        }

        /*
         * Deletes this file from the file system
         * 
         * Throws an InvalidOperationException if this file is open or does not exist on the file system
         */
        public void Delete()
        {
            // TODO write delete logic
        }

        /*
         * Reads and returns the entire file
         * 
         * Throws an InvalidOperationException if the file is closed
         */
        public string ReadFile()
        {
            // TODO Write read logic

            return "";
        }

        /*
         * Reads and outputs a single line from the file
         * Returns true if there was a line to read
         * 
         * Throws an InvalidOperationException if the file is closed
         */
        public bool ReadLine(out string output)
        {
            // TODO Write read logic

            output = "";
            return false;
        }

        /*
         * Overwrites the file with the passed in string
         * 
         * Throws an InvalidOperationException if the file is closed
         */
        public void WriteToFile(string toWrite)
        {
            ArgumentNullException.ThrowIfNull(toWrite);

            // TODO write write logic
        }

        /*
         * Appends the passed in string to the end of the file
         */
        public void AppendToFile(string toAppend)
        {
            ArgumentNullException.ThrowIfNull(toAppend);

            // TODO write append logic
        }
    }
}
