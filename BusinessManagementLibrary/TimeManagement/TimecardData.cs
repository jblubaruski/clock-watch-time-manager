using BusinessManagementLibrary.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.TimeManagement
{
    // TODO WRITE TESTS

    /*
     * Read-only container for the data used to create a Timecard
     */
    public class TimecardData : IEntryData
    {
        public DateTime? ClockInTime { get; }
        public DateTime? ClockOutTime { get; }

        /*
         * Constructs a container holding the data for a Timecard
         * 
         * Throws an ArgumentException if clockInTime and clockOutTime are not null and clockInTime is greater than or equal to clockOutTime
         */
        public TimecardData(DateTime? clockInTime, DateTime? clockOutTime)
        {
            if (clockInTime != null && clockOutTime != null && clockInTime >= clockOutTime)
                throw new ArgumentException($"clockInTime ({clockInTime}) cannot be greater than or equal to clockOutTime ({clockOutTime})");

            ClockInTime = clockInTime;
            ClockOutTime = clockOutTime;
        }

        /*
         * Returns true if the non-null clock in and/or clock out time overlaps with the clock in and clock out times (both not null) of the passed in Timecard
         */
        public bool ContainedTimesOverlapWithTimecard(Timecard card)
        {
            if (card.ClockInTime == null || card.ClockOutTime == null)
                return false;

            bool overlaps = false;

            if (ClockInTime != null)
                overlaps &= ClockInTime > card.ClockInTime && ClockInTime < card.ClockOutTime;
            if (ClockOutTime != null)
                overlaps &= ClockOutTime > card.ClockInTime && ClockOutTime < card.ClockOutTime;

            return overlaps;
        }
    }
}
