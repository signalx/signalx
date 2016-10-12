using SignalXLib.Lib;
using System;
using System.Threading.Tasks;

namespace ConsoleApplicationCSharp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var url = "http://localhost:44111";
            using (new SignalX(url, ""))
            {
                SignalX.Server("Sample", message =>
                {
                    var messageId = Guid.NewGuid().ToString();
                    SignalX.ClientPush("Myclient", messageId +" - "+ message + ": Thank you for sending me the message " );
                    SignalX.ClientPush("Myclient", messageId + " - " + message + ": Hang on i'm not done yet");
                    Task.Delay(TimeSpan.FromMilliseconds(1000)).ContinueWith(x =>
                    {
                        SignalX.ClientPush("Myclient", messageId + " - " + message + ": So im almost done");
                    });
                    Task.Delay(TimeSpan.FromMilliseconds(1000)).ContinueWith(x =>
                    {
                        SignalX.ClientPush("Myclient", messageId + " - " + message + ": Im' done!");
                    });
                });
                
                SignalX.Server("Sample2", (message,sender, replyTo) => SignalX.ClientPush(string.IsNullOrEmpty(replyTo) ? "Myclient" : replyTo, sender + " sent me this message : " + message + " and asked me to reply to " + replyTo));

                System.Diagnostics.Process.Start(url);
                Console.ReadLine();
            }
        }
    }
}