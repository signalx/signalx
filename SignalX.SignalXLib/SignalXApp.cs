using System;
using System.Collections.Concurrent;
using System.Text;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin.Hosting;

namespace SignalX.SignalXLib
{
    public class SignalX
    {
        internal static ConcurrentDictionary<string, Action<object>> _signalXServers =
            new ConcurrentDictionary<string, Action<object>>();


        public static void SignalXServer(string name, Action<object> server)
        {
            if (_signalXServers.ContainsKey(name))
            {
                throw new Exception("Server with name '" + name + "' already created");
            }
            var added = _signalXServers.TryAdd(name, server);
            var camelCased = Char.ToLowerInvariant(name[0]) + name.Substring(1);
            if (!_signalXServers.ContainsKey(camelCased))
            {
                added =  _signalXServers.TryAdd(camelCased, server) ;//&&added;
            }

            var unCamelCased = Char.ToUpperInvariant(name[0]) + name.Substring(1);
            if (!_signalXServers.ContainsKey(unCamelCased))
            {
                added = _signalXServers.TryAdd(unCamelCased, server);//&&added;
            }

            //if (!added)
            //{
            //    throw new Exception("unable to create signalx server");
            //}
        }

        public static void SendToClient(string name, object data)
        {
            var hubContext = GlobalHost.DependencyResolver.Resolve<IConnectionManager>().GetHubContext<SignalXHub>();

            hubContext.Clients.All.broadcastMessage(name, data);
        }
       
    }

    public class SignalXApp : IDisposable
    {
        public SignalXApp(string url = "http://localhost:44111")
        {
            MyApp = WebApp.Start(url);
            Console.WriteLine("Server running on {0}", url);
        }

        public IDisposable MyApp { get; set; }

        public void Dispose()
        {
            MyApp.Dispose();
        }
    }
}