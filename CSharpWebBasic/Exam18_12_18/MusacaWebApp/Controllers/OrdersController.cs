using MusacaWebApp.Models;
using MusacaWebApp.Models.Enums;
using MusacaWebApp.ViewModels.Orders;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusacaWebApp.Controllers
{
    public class OrdersController : BaseController
    {
        [HttpPost]
        public IHttpResponse Order(SimpleOrderViewModel model)
        {
            if (!this.User.IsLoggedIn)
            {
                return this.Redirect("/");
            }

            var product = this.Db.Products.FirstOrDefault(x => x.Barcode == model.Barcode);

            if (product == null)
            {
                return this.BadRequestError("Invalid Barcode!");
            }

            var cashier = this.Db.Users.First(x => x.Username == this.User.Username);

            var order = new Order
            {
                Cashier = cashier,
                Product = product,
                Quantity = model.Quantity,
                Status = OrderStatus.Active,
            };

            this.Db.Orders.Add(order);
            this.Db.SaveChanges();

            return this.Redirect("/");
        }

        [HttpPost]
        public IHttpResponse Cashout()
        {
            if (!this.User.IsLoggedIn)
            {
                return this.Redirect("/");
            }

            var ordersToCashout = this.Db.Orders.Where(x => x.Status == OrderStatus.Active).ToList();

            foreach (var o in ordersToCashout)
            {
                o.Status = OrderStatus.Completed;
            }

            var cashier = this.Db.Users.First(x => x.Username == this.User.Username);

            var receipt = new Receipt
            {
                Cashier = cashier,
                IssuedOn = DateTime.Now,
                Orders = ordersToCashout,
            };

            this.Db.Receipts.Add(receipt);
            this.Db.SaveChanges();

            return this.Redirect($"/Receipts/Details?id={receipt.Id}");
        }
    }
}
