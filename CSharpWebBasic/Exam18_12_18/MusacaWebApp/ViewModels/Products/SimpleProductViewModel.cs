using System;
using System.Collections.Generic;
using System.Text;

namespace MusacaWebApp.ViewModels.Products
{
    public class SimpleProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Barcode { get; set; }

        public string Picture { get; set; }
    }
}
