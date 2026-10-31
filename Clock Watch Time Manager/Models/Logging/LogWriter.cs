using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ClockWatchTimeManager.Models.Logging;

/*
 * Writes messages to a log file located at %MyDocuments%\PlaypenLogs\<ProgramName>
 */
internal class LogWriter
{
    // first component of the log filename
    private const string LOG_FILENAME_LEFTSIDE = "log_";
    // extension of the log file
    private const string LOG_FILENAME_EXTENSION = ".txt";
    // path to the directory containing the log files for programs licensed by playpen software
    public static readonly string CompanyLogsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PlaypenLogs");

    // the directory the logs are written to
    public string ProgramLogsDirectory { get; }
    // the filename to write log messages to
    public string LogFilename { get; }

    /*
     * Generates a file with the name log_<programname>.txt in the directory %MyDocuments%\PlaypenLogs\<ProgramName>.
     * 
     * The directory <ProgramName> should be the passed in string converted to TitleCase and should have all whitespaces removed.
     * <programname> should be the passed in string converted to lowercase and should have all invalid characters and whitespaces removed.
     * 
     * Spaces within the passed in string will be treated as a separator between words in the program's name.
     * The passed in string should not be an empty string or contain all whitespace.
     * It should form a valid filename when formed into the full filename.
     * 
     * Passing in a null pointer throws an ArgumentNullException.
     * Passing in an invalid programName throws an ArgumentException.
     * Passing in a programName that forms an invalid filename throws an ArgumentException
     */
    public LogWriter(string programName)
    {
        ArgumentNullException.ThrowIfNull(programName);
        if (!ProgramNameIsValid(programName, out string errorMessage))
            throw new ArgumentException(errorMessage);

        string baseFilename = LOG_FILENAME_LEFTSIDE + programName.ToLower().Replace(" ", "");
        
        // TODO finish filepath validation

        //if (!FileHelper.FilenameIsValid(baseFilename, LOG_FILENAME_EXTENSION, out errorMessage))
            //throw new ArgumentException(errorMessage);

        ProgramLogsDirectory = Path.Combine
                                (
                                    CompanyLogsDirectory,
                                    CultureInfo.CurrentCulture.TextInfo.ToTitleCase(programName).Replace(" ", "")
                                );
        LogFilename = baseFilename + LOG_FILENAME_EXTENSION;

        CreateFileInDirectoryWithOverwrite(ProgramLogsDirectory, LogFilename);
    }

    /*
     * Returns true if the passed in programName is valid.
     * Sets the passed in errorMessage to a string describing the false condition encountered.
     * errorMessage is set to an empty string if the programName is valid.
     * 
     * Assumes the name has already been tested for a null pointer.
     * Returns false if the passed in name is an empty string.
     * Returns false if the passed in name is entirely whilespace.
     */
    private static bool ProgramNameIsValid(string programName, out string errorMessage)
    {
        if (programName == "")
        {
            errorMessage = "programName cannot be an empty string";
            return false;
        }
        else if (programName.IsWhiteSpace())
        {
            errorMessage = "programName cannot be entirely whitespace";
            return false;
        }

        errorMessage = "";
        return true;
    }
    
    /*
     * Creates a file in the specified directory and closes the file.
     * 
     * Creates a new directory if the directory doesn't already exist.
     * Overwrites the created file if it already exists.
     */
    private static void CreateFileInDirectoryWithOverwrite(string directoryPath, string fileName)
    {
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
        using (File.Create(Path.Combine(directoryPath, fileName))) { }
    }

    public void WriteLogFileNameToDebugConsole()
    {
        Debug.WriteLine(LogFilename);
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

