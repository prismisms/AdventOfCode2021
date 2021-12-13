using System.Text;

namespace AdventOfCode2021;

public class Day12 : DayBase<string, int>
{
    private List<(string, string)> _navigationData = new();
    private List<Cave> _caves = new();

    private int _routes = 0;
    public Day12(string fileLocation) : base(FileHandler.GetFileContentsAsStrings(fileLocation))
    {
        SetNavigationData();
    }

    public override int Part1()
    {
        // start at 'start'
        // end at 'end'
        // goal: find number of distinct paths that go from start to finish, and dont visit small caves more than once
        // can visit big caves more than once
        // uppercase = big cave
        // lowercase = small cave
        _routes = 0;
        CalculateDistinctPaths();
        return _routes;
    }

    public override int Part2()
    {
        _routes = 0;
        return 0;
    }

    private void CalculateDistinctPaths()
    {
        var start = _caves.Single(x => x.CaveType == CaveType.Start);
        var route = new StringBuilder("start,");
        NavigateFromCave(start, route);
    }

    private HashSet<string> _allRoutes = new();
    private void NavigateFromCave(Cave cave, StringBuilder route)
    {
    }

    private void SetNavigationData()
    {
        foreach (var split in FileContents.Select(fileContent => fileContent.Split('-')))
        {
            _navigationData.Add((split[0], split[1]));
        }
        
        // Tryin this
        foreach (var split in FileContents.Select(fileContent => fileContent.Split('-')))
        {
            // Cave exists
            if (_caves.Any(x => x.CaveName == split[0]))
            {
                _caves.Single(x => x.CaveName == split[0]).ConnectedCaves.Add(split[1]);
            }
            else
            {
                _caves.Add(new Cave(split[0], split[1]));
            }
            
            
            if (_caves.Any(x => x.CaveName == split[1]))
            {
                _caves.Single(x => x.CaveName == split[1]).ConnectedCaves.Add(split[0]);
            }
            else
            {
                _caves.Add(new Cave(split[1], split[0]));
            }
            
        }
    }
}

public class Cave
{
    public Cave(string caveName, string connectedCaveName)
    {
        CaveName = caveName;
        CaveType = GetCaveType(caveName);
        ConnectedCaves = new List<string>
        {
            connectedCaveName
        };
        HasBeenVisited = false;
    }
    public string CaveName { get; set; }
    public List<string> ConnectedCaves { get; set; }
    // Only really matters for small caves
    public bool HasBeenVisited { get; set; }
    public CaveType CaveType { get; }

    private CaveType GetCaveType(string caveName)
    {
        if (caveName == "start")
        {
            return CaveType.Start;
        }

        if (caveName == "end")
        {
            return CaveType.End;
        }

        if (caveName.All(char.IsUpper))
        {
            return CaveType.Big;
        }

        if (caveName.All(char.IsLower))
        {
            return CaveType.Small;
        }

        throw new Exception();
    }
}

public enum CaveType
{
    Start,
    End,
    Big,
    Small
}