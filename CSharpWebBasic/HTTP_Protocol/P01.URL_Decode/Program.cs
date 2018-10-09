using System;
using System.Net;

namespace P01.URL_Decode
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var url = Console.ReadLine();

            var urlDecode = WebUtility.UrlDecode(url);

            Console.WriteLine(urlDecode);
        }
    }
}
