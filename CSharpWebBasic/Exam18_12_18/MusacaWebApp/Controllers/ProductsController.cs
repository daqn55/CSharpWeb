using MusacaWebApp.Models;
using MusacaWebApp.ViewModels.Products;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusacaWebApp.Controllers
{
    public class ProductsController : BaseController
    {
        public IHttpResponse All()
        {
            var viewModel = new AllProductsViewModel();

            viewModel.AllProducts = this.Db.Products.Select(x => new SimpleProductViewModel
            {
                Name = x.Name,
                Barcode = x.Barcode.ToString(),
                Price = x.Price.ToString("F2"),
                Picture = string.IsNullOrEmpty(x.Picture) ? "/product.png" : x.Picture
            }).ToList();

            return this.View(viewModel);
        }

        public IHttpResponse Create()
        {
            if (this.User.Role == "Admin")
            {
                return this.View();
            }

            return this.Redirect("/");
        }

        [HttpPost]
        public IHttpResponse Create(ProductCreateViewModel model)
        {
            if (this.User.Role != "Admin")
            {
                return this.Redirect("/");
            }

            var barcode = model.Barcode;
            if (barcode < 0 || barcode > 999999999999)
            {
                return this.BadRequestError("Invalid Barcode!");
            }

            var product = new Product
            {
                Name = model.Name,
                Barcode = model.Barcode,
                Picture = model.Picture,
                Price = model.Price
            };

            this.Db.Products.Add(product);
            this.Db.SaveChanges();

            return this.Redirect("/Products/All");
        }
    }
}
