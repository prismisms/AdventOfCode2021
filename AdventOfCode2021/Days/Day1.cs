namespace AdventOfCode2021
{
    public class Day1
    {
        // How many measurements are larger than the previous measurement?
        private readonly string _fileLocation;

        public Day1(string fileLocation)
        {
            _fileLocation = fileLocation;
        }


        public (int, int) GetDay1Result()
        {
            var fileContents = FileHandler.GetFileContentsAsNumbers(_fileLocation);
            return (GetNumberOfTimesDepthHasIncreased(fileContents), GetNumberOfTimesDepthHasIncreasedInSlidingWindow(fileContents));
        }

        public static int GetNumberOfTimesDepthHasIncreased(List<int> measurements)
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

        public static int GetNumberOfTimesDepthHasIncreasedInSlidingWindow(List<int> measurements)
        {
            var windows = GetWindows(measurements);
            return GetNumberOfTimesDepthHasIncreased(windows);
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
