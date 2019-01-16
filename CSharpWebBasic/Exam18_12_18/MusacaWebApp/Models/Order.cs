using MusacaWebApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusacaWebApp.Models
{
    public class Order
    {
        public int Id { get; set; }

        public OrderStatus Status { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }

        public int CashierId { get; set; }
        public virtual User Cashier { get; set; }
    }
}
