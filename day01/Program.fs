
open System
open System.IO

let part1ExInput = "1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet"

let part2ExInput = "two1nine
eightwothree
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen"

let input = 
    use reader = new StreamReader(new FileStream(@"..\..\..\input-day01.txt", FileMode.Open))
    reader.ReadToEnd()

let toLines (str:string) = (str.Split("\n")) |> Seq.where (fun s -> not (String.IsNullOrWhiteSpace(s)))

let part1 lines =
    let first = lines |> Seq.map (Seq.find (fun c -> c >= '0' && c <= '9'))
    let last = lines |> Seq.map (Seq.findBack (fun c -> c >= '0' && c <= '9'))

    Seq.map2 (fun a b -> string(a)+string(b)) first last
        |> Seq.map int
        |> Seq.sum

let part2 lines =
    let vals = [
        "zero"; "one"; "two"; "three"; "four"; "five"; "six"; "seven"; "eight"; "nine";
        "0"; "1"; "2"; "3"; "4"; "5"; "6"; "7"; "8"; "9" ]

    let dropWalk (str:string) = seq { for i in 1..str.Length do str.Substring(i - 1) }
    let dropWalkBack (str:string) = seq { for i in 1..str.Length do str.Substring(0, str.Length - i + 1) }

    let first = 
        lines
        |> Seq.map (
            fun l -> 
                l
                |> dropWalk
                |> Seq.map (
                    fun s ->
                        vals |> Seq.tryFind (fun v -> s.StartsWith(v))
                )
                |> Seq.find (fun x -> x.IsSome)
                |> Option.get
            )
    let last = 
        lines
        |> Seq.map (
            fun l -> 
                l
                |> dropWalkBack
                |> Seq.map (
                    fun s ->
                        vals |> Seq.tryFind (fun v -> s.EndsWith(v))
                )
                |> Seq.find (fun x -> x.IsSome)
                |> Option.get
            )

    Seq.map2 (
        fun a b ->
            10 * ((vals |> Seq.findIndex(fun x -> a = x)) % 10)
            + ((vals |> Seq.findIndex(fun x -> b = x)) % 10)) first last
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
printfn "Press any key to exit"
System.Console.ReadKey() |> ignore
