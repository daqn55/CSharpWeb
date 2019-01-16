using SIS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PandaWebApp.ViewModels.Packages;
using System.Globalization;
using PandaWebApp.ViewModels.Create;
using SIS.MvcFramework;
using PandaWebApp.Models;

namespace PandaWebApp.Controllers
{
    public class PackagesController : BaseController
    {
        public IHttpResponse Create()
        {
            if (this.User.IsLoggedIn && this.User.Role == "Admin")
            {
                var viewModel = new PackageCreateViewModel();
                viewModel.AllUsers = this.Db.Users
                    .Select(x => new UserViewModel
                    {
                        Id = x.Id,
                        Username = x.Username,
                    }).ToList();

                return this.View(viewModel);
            }

            return this.Redirect("/");
        }

        [HttpPost]
        public IHttpResponse Create(OnCreatePackageViewModel model)
        {
            if (this.User.IsLoggedIn && this.User.Role == "Admin")
            {
                var user = this.Db.Users.FirstOrDefault(x => x.Username == model.Recepient);

                var package = new Package
                {
                    Description = model.Description,
                    Weight = double.Parse(model.Weight),
                    ShippingAddress = model.ShippingAddress,
                    Recipient = user
                };
                this.Db.Packages.Add(package);
                this.Db.SaveChanges();

                return this.Redirect("/");
            }

            return this.Redirect("/");
        }

        public IHttpResponse Details(int id)
        {
            if (this.User.IsLoggedIn)
            {
                var package = this.Db.Packages.FirstOrDefault(x => x.Id == id);
                var dateOrStatus = string.Empty;
                if (package.Status == Models.PackageStatus.Delivered || package.Status == Models.PackageStatus.Acquired)
                {
                    dateOrStatus = "Delivered";
                }
                else if (package.EstimatedDeliveryDate != null)
                {
                    dateOrStatus = package.EstimatedDeliveryDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                else
                {
                    dateOrStatus = "N/A";
                }

                var packageViewModel = this.Db.Packages
                    .Where(x => x.Id == id)
                    .Select(x => new PackageViewModel
                    {
                        Description = x.Description,
                        EstimatedDeliveryDate = dateOrStatus,
                        ShippingAddress = x.ShippingAddress,
                        Status = x.Status.ToString(),
                        Weight = x.Weight.ToString(),
                        Recepient = x.Recipient.Username
                    }).FirstOrDefault();

                return this.View("/Packages/Details", packageViewModel);
            }

            return this.Redirect("/Users/Login");
        }

        public IHttpResponse Pending()
        {
            if (this.User.IsLoggedIn && this.User.Role == "Admin")
            {

                var viewModel = new AllPendingViewModel();
                viewModel.AllPending = this.Db.Packages.Where(x => x.Status == PackageStatus.Pending)
                                                .Select(x => new PendingViewModel
                                                {
                                                    Id = x.Id,
                                                    Description = x.Description,
                                                    Weight = x.Weight.ToString("F2"),
                                                    ShippingAddress = x.ShippingAddress,
                                                    Recepient = x.Recipient.Username
                                                }).ToList();

                return this.View(viewModel);
            }

            return this.Redirect("/");
        }

        [HttpPost]
        public IHttpResponse Pending(int id)
        {
            var package = this.Db.Packages.FirstOrDefault(x => x.Id == id);

            if (package == null)
            {
                return BadRequestError("Invalid Id!");
            }

            Random rnd = new Random();
            int randomDeliveryDays = rnd.Next(20, 40);

            package.EstimatedDeliveryDate = DateTime.Now.AddDays(randomDeliveryDays);

            package.Status = PackageStatus.Shipped;
            this.Db.SaveChanges();

            return Redirect("/Packages/Pending");
        }

        public IHttpResponse Shipped()
        {
            if (this.User.IsLoggedIn && this.User.Role == "Admin")
            {

                var viewModel = new AllPendingViewModel();
                viewModel.AllShipped = this.Db.Packages.Where(x => x.Status == PackageStatus.Shipped)
                                                .Select(x => new ShippedViewModel
                                                {
                                                    Id = x.Id,
                                                    Description = x.Description,
                                                    Weight = x.Weight.ToString("F2"),
                                                    EstimatedDeliveryDate = x.EstimatedDeliveryDate.Value.ToString("dd/MM/yyyy"),
                                                    Recepient = x.Recipient.Username
                                                }).ToList();

                return this.View(viewModel);
            }

            return this.Redirect("/");
        }

        [HttpPost]
        public IHttpResponse Shipped(int id)
        {
            var package = this.Db.Packages.FirstOrDefault(x => x.Id == id);

            if (package == null)
            {
                return BadRequestError("Invalid Id!");
            }

            package.Status = PackageStatus.Delivered;
            this.Db.SaveChanges();

            return Redirect("/Packages/Shipped");
        }

        public IHttpResponse Delivered()
        {
            if (this.User.IsLoggedIn && this.User.Role == "Admin")
            {

                var viewModel = new AllPendingViewModel();
                viewModel.AllDelivered = this.Db.Packages.Where(x => x.Status == PackageStatus.Delivered)
                                                .Select(x => new DeliveredViewModel
                                                {
                                                    Id = x.Id,
                                                    Description = x.Description,
                                                    Weight = x.Weight.ToString("F2"),
                                                    ShippingAddress = x.ShippingAddress,
                                                    Recepient = x.Recipient.Username
                                                }).ToList();

                return this.View(viewModel);
            }

            return this.Redirect("/");
        }

        [HttpPost]
        public IHttpResponse Delivered(int id)
        {
            return this.Details(id);
        }

        [HttpPost]
        public IHttpResponse Acquire(int id)
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            var package = this.Db.Packages.FirstOrDefault(x => x.Id == id);

            if (this.User.IsLoggedIn && package.Recipient.Id == user.Id)
            {
                if (package != null)
                {
                    
                    package.Status = PackageStatus.Acquired;

                    var receipt = new Receipt
                    {
                        Recipient = user,
                        Package = package,
                        Fee = (decimal)(package.Weight * 2.67)
                    };

                    this.Db.Receipts.Add(receipt);

                    this.Db.SaveChanges();
                    return this.Redirect("/");
                }
                else
                {
                    return this.BadRequestError("Invald Package Id!");
                }
            }

            return this.Redirect("/");
        }

    }
}
