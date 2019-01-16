using System;
using System.Collections.Generic;
using System.Text;

namespace TorshiaWebApp.ViewModels.Reports
{
    public class AllReportedTaskViewModel
    {
        public IEnumerable<SimpleReportedViewModel> AllReports { get; set; }
    }
}
