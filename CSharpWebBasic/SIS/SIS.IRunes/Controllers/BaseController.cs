using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.IRunes.Data;
using SIS.IRunes.Services;
using SIS.IRunes.Services.Contracts;
using SIS.WebServer.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SIS.IRunes.Controllers
{
    public abstract class BaseController
    {
        private const string RootDirectoryRelativePath = "../../../";

        private const string DirectorySeparator = "/";

        private const string ViewsFolderName = "Views";

        private const string HtmlFileExtension = ".html";

        private const string LayoutViewFileName = "_Layout";

        private const string RenderBodyConstant = "@RenderBody()";

        protected BaseController()
        {
            this.dbContext = new IRunesDbContext();
            this.UserCookieService = new UserCookieService();
        }

        protected IRunesDbContext dbContext { get; }

        protected IUserCookieService UserCookieService { get; }

        protected string GetUsername(IHttpRequest request)
        {
            if (!request.Cookies.ContainsCookie(".auth-IRunes"))
            {
                return null;
            }

            var cookie = request.Cookies.GetCookie(".auth-IRunes");
            var cookieContent = cookie.Value;
            var username = this.UserCookieService.GetUserData(cookieContent);

            return username;
        }

        protected IHttpResponse View(string viewName, IDictionary<string, object> viewBag = null, bool IsLogged = false)
        {
            var layoutView = RootDirectoryRelativePath
                                + ViewsFolderName
                                + DirectorySeparator
                                + LayoutViewFileName
                                + HtmlFileExtension;

            var loggedMenuView = RootDirectoryRelativePath
                                    + ViewsFolderName
                                    + DirectorySeparator
                                    + "Menu/"
                                    + "Logged"
                                    + HtmlFileExtension;

            var notLoggedMenuView = RootDirectoryRelativePath
                                        + ViewsFolderName
                                        + DirectorySeparator
                                        + "Menu/"
                                        + "NotLogged"
                                        + HtmlFileExtension;

            if (viewBag == null)
            {
                viewBag = new Dictionary<string, object>();
            }

            var content = GetViewContent(viewName, viewBag);

            var viewLayout = File.ReadAllText(layoutView);

            var allContent = string.Empty;
            if (IsLogged)
            {
                var viewLogged = File.ReadAllText(loggedMenuView);
                allContent = viewLayout.Replace("@MenuBody()", viewLogged);
            }
            else
            {
                var viewNotLogged = File.ReadAllText(notLoggedMenuView);
                allContent = viewLayout.Replace("@MenuBody()", viewNotLogged);
            }

            allContent = allContent.Replace(RenderBodyConstant, content);

            return new HtmlResult(allContent, HTTP.Enums.HttpResponseStatusCode.Ok);
        }

        protected IHttpResponse BadRequestError(string errorMessage)
        {
            var viewBag = new Dictionary<string, object>();

            viewBag.Add("Error", errorMessage);

            var allContent = this.GetViewContent("Errors/Error", viewBag);

            return new HtmlResult(allContent, HTTP.Enums.HttpResponseStatusCode.BadRequest);
        }

        private string GetViewContent(string viewName, IDictionary<string, object> viewBag)
        {
            var content = File.ReadAllText("Views/" + viewName + ".html");

            foreach (var item in viewBag)
            {
                content = content.Replace("@Model." + item.Key, item.Value.ToString());
            }

            return content;
        }
    }
}
