﻿using SIS.HTTP.Cookies;
using SIS.HTTP.Enums;
using SIS.HTTP.Headers;
using SIS.HTTP.Sessions;
using System.Collections.Generic;

namespace SIS.HTTP.Requests
{
    public interface IHttpRequest
    {
        IHttpSession Session { get; set; }

        IHttpCookieCollection Cookies { get; }

        string Path { get; }

        string Url { get; }

        Dictionary<string, object> FormData { get; }

        Dictionary<string, object> QueryData { get; }

        IHttpHeaderCollection Headers { get; }

        HttpRequestMethod RequestMethod { get; }
    }
}