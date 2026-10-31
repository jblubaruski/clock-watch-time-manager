using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Intrinsics.X86;
using System.Text;

namespace Clock_Watch_Time_Manager.Services
{
    /*
     * Helper class that contains methods that assist with file IO operations.
     */
    internal static class FileHelper
    {
        private const string INVALID_CHARACTERS_ANYWHERE = "? /\\%*:|\"<>,;=";
        private const string INVALID_DIRECTORY_CHARACTERS_ANYWHERE = "? %*:|\"<>,;=";
        private const string INVALID_CHARACTERS_ATSTART = "- _.";
        private static readonly string[] ReservedFileNames = 
            [
                "CON", "CONIN$", "CONOUT$", "PRN", "AUX", "CLOCK$", "NUL",
                "COM0", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9[9]",
                "LPT0", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9[9]",
                "LST", "KEYBD$", "SCREEN$", "$IDLE$", "CONFIG$", "$Mft", "$MftMirr", "$LogFile", "$Volume", 
                "$AttrDef", "$Bitmap", "$Boot", "$BadClus", "$Secure", "$Upcase", "$Extend", "$Quota", "$ObjId", "$Reparse"
            ];
        private static readonly System.Buffers.SearchValues<char> s_InvalidCharactersAnywhere = System.Buffers.SearchValues.Create(INVALID_CHARACTERS_ANYWHERE);
        private static readonly System.Buffers.SearchValues<char> s_InvalidDirectoryCharactersAnywhere = System.Buffers.SearchValues.Create(INVALID_DIRECTORY_CHARACTERS_ANYWHERE);
        private static readonly System.Buffers.SearchValues<char> s_InvalidCharactersAtStart = System.Buffers.SearchValues.Create(INVALID_CHARACTERS_ATSTART);
        private static readonly System.Buffers.SearchValues<string> s_ReservedFilenames = System.Buffers.SearchValues.Create(ReservedFileNames, StringComparison.OrdinalIgnoreCase);

        /*
         * Returns true if the filepath formed by the passed in base filename, extension, and directory path is a valid filepath.
         * 
         * The filename formed by the base filename and extension must be valid.
         * The directory path must be valid
         * The filepath cannot be more than 32,766 characters long.
         */
        public static bool FilepathIsValid(string baseFilename, string extension, string directoryPath, out string errorMessage)
        {
            // TODO Write filepath validation logic

            errorMessage = "";
            return true;
        }

        /*
         * Returns true if the filename formed by the base filename and extension is valid
         * 
         * The base filename and extension must be valid.
         * The filename cannot be more than 255 characters long.
         */
        public static bool FilenameIsValid(string baseFilename, string extension, out string errorMessage)
        {
            // TODO Write filename validation logic

            errorMessage = "";
            return true;
        }

        /*
         * Returns true if the passed in base filename is valid.
         * 
         * It should not be a null pointer
         * It should not be an empty string
         * It should not be entirely whitespace
         * It cannot start with the characters: '-', ' ', '_', '.'
         * It cannot contain any of the following characters:
         * '?', ' ', '/', '\', '%', '*', ':', '|', '"', '<', '>', ',', ';', '='
         * It cannot be any of the following strings (case-insensitive):
         * CON, CONIN$, CONOUT$, PRN, AUX, CLOCK$, NUL, COM0, COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9[9],
         * LPT0, LPT1, LPT2, LPT3, LPT4, LPT5, LPT6, LPT7, LPT8, LPT9[9], LST, KEYBD$, SCREEN$, $IDLE$, CONFIG$,
         * $Mft, $MftMirr, $LogFile, $Volume, $AttrDef, $Bitmap, $Boot, $BadClus, $Secure, $Upcase, $Extend, $Quota, $ObjId, $Reparse
         */
        public static bool BaseFilenameIsValid(string baseFilename, out string errorMessage)
        {
            if (baseFilename == null)
            {
                errorMessage = "baseFilename cannot be null";
                return false;
            }
            else if (baseFilename.Length == 0)
            {
                errorMessage = "baseFilename cannot be an empty string";
                return false;
            }
            else if (baseFilename.IsWhiteSpace())
            {
                errorMessage = "baseFilename cannot be entirely whitespace";
                return false;
            }
            else if (baseFilename.Substring(0, 1).ContainsAny(s_InvalidCharactersAtStart))
            {
                errorMessage = $"baseFilename cannot start with the characters: {INVALID_CHARACTERS_ATSTART}";
                return false;
            }
            else if (baseFilename.ContainsAny(s_InvalidCharactersAnywhere))
            {
                errorMessage = $"baseFilename cannot contain any of the characters: {INVALID_CHARACTERS_ANYWHERE}";
                return false;
            }
            else if (s_ReservedFilenames.Contains(baseFilename))
            {
                errorMessage = "baseFilename cannot be a reserved filename";
                return false;
            }

            errorMessage = "";
            return true;
        }

        /*
         * Returns true if the passed in file extension is valid.
         * 
         * It should not be a null pointer
         * It should not be an empty string
         * It should not be entirely whitespace
         * It must start with a period
         * It should only have one period
         * It cannot contain any of the following characters:
         * '?', ' ', '/', '\', '%', '*', ':', '|', '"', '<', '>', ',', ';', '='
         * It must have at least one valid character following the period
         */
        public static bool ExtensionIsValid(string extension, out string errorMessage)
        {
            if (extension == null)
            {
                errorMessage = "extension cannot be null";
                return false;
            }
            else if (extension.Length == 0)
            {
                errorMessage = "extension cannot be an empty string";
                return false;
            }
            else if (extension.IsWhiteSpace())
            {
                errorMessage = "extension cannot be entirely whitespace";
                return false;
            }
            else if (extension[..1] != ".")
            {
                errorMessage = "extension must start with a period";
                return false;
            }
            else if (extension.Count<char>('.') != 1)
            {
                errorMessage = "extension cannot have more than one period";
                return false;
            }
            else if (extension.ContainsAny(s_InvalidCharactersAnywhere))
            {
                errorMessage = $"extension cannot contain the characters: {INVALID_CHARACTERS_ANYWHERE}";
                return false;
            }
            else if (extension.Length < 2)
            {
                errorMessage = "extension must have a character following the period";
                return false;
            }

            errorMessage = "";
            return true;
        }

        /*
         * Returns true if the passed in directory path is valid.
         * 
         * It should not be a null pointer
         * It should not be an empty string
         * It should not be entirely whitespace
         * Directory names should not contain any of the following characters (except for the root):
         * '?', ' ', '%', '*', ':', '|', '"', '<', '>', ',', ';', '='
         * If the directory separator character is '\', a directory name cannot contain '/' and vice-versa
         * It cannot contain a directory name with more than two periods in a row
         * It cannot contain a directory name that starts with the characters: '-', ' ', '_', '.' (with the exception of names that are "." and "..")
         * It cannot contian a directory name that ends with a period (with the exception of names that are "." and "..")
         * It can start with a root (<drive letter><volume separator><directory separator> or <directory separator>)
         * If it stars with a root specifying a drive, the drive letter must be in the range of a-z or A-Z
         */
        public static bool DirectoryPathIsValid(string directoryPath, out string errorMessage)
        {
            // TODO Write directoryPath validation logic
            // run validation over whole directory path
            if (directoryPath == null)
            {
                errorMessage = "directoryPath cannot be null";
                return false;
            }
            else if (directoryPath.Length == 0)
            {
                errorMessage = "directoryPath cannot be an empty string";
                return false;
            }
            else if (directoryPath.IsWhiteSpace())
            {
                errorMessage = "directoryPath cannot be entirely whitespace";
                return false;
            }

            // run validation over individual directory names
            string[] directoryNames = directoryPath.Split(Path.DirectorySeparatorChar);
            
            for (int i = 0; i < directoryNames.Length; i++)
            {
                if (i == 0 && directoryNames[i].Contains(':'))
                {
                    if (!char.IsAsciiLetter(directoryNames[i].ToCharArray()[0]))
                    {
                        errorMessage = "if the path includes a drive indicator, that indicator must reference a drive with an alphabetical name";
                        return false;
                    }
                }
                else
                {
                    if (directoryNames[i].ContainsAny(s_InvalidDirectoryCharactersAnywhere))
                    {
                        errorMessage = $"directory names cannot be contain the characters: {INVALID_DIRECTORY_CHARACTERS_ANYWHERE}";
                        return false;
                    }
                    else if (directoryNames[i].Contains(Path.AltDirectorySeparatorChar))
                    {
                        errorMessage = "directory names cannot contain the alt directory separator";
                        return false;
                    }
                    else if (directoryNames[i] != "." && directoryNames[i] != ".." && StringContainsOnlyCharacter(directoryNames[i], '.') && directoryNames[i].Count<char>('.') > 2)
                    {
                        errorMessage = "There cannot be more than two periods in a directory name that is not . or .. and is only composed of periods";
                        return false;
                    }
                    else if (directoryNames[i] != "." && directoryNames[i] != ".." && directoryNames[i].ContainsAny(s_InvalidCharactersAtStart))
                    {
                        errorMessage = $"directory names cannot start with the characters: {INVALID_CHARACTERS_ATSTART}";
                        return false;
                    }
                    else if (directoryNames[i] != "." && directoryNames[i] != ".." && directoryNames[i][^1..^0] == ".")
                    {
                        errorMessage = "A directory name that is not . or .. cannot end with a period";
                        return false;
                    }
                }
            }

            errorMessage = "";
            return true;
        }
        
        /*
         * Returns true if the passed in string is composed of only the passed in character
         */
        private static bool StringContainsOnlyCharacter(string str, char c)
        {
            foreach (char letter in str)
            {
                if (letter != c)
                    return false;
            }

            return true;
        }
    }
}
