using MusacaWebApp.Data;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusacaWebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        protected ApplicationDbContext Db { get; }

        public BaseController()
        {
            this.Db = new ApplicationDbContext();
        }

    }
}
