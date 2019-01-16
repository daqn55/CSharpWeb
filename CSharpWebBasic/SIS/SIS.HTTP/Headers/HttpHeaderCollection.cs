using SIS.HTTP.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.HTTP.Headers
{
    public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();
        }

        public void Add(HttpHeader header)
        {
            this.headers.Add(header.Key, header);
        }

        public bool ContainsHeader(string key)
        {
            if (this.headers.ContainsKey(key))
            {
                return true;
            }

            return false;
        }

        public HttpHeader GetHeader(string key)
        {
            if (this.headers.ContainsKey(key))
            {
                return this.headers[key];
            }

            return null;
        }

        public override string ToString()
        {
            return string.Join(GlobalConstants.HttpNewLine, this.headers.Values);
        }
    }
}
