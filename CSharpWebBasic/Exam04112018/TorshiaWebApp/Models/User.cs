using System.Collections.Generic;
using TorshiaWebApp.Models.Enums;

namespace TorshiaWebApp.Models
{
    public class User
    {
        public User()
        {
            this.Tasks = new HashSet<UserTask>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public UserRole Role { get; set; }

        public virtual ICollection<UserTask> Tasks { get; set; }
    }
}
