﻿using System;
using System.Collections.Concurrent;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin.Hosting;

namespace SignalXLib.Lib
{
    public class SignalX : IDisposable
    {
        internal static string UiFolder { set; get; }
        internal static string BaseUiDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public static string UiDirectory => BaseUiDirectory + UiFolder;

        public SignalX(string url = "http://localhost:44111",string uiFolder="/ui",string baseUIDirectory= null)
        {
            UiFolder = uiFolder;
            BaseUiDirectory = baseUIDirectory ?? BaseUiDirectory;

            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException(nameof(url));
            MyApp = WebApp.Start(url);
        }

        public IDisposable MyApp { get; set; }

        public void Dispose()
        {
            MyApp.Dispose();
        }

        internal static ConcurrentDictionary<string, Action<object>> _signalXServers = new ConcurrentDictionary<string, Action<object>>();

        public static void Server(string name, Action<object> server)
        {
            if (_signalXServers.ContainsKey(name))
            {
                throw new Exception("Server with name '" + name + "' already created");
            }
            var added = _signalXServers.TryAdd(name, server);
            var camelCased = Char.ToLowerInvariant(name[0]) + name.Substring(1);
            if (!_signalXServers.ContainsKey(camelCased))
            {
                added = _signalXServers.TryAdd(camelCased, server);//&&added;
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

        public static void ClientPush(string name, object data)
        {
            var hubContext = GlobalHost.DependencyResolver.Resolve<IConnectionManager>().GetHubContext<SignalXHub>();

            hubContext.Clients.All.broadcastMessage(name, data);
        }
    }
}