using System.Diagnostics;

namespace AdventOfCode2021
{
    public class Day6 : DayBase<int, long>
    {
        private const int ResetAge = 6;
        private const int NewLanternFishAge = 8;
        private List<int> _lanternFish;
        private readonly string _fileLocation;

        public Day6(string fileLocation) : base(FileHandler.GetNumbersFromCommaSeparatedSingleLine(fileLocation)
            .ToList())
        {
            _fileLocation = fileLocation;
            _lanternFish = FileContents;
        }

        // Keeping this just because it was my original solution that absolutely does not work for part 2
        public override long Part1()
        {
            var numberOfDays = 80;
            while (numberOfDays > 0)
            {
                var newAdditions = new List<int>();
                for (var i = 0; i < _lanternFish.Count; i++)
                {
                    if (_lanternFish[i] == 0)
                    {
                        _lanternFish[i] = ResetAge;
                        newAdditions.Add(NewLanternFishAge);
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

        public override long Part2()
        {
            var lanternFishAges = new long[9];

            FileHandler.GetNumbersFromCommaSeparatedSingleLine(_fileLocation).ToList()
                .ForEach(x => lanternFishAges[x]++);

            for (var i = 0; i < 256; i++)
            {
                var newFish = (long) 0;

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

                lanternFishAges[NewLanternFishAge] += newFish;
                lanternFishAges[ResetAge] += newFish;
            }

            return lanternFishAges.Sum();
        }
    }
}
