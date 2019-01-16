using SIS.HTTP.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TorshiaWebApp.Models;
using TorshiaWebApp.Models.Enums;
using TorshiaWebApp.ViewModels.Reports;

namespace TorshiaWebApp.Controllers
{
    public class ReportsController : BaseController
    {
        public IHttpResponse All()
        {
            if (this.User.Role == "Admin")
            {
                var viewModel = new AllReportedTaskViewModel();
                viewModel.AllReports = this.Db.Reports.Select(x => new SimpleReportedViewModel
                {
                    Id = x.Id,
                    Level = x.Task.AffectedSectors.Count,
                    Status = x.Status.ToString(),
                    Title = x.Task.Title
                });


                return this.View(viewModel);
            }

            return this.Redirect("/");
        }

        public IHttpResponse Report(int id)
        {
            if (this.User.IsLoggedIn)
            {
                var task = this.Db.Tasks.FirstOrDefault(x => x.Id == id);

                if (task != null)
                {
                    var user = this.Db.Users.First(x => x.Username == this.User.Username);

                    var status = ReportStatus.Completed;

                    var rnd = new Random();
                    var randomNumber = rnd.Next(0, 100);
                    if (randomNumber <= 25)
                    {
                        status = ReportStatus.Archived;
                    }

                    task.IsReported = true;

                    var report = new Report
                    {
                        ReportedOn = DateTime.Now,
                        Task = task,
                        Reporter = user,
                        Status = status
                    };
                    this.Db.Reports.Add(report);

                    this.Db.SaveChanges();

                    return this.Redirect("/");
                }

                return this.BadRequestError("Invalid Task Id!");
            }

            return this.Redirect("/");
        }

        public IHttpResponse Details(int id)
        {
            if (this.User.IsLoggedIn && this.User.Role == "Admin")
            {
                var report = this.Db.Reports.FirstOrDefault(x => x.Id == id);

                if (report == null)
                {
                    return this.BadRequestError("Invalid Report Id!");
                }

                var participants = new List<string>();
                foreach (var p in report.Task.Participants)
                {
                    participants.Add(p.User.Username);
                }

                var sectors = new List<string>();
                foreach (var s in report.Task.AffectedSectors)
                {
                    sectors.Add(s.Sector.ToString());
                }

                var viewModel = new ReportDetailsViewModel
                {
                    Id = id,
                    Level = report.Task.AffectedSectors.Count,
                    Description = report.Task.Description,
                    DueDate = report.Task.DueDate.Value.ToString("dd/MM/yyyy"),
                    ReportedOn = report.ReportedOn.ToString("dd/MM/yyy"),
                    Title = report.Task.Title,
                    Reporter = report.Reporter.Username,
                    Status = report.Status.ToString(),
                    AffectedSectors = string.Join(", ", sectors),
                    Participants = string.Join(", ", participants)
                };

                return this.View(viewModel);
            }

            return this.Redirect("/");
        }
    }
}
