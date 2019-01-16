using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MusacaWebApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        [Range(0, 999999999999)]
        public long Barcode { get; set; }

        public string Picture { get; set; }
    }
}
