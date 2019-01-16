using SIS.MvcFramework;
using System;

namespace ChushkaPrepExam
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHost.Start(new Startup());
        }
    }
}
