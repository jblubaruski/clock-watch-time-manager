using BusinessManagementLibrary.Data;
using BusinessManagementLibrary.TimeManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.TimeManagement
{
    /*
     * Represents a single work period.
     * Includes a clock in time, clock out time and a list of timesheet entries
     */
    public class Timecard : IEntryManager<TimesheetEntryData, TimesheetEntry>, IEntry
    {
        public EntryType EntryType { get; }
        public int EntryID { get; }

        private DateTime? _clockInTime;
        public DateTime? ClockInTime 
        { 
            get => _clockInTime;
            // Throws an ArgumentException if the passed in clock in time is greater than or equal to the current clock out time (this condition is ignored if either time is null)
            set
            {
                if (value != null && _clockOutTime != null && value >= _clockOutTime)
                    throw new ArgumentException($"New ClockInTime must be less than the current ClockOutTime ({_clockOutTime})");
                _clockInTime = value;
            }
        }
        private DateTime? _clockOutTime;
        public DateTime? ClockOutTime 
        { 
            get => _clockOutTime;
            // Throws an ArgumentException if the passed in clock out time is less than or equal to the current clock in time (this condition is ignored if either time is null)
            set
            {
                if (value != null && _clockInTime != null && value <= _clockInTime)
                    throw new ArgumentException($"New ClockOutTime must be greater than the current ClockInTime ({_clockInTime})");
                _clockOutTime = value;
            }
        }

        protected List<TimesheetEntry> TimesheetEntries { get; } = [];

        /*
         * Constructs a new timecard with the passed in data and an empty timesheet entry list
         * 
         * timecardID is the identifier for this timecard
         * --It cannot be a negative integer
         * 
         * Throws an ArgumentException if timecardID is a negative integer
         */
        public Timecard(int timecardID, TimecardData data)
        {
            if (timecardID < 0)
                throw new ArgumentException($"timecardID ({timecardID}) cannot be a negative value");

            EntryType = EntryType.Timecard;
            EntryID = timecardID;
            ClockInTime = data.ClockInTime;
            ClockOutTime = data.ClockOutTime;
        }


        /*
         * Creates a new timesheet entry from the passed in entry data, adds it to the timesheet, and returns the entry's ID.
         * 
         * data is the container holding the data for the timesheet entry
         * --It must be of the type TimesheetEntryData
         * --Its start and end time cannot overlap with another entry in the timesheet
         * --Its non-null start time cannot come before the clock in time (if the clock in time isnt null)
         * --Its non-null end time cannot come after the clock out time (if the clock out time isnt null)
         * 
         * Throws an ArgumentException if entryData is not of the type TimesheetEntryData
         * Throws an ArgumentException if the passed in data container has a non-null start or end time that overlaps with another entry whose start and end time are both not null
         * Throws an exception if either the non-null start time of the container comes before the non-null clock in time or the non-null end time of the container comes afer the clock out time
         */
        public int AddEntry(TimesheetEntryData data)
        {
            if (EntryTimesOverlapWithAnotherEntry(data))
                throw new ArgumentException("The non-null start or end time within the passed in container cannot overlap with another entry whose start and end times are both not null");
            if (ContainerTimesAreOutOfBounds(data))
                throw new ArgumentException("The non-null start time and non-null end time must be within the bounds defined by the non-null clock in and clock out times");

            int entryID = GetHighestEntryID() + 1;

            TimesheetEntries.Add(new TimesheetEntry(entryID, data));

            return entryID;
        }

        /*
         * Retrieves the entry corresponding to the passed in entry ID
         * 
         * Returns null if an entry with the passed in value doesn't exist in the timesheet
         */
        public TimesheetEntry? GetEntry(int entryID)
        {
            foreach (TimesheetEntry entry in TimesheetEntries)
            {
                if (entry.EntryID == entryID)
                    return entry;
            }
            return null;
        }

        /*
         * Removes the entry corresponding to the passed in entry ID
         */
        public void RemoveEntry(int entryID)
        {
            foreach (TimesheetEntry entry in TimesheetEntries)
            {
                if (entry.EntryID == entryID)
                {
                    TimesheetEntries.Remove(entry);
                    break;
                }
            }
        }

        /*
         * Returns true if the timesheet contains an entry corresponding to the passed in entry ID
         */
        public bool ContainsEntry(int entryID)
        {
            foreach (TimesheetEntry entry in TimesheetEntries)
            {
                if (entry.EntryID == entryID)
                    return true;
            }
            return false;
        }

        /*
         * Returns the number of entries in the timecard
         */
        public int GetEntryCount() => TimesheetEntries.Count;


        /*
         * Returns true if the passed in IEntry is a Timecard with the same entryID
         */
        public bool Equals(IEntry? other)
        {
            if (other == null)
                return false;
            if (other is not Timecard)
                return false;
            if (EntryID == other.EntryID)
                return true;
            return false;
        }

        /*
         * Returns a string representation of this time card in the format:
         * 
         * Clock in:{20 - previous characters length # of spaces}{ClockInTime:MM/dd/yyyy hh:mm:ss tt}
         * {timesheet entry 1}
         * {timesheet entry 2}
         * ...
         * Clock out:{20 - previous characters length # of spaces}{ClockOutTime:MM/dd/yyyy hh:mm:ss tt}
         * 
         * if either ClockInDateTime or ClockOutDateTime are null they are represented with the following:
         * Clock in:{20 - previous characters length # of spaces}--/--/---- --:--:-- --
         * Clock out:{20 - previous characters length # of spaces}--/--/---- --:--:-- --
         */
        public override string ToString()
        {
            string str = "";

            if (ClockInTime != null)
                str += "Clock in:".PadRight(20) + $"{ClockInTime:MM/dd/yyyy hh:mm:ss tt}\n";
            else
                str += "Clock in:".PadRight(20) + "--/--/---- --:--:-- --\n";

            foreach (TimesheetEntry entry in TimesheetEntries)
            {
                str += entry.ToString() + "\n";
            }

            if (ClockOutTime != null)
                str += "Clock out:".PadRight(20) + $"{ClockOutTime:MM/dd/yyyy hh:mm:ss tt}";
            else
                str += "Clock out:".PadRight(20) + "--/--/---- --:--:-- --";

            return str;
        }

        /*
         * Returns true if the passed in TimesheetEntryData contains a start and/or end time that overlaps with an entry in the timesheet
         * Returns false if there are no entries in the timesheet
         */
        private bool EntryTimesOverlapWithAnotherEntry(TimesheetEntryData entryData)
        {
            foreach (TimesheetEntry entry in TimesheetEntries)
            {
                if (entryData.ContainedTimesOverlapWithEntry(entry))
                    return true;
            }
            return false;
        }

        /*
         * Returns the highest entryID in the list of timesheet entries
         * Returns -1 if there are no entries in the list
         */
        private int GetHighestEntryID()
        {
            int maxID = -1;

            foreach (TimesheetEntry entry in TimesheetEntries)
            {
                if (maxID < entry.EntryID)
                    maxID = entry.EntryID;
            }

            return maxID;
        }

        /*
         * Returns true if the non-null start time of the passed in container comes before the non-null clock in time
         * or the non-null end time of the passed in container comes after the non-null clock out time
         */
        private bool ContainerTimesAreOutOfBounds(TimesheetEntryData entryData)
        {
            bool outOfBounds = false;

            if (ClockInTime != null && entryData.StartTime != null)
                outOfBounds |= entryData.StartTime < ClockInTime;
            if (ClockOutTime != null & entryData.EndTime != null)
                outOfBounds |= entryData.EndTime > ClockOutTime;

            return outOfBounds;
        }
    }
}
