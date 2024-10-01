module Problem9

let tailRec () : int =
    let rec solve a b =
        let c = 1000 - b - a

        if not (a < b && b < c) then solve (a + 1) (a + 2)
        else if a * a + b * b = c * c then a * b * c
        else solve a (b + 1)

    solve 0 1

exception NotFoundException of string

let modular () =
    let a = [ 1..1000 ]
    let b = [ 1..1000 ]

    List.allPairs a b
    |> List.filter (fun (a, b: int) -> a < b)
    |> List.filter (fun (a: int, b) -> b < 1000 - a - b)
    |> List.map (fun (a, b) -> (a, b, 1000 - a - b))
    |> List.filter (fun (a, b: int, c) -> a * a + b * b = c * c)
    |> (fun list ->
        match list with
        | (a, b, c) :: tail -> (a * b * c)
        | [] -> raise (NotFoundException("No solution")))

let loops () =
    seq {
        for a in 1..1000 do
            for b in a + 1 .. 1000 do
                let c = 1000 - b - a

                if a * a + b * b = c * c then
                    yield a * b * c
    }
    |> Seq.head

let lazySolution () =
    let aSeq = (+) 1 |> Seq.init 1000
    let bSeq = (+) 1 |> Seq.init 1000
    let cSeq = (+) 1 |> Seq.init 1000

    Seq.allPairs aSeq bSeq
    |> Seq.allPairs cSeq
    |> Seq.map (fun (a, (b, c)) -> (a, b, c))
    |> Seq.filter (fun (a, b, c) -> a * a + b * b = c * c)
    |> Seq.find (fun (a, b, c) -> a + b + c = 1000)
    |> fun (a, b, c) -> a * b * c

let solutions = [ tailRec; modular; loops; lazySolution ]
