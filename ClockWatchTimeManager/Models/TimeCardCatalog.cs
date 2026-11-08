using System;
using System.Collections.Generic;
using System.Text;

namespace ClockWatchTimeManager.Models
{
    internal class TimeCardCatalog
    {
        /*
         * Contains and manages a collection of timecards
         */
        private List<TimeCard> Catalog { get; } = [];

        /*
         * Adds the passed in time card to the catalog
         * 
         * Throws an ArgumentNullException if a null timecard is passed in
         */
        public void AddTimeCard(TimeCard timecard)
        {
            ArgumentNullException.ThrowIfNull(timecard);
            Catalog.Add(timecard);
        }

        /*
         * Returns a string representation of this catalog 
         * 
         * The representation is formatted as:
         * Time Cards:
         * ====================
         * {Time Card 1}
         * --------------------
         * {Time Card 2}
         * --------------------
         * ...
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
                str += Catalog[^1];
            }

            return str;
        }
    }
}
