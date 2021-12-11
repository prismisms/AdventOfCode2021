using AdventOfCode2021;

GetSolutions();

static void GetSolutions()
{
    while (true)
    {
        Console.WriteLine("Type a day to generate solutions, or anything that isn't a number to quit: ");
        var dayInput = Console.ReadLine();

        if (int.TryParse(dayInput, out var day))
        {
            var (part1, part2) = GetResultsForDay(day);
            Console.WriteLine(@$"Day {day} Part 1: {part1}");
            Console.WriteLine(@$"Day {day} Part 2: {part2}");
        }
        else
        {
            Console.WriteLine("Found an input that wasn't a number. Exiting..");
            Environment.Exit(0);
        }
    }
}

static (long, long) GetResultsForDay(int day) => day switch
{
    1 => new Day1(GetFilePath(day)).GetResult(),
    2 => new Day2(GetFilePath(day)).GetResult(),
    3 => new Day3(GetFilePath(day)).GetResult(),
    4 => new Day4(GetFilePath(day)).GetResult(),
    5 => new Day5(GetFilePath(day)).GetResult(),
    6 => new Day6(GetFilePath(day)).GetResult(),
    7 => new Day7(GetFilePath(day)).GetResult(),
    8 => new Day8(GetFilePath(day)).GetResult(),
    9 => new Day9(GetFilePath(day)).GetResult(),
    10 => new Day10(GetFilePath(day)).GetResult(),
    11 => new Day11(GetFilePath(day)).GetResult(),
    _ => throw new ArgumentOutOfRangeException($"Day {day} is either out of scope or not yet implemented")
};

static string GetFilePath(int dayNumber)
{
    return $@"Data/Day{dayNumber}.txt";
}
