using MusacaWebApp.ViewModels.Orders;
using MusacaWebApp.ViewModels.Receipts;
using SIS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusacaWebApp.Controllers
{
    public class ReceiptsController : BaseController
    {
        public IHttpResponse Details(int id)
        {
            var viewModel = new AllDataReceiptViewModel();

            var receipt = this.Db.Receipts.FirstOrDefault(x => x.Id == id);

            if (receipt == null)
            {
                return this.BadRequestError("Invalid Receipt!");
            }

            viewModel.AllOrders = receipt.Orders
                                         .Select(x => new OrderViewModel
                                         {
                                             Name = x.Product.Name,
                                             Price = x.Product.Price.ToString("F2"),
                                             Quantity = x.Quantity
                                         }).ToList();

            viewModel.IssuedOn = receipt.IssuedOn.ToString("dd/MM/yyyy");
            viewModel.TotalSum = receipt.Orders.Sum(x => x.Product.Price * x.Quantity).ToString("F2");
            viewModel.CashierName = receipt.Cashier.Username;
            viewModel.ReceiptId = receipt.Id.ToString();

            return this.View(viewModel);
        }

        public IHttpResponse All()
        {
            if (this.User.Role != "Admin")
            {
                return this.Redirect("/");
            }

            var viewModel = new AllReceiptsViewModel();

            viewModel.AllReceipts = this.Db.Receipts.Select(x => new SimpleReceiptViewModel
                                                    {
                                                        Id = x.Id,
                                                        CashierName = x.Cashier.Username,
                                                        IssuedOn = x.IssuedOn.ToString("dd/MM/yyyy"),
                                                        TotalSum = x.Orders.Sum(s => s.Quantity * s.Product.Price).ToString("F2")
                                                    });

            return this.View(viewModel);
        }
    }
}
