using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.ViewModels.Packages
{
    public class AllPendingViewModel
    {
        public IEnumerable<PendingViewModel> AllPending { get; set; }

        public IEnumerable<ShippedViewModel> AllShipped { get; set; }

        public IEnumerable<DeliveredViewModel> AllDelivered { get; set; }
    }
}
