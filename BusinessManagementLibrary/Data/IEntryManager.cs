using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Data
{
    /*
     * Defines the required behavior of a class managing a list of entries
     */
    public interface IEntryManager<T, U>
        where T : IEntryData
        where U : IEntry<T>
    {
        public int AddEntry(T data);
        public U? GetEntry(int entryID);
        public void RemoveEntry(int entryID);
        public bool ContainsEntry(int entryID);
        public int GetEntryCount();
    }
}
