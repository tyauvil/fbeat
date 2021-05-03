open System

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Logging

open Giraffe

let webApp = 
    choose [
        GET >=> 
            choose [
                route "/" >=> warbler (fun _ -> json (Beats.beatsText()))
                route "/json" >=> warbler (fun _ -> json (Beats.beatsJson()))
                route "/healthz" >=> Successful.ok (text "OK")
    ]
]

let configureApp (app : IApplicationBuilder) =
    // Add Giraffe to the ASP.NET Core pipeline
    app.UseGiraffe webApp

let configureLogging (builder : ILoggingBuilder) =
    // Set up the Console logger
    builder.AddConsole() |> ignore

let configureServices (services : IServiceCollection) =
    // Add Giraffe dependencies
    services.AddGiraffe() |> ignore

[<EntryPoint>]
let main _ =
    WebHostBuilder()
        .UseKestrel()
        .Configure(Action<IApplicationBuilder> configureApp)
        .ConfigureServices(configureServices)
        .ConfigureLogging(configureLogging)
        .Build()
        .Run()
    0
