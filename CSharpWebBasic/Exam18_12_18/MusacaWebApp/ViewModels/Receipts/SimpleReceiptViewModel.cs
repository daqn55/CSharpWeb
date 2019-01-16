using System;
using System.Collections.Generic;
using System.Text;

namespace MusacaWebApp.ViewModels.Receipts
{
    public class SimpleReceiptViewModel
    {
        public int Id { get; set; }

        public string TotalSum { get; set; }

        public string IssuedOn { get; set; }

        public string CashierName { get; set; }
    }
}
