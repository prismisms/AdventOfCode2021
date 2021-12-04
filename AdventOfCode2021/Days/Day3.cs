using System.Text;

namespace AdventOfCode2021
{
    public class Day3 : DayBase<string>
    {
        public Day3(string fileLocation) : base(FileHandler.GetFileContentsAsStrings(fileLocation))
        {
        }

        // Gets power consumption
        public override int Part1() 
        {
            var (mostCommon, leastCommon) = GetMostAndLeastCommonBits(FileContents);
            var gammaRate = Convert.ToInt32(mostCommon, 2);
            var epsilonRate = Convert.ToInt32(leastCommon, 2);

            return gammaRate*epsilonRate;
        }
        public override int Part2()
        {
            return GetOxygenRating()*GetCO2ScrubberRating();
        }

        private static (string, string) GetMostAndLeastCommonBits(IReadOnlyCollection<string> contents)
        {
            var mostCommon = new StringBuilder();
            var leastCommon = new StringBuilder();

            for (var column = 0; column < contents.First().Length; column++)
            {
                var ones = contents.Count(x => x[column] == '1');
                var zeroes = contents.Count - ones;

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

        private int GetOxygenRating() 
        {
            var contents = FileContents;
            for (var column = 0; column < contents.First().Length; column++)
            {
                if (contents.Count > 1) 
                {
                    var (mostCommon, _) = GetMostAndLeastCommonBits(contents);
                    contents = contents.Where(x => x[column] == mostCommon[column]).ToList();
                }
            }
    
            return Convert.ToInt32(contents.First(), 2);
        }

        private int GetCO2ScrubberRating() 
        {
            var contents = FileContents;
            for (var column = 0; column < contents.First().Length; column++)
            {
                if (contents.Count() > 1) 
                {
                    var (_, leastCommon) = GetMostAndLeastCommonBits(contents);
                    contents = contents.Where(x => x[column] == leastCommon[column]).ToList();
                }
            }

            return Convert.ToInt32(contents.First(), 2);
        }
    }
}
