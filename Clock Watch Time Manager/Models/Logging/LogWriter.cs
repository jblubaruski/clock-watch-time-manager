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
    private static readonly string LogDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "PlaypenLogs");
    private static readonly char[] IllegalCharactersAtStart = ['-', '_', '.'];
    private static readonly char[] IllegalCharacters = ['#', '%', '&', '{', '}', '\\', '<', '>', '*', '?', '/', ' ', '$', '!', '\'', '"', ':', '@', '+', '`', '|', '='];

    //the filename to write log messages to
    public string LogFileName { get; private set; }

    /*
     * Creates the log directory for the program if it doesnt exist already.
     * Creates a text log file with the name log_<programname>_<#logFiles>.txt
     * #logFiles represents the number of log files with the same program name in the log directory 
     */
    public LogWriter(string programName)
    {
        if (!Directory.Exists(LogDirectory))
            Directory.CreateDirectory(LogDirectory);

        DateTime currentDateTime = DateTime.Now;

        string reformattedProgramName = ReformatProgramName(programName);

        int logFileCount = CountLogFiles(reformattedProgramName);

        LogFileName = $"{reformattedProgramName}_({logFileCount}).txt";
    }

    public void WriteLogFileNameToDebugConsole()
    {
        Debug.WriteLine(LogFileName);
    }

    /*
     * Reformats the passed in program name as a lowercase and file system legal name.
     * All illegal characters are removed from the filename.
     * Illegal characters at start of name: ' ', '-', '_', '.'
     * Illegal characters anywhere in the name:
     * '#', '%', '&', '{', '}', '\', '<', '>', '*', '?', '/', ' '
     * '$', '!', ''', '"', ':', '@', '+', '`', '|', '='
     * 
     * Throws an ArgumentException if programName is an empty string or doesnt contain any legal characters.
     */
    private static string ReformatProgramName(string programName)
    {
        if (programName.Length == 0)
            throw new ArgumentException("programName cannot be an empty string");

        string reformattedProgramName = programName.ToLower().Trim();

        // ensure the first character doesnt start with a hyphen, underscore or period
        bool firstCharacterIsLegal;
        do
        {
            // remove the first character if it's a hyphen, underscore or period
            char c = programName[0];
            foreach (char illegalCharacter in IllegalCharactersAtStart)
            {
                if (c == illegalCharacter)
                {
                    programName = programName.Remove(0, 1);
                    break;
                }
            }

            // make sure the first character isnt still a hyphen, underscore or period
            c = programName[0];
            firstCharacterIsLegal = true;
            foreach (char illegalCharacter in IllegalCharactersAtStart)
            {
                if (c == illegalCharacter)
                {
                    firstCharacterIsLegal = false;
                    break;
                }
            }
        } while (!firstCharacterIsLegal);
        

        // ensure the rest of the characters are legal characters
        for (int i = 0; i < programName.Length; i++)
        {
            char c = programName[i];
            foreach (char illegalCharacter in IllegalCharacters)
            {
                if (c == illegalCharacter)
                {
                    programName = programName.Remove(i, 1);
                    i -= 1;
                }
            }
        }

        if (programName.Length == 0)
            throw new ArgumentException("programName must contain at least one legal character.");
        
        return reformattedProgramName;
    }

    /*
     * Counts the number of log files with the same passed in program name in the logs directory.
     */
    private static int CountLogFiles(string programName)
    {
        int logFileCount = 0;

        return logFileCount;
    }
}

