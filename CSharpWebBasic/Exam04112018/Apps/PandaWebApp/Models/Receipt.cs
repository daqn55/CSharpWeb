using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.Models
{
    public class Receipt
    {
        public int Id { get; set; }

        public DateTime IssuedOn { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public virtual User Recipient { get; set; }

        public int PackageId { get; set; }
        public virtual Package Package { get; set; }

        public decimal Fee { get; set; }
    }
}
