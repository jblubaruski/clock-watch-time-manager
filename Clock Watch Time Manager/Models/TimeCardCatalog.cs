using System;
using System.Collections.Generic;
using System.Text;

namespace Clock_Watch_Time_Manager.Models
{
    internal class TimeCardCatalog
    {
        private List<TimeCard> Catalog { get; } = [];

        /*
         * Adds the passed in time card to the catalog
         */
        public void AddTimeCard(TimeCard timeCard)
        {
            Catalog.Add(timeCard);
        }

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
