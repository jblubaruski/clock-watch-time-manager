using BusinessManagementLibrary.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.TimeManagement
{
    /*
     * Represents a single entry in a timesheet.
     */
    public class TimesheetEntry : IEntry
    {
        public EntryType EntryType { get; }
        public int EntryID { get; }

        public string? Description { get; set; }

        private DateTime? _startTime;
        public DateTime? StartTime 
        {
            get => _startTime;
            // Throws an ArgumentException if the passed in start time is greater than or equal to the current end time (this condition is ignored if either time is null)
            set
            {
                if (value != null && _endTime != null && value >= _endTime)
                    throw new ArgumentException($"New StartTime must be less than the current EndTime ({_endTime})");
                _startTime = value;
            }
        }

        private DateTime? _endTime;
        public DateTime? EndTime
        {
            get => _endTime;
            // Throws an ArgumentException if the passed in end time is less than or equal to the current start time (this condition is ignored if either time is null)
            set
            {
                if (value != null && _startTime != null && value <= _startTime)
                    throw new ArgumentException($"New EndTime must be greater than the current StartTime ({_startTime})");
                _endTime = value;
            }
        }

        /*
         * Constructs a new timesheet entry from the passed in data.
         * 
         * entryID is the identifying number for this entry
         * --It cannot be a negative value
         * entryData contains the data contained within this timesheet entry
         * 
         * Throws an ArgumentException if entryID is a negative integer
         */
        public TimesheetEntry(int entryID, TimesheetEntryData entryData)
        {
            if (entryID < 0)
                throw new ArgumentException($"entryID cannot be a negative value (was {entryID})");
            
            EntryType   = entryData.IsBreak ? EntryType.Break : EntryType.Task;
            EntryID     = entryID;
            _startTime  = entryData.StartTime;
            _endTime    = entryData.EndTime;
            Description = entryData.Description;
        }

        /*
         * Returns true if the passed in IEntry is a TimesheetEntry with the same entryID
         */
        public bool Equals(IEntry? other)
        {
            if (other == null)
                return false;
            if (other is not TimesheetEntry)
                return false;
            if (EntryID == other.EntryID)
                return true;
            return false;
        }

        /*
         * Returns a string representation of this timesheet entry
         * 
         * The representation is formatted as:
         * {entry type} start:{20 - previous characters length # of spaces}{StartTime:MM/dd/yyyy hh:mm:ss tt}
         * {break description}
         * {entry type} end:{20 - previous characters length # of spaces}{EndTime:MM/dd/yyyy hh:mm:ss tt}
         * 
         * Each line is shown as the following if null:
         * {entry type} start:{20 - previous characters length # of spaces}--/--/---- --:--:-- --
         * ---
         * {entry type} end:{20 - previous characters length # of spaces}--/--/---- --:--:-- --
         */
        public override string ToString()
        {
            string entryType = EntryType.ToString();
            string taskString = "";

            if (StartTime != null)
                taskString += $"{entryType} start:".PadRight(20) + $"{StartTime:MM/dd/yyyy hh:mm:ss tt}\n";
            else
                taskString += $"{entryType} start:".PadRight(20) + "--/--/---- --:--:-- --\n";

            if (Description != null)
                taskString += $"{Description}\n";
            else
                taskString += "---\n";

            if (EndTime != null)
                taskString += $"{entryType} end:".PadRight(20) + $"{EndTime:MM/dd/yyyy hh:mm:ss tt}";
            else
                taskString += $"{entryType} end:".PadRight(20) + "--/--/---- --:--:-- --";

            return taskString;
        }
    }
}
