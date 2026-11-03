using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace FileUtility
{
    /*
     * Encapsulates a filepath representing the location of a text file while carrying out operations on and managing the file's state.
     */
    public abstract class FileManager : IFileManager
    {
        private const FileMode FILEMODE = FileMode.Open;

        protected string Filepath { get; set; }
        protected string DirectoryPath { get; set; }
        protected string Extension { get; set; }
        protected StreamReader? Reader { get; set; }
        protected StreamWriter? Writer { get; set; }

        /*
         * Validates the passed in filepath and creates a file at that location while creating a directory if needed.
         * 
         * baseFilename is the base filename of the file
         * directoryPath is the path to the directory where the file is located
         * if open is true, the filestream isnt closed after creation and a reader and writer is constructed.
         */
        public FileManager(string baseFilename, string extension, string directoryPath)
        {
            if (!FileValidator.FilepathIsValid(baseFilename, extension, directoryPath, out string errorMessage))
                throw new ArgumentException(errorMessage);

            Filepath = Path.Combine(directoryPath, baseFilename) + extension;
            DirectoryPath = directoryPath;
            Extension = extension;

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
            if (!FileExists())
                throw new InvalidOperationException($"the file at {Filepath} must exist in order to open it");
            if (IsOpen())
                throw new InvalidOperationException($"cannot open the file at {Filepath} while it is already open");

            FileStream fs = File.Open(Filepath, FILEMODE, fileAccess, fileShare);
            if (fileAccess == FileAccess.Read)
            {
                Reader = new(fs);
            }
            else if (fileAccess == FileAccess.Write)
            {
                Writer = new(fs);
            }
            else
            {
                Reader = new(fs);
                Writer = new(fs);
            }
        }

        /*
         * Closes the file.
         * 
         * Throws an InvalidOperationException if this file does not exist on the file system
         */
        public void Close()
        {
            if (!FileExists())
                throw new InvalidOperationException("the file must exist on the file system in order to close it");
            
            if (Reader != null && IsOpen())
            {
                Reader.Close();
            }   
            else if (Writer != null && IsOpen())
            {
                Writer.Close();
            }

            Reader = null;
            Writer = null;
        }

        /*
         * Renames the file to a new filename while giving the new filename the same extension as the old filename
         * 
         * newFilename is the base name of the new filename
         * 
         * Throws an ArgumentException if the newFilename is an invalid base filename
         * Throws an ArgumentException if a file with the same name as the new name (case-insensitive) already exists
         * Throws an InvalidOperationException if the original file is open
         */
        public void Rename(string newFilename)
        {
            // TODO write rename logic
            if (!FileValidator.BaseFilenameIsValid(newFilename, out string errorMessage))
                throw new ArgumentException(errorMessage);
            if (newFilename == Path.GetFileNameWithoutExtension(Filepath))
                throw new ArgumentException($"newFilename ({newFilename}) cannot be the same as the current filename ({Path.GetFileNameWithoutExtension(Filepath)})");
            if (IsOpen())
                throw new InvalidOperationException("An open file cannot be renamed");

            string newFilepath = Path.Combine(DirectoryPath, newFilename + Extension);

            if (FileExists())
                File.Move(Filepath, newFilepath);

            Filepath = newFilepath;
        }

        /*
         * Copies the contents of this file to a new file in this file's directory.
         * 
         * newFilename is the filename of the new file
         * 
         * Throws an ArgumentException if the newFilename is an invalid base filename
         * Throws an InvalidOperationException if this file does not exist on the file system
         */
        public IFileManager Copy(string newFilename)
        {
            // TODO write copy logic

            return null!;
        }

        /*
         * Copies the contents of this file to a new file at a new location.
         * 
         * newFilename is the filename of the new file
         * newDirectoryPath is the location of the new file
         * 
         * Throws an ArgumentException if the filepath formed by the passed in strings is invalid
         * Throws an InvalidOperationException if this file does not exist on the file system
         */
        public IFileManager Copy(string newFilename, string newDirectoryPath)
        {
            // TODO write copy logic

            return null!;
        }

        /*
         * Moves this file to a new location
         * 
         * newDirectoryPath is the new location of this file
         * 
         * Throws an ArgumentException if newDirectoryPath is an invalid directory path
         * Throws an InvalidOperationException if this file does not exist on the file system
         */
        public void Move(string newDirectoryPath)
        {
            // TODO write move logic
        }

        /*
         * Deletes this file from the file system
         * 
         * Throws an InvalidOperationException if this file is open
         */
        public void Delete()
        {
            if (IsOpen())
                throw new InvalidOperationException($"{Filepath} cannot be deleted if it is currently open");

            File.Delete(Filepath);
        }
    }
}
