for a in range(1, 1001):
    for b in range(a + 1, 1001):
        c = 1000 - b - a
        if a**2 + b**2 == c**2:
            print(a * b * c)
            break
