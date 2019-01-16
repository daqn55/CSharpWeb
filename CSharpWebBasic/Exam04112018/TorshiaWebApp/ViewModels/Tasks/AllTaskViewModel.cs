using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TorshiaWebApp.ViewModels.Tasks
{
    public class AllTaskViewModel
    {
        public IEnumerable<SimpleTaskViewModel> AllTasks { get; set; }
    }
}
