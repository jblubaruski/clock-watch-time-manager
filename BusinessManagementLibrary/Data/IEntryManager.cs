using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Data
{
    /*
     * Defines the required behavior of a class managing a list of entries
     */
    public interface IEntryManager
    {
        public List<IEntry> Entries { get; }
        public int AddEntry(IEntryData data);
        public IEntry? GetEntry(int entryID);
        public void RemoveEntry(int entryID);
        public bool ContainsEntry(int entryID);
    }
}
