using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Data
{
    /*
     * Defines the required properties for any class or struct that acts as an entry
     */
    public interface IEntry
    {
        public EntryType EntryType { get; }
        public int EntryID { get; }
    }
}
