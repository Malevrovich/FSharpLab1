# Problem 9

<p>A Pythagorean triplet is a set of three natural numbers, $a \lt b \lt c$, for which,
$$a^2 + b^2 = c^2.$$</p>
<p>For example, $3^2 + 4^2 = 9 + 16 = 25 = 5^2$.</p>
<p>There exists exactly one Pythagorean triplet for which $a + b + c = 1000$.<br>Find the product $abc$.</p>

# Problem 22

<p>Using <a href="resources/documents/0022_names.txt">names.txt</a> (right click and 'Save Link/Target As...'), a 46K text file containing over five-thousand first names, begin by sorting it into alphabetical order. Then working out the alphabetical value for each name, multiply this value by its alphabetical position in the list to obtain a name score.</p>
<p>For example, when the list is sorted into alphabetical order, COLIN, which is worth $3 + 15 + 12 + 9 + 14 = 53$, is the $938$th name in the list. So, COLIN would obtain a score of $938 \times 53 = 49714$.</p>
<p>What is the total of all the name scores in the file?</p>

# Problem 9 solutions

1. Хвостовая рекурсия:
```f#
let tailRec () : int =
    let rec solve a b =
        let c = 1000 - b - a

        if not (a < b && b < c) then solve (a + 1) (a + 2)
        else if a * a + b * b = c * c then a * b * c
        else solve a (b + 1)

    solve 0 1
```
2. Модульное + map:
```f#
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
```
3. Спец. синтаксис для циклов:
```f#
let loops () =
    seq {
        for a in 1..1000 do
            for b in a + 1 .. 1000 do
                let c = 1000 - b - a

                if a * a + b * b = c * c then
                    yield a * b * c
    }
    |> Seq.head
```
4. Ленивый список
```f#
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
```
5. Результаты в тесте:
```f#
// in Problem9.fs
let solutions = [ tailRec; modular; loops; lazySolution ]

[<Fact>]
let ``Check Problem 9`` () =
    Problem9.solutions
    |> List.iter (fun solution ->
        let res: int = solution ()
        Assert.Equal(res, 31875000))
```
