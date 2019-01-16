using ChushkaPrepExam.ViewModels.Home;
using ChushkaPrepExam.ViewModels.Product;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChushkaPrepExam.Controllers
{
    public class HomeController : BaseController
    {
        public IHttpResponse Index()
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (user != null)
            {
                var viewModel = new LoggedInIndexViewModel();

                viewModel.FullName = user.FullName;
                viewModel.Products = this.Db.Products.Where(p => !p.IsDeleted)
                    .Select(x => new BaseProductViewModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Desctription = x.Description,
                        Price = x.Price
                    }).ToList();

                return this.View("/Home/LoggedIn-Index", viewModel);
            }

            return this.View();
        }
    }
}
