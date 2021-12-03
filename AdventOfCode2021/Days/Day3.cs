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

        public int GetPowerConsumption()
        {
            var mostCommon = new StringBuilder();
            var leastCommon = new StringBuilder();

            var numberOfRows = _fileContents.Count();

            for (var column = 0; column < 12; column++)
            {
                var ones = 0;
                var zeroes = 1;

                for (var i = 0; i < numberOfRows; i++)
                {
                    if (_fileContents[i][column] == '0')
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
                    leastCommon.Append("0");
                    mostCommon.Append("1");
                }

            }

            var gammaRate = Convert.ToInt32(mostCommon.ToString(), 2);
            var epsilonRate = Convert.ToInt32(leastCommon.ToString(), 2);

            return gammaRate*epsilonRate;
        }

        public int GetPart2Value()
        {
            return GetOxygenGeneratorRating() * GetCO2ScrubberRating();
        }

        public int GetOxygenGeneratorRating()
        {
            var numberOfRows = _fileContents.Count();
            var oxygenRating = _fileContents;

            for (var column = 0; column < 12; column++)
            {
                var ones = 0;
                var zeroes = 1;

                if (oxygenRating.Count() > 1)
                {
                    for (var i = 0; i < numberOfRows; i++)
                    {
                        if (_fileContents[i][column] == '0')
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

                        oxygenRating = GetOxygenRating(column, '0', oxygenRating);

                    }
                    else if (ones > zeroes || ones == zeroes)
                    {
                        oxygenRating = GetOxygenRating(column, '1', oxygenRating);
                    }
                }
            }

            return Convert.ToInt32(oxygenRating.First(), 2);
        }

        public int GetCO2ScrubberRating()
        {
            var numberOfRows = _fileContents.Count();
            var oxygenRating = _fileContents;

            for (var column = 0; column < 12; column++)
            {
                var ones = 0;
                var zeroes = 1;

                if (oxygenRating.Count > 1)
                {
                    for (var i = 0; i < numberOfRows; i++)
                    {
                        if (_fileContents[i][column] == '0')
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

                        oxygenRating = GetOxygenRating(column, '1', oxygenRating);

                    }
                    else if (ones > zeroes || ones == zeroes)
                    {
                        oxygenRating = GetOxygenRating(column, '0', oxygenRating);
                    }
                }
            }

            return Convert.ToInt32(oxygenRating.First(), 2);
        }

        private List<string> GetOxygenRating(int column, char firstBit, List<string> input)
        {
            var oxygenRating = new List<string>();
            var numbers = input;
            for (var j = 0; j < numbers.Count; j++)
            {
                if (numbers.Count > 1)
                {
                    var row = numbers[j];
                    var firstActualBit = row[column];
                    if (numbers[j][column] == firstBit) 
                    {
                        oxygenRating.Add(_fileContents[j]);
                    }
                }
            }

            return oxygenRating;
        }

    }
}
