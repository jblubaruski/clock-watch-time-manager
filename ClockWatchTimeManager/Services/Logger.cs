using FileUtility.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWatchTimeManager.Services
{
    /*
     * Writes logs to the program's log file
     */
    internal class Logger : TextFileManager
    {
        /*
         * Constructs a logger and then removes all content from the logs file if it exists
         */
        public Logger(string baseFilename, string directoryPath) : base(baseFilename, directoryPath)
        {
            if (FileExists())
                Delete();
            CreateFile();
        }

        /*
         * Logs an entry in the log file
         * 
         * Entries are formatted as:
         * ************************************************************
         * {entry time (HH:mm:ss zzz)} on {entry date (MM/dd/yyyy)}
         * ************************************************************
         * {entry data}
         * 
         */
        public void LogEntry(params string[] entryLines)
        {
            string entry = "".PadLeft(60, '*') + "\n";

            DateTime currentTime = DateTime.Now;
            entry += $"{currentTime:HH:mm:ss zzz on MM/dd/yyyy}\n";
            entry += "".PadLeft(60, '*') + "\n";

            foreach (string line in entryLines)
            {
                entry += line + "\n";
            }

            AppendToFile(entry);
        }
    }
}
