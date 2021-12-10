using System;
namespace AdventOfCode2021
{
    public class Day10 : DayBase<string, int>
    {
        private readonly Dictionary<char, int> _pointsRef = SetPoints();

        private readonly string _leftBrackets = "{[(<";
        private readonly string _rightBrackets = "}])>";
        private readonly string _allBrackets = "{[(<}])>";

        private readonly List<string> bracketPairs = new List<string>
        {
            "[]",
            "()",
            "{}",
            "<>"
        };

        public Day10(string fileLocation) : base(FileHandler.GetFileContentsAsStrings(fileLocation))
        {
        }

        public override int Part1()
        {
            var illegalBracketValue = 0;
            FileContents.ForEach(line =>
            {
                var remainingLine = RemoveChunks(line);
                if (remainingLine.All(x => _leftBrackets.Contains(x)))
                {
                    // This is just an incomplete line
                    return;
                }
                else
                {
                    var firstIllegalCharacter = remainingLine.First(x => _rightBrackets.Contains(x));
                    illegalBracketValue += _pointsRef[firstIllegalCharacter];
                }
      
            });

            return illegalBracketValue;
        }

        private string RemoveChunks(string line)
        {
            bracketPairs.ForEach(pair =>
            {
                line = line.Replace(pair, "");
            });

            if (bracketPairs.Any(pair => line.Contains(pair)))
            {
                line = RemoveChunks(line);
            }

            return line;
        }

        public override int Part2()
        {
            return 0;
        }

        private static Dictionary<char, int> SetPoints()
        {
            return new Dictionary<char, int>
            {
                { ')', 3 },
                { ']', 57 },
                { '}', 1197 },
                { '>', 25137 },
                { '(', 3 },
                { '[', 57 },
                { '{', 1197 },
                { '<', 25137 }
            };
        }
    }
}

