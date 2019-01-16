using System;
using System.Collections.Generic;
using System.Text;

namespace MusacaWebApp.Models
{
    public class Receipt
    {
        public Receipt()
        {
            this.Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public int CashierId { get; set; }
        public virtual User Cashier { get; set; }
    }
}
