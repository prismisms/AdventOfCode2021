namespace AdventOfCode2021
{
    public class Day1 : DayBase<int, int>
    {
        // How many measurements are larger than the previous measurement?
        public Day1(string fileLocation) 
            : base(FileHandler.GetFileContentsAsNumbers(fileLocation))
        {
        }

        public override int Part1()
        {
            return GetNumberOfTimesDepthHasIncreased(FileContents);
        }

        public override int Part2()
        {
            var windows = GetWindows(FileContents);
            return GetNumberOfTimesDepthHasIncreased(windows);
        }

        private static int GetNumberOfTimesDepthHasIncreased(List<int> measurements)
        {
            var output = 0;
            int? lastMeasurement = null;
            measurements.ForEach(measurement =>
            {
                if (measurement > lastMeasurement)
                {
                    output++;
                }

                lastMeasurement = measurement;
            });

            return output;
        }

        private static List<int> GetWindows(IReadOnlyList<int> measurements)
        {
            var windows = new List<int>();
            for (var i = 0; i < measurements.Count; i++)
            {
                if (measurements.Count > i + 2)
                {
                    windows.Add(measurements[i] + measurements[i + 1] + measurements[i + 2]);
                }

            }
            return windows;
        }
    }
}
