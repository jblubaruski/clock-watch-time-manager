using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Clock_Watch_Time_Manager.Models.Logging;

/*
 * Writes messages to a log file located at %MyDocuments%\PlaypenLogs\<ProgramName>
 */
internal class LogWriter
{
    public const int MAX_PROGRAM_NAME_LENGTH = 247;
    public static readonly string CompanyLogsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PlaypenLogs");
    private static readonly string[] ReservedWindowsFileNames = ["CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM¹", "COM²", "COM³", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9", "LPT¹", "LPT²", "LPT³"];
    private static readonly char[] InvalidCharactersAtStart = [' ', '-', '_', '.'];
    private static readonly char[] InvalidCharactersAnywhere = ['#', '%', '&', '{', '}', '\\', '<', '>', '*', '?', '/', '$', '!', '\'', '"', ':', '@', '+', '`', '|', '='];

    // the directory the logs are written to
    public string ProgramLogsDirectory { get; }
    // the filename to write log messages to
    public string LogFileName { get; }

    /*
     * Generates a file with the name log_<programname>.txt in the directory %MyDocuments%\PlaypenLogs\<ProgramName>.
     * Spaces within programName will be treated as a separator between words in the program's name.
     * 
     * if the argument test is true:
     * Generates a text file with the name logwriter-test-log.txt in the directory %MyDocuments%\PlaypenLogs\Testing
     * 
     * The passed in string should not be more than 247 characters.
     * It cannot be an empty string or contain all whitespace.
     * It cant be any of the following reserved windows file names:
     * CON, PRN, AUX, NUL, COM1, COM2, COM3, COM4, COM5, COM6, COM7, COM8, COM9, COM¹, COM², COM³, LPT1, LPT2, LPT3, LPT4, LPT5, LPT6, LPT7, LPT8, LPT9, LPT¹, LPT², LPT³
     * It should not have any of the following characters:
     * at start of the string: ' ', '-', '_', '.'
     * anywhere in the string: '#', '%', '&', '{', '}', '\', '<', '>', '*', '?', '/', '$', '!', ''', '"', ':', '@', '+', '`', '|', '='
     * 
     * The directory <ProgramName> should be the passed in string converted to PascalCase and should have all whitespaces removed.
     * <programname> should be the passed in string converted to lowercase and should have all invalid characters and whitespaces removed.
     * 
     * if the argument test is false:
     * Passing in a null pointer throws an ArgumentNullException
     * Passing in an empty string throws an ArgumentException
     * Passing in a string with all whitespace characters throws an ArgumentException
     * Passing in a string with more than 247 characters throws an ArgumentException
     * Passing in a string matching any of the reserved windows file names (case-insensitive) throws an ArgumentException
     * Passing in a string starting with a whitespace character, hyphen, underscore or period throws an ArgumentException
     * Passing in a string containing any of the invalid file name characters throws an ArgumentException
     */
    public LogWriter(string programName, bool test = false)
    {
        if (test)
        {
            ProgramLogsDirectory = Path.Combine(CompanyLogsDirectory, "Testing");
            LogFileName = "logwriter-test-log.txt";
        }    
        else
        {
            ArgumentNullException.ThrowIfNull(programName);
            if (programName == "")
                throw new ArgumentException("programName cannot be an empty string");
            else if (programName.IsWhiteSpace())
                throw new ArgumentException("programName cannot be entirely whitespace");
            else if (programName.Length > MAX_PROGRAM_NAME_LENGTH)
                throw new ArgumentException($"programName cannot have more that {MAX_PROGRAM_NAME_LENGTH} characters");
            foreach (string reservedName in ReservedWindowsFileNames)
            {
                if (programName.ToUpper() == reservedName)
                    throw new ArgumentException($"programName cannot be {reservedName}");
            }
            foreach (char c in InvalidCharactersAtStart)
            {
                if (programName[0] == c)
                    throw new ArgumentException($"programName cannot start with the character {c}");
            }
            foreach (char c in InvalidCharactersAnywhere)
            {
                if (programName.Contains(c))
                    throw new ArgumentException($"programName cannot be the character {c}");
            }

            string[] programNameWords = programName.Split(' ');

            // convert programName to PascalCase
            string programNamePascal = "";
            foreach (string word in programNameWords)
            {
                if (word.Length == 1)
                    programNamePascal += word.ToUpper();
                else
                    programNamePascal += word[..1].ToUpper() + word[1..].ToLower();
            }
            ProgramLogsDirectory = Path.Combine(CompanyLogsDirectory, programNamePascal);

            string programNameLowercase = string.Concat(programNameWords).ToLower();
            LogFileName = $"log_{programNameLowercase}.txt";
        }

        // create the logs file (or overwrite it if it already exists)
        if (!Directory.Exists(ProgramLogsDirectory))
            Directory.CreateDirectory(ProgramLogsDirectory);
        using (File.Create(Path.Combine(ProgramLogsDirectory, LogFileName))) { }
    }

    public void WriteLogFileNameToDebugConsole()
    {
        Debug.WriteLine(LogFileName);
    }

    /*
     * Appends a passed in string to the logwriter test log file without overwriting other messages.
     * 
     * Passing in a null string throws an ArgumentNullException
     */
    public void LogMessage(string message)
    {

    }
}

