using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SIS.HTTP.Common;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Headers;
using SIS.HTTP.Sessions;

namespace SIS.HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {
        private const char HttpRequestUrlQuerySeparator = '?';

        private const char HttpRequestUrlFragmentSeparator = '#';

        private const string HttpRequestHeaderNameValueSeparator = ": ";

        private const string HttpRequestCookiesSeparator = "; ";

        private const char HttpRequestCookieNameValueSeparator = '=';

        private const char HttpRequestParameterSeparator = '&';

        private const char HttpRequestParameterNameValueSeparator = '=';

        public HttpRequest(string requestString)
        {
            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();

            this.ParseRequest(requestString);
        }

        public IHttpSession Session { get; set; }

        public IHttpCookieCollection Cookies { get; }

        public string Path { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, object> FormData { get; }

        public Dictionary<string, object> QueryData { get; }

        public IHttpHeaderCollection Headers { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        private bool IsValidRequestLine(string[] requestLine)
        {
            if (requestLine.Length == 3)
            {
                if (requestLine[2] == "HTTP/1.1")
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsValidRequestQueryString(string queryString, string[] queryParameters)
        {
            if (string.IsNullOrWhiteSpace(queryString))
            {
                return false;
            }

            if (queryParameters.Length == 0)
            {
                return false;
            }

            return true;
        }

        private void ParseRequestMethod(string[] requestLine)
        {
            var lettersExeptFirst = requestLine[0].Substring(1).ToLower();
            var requestMethod = requestLine[0][0] + lettersExeptFirst;

            this.RequestMethod = Enum.Parse<HttpRequestMethod>(requestMethod);
        }

        private void ParseRequestUrl(string[] requestLine)
        {
            this.Url = requestLine[1];

        }

        private void ParseRequestPath()
        {
            var path = this.Url.Split('?').FirstOrDefault();

            if (string.IsNullOrEmpty(path))
            {
                throw new BadRequestException();
            }

            this.Path = path;
        }

        private void ParseHeaders(string[] requestContent)
        {
            int currentIndex = 0;

            while (requestContent.Length > currentIndex)
            {
                string[] headerArguments = requestContent[currentIndex++].Split(HttpRequestHeaderNameValueSeparator);
                if (headerArguments.Length > 1)
                {
                    this.Headers.Add(new HttpHeader(headerArguments[0], headerArguments[1]));
                }
            }

            if (!this.Headers.ContainsHeader(GlobalConstants.HostHeaderKey))
            {
                throw new BadRequestException();
            }
        }

        private void ParseQueryParameters()
        {
            if (!this.Url.Contains('?'))
            {
                return;
            }

            string queryString = this.Url
                .Split(new[] { '?', '#' }, StringSplitOptions.None)[1];

            if (string.IsNullOrWhiteSpace(queryString))
            {
                return;
            }

            string[] queryParameters = queryString.Split('&');

            if (!this.IsValidRequestQueryString(queryString, queryParameters))
            {
                throw new BadRequestException();
            }

            foreach (var queryParameter in queryParameters)
            {
                string[] parameterArguments = queryParameter
                    .Split('=', StringSplitOptions.RemoveEmptyEntries);

                this.QueryData.Add(parameterArguments[0], parameterArguments[1]);
            }
        }

        private void ParseFormDataParameters(string formData)
        {
            if (string.IsNullOrEmpty(formData))
            {
                return;
            }

            var paramethers = formData.Split('&', StringSplitOptions.RemoveEmptyEntries);
            foreach (var param in paramethers)
            {
                var tokens = param.Split('=', StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length != 2)
                {
                    return;
                }

                var key = tokens[0];
                var value = tokens[1];

                this.FormData.Add(key, value);
            }
        }

        private void ParseRequestParameters(string formData)
        {
            this.ParseQueryParameters();

            this.ParseFormDataParameters(formData);
        }

        private void ParseCookies()
        {
            if (!this.Headers.ContainsHeader("Cookie")) return;

            string cookiesString = this.Headers.GetHeader("Cookie").Value;

            if (string.IsNullOrEmpty(cookiesString)) return;

            string[] splitCookies = cookiesString.Split(HttpRequestCookiesSeparator);

            foreach (var splitCookie in splitCookies)
            {
                string[] cookieParts = splitCookie.Split(HttpRequestCookieNameValueSeparator, 2, StringSplitOptions.RemoveEmptyEntries);

                if (cookieParts.Length != 2) continue;

                string key = cookieParts[0];
                string value = cookieParts[1];

                this.Cookies.Add(new HttpCookie(key, value, false));
            }
        }

        private void ParseRequest(string requestString)
        {
            string[] splitRequestContent = requestString
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            string[] requestLine = splitRequestContent[0].Trim().
                Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (!this.IsValidRequestLine(requestLine))
            {
                throw new BadRequestException();
            }

            this.ParseRequestMethod(requestLine);
            this.ParseRequestUrl(requestLine);
            this.ParseRequestPath();

            this.ParseHeaders(splitRequestContent.Skip(1).ToArray());
            this.ParseCookies();

            var formData = splitRequestContent[splitRequestContent.Length - 1];
            this.ParseRequestParameters(formData);
        }
    }
}
