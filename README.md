![sxs](https://cloud.githubusercontent.com/assets/2102748/18841414/a912f0bc-83df-11e6-81ca-608ac62cac47.png) 
# SignalX
simplifying sigalr front and backend  setups

No more worrying about setup, cases, etc, just simple javascript to .NET lambda as a server


Backend :-

	SignalX.Server("Sample",fun request -> request.Respond(request.ReplyTo))	
	
FrontEnd :-
    
    signalx.server.sample("Hey",function(response){ console.log(response);});

MORE INFORMATION
==================================================================

Backend :-

    type public Startup() =
        member x.Configuration (app:IAppBuilder) = app.UseSignalX( new SignalX("")) |> ignore
		
    [<EntryPoint>]
    let main argv = 
    let url="http://localhost:44111"
    use server=WebApp.Start<Startup>(url)
	SignalX.Server("Sample",fun request -> request.Respond(request.ReplyTo))	
	
FrontEnd :-
    
    signalx.server.sample("Hey",function(response){ console.log(response);});	
	
MORE INFORMATION
==================================================================
	
Include scripts
----------------------------------------------------------------

      <script src="jquery.min.js"></script>      
      <script src="jquery.signalr.min.js">
      </script><script src="signalx.js"></script>


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
 
Do things from anywhere
=========================================================

 signalx.server.sample("GetSomething",function(something){ console.log(something);});
 
Specify callback method
=========================================================

 signalx.server.sample("GetSomething","getSomethingCompleted");
 
 signalx.client.getSomethingCompleted = function (something) {
        console.log(something);
    };
 
 
Specify a promise
=========================================================

 var getSomethingCompletedPromise = signalx.server.sample("GetSomething");
 
 getSomethingCompletedPromise.always(function (something) {
        console.log(something);
    });
 
 
 
 
 
 
 
 
