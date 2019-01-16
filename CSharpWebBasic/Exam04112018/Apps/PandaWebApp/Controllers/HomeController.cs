namespace PandaWebApp.Controllers
{
    using PandaWebApp.Models;
    using PandaWebApp.ViewModels.Home;
    using PandaWebApp.ViewModels.Packages;
    using SIS.HTTP.Responses;
    using SIS.MvcFramework;
    using System.Linq;

    public class HomeController : BaseController
    {
        public IHttpResponse Index()
        {
            if (this.User.IsLoggedIn)
            {
                var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
                if (user != null)
                {
                    var viewModel = new LoggedInIndexViewModel();
                    viewModel.YourPending = this.Db.Packages.Where(
                            x => x.Status == PackageStatus.Pending && x.Recipient.Id == user.Id)
                        .Select(x => new BasePackageViewModel
                        {
                            Id = x.Id,
                            Name = x.Description,
                            Status = x.Status.ToString(),
                        }).ToList();

                    viewModel.YourShipped = this.Db.Packages.Where(
                            x => x.Status == PackageStatus.Shipped && x.Recipient.Id == user.Id)
                        .Select(x => new BasePackageViewModel
                        {
                            Id = x.Id,
                            Name = x.Description,
                            Status = x.Status.ToString(),
                        }).ToList();

                    viewModel.YourDelivered = this.Db.Packages.Where(
                            x => x.Status == PackageStatus.Delivered && x.Recipient.Id == user.Id)
                        .Select(x => new BasePackageViewModel
                        {
                            Id = x.Id,
                            Name = x.Description,
                            Status = x.Status.ToString(),
                        }).ToList();

                    return this.View("/Home/IndexLoggedIn", viewModel);
                }
            }

            return this.View();
        }
    }
}
