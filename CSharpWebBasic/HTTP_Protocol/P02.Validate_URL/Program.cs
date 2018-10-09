using System;
using System.Net;
using System.Text;

namespace P02.Validate_URL
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = WebUtility.UrlDecode(Console.ReadLine());

            try
            {
                var uri = new Uri(url);

                var protocol = uri.Scheme;
                var host = uri.Host;
                var port = uri.Port;
                var path = uri.AbsolutePath;

                var query = uri.Query;
                var fragment = uri.Fragment;

                var isUrlValid = true;
                var sb = new StringBuilder();

                if (protocol != "http" && protocol != "https")
                {
                    isUrlValid = false;
                }
                else if (host == "")
                {
                    isUrlValid = false;
                }
                else if (port != 80 && port != 443)
                {
                    isUrlValid = false;
                }

                if (protocol == "http" && port != 80)
                {
                    isUrlValid = false;
                }
                else if (protocol == "https" && port != 443)
                {
                    isUrlValid = false;
                }

                if (isUrlValid)
                {
                    sb.AppendLine($"Protocol: {protocol}")
                        .AppendLine($"Host: {host}")
                        .AppendLine($"Port: {port}")
                        .AppendLine($"Path: {path}");

                    if (query != "")
                    {
                        var querySub = query.Substring(1);
                        sb.AppendLine($"Query: {querySub}");
                    }

                    if (fragment != "")
                    {
                        var fragmentSub = fragment.Substring(1);
                        sb.AppendLine($"Fragment: {fragmentSub}");
                    }
                }
                else
                {
                    sb.AppendLine("Invalid URL");
                }

                Console.WriteLine(sb.ToString().TrimEnd());
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid URL");
            }
            
        }
    }
}
