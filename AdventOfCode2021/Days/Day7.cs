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
                    nonLinearFuelToPosition += GetChange(initialPosition, i);
                });

                if (nonLinearFuelToPosition < previousNonLinearFuelToPosition || previousNonLinearFuelToPosition == 0)
                {
                    previousNonLinearFuelToPosition = nonLinearFuelToPosition;
                }

                nonLinearFuelToPosition = 0;

            }

            return previousNonLinearFuelToPosition;
        }

        private int GetChange(int startPosition, int endPosition)
        {
            var positionMax = endPosition;
            var positionMin = startPosition;

            if (startPosition > endPosition)
            {
                positionMax = startPosition;
                positionMin = endPosition;
            }

            var result = 0;
            var step = 0;
            for (var i = positionMin; i < positionMax; i++)
            {
                step++;
                result += step;
            }

            return result;

        }
    }
}

