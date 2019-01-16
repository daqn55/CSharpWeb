using System;
using System.Collections.Generic;
using System.Text;

namespace TorshiaWebApp.Models
{
    public class UserTask
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}
