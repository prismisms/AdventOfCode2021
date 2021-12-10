using System;
namespace AdventOfCode2021
{
    public class Day10 : DayBase<string, long>
    {
        private readonly Dictionary<char, int> _illegalScores = SetIllegalScores();
        private readonly Dictionary<char, int> _autocompleteScores = SetAutocompleteScores();

        private readonly string _leftBrackets = "{[(<";
        private readonly string _rightBrackets = "}])>";

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

        public override long Part1()
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
                    illegalBracketValue += _illegalScores[firstIllegalCharacter];
                }
      
            });

            return (long)illegalBracketValue;
        }

        public override long Part2()
        {

            var incompleteLines = GetIncompleteLines();

            var incompleteLineScores = incompleteLines.Select(line =>
            {
                var value = (long)0;

                // Holy shit finally a situation where I need to reverse a string
                var reversedLine = Reverse(line);

                reversedLine.ToList().ForEach(character =>
                { 
                    value = (value * 5) + _autocompleteScores[character];
                });

                return value;

            }).ToList().OrderByDescending(x => x).ToList();

            return incompleteLineScores[(incompleteLineScores.Count - 1) / 2]);
        }

        private List<string> GetIncompleteLines()
        {
            var incompleteLines = new List<string>();
            FileContents.ForEach(line =>
            {
                var remainingLine = RemoveChunks(line);
                if (remainingLine.All(x => _leftBrackets.Contains(x)))
                {
                    // This is just an incomplete line
                    incompleteLines.Add(remainingLine);
                }
            });

            return incompleteLines;
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

        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private static Dictionary<char, int> SetIllegalScores()
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

        private static Dictionary<char, int> SetAutocompleteScores()
        {
            return new Dictionary<char, int>()
            {
                { '(', 1 },
                { '[', 2 },
                { '{', 3 },
                { '<', 4 }
            };
        }
    }
}

