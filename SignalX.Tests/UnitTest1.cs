using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SignalXLib.Lib;


namespace SignalXLib.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (new SignalX("http://localhost:44111","/ui"))
            {
                try
                {
                    SignalX.Server("Sample",message =>SignalX.ClientPush("Myclient", "yooo server : " + message));
                    Console.WriteLine("quiting server in next 5 minute");
                    Reader.ReadLine(TimeSpan.FromMinutes(5));
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("session ended.");
                }
            }
        }
    }
}