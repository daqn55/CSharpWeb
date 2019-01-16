using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIS.HTTP.Cookies
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        private readonly Dictionary<string, HttpCookie> cookies;

        public HttpCookieCollection()
        {
            this.cookies = new Dictionary<string, HttpCookie>();
        }

        public void Add(HttpCookie cookie)
        {
            this.cookies.Add(cookie.Key, cookie);
        }

        public bool ContainsCookie(string key)
        {
            var cookie = this.cookies.ContainsKey(key);

            return cookie;
        }

        public HttpCookie GetCookie(string key)
        {
            return this.cookies.GetValueOrDefault(key, null);
        }

        public bool HasCookies()
        {
            var haveCookie = this.cookies.Count > 0;

            return haveCookie;
        }

        public IEnumerator<HttpCookie> GetEnumerator()
        {
            foreach (var cookie in this.cookies)
            {
                yield return cookie.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join("; ", this.cookies.Values);
        }
    }
}
