using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Days
{
    public class Day8 : DayBase<string, int>
    {
        private readonly Dictionary<int, List<string>> segments = new();
        public Day8(string fileLocation) : base(FileHandler.GetFileContentsWithDelimiter(fileLocation, '|').ToList())
        { }
           
    }
}
