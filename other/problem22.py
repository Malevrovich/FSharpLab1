def parseLine(line: str) -> list[str]:
    return list(map(lambda x: x.strip('"'), line.split(",")))


def namesScore(name: str) -> int:
    res = 0
    for c in name.lower():
        res += ord(c) - ord("a") + 1
    return res


with open("../resources/0022_names.txt") as f:
    names = []
    for line in f.readlines():
        names += parseLine(line)

    names = sorted(names)
    res = 0
    for i, name in enumerate(names, start=1):
        res += i * namesScore(name)
    print(res)
