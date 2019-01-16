using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.Models
{
    public class Package
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public double Weight { get; set; }

        public string ShippingAddress { get; set; }

        public PackageStatus Status { get; set; } = PackageStatus.Pending;

        public DateTime? EstimatedDeliveryDate { get; set; }

        public int UserId { get; set; }
        public virtual User Recipient { get; set; }

        public int? ReceiptId { get; set; }
        public virtual Receipt Receipt { get; set; }
    }
}
