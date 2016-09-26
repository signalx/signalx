# SignalX
simplifying sigalr front and backend  setups

No more worrying about setup, cases, etc, just simple javascript to .NET lambda as a server

Backend :-

    let server=new SignalXApp()
    SignalX.SignalXServer("Sample",fun message -> SignalX.SendToClient("Myclient",message ))
    
FrontEnd:-
    
    signalx.client.myclient = function(message) {
       console.log(message);
    };

    signalx.ready(function (server) {
       server.sample("RR 1 Hi, its sam");
    });
