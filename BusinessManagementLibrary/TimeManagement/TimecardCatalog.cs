using BusinessManagementLibrary.Data;
using BusinessManagementLibrary.TimeManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessManagementLibrary.TimeManagement
{
    // TODO WRITE TESTS
    
    /*
     * Contains and manages a collection of timecards
     */
    public class TimecardCatalog : IEntryManager<TimecardData, Timecard>
    {
        
        private List<Timecard> Catalog { get; } = [];

        /*
         * Creates a new timecard from the passed in timecard data, adds it to the catalog, and returns the timecard's ID
         * 
         * data is the container holding the clock in and clock out times for the new timecard
         * --It must be of the type TimecardData
         * --It cannot contain a non-null clock in time and/or non-null clock out time that overlaps with the non-null clock in and out times of another timecard
         * 
         * Throws an ArgumentException if data is not of the type TimecardData
         * Throws an ArgumentException if the passed in timecard contains a non-null clock in or clock out time that overlaps with another timecard containing non-null clock in and out times
         */
        public int AddEntry(TimecardData data)
        {
            if (TimecardTimesOverlapWithAnotherTimecard(data))
                throw new ArgumentException("The non-null clock in or clock out time within the passed in container cannot overlap with another timecard whose clock in and out times are both not null");

            int timecardID = GetHighestEntryID() + 1;

            Catalog.Add(new Timecard(timecardID, data));

            return timecardID;
        }

        /*
         * Retrieves the timecard corresponding to the passed in entry ID
         * 
         * Returns null if a timecard with the passed in value doesn't exist in the catalog
         */
        public Timecard? GetEntry(int entryID)
        {
            foreach (Timecard entry in Catalog)
            {
                if (entry.EntryID == entryID)
                    return entry;
            }
            return null;
        }

        /*
         * Removes the timecard corresponding to the passed in entry ID
         */
        public void RemoveEntry(int entryID)
        {
            foreach (Timecard entry in Catalog)
            {
                if (entry.EntryID == entryID)
                    Catalog.Remove(entry);
            }
        }

        /*
         * Returns true if the timesheet contains an entry corresponding to the passed in entry ID
         */
        public bool ContainsEntry(int entryID)
        {
            foreach (Timecard entry in Catalog)
            {
                if (entry.EntryID == entryID)
                    return true;
            }
            return false;
        }

        /*
         * Returns the number of timecards in the catalog
         */
        public int GetEntryCount() => Catalog.Count;

        /*
         * Returns a string representation of this catalog 
         * 
         * The representation is formatted as:
         * Time Cards:
         * {'=' repeated 20 times}
         * {Time Card 1}
         * {'-' repeated 20 times}
         * {Time Card 2}
         * {'-' repeated 20 times}
         * ...
         * {'=' repeated 20 times}
         */
        public override string ToString()
        {
            string str = "Time Cards:\n";

            str += "".PadLeft(20, '=');

            if (Catalog.Count != 0)
            {
                str += "\n";
                for (int i = 0; i < Catalog.Count - 1; i++)
                {
                    str += Catalog[i] + "\n";
                    str += "".PadLeft(20, '-') + "\n";
                }
                str += Catalog[^1] + "\n";
            }

            return str + "".PadLeft(20, '=');
        }

        /*
         * Returns true if the passed in TimesheetEntryData contains a clock in and/or clock out time that overlaps with a timecard in the catalog
         * Returns false if there are no timecards in the catalog
         */
        private bool TimecardTimesOverlapWithAnotherTimecard(TimecardData entryData)
        {
            foreach (Timecard entry in Catalog)
            {
                if (entryData.ContainedTimesOverlapWithTimecard(entry))
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

            foreach (Timecard entry in Catalog)
            {
                if (maxID < entry.EntryID)
                    maxID = entry.EntryID;
            }

            return maxID;
        }
    }
}
