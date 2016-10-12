// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.
open System
open SignalXLib.Lib

[<EntryPoint>]
let main argv = 
    let server=new SignalX("http://localhost:44111","/ui")
    SignalX.Server("Sample",fun message -> SignalX.ClientPush("Myclient",message ))
    Console.ReadLine()
    server.Dispose()
    0 // return an integer exit code
