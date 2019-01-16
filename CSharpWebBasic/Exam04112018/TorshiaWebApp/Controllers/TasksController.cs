using SIS.HTTP.Responses;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TorshiaWebApp.Models;
using TorshiaWebApp.Models.Enums;
using TorshiaWebApp.ViewModels.Tasks;

namespace TorshiaWebApp.Controllers
{
    public class TasksController : BaseController
    {
        public IHttpResponse Create()
        {
            if (this.User.IsLoggedIn && this.User.Role == "Admin")
            {
                return this.View();
            }

            return this.Redirect("/");
        }

        [HttpPost]
        public IHttpResponse Create(CreateTaskViewModel model)
        {
            if (this.User.IsLoggedIn && this.User.Role == "Admin")
            {
                var usersNames = model.Participants.Split(new string[] { ", ", ","}, StringSplitOptions.RemoveEmptyEntries);
                var users = this.Db.Users.Where(x => usersNames.Contains(x.Username)).ToList();

                var sectors = new List<Sector>();
                if (model.CustomersCheckbox != null)
                {
                    sectors.Add(Sector.Customers);
                }
                if (model.FinancesCheckbox != null)
                {
                    sectors.Add(Sector.Finances);
                }
                if (model.InternalCheckbox != null)
                {
                    sectors.Add(Sector.Internal);
                }
                if (model.ManagementCheckbox != null)
                {
                    sectors.Add(Sector.Management);
                }
                if (model.MarketingCheckbox != null)
                {
                    sectors.Add(Sector.Marketing);
                }

                var task = new Task
                {
                    Title = model.Title,
                    DueDate = model.DueDate,
                    Description = model.Description,
                };

                var usersTasks = new List<UserTask>();
                foreach (var u in users)
                {
                    var userTask = new UserTask
                    {
                        Task = task,
                        User = u
                    };
                    usersTasks.Add(userTask);
                }

                var tasksSectors = new List<TaskSector>();
                foreach (var s in sectors)
                {
                    var taskSector = new TaskSector
                    {
                        Sector = s,
                        Task = task
                    };
                    tasksSectors.Add(taskSector);
                }

                this.Db.Tasks.Add(task);
                this.Db.UsersTasks.AddRange(usersTasks);
                this.Db.TasksSectors.AddRange(tasksSectors);

                this.Db.SaveChanges();
            }

            return this.Redirect("/");
        }

        public IHttpResponse Details(int id)
        {
            if (this.User.IsLoggedIn)
            {
                var task = this.Db.Tasks.FirstOrDefault(x => x.Id == id);

                if (task != null)
                {
                    var participants = new List<string>();
                    foreach (var p in task.Participants)
                    {
                        participants.Add(p.User.Username);
                    }

                    var sectors = new List<string>();
                    foreach (var s in task.AffectedSectors)
                    {
                        sectors.Add(s.Sector.ToString());
                    }

                    var viewModel = new DetailsTaskViewModel
                    {
                        Title = task.Title,
                        DueDate = task.DueDate.Value.ToString("dd/MM/yyyy"),
                        Level = task.AffectedSectors.Count,
                        Description = task.Description,
                        Participants = string.Join(", ", participants),
                        AffectedSectors = string.Join(", ", sectors)
                    };

                    return this.View(viewModel);
                }

                return this.BadRequestError("Invalid Task Id!");
            }

            return this.Redirect("/");
        }
    }
}
