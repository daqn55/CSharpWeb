using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.ViewModels.Receipts
{
    public class ReceiptIndexViewModel
    {
        public IEnumerable<BaseReceiptViewModel> YourReceipts { get; set; }
    }
}
