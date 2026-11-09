using BusinessManagementLibrary.TimeManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.TimeManagement
{
    /*
     * Contains and manages all the breaks and tasks taken and carried out during the work period.
     */
    public class Timesheet
    {
        private List<TimesheetEntry> EntryList { get; } = [];

        /*
         * Creates a new timesheet entry from the passed in data, adds it to the timesheet, and returns the entries's ID.
         * 
         * startTime is the entry's start time and date
         * endTime is the entry's end time and date
         * description is the entry's description
         * isBreak determines whether the entry is a break or task (true is a break, false is a task)
         * 
         * The start time must be less than the end time and they cannot be equal to each other (if both are not null)
         * Both the start and end time must exist entirely outide any timespan covered by another entry in the timesheet (if either are not null)
         * 
         * Returns -1 if the entry could not be added
         */
        public int AddTimesheetEntry(DateTime? startTime, DateTime? endTime, string? description, bool isBreak = true)
        {
            bool startTimeIsNull = startTime == null;
            bool endTimeIsNull = endTime == null;

            if (!startTimeIsNull && !endTimeIsNull && endTime <= startTime)
                return -1;
            if (!startTimeIsNull && OverlapsWithEntry(startTime))
                return -1;
            if (!endTimeIsNull && OverlapsWithEntry(endTime))
                return -1;

            // assign the break ID to be one above the highest already existing breakID
            int breakID = 0;

            return breakID;
        }

        /*
         * Returns true if the passed in start and end time overlaps with another break or a task
         */
        private bool OverlapsWithEntry(DateTime? testTime)
        {
            foreach (TimesheetEntry entry in EntryList)
            {
                if (entry.StartTime != null && entry.EndTime != null)
                {

                }
            }    
            return false;
        }
    }
}
