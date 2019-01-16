using System;
using System.Collections.Generic;
using System.Text;

namespace MusacaWebApp.ViewModels.Orders
{
    public class AllOrdersViewModel
    {
        public IEnumerable<OrderViewModel> AllOrders { get; set; }

        public string TotalSum { get; set; }
    }
}
