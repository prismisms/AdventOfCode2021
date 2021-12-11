using System;
namespace AdventOfCode2021
{
    // Octopus energy levels
    public class Day11 : DayBase<string, int>
    {
        private int[,] _octopuses;

        public Day11(string fileLocation) : base(FileHandler.GetFileContentsAsStrings(fileLocation))
        {
            SetOctopusArray();
        }

        public override int Part1()
        {
            var xSize = FileContents.First().Length;
            var ySize = FileContents.Count;

            var numberOfFlashes = 0;

            for(var y = 0; y < ySize; y++)
            {
                for(var x = 0; x < xSize; x++)
                {
                    var octopus = _octopuses[x, y];
                    if (octopus == 9)
                    {
                        Flash(x, y);
                        numberOfFlashes++;
                    }
                }
            }
            return 0;
        }

        public override int Part2()
        {
            return 0;
        }

        private void Flash(int x, int y)
        {

        }

        // Parse octopuses into 2D array
        private void SetOctopusArray()
        {
            var xSize = FileContents.First().Length;
            var ySize = FileContents.Count;

            _octopuses = new int[xSize, ySize];

            for (var y = 0; y < ySize; y++)
            {
                for (var x = 0; x < xSize; x++)
                {
                    _octopuses[x, y] = FileContents[y][x];
                }
            }
        }
    }

    // Would this just be easier?
    public class Octopus
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int Value { get; set; }
    }
}

