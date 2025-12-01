/*
* Riddle for 2025 Day 01 - https://adventofcode.com/2025/day/1
*/

var input = File.ReadAllLines("input.txt")
    .Select(ParseInstruction)
    .ToArray();

Console.WriteLine($"Part 1: {Part1(input)}");
Console.WriteLine($"Part 2: {Part2(input)}");

static KeyValuePair<int, int> ParseInstruction(string line) => line switch
{
    ['R', .. var numbers] => new(1, int.Parse(numbers)),
    ['L', .. var numbers] => new(-1, int.Parse(numbers)),
    _ => throw new InvalidOperationException($"Invalid input: {line}")
};

static int Part1(IEnumerable<KeyValuePair<int, int>> input)
{
    return GetPositions(50)
        .Count(x => x == 0);

    IEnumerable<int> GetPositions(int position)
    {
        foreach (var item in input)
        {
            position += item.Key * item.Value;
            position = WrapPosition(position);

            yield return position;
        }
    }
}

static int Part2(IEnumerable<KeyValuePair<int, int>> input)
{
    return GetPositions(50)
        .Count(x => x == 0);

    IEnumerable<int> GetPositions(int position)
    {
        foreach (var item in input)
        {
            for (int i = 0; i < item.Value; i++)
            {
                position += item.Key;
                position = WrapPosition(position);

                yield return position;
            }
        }
    }
}

static int WrapPosition(int position) => ((position % 100) + 100) % 100;