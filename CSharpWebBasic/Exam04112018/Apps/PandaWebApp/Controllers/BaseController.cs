namespace PandaWebApp.Controllers
{
    using PandaWebApp.Data;
    using SIS.MvcFramework;

    public class BaseController : Controller
    {
        protected ApplicationDbContext Db { get; }

        public BaseController()
        {
            this.Db = new ApplicationDbContext();
        }

    }
}
