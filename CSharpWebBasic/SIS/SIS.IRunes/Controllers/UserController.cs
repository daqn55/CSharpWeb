using SIS.HTTP.Cookies;
using SIS.HTTP.Requests;
using SIS.HTTP.Responses;
using SIS.IRunes.Models;
using SIS.IRunes.Services;
using SIS.WebServer.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.IRunes.Controllers
{
    public class UserController : BaseController
    {
        private IHashSevice hashSevice;

        public UserController()
        {
            this.hashSevice = new HashService();
        }

        public IHttpResponse Login()
        {
            return this.View("Users/Login");
        }

        public IHttpResponse Register()
        {
            return this.View("Users/Register");
        }

        public IHttpResponse DoRegister(IHttpRequest request)
        {
            var username = request.FormData["username"].ToString().Trim();
            var password = request.FormData["password"].ToString();
            var confirmPassword = request.FormData["confirmPassword"].ToString();
            var email = request.FormData["email"].ToString().Trim();

            //Validate
            if (string.IsNullOrWhiteSpace(username) || username.Length < 3)
            {
                return this.BadRequestError("Please provide valid username with length of 3 or more characters.");
            }

            if (this.dbContext.Users.Any(x => x.Username == username))
            {
                return this.BadRequestError("User with the same name already exists.");
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 2)
            {
                return this.BadRequestError("Please provide password of length 2 or more.");
            }

            if (password != confirmPassword)
            {
                return this.BadRequestError("Passwords do not match.");
            }

            if (this.dbContext.Users.Any(x => x.Email.ToLower() == email.ToLower()))
            {
                return this.BadRequestError("User with the same email already exists.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return this.BadRequestError("Please provide valid email.");
            }

            //Hash password
            var hashedPassword = this.hashSevice.Hash(password);

            //Create user
            var user = new User
            {
                Username = username,
                Password = hashedPassword,
                Email = email
            };
            this.dbContext.Users.Add(user);

            try
            {
                this.dbContext.SaveChanges();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return new RedirectResult("/");
        }

        public IHttpResponse DoLogin(IHttpRequest request)
        {
            var username = request.FormData["username"].ToString().Trim();
            var password = request.FormData["password"].ToString();

            var hashedPassword = this.hashSevice.Hash(password);

            var user = this.dbContext.Users.FirstOrDefault(
                                 x => x.Username == username &&
                                      x.Password == hashedPassword);

            if (user == null)
            {
                return this.BadRequestError("Invalid username or password.");
            }

            var cookieContent = this.UserCookieService.GetUserCookie(user.Username);

            var response = new RedirectResult("/");
            var cookie = new HttpCookie(".auth-IRunes", cookieContent, 7) { HttpOnly=true };
            response.Cookies.Add(cookie);

            return response;
        }

        public IHttpResponse Logout(IHttpRequest request)
        {
            if (!request.Cookies.ContainsCookie(".auth-IRunes"))
            {
                return new RedirectResult("/");
            }

            var cookie = request.Cookies.GetCookie(".auth-IRunes");
            cookie.Delete();
            var response = new RedirectResult("/");
            response.Cookies.Add(cookie);

            return response;
        }
    }
}
