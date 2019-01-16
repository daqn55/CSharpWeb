using MusacaWebApp.ViewModels.Orders;
using SIS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusacaWebApp.Controllers
{
    public class HomeController : BaseController
    {
        public IHttpResponse Index()
        {
            if (!this.User.IsLoggedIn)
            {
                return this.View();
            }

            var viewModel = new AllOrdersViewModel();

            viewModel.AllOrders = this.Db.Orders.Where(x => x.Status == Models.Enums.OrderStatus.Active)
                                                .Select(x => new OrderViewModel
                                                {
                                                    Name = x.Product.Name,
                                                    Price = x.Product.Price.ToString("F2"),
                                                    Quantity = x.Quantity
                                                }).ToList();

            viewModel.TotalSum = viewModel.AllOrders.Sum(x => x.Quantity * decimal.Parse(x.Price)).ToString("F2");

            return this.View("/Home/IndexLoggedIn", viewModel);
        }
    }
}
