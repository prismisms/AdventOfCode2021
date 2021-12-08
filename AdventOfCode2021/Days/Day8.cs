using System.Linq;
using System.Text;

namespace AdventOfCode2021
{
    public class Day8 : DayBase<string, int>
    {
        private readonly Dictionary<int, string> _segments = new();

        public Day8(string fileLocation) : base(FileHandler.GetFileContentsAsStrings(fileLocation).ToList())
        {
            _segments = new Dictionary<int, string>
            {
                { 0, "abcefg"},
                { 1, "cf" },
                { 2, "acdeg" },
                { 3, "acdfg" },
                { 4, "bcdf" },
                { 5, "abdfg" },
                { 6, "abdefg" },
                { 7, "acf" },
                { 8, "abcdefg" }

            };
        }

        public override int Part1()
        {
            var segments = new List<string>();
            var split = new List<string[]>();
            FileContents.ForEach(line => split.Add(line.Split(" | ")));
           
            split.ForEach(line => line[1].Split(' ').ToList().ForEach(x => segments.Add(x)));

            var result = 0;
            var numbers = new List<int> {1, 4, 7, 8};

            numbers.ForEach(number => result += segments.Count(x => x.Length == _segments[number].Length));

            return result;
        }

        // This sucks and I hate it, must be nicer intersections..
        public override int Part2()
        {
            var result = 0;

            foreach (var line in FileContents)
            {
                var signalPatterns = line.Split(" | ")[0].Split(' ').ToList();
                var output = line.Split(" | ")[1].Split(' ');

                // First set ezpz
                var numbers = new List<int> { 1, 7, 4, 8 };
                numbers.ForEach(number =>
                    _segments[number] = signalPatterns.First(x => x.Length == _segments[number].Length));

                // Following sets not ezpz
                // Start with 6, as it's the easiest to find from what we already know
                _segments[6] = signalPatterns.First(x => x.Length == 6 && x.Intersect(_segments[1]).Count() == 1);

                // Leftover  from the intersection must be bottom right
                var bottomRightCharacter = _segments[1].Intersect(_segments[6]).First();
                // And the other one left from the segment numbered 1 is top right
                var topRightCharacter = _segments[1].First(x => x != bottomRightCharacter);

                _segments[2] = signalPatterns.First(x => x.Length == 5 && x.Contains(topRightCharacter) && !x.Contains(bottomRightCharacter));
                _segments[3] = signalPatterns.First(x => x.Length == 5 &&x.Contains(topRightCharacter) && x.Contains(bottomRightCharacter));
                _segments[5] = signalPatterns.First(x => x.Length == 5 && !x.Contains(topRightCharacter) && x.Contains(bottomRightCharacter));

                var bottomLeftCharacter = _segments[2].First(x => !_segments[5].Contains(x) && x != topRightCharacter);
                _segments[0] = signalPatterns.First(x => x.Length == 6 && x != _segments[6] && x.Contains(bottomLeftCharacter));
                _segments[9] = signalPatterns.First(x => x.Length == 6 && x != _segments[6] && x != _segments[0]);

                var outputEntry = new StringBuilder();
                foreach (var segmentString in output)
                {
                    for (var i = 0; i < 10; i++)
                    {
                        if (_segments[i].Length == segmentString.Length &&
                                _segments[i].Intersect(segmentString).Count() == segmentString.Length)
                        {
                            outputEntry.Append(i);
                            break;
                        }
                    }
                }

                result += int.Parse(outputEntry.ToString());
            }

            return result;
        }
    }
}
