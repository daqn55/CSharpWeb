﻿using PandaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.ViewModels.Receipts
{
    public class ReceiptViewModel
    {
        public string Description { get; set; }

        public string Weight { get; set; }

        public string ShippingAddress { get; set; }

        public string Status { get; set; }

        public string EstimatedDeliveryDate { get; set; }

        public string Recepient { get; set; }
    }
}
