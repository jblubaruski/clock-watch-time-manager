using BusinessManagementLibrary.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.TimeManagement
{
    /*
     * Read-only container for the data used to create a TimesheetEntry
     */
    public class TimesheetEntryData : IEntryData
    {
        public DateTime? StartTime { get; }
        public DateTime? EndTime { get; }
        public string? Description { get; }
        public bool IsBreak { get; }

        /*
         * Constructs a container holding the data for a TimesheetEntry
         * 
         * Throws an ArgumentException if startTime and endTime are not null and startTime is greater than or equal to endTime
         */
        public TimesheetEntryData(DateTime? startTime, DateTime? endTime, string? description, bool isBreak)
        {
            if (startTime != null && endTime != null && startTime >= endTime)
                throw new ArgumentException($"startTime ({startTime}) cannot be greater than or equal to endTime ({endTime}) if both are null");

            StartTime = startTime;
            EndTime = endTime;
            Description = description;
            IsBreak = isBreak;
        }

        /*
         * Returns true if the non-null start and/or end time overlaps with the start and end time (both not null) of the passed in TimesheetEntry
         */
        public bool ContainedTimesOverlapWithEntry(TimesheetEntry entry)
        {
            if (entry.StartTime == null || entry.EndTime == null)
                return false;

            bool overlaps = false;

            if (StartTime != null)
                overlaps |= StartTime > entry.StartTime && StartTime < entry.EndTime;
            if (EndTime != null)
                overlaps |= EndTime > entry.StartTime && EndTime < entry.EndTime;

            return overlaps;
        }
    }
}
