open System

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Hosting

open Giraffe

let beats() =
    let utc = 
        (DateTime.UtcNow.AddHours(1.0).TimeOfDay.TotalSeconds
        |> float) * 0.011_574 
        |> int


    let prefix = match utc with
                 | i when i < 10 -> "00"
                 | i when i < 100 -> "0"
                 | _ -> ""
    
    sprintf "@%s%i.beats" prefix utc

let webApp = route "/" >=> warbler (fun _ -> text (beats()))

let configureApp (app : IApplicationBuilder) =
    // Add Giraffe to the ASP.NET Core pipeline
    app.UseGiraffe webApp

let configureServices (services : IServiceCollection) =
    // Add Giraffe dependencies
    services.AddGiraffe() |> ignore

[<EntryPoint>]
let main _ =
    WebHostBuilder()
        .UseKestrel()
        .Configure(Action<IApplicationBuilder> configureApp)
        .ConfigureServices(configureServices)
        .Build()
        .Run()
    0
