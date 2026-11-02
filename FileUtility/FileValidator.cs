using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Text;

namespace FileUtility
{
    /*
     * Contains methods that validates filepaths and their components.
     */
    public static class FileValidator
    {
        public const int MAX_FILENAME_LENGTH = 255;
        public const int MAX_FILEPATH_LENGTH = 32766;
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
        public static readonly System.Buffers.SearchValues<char> s_InvalidCharactersAnywhere = System.Buffers.SearchValues.Create(INVALID_CHARACTERS_ANYWHERE);
        public static readonly System.Buffers.SearchValues<char> s_InvalidDirectoryCharactersAnywhere = System.Buffers.SearchValues.Create(INVALID_DIRECTORY_CHARACTERS_ANYWHERE);
        public static readonly System.Buffers.SearchValues<char> s_InvalidCharactersAtStart = System.Buffers.SearchValues.Create(INVALID_CHARACTERS_ATSTART);
        public static readonly System.Buffers.SearchValues<string> s_ReservedFilenames = System.Buffers.SearchValues.Create(ReservedFileNames, StringComparison.OrdinalIgnoreCase);

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
            if (!FilenameIsValid(baseFilename, extension, out errorMessage))
            {
                return false;
            }
            else if (!DirectoryPathIsValid(directoryPath, out errorMessage))
            {
                return false;
            }
            else if (baseFilename.Length + extension.Length + directoryPath.Length > MAX_FILEPATH_LENGTH)
            {
                errorMessage = $"The combined length of baseFilename, extension and directoryPath cannot be greater than {MAX_FILEPATH_LENGTH}";
                return false;
            }

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
            if (!BaseFilenameIsValid(baseFilename, out errorMessage))
            {
                return false;
            }
            else if (!ExtensionIsValid(extension, out errorMessage))
            {
                return false;
            }
            else if (baseFilename.Length + extension.Length > MAX_FILENAME_LENGTH)
            {
                errorMessage = $"The combined length of baseFilename and extension cannot be greater than {MAX_FILENAME_LENGTH}";
                return false;
            }

            errorMessage = "";
            return true;
        }

        /*
         * Returns true if the passed in base filename is valid.
         * 
         * It cannot not be a null pointer
         * It cannot not be an empty string
         * It cannot not be entirely whitespace
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
         * It cannot not be a null pointer
         * It cannot not be an empty string
         * It cannot not be entirely whitespace
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
         * It cannot not be a null pointer
         * It cannot not be an empty string
         * It cannot not be entirely whitespace
         * It can start with a root (<drive letter><volume separator><directory separator> or <directory separator>)
         * Each directory name contained in the path must be a valid directory name
         */
        public static bool DirectoryPathIsValid(string directoryPath, out string errorMessage)
        {
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

            string[] directoryNames = directoryPath.Split(Path.DirectorySeparatorChar);
            int directoryNamesStartIndex = 0;

            if (directoryNames[0].Length == 0)
            {
                directoryNamesStartIndex = 1;
            }
            else if (directoryNames[0].Contains(Path.VolumeSeparatorChar))
            {
                directoryNamesStartIndex = 1;

                if (!DriveIndicatorIsValid(directoryNames[0], out errorMessage))
                    return false;
            }
            
            for (int i = directoryNamesStartIndex; i < directoryNames.Length; i++)
            {
                if (directoryNames[i].Length == 0 && i != directoryNames.Length - 1)
                {
                    if (!DirectoryNameIsValid(directoryNames[i], out errorMessage))
                        return false;
                }
                else if (directoryNames[i].Length != 0)
                {
                    if (!DirectoryNameIsValid(directoryNames[i], out errorMessage))
                        return false;
                }
            }

            errorMessage = "";
            return true;
        }

        /*
         * Returns true if the passed in drive indicator is valid
         * 
         * It cannot not be a null pointer
         * It must start with a single ascii letter character
         * The next character must be a volume separator
         * There cannot be any more characters after the volume separator
         */
        public static bool DriveIndicatorIsValid(string driveIndicator, out string errorMessage)
        {
            if (driveIndicator == null)
            {
                errorMessage = "directoryPath cannot be null";
                return false;
            }
            else if (driveIndicator.Length != 2)
            {
                errorMessage = "driveIndicator must be exactly two characters in length";
                return false;
            }
            else if (!char.IsAsciiLetter(driveIndicator.ToCharArray()[0]))
            {
                errorMessage = "driveIndicator must start with an ascii letter";
                return false;
            }
            else if (driveIndicator.ToCharArray()[1] != Path.VolumeSeparatorChar)
            {
                errorMessage = "driveIndicator must end with a volume separator";
                return false;
            }

            errorMessage = "";
            return true;
        }

        /*
         * Returns true if the passed in directory name is valid
         * 
         * It cannot not be a null pointer
         * It cannot not be an empty string
         * It cannot not be entirely whitespace
         * It should not contain any of the following characters (except for the root):
         * '?', ' ', '%', '*', ':', '|', '"', '<', '>', ',', ';', '='
         * If the directory separator character is '\', a it cannot contain '/' and vice-versa
         * It cannot contain be more than two periods in a row
         * It cannot start with the characters: '-', ' ', '_', '.' (with the exception of names that are "." and "..")
         * It cannot end with a period (with the exception of names that are "." and "..")
         */
        public static bool DirectoryNameIsValid(string directoryName, out string errorMessage)
        {
            if (directoryName == null)
            {
                errorMessage = "directoryName cannot be null";
                return false;
            }
            else if (directoryName.Length == 0)
            {
                errorMessage = "directoryName cannot be an empty string";
                return false;
            }
            else if (directoryName.IsWhiteSpace())
            {
                errorMessage = "directoryName cannot be entirely whitespace";
                return false;
            }
            else if (directoryName.ContainsAny(s_InvalidDirectoryCharactersAnywhere))
            {
                errorMessage = $"directoryName cannot contain the characters: {INVALID_DIRECTORY_CHARACTERS_ANYWHERE}";
                return false;
            }
            else if (directoryName.Contains(Path.AltDirectorySeparatorChar))
            {
                errorMessage = "directoryName cannot contain the alt directory separator";
                return false;
            }
            else if (directoryName != "." && directoryName != ".." && StringContainsOnlyCharacter(directoryName, '.') && directoryName.Count<char>('.') > 2)
            {
                errorMessage = "There cannot be more than two periods in a directory name that is not . or .. and is only composed of periods";
                return false;
            }
            else if (directoryName != "." && directoryName != ".." && directoryName.Substring(0, 1).ContainsAny(s_InvalidCharactersAtStart))
            {
                errorMessage = $"directory names cannot start with the characters: {INVALID_CHARACTERS_ATSTART}";
                return false;
            }
            else if (directoryName != "." && directoryName != ".." && directoryName[^1..^0] == ".")
            {
                errorMessage = "A directory name that is not . or .. cannot end with a period";
                return false;
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
