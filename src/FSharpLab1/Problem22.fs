module Problem22

open System.IO

let namesPath =
    Path.Combine(__SOURCE_DIRECTORY__, "..", "..", "resources", "0022_names.txt")

let parseLine (line: string) =
    line.Split(',') |> Array.map (fun line -> line.Trim('"'))

let getNames path =
    let data = File.ReadLines path
    data |> Seq.collect parseLine

let getSortedEnumeratedNames path =
    getNames path |> Seq.sort |> Seq.mapi (fun i name -> (i + 1, name))

let nameScore (name: string) =
    name.ToLower() |> Seq.fold (fun acc char -> acc + (int char - int 'a' + 1)) 0

let modal sortedEnumeratedNames =
    sortedEnumeratedNames
    |> Seq.fold (fun (acc: int) (idx, name) -> acc + idx * nameScore name) 0

let recursive sortedEnumeratedNames =
    let rec solution names =
        match names with
        | [] -> 0
        | (idx, name) :: tail -> idx * nameScore name + solution tail

    sortedEnumeratedNames |> Seq.toList |> solution

let tailRecursive sortedEnumeratedNames =
    let rec solution acc names =
        match names with
        | [] -> acc
        | (idx, name) :: tail -> solution (acc + idx * nameScore name) tail

    sortedEnumeratedNames |> Seq.toList |> solution 0

let solutions = [ modal; recursive; tailRecursive ]
