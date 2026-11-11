using BusinessManagementLibrary.TimeManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessManagementLibrary.Tests.TimeManagement.TestingWrappers
{
    internal class TimecardCatalog_Wrapper : TimecardCatalog
    {
        public List<Timecard> ExposedCatalog { get; }

        public TimecardCatalog_Wrapper()
        {
            ExposedCatalog = Catalog;
        }
    }
}
