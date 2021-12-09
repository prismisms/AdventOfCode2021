using System;
namespace AdventOfCode2021
{
    public class Day9 : DayBase<string, int>
    {
        private readonly List<MapPoint> _map = new();

        public Day9(string fileLocation) : base(FileHandler.GetFileContentsAsStrings(fileLocation))
        {
            for (var i = 0; i < FileContents.Count; i++)
            {
                var ints = FileContents[i].Select(x => int.Parse(x.ToString())).ToList();
                for (var j = 0; j < ints.Count; j++)
                {
                    var mapValue = ints[j];
                    _map.Add(new MapPoint
                    {
                        XLocation = j,
                        YLocation = i,
                        Value = mapValue,
                        RiskFactor = mapValue + 1
                    });
                }
            }
        }

        public override int Part1()
        {
            return GetLowPoints(_map).Select(x => x.RiskFactor).ToList().Sum();
        }

        // Get 3 largest basins and multiple up their results
        public override int Part2()
        {
            var lowPoints = GetLowPoints(_map);
            var basins = GetBasins(lowPoints).ToList().OrderByDescending(list => list.Count).ToList();

            return basins[0].Count * basins[1].Count * basins[2].Count;
        }

        private IEnumerable<List<MapPoint>> GetBasins(List<MapPoint> lowPoints)
        {
            var basins = new List<List<MapPoint>>();
            foreach(var value in lowPoints)
            {
                var lowPoint = value.Value;
                var basin = new List<MapPoint> { value };

                var adjacentPoints = GetAdjacentPoints(value);
                
                while (adjacentPoints.Any() && adjacentPoints.Any(x => x.Value > lowPoint))
                {
                    var newMapPoints = new List<MapPoint>();
                    
                    adjacentPoints.ForEach(point =>
                    {
                        if (point.Value != 9 && point.Value > lowPoint
                            && !basin.Any(x => x.XLocation == point.XLocation && x.YLocation == point.YLocation)
                            && !basins.Any(z => z.Any(x => x.XLocation == point.XLocation && x.YLocation == point.YLocation)))
                        {
                            newMapPoints.Add(point);
                            basin.Add(point);
                        }
                    });

                    adjacentPoints = newMapPoints.SelectMany(GetAdjacentPoints).ToList();
                    lowPoint++;
                }

                basins.Add(basin);
            }

            return basins;
        }

        private List<MapPoint> GetAdjacentPoints(MapPoint mapPoint)
        {
            var adjacentPoints = new List<MapPoint>
            {
                _map.FirstOrDefault(x => x.XLocation == mapPoint.XLocation && x.YLocation == mapPoint.YLocation - 1),
                _map.FirstOrDefault(x => x.XLocation == mapPoint.XLocation && x.YLocation == mapPoint.YLocation + 1),
                _map.FirstOrDefault(x => x.XLocation == mapPoint.XLocation - 1 && x.YLocation == mapPoint.YLocation),
                _map.FirstOrDefault(x => x.XLocation == mapPoint.XLocation + 1 && x.YLocation == mapPoint.YLocation)
            };

            return adjacentPoints.Where(x => x != null).ToList();
        }

        private List<MapPoint> GetLowPoints(List<MapPoint> map)
        {
            var lowPoints = new List<MapPoint>();
            // Locations lower than any adjacent locations
            map.ForEach(mapPoint =>
            {
                var adjacentPoints = new List<MapPoint>
                {
                    map.FirstOrDefault(x => x.XLocation == mapPoint.XLocation && x.YLocation == mapPoint.YLocation - 1),
                    map.FirstOrDefault(x => x.XLocation == mapPoint.XLocation && x.YLocation == mapPoint.YLocation + 1),
                    map.FirstOrDefault(x => x.XLocation == mapPoint.XLocation - 1 && x.YLocation == mapPoint.YLocation),
                    map.FirstOrDefault(x => x.XLocation == mapPoint.XLocation + 1 && x.YLocation == mapPoint.YLocation)
                };

                if (adjacentPoints.Where(x => x != null).All(x => x != null && mapPoint.Value < x?.Value))
                {
                    lowPoints.Add(mapPoint);
                }
              
            });

            return lowPoints;
        }
    }
    
    public class MapPoint
    {
        public MapPoint()
        {
            RiskFactor = Value + 1;
        }

        public int XLocation { get; set; }
        public int YLocation { get; set; }
        public int Value { get; set; }
        public int RiskFactor { get; set; }
    }
}

