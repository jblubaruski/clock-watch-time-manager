using BusinessManagementLibrary.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.TimeManagement
{
    // TODO MERGE WITH TIMECARD

    /*
     * Contains and manages the entries (breaks and tasks) on the timesheet
     */
    public class Timesheet
    {
        private List<IEntry> EntryList { get; } = [];

        /*
         * Creates a new timesheet entry from the passed in entry data, adds it to the timesheet, and returns the entry's ID.
         * 
         * data is the container holding the data for the timesheet entry
         * --It must be of the type TimesheetEntryData
         * --Its start and end time cannot overlap with another entry in the timesheet
         * 
         * Throws an ArgumentException if entryData is not of the type TimesheetEntryData
         * Throws an ArgumentException if the passed in data container has a non-null start or end time that overlaps with another entry whose start and end time are both not null
         */
        public int AddEntry(IEntryData data)
        {
            if (data is not TimesheetEntryData)
                throw new ArgumentException($"entryData must be of the type {typeof(TimesheetEntryData)} but is {data.GetType()} instead");

            TimesheetEntryData entryData = (TimesheetEntryData)data;

            if (EntryTimesOverlapWithAnotherEntry(entryData))
                throw new ArgumentException($"The non-null start or end time within the passed in container cannot overlap with another entry whose start and end times are both not null");

            int entryID = GetHighestEntryID() + 1;

            EntryList.Add(new TimesheetEntry(entryID, entryData));

            return entryID;
        }

        /*
         * Retrieves the entry corresponding to the passed in entry ID
         * 
         * Returns null if an entry with the passed in value doesn't exist in the timesheet
         */
        public IEntry? GetEntry(int entryID)
        {
            IEntry? requestedEntry = null;

            foreach (IEntry entry in EntryList)
            {
                if (entry.EntryID == entryID)
                    requestedEntry = entry;
            }

            return requestedEntry;
        }

        /*
         * Removes the entry corresponding to the passed in entry ID
         */
        public void RemoveEntry(int entryID)
        {
            foreach(IEntry entry in EntryList)
            {
                if (entry.EntryID == entryID)
                    EntryList.Remove(entry);
            }
        }

        /*
         * Returns true if the timesheet contains an entry corresponding to the passed in entry ID
         */
        public bool ContainsEntry(int entryID)
        {
            foreach (IEntry entry in EntryList)
            {
                if (entry.EntryID == entryID)
                    return true;
            }
            return false;
        }

        /*
         * Returns true if the passed in TimesheetEntryData contains a start and/or end time that overlaps with an entry in the timesheet
         * Returns false if there are no entries in the timesheet
         */
        private bool EntryTimesOverlapWithAnotherEntry(TimesheetEntryData entryData)
        {
            foreach (TimesheetEntry entry in EntryList.Cast<TimesheetEntry>())
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

            foreach (TimesheetEntry entry in EntryList.Cast<TimesheetEntry>())
            {
                if (maxID < entry.EntryID)
                    maxID = entry.EntryID;
            }

            return maxID;
        }
    }
}
