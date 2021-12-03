using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AdventOfCode2021
{
    public class Day3
    {
        private readonly List<string> _fileContents;

        public Day3(string fileLocation)
        {
            _fileContents = FileHandler.GetFileContentsAsStrings(fileLocation);
        }

        public (int, int) GetDay3Result()
        {
            return (GetPowerConsumption(), GetPart2Value());
        }

        public (string, string) GetMostAndLeastCommonBits(List<string> contents)
        {
            var mostCommon = new StringBuilder();
            var leastCommon = new StringBuilder();

            var numberOfRows = contents.Count();

            for (var column = 0; column < 12; column++)
            {
                var ones = 0;
                var zeroes = 1;

                for (var i = 0; i < numberOfRows; i++)
                {
                    if (contents[i][column] == '0')
                    {
                        zeroes++;
                    }
                    else
                    {
                        ones++;
                    }
                }

                if (zeroes > ones)
                {
                    mostCommon.Append("0");
                    leastCommon.Append("1");
                }
                else
                {
                    mostCommon.Append("1");
                    leastCommon.Append("0");
                }
            }

            return (mostCommon.ToString(), leastCommon.ToString());
        }

        public int GetPowerConsumption() 
        {
            var (mostCommon, leastCommon) = GetMostAndLeastCommonBits(_fileContents);
            var gammaRate = Convert.ToInt32(mostCommon, 2);
            var epsilonRate = Convert.ToInt32(leastCommon, 2);

            return gammaRate*epsilonRate;
        }

        public int GetPart2Value()
        {
            return GetOxygenRating()*GetCO2ScrubberRating();
        }

        private int GetOxygenRating() 
        {
            var contents = _fileContents;
            for (var column = 0; column < 12; column++)
            {
                if (contents.Count() > 1) 
                {
                    var (mostCommon, leastCommon) = GetMostAndLeastCommonBits(contents);
                    var content = new List<string>();
                    var bit = mostCommon[column];
                    contents.ForEach(x => 
                    {
                        if (x[column] == bit) 
                        {
                            content.Add(x);
                        }
                    });
                    contents = content;
                }
            }
            return Convert.ToInt32(contents.First(), 2);
        }

        private int GetCO2ScrubberRating() 
        {
            var contents = _fileContents;
            for (var column = 0; column < 12; column++)
            {
                if (contents.Count() > 1) 
                {
                    var (mostCommon, leastCommon) = GetMostAndLeastCommonBits(contents);
                    var content = new List<string>();
                    var bit = leastCommon[column];
                    contents.ForEach(x => 
                    {
                        if (x[column] == bit) 
                        {
                            content.Add(x);
                        }
                    });
                    contents = content;
                }
            }
            return Convert.ToInt32(contents.First(), 2);
        }
    }
}
