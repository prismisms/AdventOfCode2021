using System.ComponentModel;
using System.Text;

namespace AdventOfCode2021;

public class Day13 : DayBase<string, int>
{
    private readonly List<(string, int)> _foldAlongLines = new();
    private readonly List<Coordinate> _coordinates = new();
    
    public Day13(string fileContents) : base(FileHandler.GetFileContentsAsStrings(fileContents))
    {
        GetFolds();
        GetCoordinates();
    }

    public override int Part1()
    {
        // Find visible dots after one fold
        return CalculateVisibleAfterFold(0);
    }

    public override int Part2()
    {
        GetFolds();
        GetCoordinates();
        
        for (var i = 0; i < _foldAlongLines.Count; i++)
        {
             CalculateVisibleAfterFold(i);
        }
        
        var anotherResult = _coordinates.GroupBy(x => new {x.X, x.Y}).ToList();

        var width = _coordinates.Select(x => x.X).Max();
        var height = _coordinates.Select(x => x.Y).Max();

        for (var i = 0; i <= height; i++)
        {
            var line = new StringBuilder();
            for (var j = 0; j <= width; j++)
            {
                line.Append(anotherResult.Any(x => x.Key.X == j && x.Key.Y == i) ? "#  " : ".  ");
            }
            
            Console.WriteLine(line);
        }
        
        return anotherResult.Count;
    }

    private int CalculateVisibleAfterFold(int foldNumber)
    {
        var (dimension, foldAtCoordinate) = _foldAlongLines[foldNumber];
        foreach (var coordinate in _coordinates)
        {
            switch (dimension)
            {
                case "y" when coordinate.Y > foldAtCoordinate:
                {
                    var change = coordinate.Y - foldAtCoordinate;
                    coordinate.Y = foldAtCoordinate - change;
                    break;
                }
                case "x" when coordinate.X > foldAtCoordinate:
                {
                    var change = coordinate.X - foldAtCoordinate;
                    coordinate.X = foldAtCoordinate - change;
                    break;
                }
            }
        }

        var distinct = _coordinates.GroupBy(x => new {x.X, x.Y});
        return distinct.Count();
    }

    private void GetFolds()
    {
        var indexOfFirstFold = 0;
        
        for (var i = FileContents.Count - 1; i > 0; i--)
        {
            if (!FileContents[i].Contains("fold"))
            {
                indexOfFirstFold = i + 1;
                break;
            }
        }
        
        for (var i = indexOfFirstFold; i < FileContents.Count; i++)
        {
            var parametersOnly = FileContents[i].Split("fold along ")[1];
            _foldAlongLines.Add((parametersOnly[0].ToString(), int.Parse(parametersOnly[2..])));
        }
    }

    private void GetCoordinates()
    {
        foreach (var coordinates in FileContents.TakeWhile(line => !string.IsNullOrWhiteSpace(line)).Select(line => line.Split(',')))
        {
            _coordinates.Add(new Coordinate
            {
                X = int.Parse(coordinates[0]),
                Y = int.Parse(coordinates[1])
            });
        }
    }

}

public class Coordinate
{
    public int X { get; set; }
    public int Y { get; set; }
}