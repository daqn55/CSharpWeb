using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.ViewModels.Packages
{
    public class ShippedViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Weight { get; set; }

        public string EstimatedDeliveryDate { get; set; }

        public string Recepient { get; set; }
    }
}
