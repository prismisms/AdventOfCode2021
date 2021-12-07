using System;
namespace AdventOfCode2021
{
    public class Day7 : DayBase<int, int>
    {
        public Day7(string fileLocation) : base(FileHandler.GetNumbersFromCommaSeparatedSingleLine(fileLocation).ToList())
        {
        }

        public override int Part1()
        {
            var maxHorizontalPosition = FileContents.Max();
            var minHorizontalPosition = FileContents.Min();
            var previousFuelToPosition = 0;
            var fuelToPosition = 0;

            for (var i = minHorizontalPosition; i <= maxHorizontalPosition; i++)
            {
                
                FileContents.ForEach(initialPosition =>
                {
                    fuelToPosition += Math.Abs(initialPosition - i);
                });

                if (fuelToPosition < previousFuelToPosition || previousFuelToPosition == 0)
                {
                    previousFuelToPosition = fuelToPosition;
                }

                    fuelToPosition = 0;

            }

            return previousFuelToPosition;
        }

        public override int Part2()
        {
            var maxHorizontalPosition = FileContents.Max();
            var minHorizontalPosition = FileContents.Min();
            var previousNonLinearFuelToPosition = 0;
            var nonLinearFuelToPosition = 0;

            for (var i = minHorizontalPosition; i <= maxHorizontalPosition; i++)
            {
                FileContents.ForEach(initialPosition =>
                {
                    nonLinearFuelToPosition += GetChange(Math.Abs(initialPosition - i));
                });

                if (nonLinearFuelToPosition < previousNonLinearFuelToPosition || previousNonLinearFuelToPosition == 0)
                {
                    previousNonLinearFuelToPosition = nonLinearFuelToPosition;
                }

                nonLinearFuelToPosition = 0;
            }
            
            return previousNonLinearFuelToPosition;
        }

        private static int GetChange(int change)
        {
            var result = 0;
            for (var i = 0; i <= change; i++)
            {
                result += i;
            }

            return result;

        }
    }
}

