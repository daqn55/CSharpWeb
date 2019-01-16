using System;
using System.Collections.Generic;
using System.Text;

namespace MusacaWebApp.ViewModels.Products
{
    public class AllProductsViewModel
    {
        public IEnumerable<SimpleProductViewModel> AllProducts { get; set; }
    }
}
