/*
* Riddle for 2025 Day 03 - https://adventofcode.com/2025/day/3
*/

var input = File.ReadAllLines("input.txt");

Console.WriteLine($"Part 1: {Part1(input)}");
Console.WriteLine($"Part 2: {Part2(input)}");

static long Part1(string[] banks)
{
    long result = 0;

    foreach (ReadOnlySpan<char> bank in banks.AsSpan())
    {
        result += ExtractHighestTwoDigits(bank);
    }

    return result;

    static int ExtractHighestTwoDigits(ReadOnlySpan<char> bank)
    {
        char firstDigit = (char)('0' - 1);
        char secondDigit = (char)('0' - 1);

        for (var i = 0; i < bank.Length; i++)
        {
            var digit = bank[i];

            if (digit > firstDigit && i < bank.Length - 1)
            {
                firstDigit = digit;
                secondDigit = (char)('0' - 1);
                continue;
            }

            if (digit > secondDigit)
            {
                secondDigit = digit;
                continue;
            }
        }

        return int.Parse([firstDigit, secondDigit]);
    }
}

static long Part2(string[] banks)
{
    long result = 0;

    foreach (ReadOnlySpan<char> bank in banks.AsSpan())
    {
        result += ExtractLargestJoltage(bank, 12);
    }

    return result;

    static long ExtractLargestJoltage(ReadOnlySpan<char> bank, int numberOfDigitsToExtract)
    {
        int removeableCount = bank.Length - numberOfDigitsToExtract;

        Span<char> stack = stackalloc char[bank.Length];
        int stackPointer = 0;

        for (int i = 0; i < bank.Length; i++)
        {
            char currentDigit = bank[i];

            while (removeableCount > 0 && stackPointer > 0 && currentDigit > stack[stackPointer - 1])
            {
                stackPointer--;
                removeableCount--;
            }

            stack[stackPointer] = currentDigit;
            stackPointer++;
        }

        // Entferne überschüssige Ziffern vom Ende des Stacks
        stackPointer -= removeableCount;

        return long.Parse(stack[..numberOfDigitsToExtract]);
    }
}