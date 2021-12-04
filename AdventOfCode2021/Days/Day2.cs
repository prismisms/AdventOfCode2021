namespace AdventOfCode2021
{
    public class Day2 : DayBase<string>
    {
        private readonly List<(string, int)> _formattedFileContents;

        public Day2(string fileLocation) : base(FileHandler.GetFileContentsAsStrings(fileLocation))
        {
            _formattedFileContents = SplitFile();
        }

        // Calculates horizontal and vertical depth
        public override int Part1()
        {
            var horizontalPosition = 0;
            var verticalPosition = 0;
            _formattedFileContents.ForEach(line =>
            {
                var (direction, distance) = line;
                switch (direction)
                {
                    case "forward":
                        horizontalPosition += distance;
                        break;
                    case "down":
                        verticalPosition += distance;
                        break;
                    case "up":
                        verticalPosition -= distance;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"That's not a direction I know..");
                }
            });

            return horizontalPosition * verticalPosition;
        }

        // Calculates horizontal and vertical depth, taking aim into account
        public override int Part2()
        {
            var horizontalPosition = 0;
            var verticalPosition = 0;
            var aim = 0;
            _formattedFileContents.ForEach(line =>
            {
                var (direction, distance) = line;
                switch (direction)
                {
                    case "forward":
                        if (aim == 0)
                        {
                            horizontalPosition += distance;
                        }
                        else
                        {
                            horizontalPosition += distance;
                            verticalPosition += distance * aim;
                        }

                        break;
                    case "down":
                        aim += distance;
                        break;
                    case "up":
                        aim -= distance;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"That's not a direction I know..");
                }
            });

            return horizontalPosition * verticalPosition;
        }

        private List<(string, int)> SplitFile()
        {
            var result = new List<(string, int)>();
            FileContents.ForEach(x =>
            {
                var splitLine = x.Split(' ');
                result.Add((splitLine[0], int.Parse(splitLine[1])));
            });

            return result;
        }
    }
}
