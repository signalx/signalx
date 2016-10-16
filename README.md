![sxs](https://cloud.githubusercontent.com/assets/2102748/18841414/a912f0bc-83df-11e6-81ca-608ac62cac47.png) 

[![NuGet version](https://img.shields.io/nuget/v/Honjo.svg?style=flat-square)](https://www.nuget.org/packages/signalx)



# SignalX
Simplifying sigalr front and backend  setups

No more worrying about setup, cases, etc, just simple javascript to .NET lambda as a server


Backend :-

	SignalX.Server("Sample",fun request -> request.Respond("response"))	
	
FrontEnd :-
    
    signalx.server.sample("Hey",function(response){ console.log(response);});

MORE INFORMATION
==================================================================

Backend :-

    open System
    open Owin
    open Microsoft.Owin
    open SignalXLib.Lib
    open Microsoft.Owin.Hosting
	
    type public Startup() =
        member x.Configuration (app:IAppBuilder) = app.UseSignalX( new SignalX("")) |> ignore
		
    [<EntryPoint>]
    let main argv = 
    let url="http://localhost:44111"
    use server=WebApp.Start<Startup>(url)
	SignalX.Server("Sample",fun request -> request.Respond(request.ReplyTo))	
	
FrontEnd :-
	
Include scripts
----------------------------------------------------------------

      <script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.9.0.min.js"></script>     
      <script src="https://ajax.aspnetcdn.com/ajax/signalr/jquery.signalr-2.2.0.js"></script>
      <script src="https://unpkg.com/signalx@0.1.0-pre"></script>


Report debug information
=========================================================

      signalx.debug(function (o) { console.log(o); });
      signalx.error(function (o) { console.log(o); });
 
Do things only when connection is ready
=========================================================
 
    signalx.ready(function (server) {
      console.log("signalx is ready");
    });
 
    signalx.ready(function (server) {
       server.sample("GetSomething",function(something){ console.log(something);});
    });
 
Do things from anywhere, specify a callback
=========================================================

    signalx.server.sample("GetSomething",function(something){ console.log(something);});
 
Register handler
=========================================================

    signalx.server.sample("GetSomething","getSomethingCompleted");
 
    signalx.client.getSomethingCompleted = function (something) {
        console.log(something);
     };
 
 
Return a promise
=========================================================

    var getSomethingCompletedPromise = signalx.server.sample("GetSomething");
 
    getSomethingCompletedPromise.always(function (something) {
        console.log(something);
    });
 
 
 
 
 
 
 
 
