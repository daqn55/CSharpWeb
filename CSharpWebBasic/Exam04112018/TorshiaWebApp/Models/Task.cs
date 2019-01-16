using System;
using System.Collections.Generic;
using TorshiaWebApp.Models.Enums;

namespace TorshiaWebApp.Models
{
    public class Task
    {
        public Task()
        {
            this.Participants = new HashSet<UserTask>();
            this.AffectedSectors = new HashSet<TaskSector>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsReported { get; set; }

        public string Description { get; set; }

        public virtual ICollection<UserTask> Participants { get; set; }

        public virtual ICollection<TaskSector> AffectedSectors { get; set; }
    }
}
