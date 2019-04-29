module Api.App

open System
open System.IO
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Cors.Infrastructure
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Microsoft.AspNetCore.Http
open System.Threading.Tasks
open FSharp.Control.Tasks.V2

type IUserService =
    abstract getUser:     Guid -> Task<User>
    abstract createUser:  User -> Task<unit>
    abstract deleteUser:  Guid -> Task<unit>
    abstract getAllUsers: unit -> Task<User array>

and User = 
    { Id   : Guid 
      Name : string }

let userService: IUserService = failwith "mock"

type Request = 
    | CreateUser of User
    | GetUser    of Guid
    | DeleteUser of Guid
    | GetAllUsers

type Response = 
    | UserCreated
    | UserDeleted
    | User of User
    | AllUsers of User array
    | RequestFailed of Request * exn

let processRequest = function
    | CreateUser user -> task {
        do! userService.createUser user
        return UserCreated }

    | GetUser userId -> task {
        let! user = userService.getUser userId
        return User user }

    | DeleteUser userId -> task {
        do! userService.deleteUser userId
        return UserDeleted }

    | GetAllUsers -> task {
        let! users = userService.getAllUsers()
        return AllUsers users }

let tryProcess req next ctx =
    task {
        try 
            let! resp = processRequest req
            return! Successful.OK resp next ctx
        with e -> 
            let resp = RequestFailed (req,e)
            return! ServerErrors.internalError (json resp) next ctx
    }

let createUser (next : HttpFunc) (ctx : HttpContext) = 
    task {
        let! user = ctx.BindJsonAsync<User>()
        return! tryProcess (CreateUser user) next ctx
    }

let webApi =
    choose [
        GET  >=> routef "/user/%O" (GetUser >> tryProcess)
        GET  >=> route  "/user/" >=> tryProcess GetAllUsers
        POST >=> route  "/user/" >=> createUser
        POST >=> routef "/user/%O/delete" (DeleteUser >> tryProcess)
    ]

// ---------------------------------
// Config and Main
// ---------------------------------

let configureCors (builder : CorsPolicyBuilder) =
    builder.WithOrigins("http://localhost:8080")
           .AllowAnyMethod()
           .AllowAnyHeader()
           |> ignore

let configureApp (app : IApplicationBuilder) =
    app.UseHttpsRedirection()
       .UseCors(configureCors)
       .UseGiraffe(webApi)

let configureServices (services : IServiceCollection) =
    services.AddCors()    |> ignore
    services.AddGiraffe() |> ignore

let configureLogging (builder : ILoggingBuilder) =
    let filter (l : LogLevel) = l.Equals LogLevel.Error
    builder.AddFilter(filter).AddConsole().AddDebug() |> ignore

[<EntryPoint>]
let main _ =
    let contentRoot = Directory.GetCurrentDirectory()
    let webRoot     = Path.Combine(contentRoot, "WebRoot")
    WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(contentRoot)
        .UseIISIntegration()
        .UseWebRoot(webRoot)
        .Configure(Action<IApplicationBuilder> configureApp)
        .ConfigureLogging(configureLogging)
        .Build()
        .Run()
    0