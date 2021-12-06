using System.Diagnostics;

namespace AdventOfCode2021
{
    public class Day6 : DayBase<int>
    {
        private const int ResetNumber = 6;
        private const int NewLanternFishNumber = 8;
        private List<int> _lanternFish;
        private readonly string _fileLocation;

        public Day6(string fileLocation) : base(FileHandler.GetNumbersFromCommaSeparatedSingleLine(fileLocation)
            .ToList())
        {
            _fileLocation = fileLocation;
            _lanternFish = FileContents;
        }

        public override int Part1()
        {
            var numberOfDays = 80;
            while (numberOfDays > 0)
            {
                var newAdditions = new List<int>();
                for (var i = 0; i < _lanternFish.Count; i++)
                {
                    if (_lanternFish[i] == 0)
                    {
                        _lanternFish[i] = 6;
                        newAdditions.Add(8);
                    }
                    else
                    {
                        _lanternFish[i]--;
                    }
                }

                _lanternFish = _lanternFish.Concat(newAdditions).ToList();
                numberOfDays--;
            }

            return _lanternFish.Count;
        }

        public override int Part2()
        {
            var lanternFishAges = new long[9];

            FileHandler.GetNumbersFromCommaSeparatedSingleLine(_fileLocation).ToList().ForEach(x => lanternFishAges[x]++);

            for (var i = 0; i < 256; i++)
            {
                var newFish = (long)0;

                for (var lanternFishAge = 0; lanternFishAge < lanternFishAges.Length; lanternFishAge++)
                {
                    var numberOfFish = lanternFishAges[lanternFishAge];

                    if (lanternFishAge == 0)
                    {
                        newFish = numberOfFish;
                        lanternFishAges[0] -= numberOfFish;
                    }
                    else
                    {
                        lanternFishAges[lanternFishAge] -= numberOfFish;
                        lanternFishAges[lanternFishAge - 1] += numberOfFish;
                    }
                }

                lanternFishAges[8] += newFish;
                lanternFishAges[6] += newFish;
            }

            Console.WriteLine(lanternFishAges.Sum());

            return 0;
        }



    }
}
