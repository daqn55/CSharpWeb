using SIS.MvcFramework;
using System;

namespace MusacaWebApp
{
    class Program
    {
        public static void Main()
        {
            WebHost.Start(new Startup());
        }
    }
}
