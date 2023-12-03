
open System
open System.IO

let part1ExInput = "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"

let part2ExInput = part1ExInput

let input = 
    use reader = new StreamReader(new FileStream(@"input-day02.txt", FileMode.Open))
    reader.ReadToEnd()

let toLines (str:string) = (str.Split("\n")) |> Seq.where (fun s -> not (String.IsNullOrWhiteSpace(s)))

let part1 lines =
    let cube_count = dict [
        ("red", 12)
        ("green", 13)
        ("blue", 14)
    ]

    let rounds (line:string) = line.Split(":").[1].Split(";") |> Seq.map (fun s -> s.Trim())
    let round_dict (round:string) = 
        round.Split(",")
        |> Seq.map (fun s -> s.Trim())
        |> Seq.map (fun s -> s.Split(" "))
        |> Seq.map (fun a -> (a.[1], int <| a.[0]))
        |> Map.ofSeq

    lines
    |> Seq.mapi (fun i g -> (i+1, rounds g))
    |> Seq.map (fun (i, rounds) -> (i, (rounds |> Seq.map round_dict)))
    |> Seq.where (fun (_, rounds) -> 
        rounds |> Seq.forall (fun round -> round |> Seq.forall (fun color -> color.Value <= cube_count.[color.Key])))
    |> Seq.map (fun (i, _) -> i)
    |> Seq.sum

let part2 lines =
    let rounds (line:string) = line.Split(":").[1].Split(";") |> Seq.map (fun s -> s.Trim())
    let round_dict (round:string) = 
        round.Split(",")
        |> Seq.map (fun s -> s.Trim())
        |> Seq.map (fun s -> s.Split(" "))
        |> Seq.map (fun a -> (a.[1], int <| a.[0]))
        |> Map.ofSeq

    lines
    |> Seq.map (fun g -> rounds g)
    |> Seq.map (fun rounds -> (rounds |> Seq.map round_dict))
    |> Seq.map (fun rounds -> 
        rounds
        |> Seq.concat
        |> Seq.groupBy (fun x -> x.Key))
    |> Seq.map (fun rounds ->
        rounds
        |> Seq.map (fun (color, blocks) -> (blocks |> Seq.map (fun x -> x.Value) |> Seq.max))
        |> Seq.fold (fun state i -> state * i) 1)
    |> Seq.sum



let part1Ex = toLines part1ExInput |> part1
printfn "Part 1 Example: %d" part1Ex
let part1Result = toLines input |> part1
printfn "Part 1 Result: %d" part1Result


let part2Ex = toLines part2ExInput |> part2
printfn "Part 2 Example: %d" part2Ex
let part2Result = toLines input |> part2
printfn "Part 2 Result: %d" part2Result

printfn ""
// printfn "Press ENTER to exit"
// Console.Read() |> ignore
