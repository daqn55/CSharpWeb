using System;
using System.Collections.Generic;
using System.Net;

namespace P03.Request_Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            var allRoutes = new Dictionary<string, HashSet<string>>();

            var input = Console.ReadLine().ToUpper();

            while (input != "END")
            {
                var splitRoute = input.Split('/', StringSplitOptions.RemoveEmptyEntries);

                if (allRoutes.ContainsKey(splitRoute[0]))
                {
                    allRoutes[splitRoute[0]].Add(splitRoute[1]);
                }
                else
                {
                    allRoutes[splitRoute[0]] = new HashSet<string>() { splitRoute[1] };
                }

                input = Console.ReadLine();
            }

            var requestPath = Console.ReadLine().ToUpper();

            var requestPathSplit = requestPath.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var substringPath = requestPathSplit[1].Substring(1);

            var isRequestValid = false;
            if (allRoutes.ContainsKey(substringPath))
            {
                if (allRoutes[substringPath].Contains(requestPathSplit[0]))
                {
                    isRequestValid = true;
                }
            }

            var response = string.Empty;
            if (isRequestValid)
            {
                response = $"{requestPathSplit[2]} {(int)HttpStatusCode.OK} {HttpStatusCode.OK}\r\n" +
                    $"Content-Length: {HttpStatusCode.OK.ToString().Length}\r\n" +
                    $"Content-Type: text/plain\r\n\r\n" +
                    $"{HttpStatusCode.OK}";
            }
            else
            {
                response = $"{requestPathSplit[2]} {(int)HttpStatusCode.NotFound} {HttpStatusCode.NotFound}\r\n" +
                    $"Content-Length: {HttpStatusCode.NotFound.ToString().Length}\r\n" +
                    $"Content-Type: text/plain\r\n\r\n" +
                    $"{HttpStatusCode.NotFound}";
            }

            Console.WriteLine(response);
        }
    }
}
