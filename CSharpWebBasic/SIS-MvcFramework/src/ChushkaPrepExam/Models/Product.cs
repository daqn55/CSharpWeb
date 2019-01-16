using System;
using System.Collections.Generic;
using System.Text;

namespace ChushkaPrepExam.Models
{
    public class Product
    {
        public Product()
        {
            this.Orders = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public ProductType Type { get; set; }

        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<OrderProduct> Orders { get; set; }
    }
}
