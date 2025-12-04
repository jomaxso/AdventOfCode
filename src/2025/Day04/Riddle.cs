/*
* Riddle for 2025 Day 04 - https://adventofcode.com/2025/day/4
*/

var input = File.ReadAllLines("input.txt");

Console.WriteLine($"Part 1: {Part1(input)}");
Console.WriteLine($"Part 2: {Part2(input)}");

static long Part1(string[] input)
{
    int rows = input.Length;
    int cols = input[0].Length;

    var directions = GetDirections();

    return Enumerable.Range(0, rows)
        .SelectMany(row => Enumerable.Range(0, cols)
            .Select(col => (row, col)))
        .Where(pos => input[pos.row][pos.col] == '@')
        .Select(pos => new
        {
            pos,
            adjacentCount = directions.Count(d =>
            {
                int newRow = pos.row + d.dr;
                int newCol = pos.col + d.dc;
                return newRow >= 0 && newRow < rows &&
                       newCol >= 0 && newCol < cols &&
                       input[newRow][newCol] == '@';
            })
        })
        .Count(x => x.adjacentCount < 4);
}

static long Part2(string[] input)
{
    int rows = input.Length;
    int cols = input[0].Length;

    var grid = input
        .Select(line => line.ToCharArray())
        .ToArray();

    var directions = GetDirections();

    int totalRemoved = 0;

    while (true)
    {
        var accessible = Enumerable.Range(0, rows)
            .SelectMany(row => Enumerable.Range(0, cols)
                .Select(col => (row, col)))
            .Where(pos => grid[pos.row][pos.col] == '@')
            .Where(pos => directions
                .Count(d =>
                {
                    int newRow = pos.row + d.dr;
                    int newCol = pos.col + d.dc;

                    return newRow >= 0 && newRow < rows &&
                            newCol >= 0 && newCol < cols &&
                            grid[newRow][newCol] == '@';
                }) < 4)
            .ToArray();

        if (accessible.Length == 0)
            break;

        foreach (var pos in accessible)
        {
            grid[pos.row][pos.col] = '.';
        }

        totalRemoved += accessible.Length;
    }

    return totalRemoved;
}

static (int dr, int dc)[] GetDirections() => Enumerable
    .Range(-1, 3)
    .SelectMany(dr => Enumerable.Range(-1, 3)
        .Select(dc => (dr, dc)))
    .Where(d => d.dr != 0 || d.dc != 0)
    .ToArray();