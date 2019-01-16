using MusacaWebApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusacaWebApp.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public UserRole Role { get; set; }
    }
}
