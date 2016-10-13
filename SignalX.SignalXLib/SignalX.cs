using System;
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
        internal static Action<Exception> ExceptionHandler { set; get; }

        public static void OnException(Action<Exception> handler)
        {
            ExceptionHandler = handler;
        }

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

        internal static ConcurrentDictionary<string, Action<object, object, string, string>> _signalXServers = new ConcurrentDictionary<string, Action<object, object, string, string>>();

        public static void Server(string name, Action<object, object,string, string> server)
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

        public class SignalXRequest
        {
            public SignalXRequest(string replyTo, object sender, string messageId, object message)
            {
                ReplyTo = replyTo;
                Sender = sender;
                MessageId = messageId;
                Message = message;
            }

            public string ReplyTo { get; }
            public object Sender { get; }
            public string MessageId { get; }
            public object Message { get; }
            public void Respond(object response)
            {
             if(!string.IsNullOrEmpty(ReplyTo))
                    SignalX.RespondTo(ReplyTo, response);
            }
            public void RespondTo(string replyTo,object response)
            {
                if (replyTo == null) throw new ArgumentNullException(nameof(replyTo));
                    SignalX.RespondTo(replyTo, response);
            }
        }

     

        public static void Server(string name, Action<object, object,string> server)
        {
            Server(name, (message, sender, replyTo, messageId) => server(message, sender, replyTo));
        }
        public static void Server(string name, Action<object, object> server)
        {
            Server(name, (message,sender, replyTo,messageId) =>server(message, sender));
        }

        public static void Server(string name, Action<SignalXRequest> server)
        {
            Server(name, (message, sender, replyTo, messageId) => server(new SignalXRequest(replyTo, sender, messageId, message)));
        }

        public static void RespondTo(string name, object data)
        {
            var hubContext = GlobalHost.DependencyResolver.Resolve<IConnectionManager>().GetHubContext<SignalXHub>();

            hubContext.Clients.All.broadcastMessage(name, data);
        }
    }
}