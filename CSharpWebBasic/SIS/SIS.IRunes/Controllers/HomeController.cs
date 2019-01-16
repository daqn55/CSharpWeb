using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.WebServer.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.IRunes.Controllers
{
    public class HomeController : BaseController
    {
        public IHttpResponse Index(IHttpRequest request)
        {
            if (!string.IsNullOrEmpty(this.GetUsername(request)))
            {
                var viewBag = new Dictionary<string, object>();
                viewBag.Add("Username", this.GetUsername(request));

                return this.View("Home/IndexLoggedIn", viewBag, true);
            }

            return this.View("Home/Index");
        }
    }
}
