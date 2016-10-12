using SignalXLib.Lib;
using System;

namespace ConsoleApplicationCSharp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var url = "http://localhost:44111";
            using (new SignalX(url, ""))
            {
                SignalX.Server("Sample", message => SignalX.ClientPush("Myclient", "yooo server : " + message));
                System.Diagnostics.Process.Start(url);
                Console.ReadLine();
            }
        }
    }
}