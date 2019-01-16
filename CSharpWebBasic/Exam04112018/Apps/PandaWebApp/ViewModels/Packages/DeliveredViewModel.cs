using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.ViewModels.Packages
{
    public class DeliveredViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Weight { get; set; }

        public string ShippingAddress { get; set; }

        public string Recepient { get; set; }
    }
}
