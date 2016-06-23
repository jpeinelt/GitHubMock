open Suave
open Suave.Operators
open Suave.Successful
open Suave.Filters
open System.IO


[<EntryPoint>]
let main argv = 
    let json fileName =
        let content = File.ReadAllText fileName
        content.Replace("\r", "").Replace("\n", "")
        |> OK >=> Writers.setMimeType "application/json"

    let user = pathScan "/users/%s" (fun _ -> "user.json" |> json)
    let repos = pathScan "/users/%s/repos" (fun _ -> "repos.json" |> json)
    let mockApi = choose [repos; user]

    startWebServer defaultConfig mockApi
    0
