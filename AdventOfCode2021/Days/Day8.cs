namespace AdventOfCode2021
{
    public class Day8 : DayBase<string, int>
    {
        private readonly Dictionary<int, List<char>> _segments = new();

        public Day8(string fileLocation) : base(FileHandler.GetFileContentsAsStrings(fileLocation).ToList())
        {
            SetSegments();
        }

        public override int Part1()
        {
            var segments = new List<string>();
            var split = new List<string[]>();
            FileContents.ForEach(line => split.Add(line.Split(" | ")));
           
            split.ForEach(line => line[1].Split(' ').ToList().ForEach(x => segments.Add(x)));

            var result = 0;
            var numbers = new List<int> {1, 4, 7, 8};


            numbers.ForEach(number => result += segments.Count(x => SameLength(x, _segments[number])));

            return result;
        }

        public override int Part2()
        {
            var segments = new List<string>();
            var split = new List<string[]>();
            FileContents.ForEach(line => split.Add(line.Split(" | ")));

            split.ForEach(line => line[1].Split(' ').ToList().ForEach(x => segments.Add(x)));

            var result = 0;
            var numbers = new List<int> { 1, 4, 7, 8 };

            numbers.ForEach(number =>
            {
                result += segments.Count(x => SameLength(x, _segments[number]));
                
            });

            return result;
        }

        private List<string> remove = new List<string>();

        private void SetSegments()
        {
            _segments.Add(0, new List<char>{ 'a', 'b', 'c', 'e', 'f', 'g'});
            _segments.Add(1, new List<char> { 'c', 'f' });
            _segments.Add(2, new List<char> { 'a','c', 'd', 'e', 'g' });
            _segments.Add(3, new List<char> { 'a', 'c', 'd', 'f', 'g' });
            _segments.Add(4, new List<char> { 'b', 'c', 'd', 'f' });
            _segments.Add(5, new List<char> { 'a', 'b', 'd', 'f', 'g' });
            _segments.Add(6, new List<char> { 'a', 'b', 'd', 'e', 'f', 'g' });
            _segments.Add(7, new List<char> { 'a', 'c', 'f' });
            _segments.Add(8, new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g' });
            _segments.Add(9, new List<char> { 'a', 'b', 'c', 'd', 'f', 'g' });

        }

        public static bool SameLength(string a, List<char> b)
        {
            return a.ToList().Count == b.Count;
        }

        public static bool ContainsAllItems(string a, List<char> b)
        {
            var result = a.ToList();
            return !result.Except(b).Any();
        }
    }
}
