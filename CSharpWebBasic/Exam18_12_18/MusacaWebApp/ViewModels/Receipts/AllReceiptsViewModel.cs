using System;
using System.Collections.Generic;
using System.Text;

namespace MusacaWebApp.ViewModels.Receipts
{
    public class AllReceiptsViewModel
    {
        public IEnumerable<SimpleReceiptViewModel> AllReceipts { get; set; }
    }
}
