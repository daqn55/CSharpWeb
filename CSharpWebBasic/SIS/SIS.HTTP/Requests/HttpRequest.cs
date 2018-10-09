using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Exceptions;
using SIS.HTTP.Headers;
using SIS.HTTP.Sessions;

namespace SIS.HTTP.Requests
{
    public class HttpRequest : IHttpRequest
    {

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
            for (int i = 0; i < requestContent.Length; i++)
            {
                var content = requestContent[i].Split(": ", StringSplitOptions.RemoveEmptyEntries);

                if (content.Length == 0)
                {
                    break;
                }
                var header = new HttpHeader(content[0], content[1]);
                this.Headers.Add(header);
            }

            if (!this.Headers.ContainsHeader("Host"))
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
            var checkForCookie = this.Headers.ContainsHeader("Cookie");

            if (checkForCookie)
            {
                var cookies = this.Headers.GetHeader("Cookie").Value.Split("; ", StringSplitOptions.RemoveEmptyEntries);

                foreach (var cookie in cookies)
                {
                    var splitedCookie = cookie.Split('=');

                    this.Cookies.Add(new HttpCookie(splitedCookie[0], splitedCookie[1]));
                }
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
