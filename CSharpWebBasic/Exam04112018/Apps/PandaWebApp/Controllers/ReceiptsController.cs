using PandaWebApp.ViewModels.Receipts;
using SIS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PandaWebApp.Controllers
{
    public class ReceiptsController : BaseController
    {
        public IHttpResponse Details(int id)
        {
            if (this.User.IsLoggedIn)
            {

            }

            return this.Redirect("/Users/Login");
        }

        public IHttpResponse Index()
        {
            if (this.User.IsLoggedIn)
            {
                var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);

                var viewModel = new ReceiptIndexViewModel();
                var test = this.Db.Receipts;
                viewModel.YourReceipts = this.Db.Receipts.Where(
                        x => x.Recipient.Username == this.User.Username)
                    .Select(x => new BaseReceiptViewModel
                    {
                        Id = x.Id,
                        Fee = x.Fee.ToString(),
                        IssuedOn = x.IssuedOn.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                        User = x.Recipient.Username
                    }).ToList();

                return this.View(viewModel);
            }

            return this.Redirect("/Users/Login");
        }
    }
}
