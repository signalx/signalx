using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SignalX.SignalXLib;

namespace SignalX.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (new SignalXApp())
            {
                try
                {
                    SignalXLib.SignalX.SignalXServer("Sample",
                        message => SignalXLib.SignalX.SendToClient(
                            "Myclient", "yooo server : " + message));

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