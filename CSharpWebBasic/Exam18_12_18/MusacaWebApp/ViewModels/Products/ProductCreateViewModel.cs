using System;
using System.Collections.Generic;
using System.Text;

namespace MusacaWebApp.ViewModels.Products
{
    public class ProductCreateViewModel
    {
        public string Name { get; set; }

        public string Picture { get; set; }

        public decimal Price { get; set; }

        public long Barcode { get; set; }
    }
}
