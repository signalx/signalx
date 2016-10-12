using System.Linq;
using Microsoft.AspNet.SignalR;

namespace SignalXLib.Lib
{
    public class SignalXHub : Hub
    {
        public void Send(string handler, object message,object sender,string replyTo)
        {
           SignalX._signalXServers[handler].Invoke(message, sender, replyTo);
            //  Clients.All.addMessage(message);
        }

        public void GetMethods()
        {
            var methods = SignalX._signalXServers.Aggregate("var $sx= {", (current, signalXServer) => current + (signalXServer.Key +@":function(m,sen,repTo){ window.signalxid=window.signalxid||'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {    var r = Math.random()*16|0, v = c == 'x' ? r : (r&0x3|0x8);    return v.toString(16);});   sen=sen||window.signalxid;repTo=repTo||'';  chat.server.send('" + signalXServer.Key + "',m,sen,repTo);   },")) +"}; $sx; ";

            Clients.All.addMessage(methods);
        }

       
    }
}