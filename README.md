![sxs](https://cloud.githubusercontent.com/assets/2102748/18841414/a912f0bc-83df-11e6-81ca-608ac62cac47.png) 
# SignalX
simplifying sigalr front and backend  setups

No more worrying about setup, cases, etc, just simple javascript to .NET lambda as a server

Backend :-

    let server=new SignalX()
    SignalX.Server("Sample", fun request -> request.Respond("Hi"));
    
FrontEnd :-
    
    signalx.server.sample("Hey",function(response){ console.log(response);});
