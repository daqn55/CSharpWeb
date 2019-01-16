using MusacaWebApp.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusacaWebApp.ViewModels.Receipts
{
    public class AllDataReceiptViewModel
    {
        public IEnumerable<OrderViewModel> AllOrders { get; set; }

        public string TotalSum { get; set; }

        public string IssuedOn { get; set; }

        public string CashierName { get; set; }

        public string ReceiptId { get; set; }
    }
}
