using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.ViewModels.Receipts
{
    public class BaseReceiptViewModel
    {
        public int Id { get; set; }

        public string Fee { get; set; }

        public string IssuedOn { get; set; }

        public string User { get; set; }
    }
}
