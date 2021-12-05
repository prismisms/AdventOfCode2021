using System.Security.Cryptography.X509Certificates;

namespace AdventOfCode2021
{
    public class Day5 : DayBase<string>
    {
        private readonly List<Coordinates> _coordinates = new();
        private readonly Dictionary<(int, int), int> _pointsOnMap = new();

        public Day5(string fileLocation) : base(FileHandler.GetFileContentsAsStrings(fileLocation))
        {
            ParseCoordinates();
        }

        public override int Part1()
        {
            return CountOverlappingLines(false);
        }

        public override int Part2()
        {
            return CountOverlappingLines(true);
        }

        private int CountOverlappingLines(bool careAboutDiagonal)
        {
            _pointsOnMap.Clear();
            _coordinates.ForEach(coordinate =>
            {
                if ((!careAboutDiagonal && (coordinate.X1 == coordinate.X2 || coordinate.Y1 == coordinate.Y2))
                    || careAboutDiagonal)
                {
                    AllCoordinatesBetween(coordinate).ToList().ForEach(position =>
                    {
                        var (x, y) = position;
                        AddPointsOnMap(x, y);
                    });
                }
            });

            return _pointsOnMap
                .Count(x => x.Value > 1);
        }

        private void AddPointsOnMap(int x, int y)
        {
            if (!_pointsOnMap.TryAdd((x, y), 1))
            {
                _pointsOnMap[(x, y)] += 1;
            }
        }

        public static IEnumerable<(int, int)> AllCoordinatesBetween(Coordinates coord)
        {
            var dx = Math.Sign(coord.X2 - coord.X1);
            var dy = Math.Sign(coord.Y2 - coord.Y1);
            var steps = Math.Max(Math.Abs(coord.X2 - coord.X1), Math.Abs(coord.Y2 - coord.Y1)) + 1;
            var x = coord.X1;
            var y = coord.Y1;

            for (var i = 1; i <= steps; ++i)
            {
                yield return (x, y);

                y = y == coord.Y2 ? coord.Y2 : y + dy;
            }
        }

        private void ParseCoordinates()
        {
            FileContents.ForEach(line =>
            {
                _coordinates.Add( new Coordinates(line));
            });
        }
    }

    public class Coordinates
    {
        public Coordinates(string line)
        {
            var firstCommaIndex = line.IndexOf(',');
            var firstWhitespaceIndex = line.IndexOf(' ');
            var secondWhitespaceIndex = line.IndexOf(' ', line.IndexOf(' ') + 1);
            var secondCommaIndex = line.IndexOf(',', line.IndexOf(',') + 1);

            X1 = int.Parse(line[..firstCommaIndex]);
            Y1 = int.Parse(line[(firstCommaIndex + 1)..firstWhitespaceIndex]);
            X2 = int.Parse(line[(secondWhitespaceIndex + 1)..secondCommaIndex]);
            Y2 = int.Parse(line[(secondCommaIndex + 1)..]);
        }

        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
    }
}
