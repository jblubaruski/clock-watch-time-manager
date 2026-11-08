using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace FileUtility
{
    /*
     * Encapsulates a filepath representing the location of a text file while carrying out operations on and managing the file's state.
     */
    public class FileManager : IFileManager, IEquatable<FileManager>
    {
        protected string Filepath { get; set; }
        protected string BaseFilename { get; set; }
        protected string Extension { get; set; }
        protected string DirectoryPath { get; set; }

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

            Filepath = Path.Combine(directoryPath, baseFilename + extension);
            BaseFilename = baseFilename;
            Extension = extension;
            DirectoryPath = directoryPath;

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            using FileStream fs = File.Create(Filepath);
        }

        /*
         * Returns true if this file exists on the file system
         */
        public bool FileExists() => File.Exists(Filepath);

        /*
         * Creates the file on the file system if it doesnt already exist.
         * 
         * Does nothing if the file already exists
         */
        public void CreateFile()
        {
            if (!FileExists())
            {
                using FileStream fs = File.Create(Filepath);
            }
        }

        /*
         * Renames the file to a new filename while giving the new filename the same extension as the old filename
         * 
         * newFilename is the base name of the new filename
         * 
         * Throws an ArgumentException if the newFilename is an invalid base filename
         * Throws an ArgumentException if a file with the same name as the new name (case-insensitive) already exists in the current directory
         */
        public void Rename(string newFilename)
        {
            // TODO write rename logic
            if (!FileValidator.BaseFilenameIsValid(newFilename, out string errorMessage))
                throw new ArgumentException(errorMessage);
            if (FileExistsInDirectory(newFilename, Extension, DirectoryPath))
                throw new ArgumentException($"newFilename ({newFilename}) cannot match another filename in {DirectoryPath}");

            string newFilepath = Path.Combine(DirectoryPath, newFilename + Extension);

            if (FileExists())
                File.Move(Filepath, newFilepath);

            Filepath = newFilepath;
        }

        /*
         * Copies the contents of this file to a new file in this file's directory.
         * 
         * newFilename is the filename of the new file
         * A file with the same name as newFilename is overwritten if overwrite is true
         * 
         * Throws an ArgumentException if the newFilename is an invalid base filename
         * Throws an ArgumentException if the newFilename is the same as another file in the current directory and overwrite is false
         * Throws an InvalidOperationException if this file does not exist on the file system
         */
        public IFileManager Copy(string newFilename, bool overwrite) => Copy(newFilename, DirectoryPath, overwrite);

        /*
         * Copies the contents of this file to a new file at a new location.
         * 
         * newFilename is the filename of the new file
         * newDirectoryPath is the location of the new file
         * A file with the same name as newFilename is overwritten if overwrite is true
         * 
         * Throws an ArgumentException if the filepath formed by the passed in strings is invalid
         * Throws an ArgumentException if the newFilename is the same as the original file's filename while overwrite is true and newDirectory is the same as DirectoryPath
         * Throws an ArgumentException if the newFilename is the same as another file in the new directory and overwrite is false
         * Throws an InvalidOperationException if this file does not exist on the file system
         */
        public IFileManager Copy(string newFilename, string newDirectoryPath, bool overwrite)
        {
            // TODO write copy logic
            if (!FileValidator.BaseFilenameIsValid(newFilename, out string errorMessage))
                throw new ArgumentException(errorMessage);
            if (overwrite && newDirectoryPath == DirectoryPath && newFilename == Path.GetFileNameWithoutExtension(Filepath))
                throw new ArgumentException("the original file cannot be overwritten by a copy operation");
            if (!overwrite && FileExistsInDirectory(newFilename, Extension, newDirectoryPath))
                throw new ArgumentException($"newFilename ({newFilename}) cannot match another filename in {newDirectoryPath}");
            if (!FileExists())
                throw new InvalidOperationException("A file can only be moved if it exists in the file system");

            FileManager newManager = new(newFilename, Extension, newDirectoryPath);
            File.Copy(Filepath, Path.Combine(newDirectoryPath, newFilename + Extension), true);

            return newManager;
        }

        /*
         * Moves this file to a new location
         * 
         * newDirectoryPath is the new location of this file
         * A file with the same name as newFilename is overwritten if overwrite is true
         * 
         * Throws an ArgumentException if newDirectoryPath is an invalid directory path
         * Throws an ArgumentException if the newFilename is the same as another file in the new directory and overwrite is false
         * Throws an InvalidOperationException if this file does not exist on the file system
         */
        public void Move(string newDirectoryPath, bool overwrite)
        {
            if (!FileValidator.DirectoryPathIsValid(newDirectoryPath, out string errorMessage))
                throw new ArgumentException(errorMessage);
            if (!overwrite && FileExistsInDirectory(Path.GetFileNameWithoutExtension(Filepath), Extension, newDirectoryPath))
                throw new ArgumentException($"newFilename ({Path.GetFileName(Filepath)}) cannot match another filename in {newDirectoryPath}");
            if (!FileExists())
                throw new InvalidOperationException("A file can only be moved if it exists in the file system");

            File.Move(Filepath, Path.Combine(newDirectoryPath, Path.GetFileName(Filepath)), overwrite);
        }

        /*
         * Deletes this file from the file system
         * 
         * Does nothing if the file doesnt exist already
         */
        public void Delete()
        {
            if (FileExists())
                File.Delete(Filepath);
        }

        /*
         * Returns true if this file manager's filepath is the same as the passed in file manager's filepath.
         */
        public bool Equals(FileManager? other)
        {
            if (other != null && Filepath == other.Filepath)
                return true;

            return false;
        }

        /*
         * Returns true if a file with the passed in filename exists in the passed in directory
         */
        private static bool FileExistsInDirectory(string filename, string extension, string directoryPath)
        {
            foreach(string otherFilename in Directory.GetFiles(directoryPath))
            {
                if (otherFilename == Path.Combine(directoryPath, filename + extension))
                    return true;
            }

            return false;
        }
    }
}
