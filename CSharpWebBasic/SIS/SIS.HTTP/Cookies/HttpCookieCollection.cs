using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly List<HttpCookie> cookies;

        public HttpCookieCollection()
        {
            this.cookies = new List<HttpCookie>();
        }

        public void Add(HttpCookie cookie)
        {
            this.cookies.Add(cookie);
        }

        public bool ContainsCookie(string key)
        {
            var cookie = this.cookies.FirstOrDefault(c => c.Key == key);

            if (cookie == null)
            {
                return false;
            }

            return true;
        }

        public HttpCookie GetCookie(string key)
        {
            var cookie = this.cookies.FirstOrDefault(c => c.Key == key);

            if (cookie != null)
            {
                return cookie;
            }

            return null;
        }

        public bool HasCookies()
        {
            var haveCookie = this.cookies.Count > 0;

            if (haveCookie)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return string.Join("; ", this.cookies);
        }
    }
}
