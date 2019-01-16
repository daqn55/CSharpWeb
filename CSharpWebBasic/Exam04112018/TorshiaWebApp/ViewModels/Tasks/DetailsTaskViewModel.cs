using System;
using System.Collections.Generic;
using System.Text;

namespace TorshiaWebApp.ViewModels.Tasks
{
    public class DetailsTaskViewModel
    {
        public string Title { get; set; }

        public int Level { get; set; }

        public string DueDate { get; set; }

        public string Participants { get; set; }

        public string AffectedSectors { get; set; }

        public string Description { get; set; }
    }
}
