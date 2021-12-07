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
            return GetFuelToMoveAllCrabsToPosition(true);
        }

        public override int Part2()
        {
            return GetFuelToMoveAllCrabsToPosition(false);
        }

        private int GetFuelToMoveAllCrabsToPosition(bool linear)
        {
            var maxHorizontalPosition = FileContents.Max();
            var minHorizontalPosition = FileContents.Min();
            var previousFuelToPosition = 0;
            var fuelToPosition = 0;

            for (var i = minHorizontalPosition; i <= maxHorizontalPosition; i++)
            {

                FileContents.ForEach(initialPosition =>
                {
                    var linearFuel = Math.Abs(initialPosition - i);
                    fuelToPosition += linear ? linearFuel : GetNonLinearFuel(linearFuel);
                });

                if (fuelToPosition < previousFuelToPosition || previousFuelToPosition == 0)
                {
                    previousFuelToPosition = fuelToPosition;
                }

                fuelToPosition = 0;

            }

            return previousFuelToPosition;
        }

        private static int GetNonLinearFuel(int change)
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

