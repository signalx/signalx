![sxs](https://cloud.githubusercontent.com/assets/2102748/18841414/a912f0bc-83df-11e6-81ca-608ac62cac47.png) 
# SignalX
simplifying sigalr front and backend  setups

No more worrying about setup, cases, etc, just simple javascript to .NET lambda as a server

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
