using ChushkaPrepExam.ViewModels.Product;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChushkaPrepExam.Controllers
{
    public class ProductsController : BaseController
    {
        [Authorize]
        public IHttpResponse Details(int id)
        {
            var productViewModel = this.Db.Products.Where(x => x.Id == id)
                .Select(x => new ProductViewModel
                {
                    Type = x.Type,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                }).FirstOrDefault();

            if (productViewModel == null)
            {
                return this.BadRequestError("Invalid channel id.");
            }

            return this.View(productViewModel);
        }

        [Authorize("Admin")]
        public IHttpResponse Create()
        {
            return this.View();
        }
    }
}
