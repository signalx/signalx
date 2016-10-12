![sxs](https://cloud.githubusercontent.com/assets/2102748/18841414/a912f0bc-83df-11e6-81ca-608ac62cac47.png) 
# SignalX
simplifying sigalr front and backend  setups

No more worrying about setup, cases, etc, just simple javascript to .NET lambda as a server

Backend :-

    let server=new SignalX("http://localhost:44111","/ui")
    SignalX.Server("Sample",fun message -> SignalX.ClientPush("Myclient",message ))
    
FrontEnd:-
    
    signalx.client.myclient = function(message) {
       console.log(message);
    };

    signalx.ready(function (server) {
       server.sample("Hello, its sam");
    });
