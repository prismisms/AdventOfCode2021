using System;

namespace AdventOfCode2021
{
    // Octopus energy levels
    public class Day11 : DayBase<string, int>
    { 
        private int[,] _octopuses;
        private List<(int, int)> _flashedOctopuses = new();
        private int _rows = 0;
        private int _columns = 0;
        private int _numberOfFlashes = 0;
        
        /**
         * First, the energy level of each octopus increases by 1.
         * Then, any octopus with an energy level greater than 9 flashes.
         * This increases the energy level of all adjacent octopuses by 1, including octopuses that are diagonally adjacent.
         * If this causes an octopus to have an energy level greater than 9, it also flashes.
         * This process continues as long as new octopuses keep having their energy level increased beyond 9.
         * (An octopus can only flash at most once per step.)
         * Finally, any octopus that flashed during this step has its energy level set to 0, as it used all of its energy to flash
         */
        public Day11(string fileLocation) : base(FileHandler.GetFileContentsAsStrings(fileLocation))
        {
            SetOctopusArray();
            _rows = FileContents.Count;
            _columns = FileContents.First().Length;
        }

        public override int Part1()
        {
            for(var i = 0; i < 100; i++)
            {
                _flashedOctopuses.Clear();
                for (var y = 0; y < _rows; y++)
                {
                    for (var x = 0; x < _columns; x++)
                    {
                        _octopuses[x, y] += 1;
                        if (_octopuses[x, y] > 9 && !_flashedOctopuses.Contains((x, y)))
                        {
                            Flash(x, y);
                        }
                    }
                }

                foreach (var (x, y) in _flashedOctopuses)
                {
                    _numberOfFlashes++;
                    _octopuses[x, y] = 0;
                }
            }
        
            return _numberOfFlashes;
        }
        
        public override int Part2()
        {
            SetOctopusArray();
            var steps = 0;
            var allOctopusesFlashed = false;
            while(!allOctopusesFlashed)
            {
                steps++;
                _flashedOctopuses.Clear();
                allOctopusesFlashed = true;

                for (var y = 0; y < _rows; y++)
                {
                    for (var x = 0; x < _columns; x++)
                    {
                        _octopuses[x, y] += 1;
                        if (_octopuses[x, y] > 9 && !_flashedOctopuses.Contains((x, y)))
                        {
                            Flash(x, y);
                        }
                    }
                }

                foreach (var (x, y) in _flashedOctopuses)
                {
                    _octopuses[x, y] = 0;
                }
                
                for (var y = 0; y < _rows; y++)
                {
                    for (var x = 0; x < _columns; x++)
                    {
                        if (_octopuses[x, y] != 0)
                        {
                            allOctopusesFlashed = false;
                        }
                    }
                }
            }

            return steps;
        }
    /*
     *   1 2 3
     *   4 5 6
     *   7 8 9
     */
        private void Flash(int x, int y)
        {
            _flashedOctopuses.Add((x, y));

            var adjacent = new List<(int, int)>
            {
                // Above, below, left, right
                (x, y - 1), // 2
                (x, y + 1), // 8
                (x - 1, y), //4
                (x + 1, y), // 6
                (x + 1, y + 1), // 9
                (x + 1, y - 1), // 3
                (x - 1, y + 1), // 7
                (x - 1, y - 1) // 1
            }.Where((value) => value.Item1 > -1 && value.Item2 > -1 && value.Item1 < _columns && value.Item2 < _rows).ToList();
            
            adjacent.ForEach(value =>
            {
                var (item1, item2) = value;
                _octopuses[item1, item2] += 1;
                var result = _octopuses[item1, item2];

                if (result > 9 && !_flashedOctopuses.Contains((item1, item2)))
                {
                    Flash(item1, item2);
                }
            });
        }
        
        // Parse octopuses into 2D array
        private void SetOctopusArray()
        {
            _columns = FileContents.First().Length;
            _rows = FileContents.Count;
        
            _octopuses = new int[_columns, _rows];
        
            for (var x = 0; x < _columns; x++)
            {
                var line = FileContents[x];
        
                for (var y = 0; y < _columns; y++)
                {
                    _octopuses[x, y] = int.Parse(line[y].ToString());
                }
            }
        }
    }
}

