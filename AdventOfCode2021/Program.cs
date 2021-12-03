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

static (int, int) GetResultsForDay(int day) => day switch
{
    1 => new Day1("Data/Day1.txt").GetDay1Result(),
    2 => new Day2("Data/Day2.txt").GetDay2Result(),
    3 => new Day3("Data/Day3.txt").GetDay3Result(),
    4 => new Day4("Data/Day4.txt").GetDay4Result(),
    _ => throw new ArgumentOutOfRangeException($"Day {day} is either out of scope or not yet implemented")
};
