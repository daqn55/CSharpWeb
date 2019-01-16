using PandaWebApp.ViewModels.Packages;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.ViewModels.Home
{
    public class LoggedInIndexViewModel
    {
        public IEnumerable<BasePackageViewModel> YourPending { get; set; }

        public IEnumerable<BasePackageViewModel> YourShipped { get; set; }

        public IEnumerable<BasePackageViewModel> YourDelivered { get; set; }

    }
}
