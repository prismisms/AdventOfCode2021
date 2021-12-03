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
                var ones = contents.Count(x => x[column] == '1');
                var zeroes = contents.Count() - ones;

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
                    var (mostCommon, _) = GetMostAndLeastCommonBits(contents);
                    contents = contents.Where(x => x[column] == mostCommon[column]).ToList();
                }
            }
    
            var returnVal = Convert.ToInt32(contents[0], 2);
            return Convert.ToInt32(contents[0], 2);
        }

        private int GetCO2ScrubberRating() 
        {
            var contents = _fileContents;
            for (var column = 0; column < 12; column++)
            {
                if (contents.Count() > 1) 
                {
                    var (_, leastCommon) = GetMostAndLeastCommonBits(contents);
                    contents = contents.Where(x => x[column] == leastCommon[column]).ToList();
                }
            }

            var returnVal = Convert.ToInt32(contents.First(), 2);
            return Convert.ToInt32(contents.First(), 2);
        }
    }
}
