// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System
open SignalX.SignalXLib

[<EntryPoint>]
let main argv = 
    let server=new SignalXApp()
    SignalX.SignalXServer("Sample",fun message -> SignalX.SendToClient("Myclient",message ))
    let y=Console.ReadLine()
    server.Dispose()
    0 // return an integer exit code
