using Microsoft.AspNet.SignalR;

namespace SignalXLib.Lib
{
    public class SignalXHub : Hub
    {
        public void Send(string handler, object message)
        {
            global::SignalXLib.Lib.SignalX._signalXServers[handler].Invoke(message);
            //  Clients.All.addMessage(message);
        }

        public void GetMethods()
        {
            var methods = "var $sx= {";
            foreach (var signalXServer in global::SignalXLib.Lib.SignalX._signalXServers)
            {

                methods += signalXServer.Key + ":function(m){  chat.server.send('" + signalXServer.Key + "',m);   },";
            }
            methods += "}; $sx; ";
            Clients.All.addMessage(methods);
        }

       
    }
}