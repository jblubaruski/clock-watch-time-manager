using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Clock_Watch_Time_Manager.Services
{
    /*
     * Helper class that contains methods that assist with file IO operations.
     */
    internal static class FileHelper
    {
        /*
         * Returns true if the filepath formed by the passed in directoryPath, baseFileName and extension is a valid filepath on windows.
         * 
         * Filenames are assumed to be case insensitive.
         * 
         * The passed in directory path cannot start with a period.
         * It cannot end with a period.
         * 
         * The passed in extension must start with a period.
         * It must have a character that is not a disallowed special character.
         * It cannot end in a period.
         * 
         * The base filename cannot start with the characters: '-', ' ', '_', '.'
         * the base filename cannot be any of the following strings:
         * CON, CONIN$, CONOUT$, PRN, AUX, CLOCK$, NUL, COM0, COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9[9],
         * LPT0, LPT1, LPT2, LPT3, LPT4, LPT5, LPT6, LPT7, LPT8, LPT9[9], LST, KEYBD$, SCREEN$, $IDLE$, CONFIG$,
         * $Mft, $MftMirr, $LogFile, $Volume, $AttrDef, $Bitmap, $Boot, $BadClus, $Secure, $Upcase, $Extend, $Quota, $ObjId, $Reparse
         * 
         * The filename (baseFilename + extension) cannot be more than 255 characters long.
         * It should not contain any of the following characters
         * '?', ' ', '/', '\', '%', '*', ':', '|', '"', '<', '>', ',', ';', '='
         * 
         * The filepath cannot be more than 32,766 characters long.
         * It should not contain any of the following characters
         * '?', ' ', '%', '*', ':', '|', '"', '<', '>', ',', ';', '='
         */
        public static bool FilepathIsValid(string baseFilename, string extension, string directoryPath, out string errorMessage)
        {
            // TODO Write filepath validation logic
            errorMessage = "";
            return true;
        }

        public static bool FilenameIsValid(string baseFilename, string extension, out string errorMessage)
        {
            // TODO Write filename validation logic

            errorMessage = "";
            return true;
        }

        public static bool BaseFilenameIsValid(string baseFilename, out string errorMessage)
        {
            // TODO Write filename validation logic

            errorMessage = "";
            return true;
        }

        public static bool ExtensionIsValid(string extension, out string errorMessage)
        {
            // TODO Write extension validation logic

            errorMessage = "";
            return true;
        }
        
        public static bool DirectoryPathIsValid(string directoryPath, out string errorMessage)
        {
            // TODO Write directoryPath validation logic

            errorMessage = "";
            return true;
        }
    }
}
