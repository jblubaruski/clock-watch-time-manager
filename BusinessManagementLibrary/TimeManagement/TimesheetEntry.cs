using BusinessManagementLibrary.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.TimeManagement
{
    /*
     * Represents a single entry in a timesheet.
     */
    public class TimesheetEntry
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
                    throw new ArgumentException($"New StartTime must be less than current EndTime ({_endTime})");
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
                    throw new ArgumentException($"New EndTime must be greater than current StartTime ({_startTime})");
                _endTime = value;
            }
        }

        /*
         * Constructs a new timesheet entry from the passed in data.
         * 
         * entryType is this entry's EntryType
         * --It can either be EntryType.Break or EntryType.Task
         * entryID is the identifying number for this entry
         * --It cannot be a negative value
         * startTime is this entry's starting time and date
         * --It cannot be greater than or equal to endTime if both startTime and endTime are not null
         * endTime is this entry's end time and date
         * --It cannot be less than or equal to startTime if both startTime and endTime are not null
         * description is this entry's description
         * 
         * Throws an ArgumentException if entryType is neither an EntryType.Break or EntryType.Task
         * Throws an ArgumentException if entryID is a negative integer
         * Throws an ArgumentException if both startTime and endTime arent null and startTime is greater than or equal to endTime
         */
        public TimesheetEntry(EntryType entryType, int entryID, DateTime? startTime, DateTime? endTime, string? description)
        {
            if (entryType != EntryType.Break && entryType != EntryType.Task)
                throw new ArgumentException($"entryType can only be either EntryType.Break or EntryType.Task (was {entryType})");
            if (entryID < 0)
                throw new ArgumentException($"entryID cannot be a negative value (was {entryID})");
            if (startTime != null && endTime != null)
            {
                if (startTime >= endTime)
                    throw new ArgumentException($"startTime ({startTime}) must be less than endTime ({endTime})");
            }
            
            EntryType = entryType;
            EntryID = entryID;
            _startTime = startTime;
            _endTime = endTime;
            Description = description;
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
