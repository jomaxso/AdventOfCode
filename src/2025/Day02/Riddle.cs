/*
* Riddle for 2025 Day 02 - https://adventofcode.com/2025/day/2
*/

var input = File.ReadAllText("input.txt")
    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
    .Select(range => range.Split('-', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
    .Select(parts => (long.Parse(parts[0]), long.Parse(parts[1])))
    .ToArray();

Console.WriteLine($"Part 1: {Part1(input)}");
Console.WriteLine($"Part 2: {Part2(input)}");

static long Part1(IEnumerable<(long Start, long End)> input)
{
    long result = 0;

    foreach (var (Start, End) in input)
    {
        for (long i = Start; i <= End; i++)
        {
            if (IsMirroredPattern(i))
            {
                result += i;
            }
        }
    }

    return result;

    static bool IsMirroredPattern(long number)
    {
        ReadOnlySpan<char> s = number.ToString();

        if (s.Length % 2 != 0)
            return false;

        var halfLength = s.Length / 2;

        return s[..halfLength].SequenceEqual(s[halfLength..]);
    }
}

static long Part2(IEnumerable<(long Start, long End)> input)
{
    long result = 0;

    foreach (var (Start, End) in input)
    {
        for (long i = Start; i <= End; i++)
        {
            if (IsRepeadedPattern(i))
            {
                result += i;
            }
        }
    }

    return result;

    static bool IsRepeadedPattern(long number)
    {
        ReadOnlySpan<char> s = number.ToString();

        int length = s.Length;

        for (int patternLength = 1; patternLength <= length / 2; patternLength++)
        {
            if (length % patternLength != 0)
                continue;

            var pattern = s[..patternLength];
            bool matches = true;

            for (int pos = patternLength; pos < length; pos += patternLength)
            {
                var segment = s.Slice(pos, patternLength);

                if (!segment.SequenceEqual(pattern))
                {
                    matches = false;
                    break;
                }
            }

            if (matches)
                return true;
        }

        return false;
    }
}