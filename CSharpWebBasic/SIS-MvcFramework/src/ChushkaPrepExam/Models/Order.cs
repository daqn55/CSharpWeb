using System;
using System.Collections.Generic;
using System.Text;

namespace ChushkaPrepExam.Models
{
    public class Order
    {
        public Order()
        {
            this.Products = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }

        public DateTime OrderedOn { get; set; } = DateTime.UtcNow;

        public int ClientId { get; set; }
        public virtual User Client { get; set; }

        public virtual ICollection<OrderProduct> Products { get; set; }
    }
}
