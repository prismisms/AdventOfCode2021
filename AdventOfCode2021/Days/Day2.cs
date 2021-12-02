namespace AdventOfCode2021
{
    public class Day2
    {
        private readonly List<(string, int)> _fileContents;

        public Day2(string fileLocation)
        {
            _fileContents = ParseFile(fileLocation);
        }

        public (int, int) GetDay2Result()
        {
            return (CalculateHorizontalAndVerticalDepth(), CalculateHorizontalAndVerticalDepthBasedOnAim());
        }

        private int CalculateHorizontalAndVerticalDepth()
        {
            var horizontalPosition = 0;
            var verticalPosition = 0;
            _fileContents.ForEach(line =>
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

        private int CalculateHorizontalAndVerticalDepthBasedOnAim()
        {
            var horizontalPosition = 0;
            var verticalPosition = 0;
            var aim = 0;
            _fileContents.ForEach(line =>
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

        private static List<(string, int)> ParseFile(string fileLocation)
        {
            var result = new List<(string, int)>();
            var lines = FileHandler.GetFileContentsAsStrings(fileLocation);
            lines.ForEach(x =>
            {
                var splitLine = x.Split(' ');
                result.Add((splitLine[0], int.Parse(splitLine[1])));
            });

            return result;
        }
    }
}
