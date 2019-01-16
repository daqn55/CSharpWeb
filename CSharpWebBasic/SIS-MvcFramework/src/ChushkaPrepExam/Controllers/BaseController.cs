using ChushkaPrepExam.Data;
using SIS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChushkaPrepExam.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            this.Db = new ChushkaDbContext();
        }

        protected ChushkaDbContext Db { get; }
    }
}
