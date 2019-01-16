using ChushkaPrepExam.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChushkaPrepExam.ViewModels.Product
{
    public class ProductViewModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public ProductType Type { get; set; }
    }
}
