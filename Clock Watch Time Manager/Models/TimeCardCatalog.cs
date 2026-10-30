using System;
using System.Collections.Generic;
using System.Text;

namespace Clock_Watch_Time_Manager.Models
{
    internal class TimeCardCatalog
    {
        /*
         * Contains and manages a collection of time cards
         */
        private List<TimeCard> Catalog { get; } = [];

        /*
         * Adds the passed in time card to the catalog
         */
        public void AddTimeCard(TimeCard timeCard)
        {
            ArgumentNullException.ThrowIfNull(timeCard);
            Catalog.Add(timeCard);
        }

        /*
         * Returns a string representation of this catalog in the format:
         * Time Cards:
         * ********************
         * <Time Card 1>
         * --------------------
         * <Time Card 2>
         * --------------------
         * ...
         * --------------------
         * <Time Card N>
         */
        public override string ToString()
        {
            string str = "Time Cards:\n";

            str += "".PadLeft(20, '*');

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
