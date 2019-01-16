using SIS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TorshiaWebApp.ViewModels.Tasks;

namespace TorshiaWebApp.Controllers
{
    public class HomeController : BaseController
    {
        public IHttpResponse Index()
        {
            if (this.User.IsLoggedIn)
            {
                var viewModel = new AllTaskViewModel();

                viewModel.AllTasks = this.Db.Tasks.Where(x => x.Participants.Any(u => u.User.Username == this.User.Username) && x.IsReported == false)
                                                  .Select(x => new SimpleTaskViewModel
                                                  {
                                                      Id = x.Id,
                                                      Title = x.Title,
                                                      Level = x.AffectedSectors.Count
                                                  }).ToList();
                
                return this.View("/Home/IndexLoggedIn", viewModel);
            }
             
            return this.View();
        }
    }
}
