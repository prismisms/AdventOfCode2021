using FluentAssertions;
using Xunit;

namespace AdventOfCode2021.Tests
{
    public class Day5Tests
    {
        private readonly Day5 _day5 = new("TestData/Day5.txt");

        [Fact]
        public void ShouldCalculatePart1Correctly()
        {
            var part1Answer = _day5.Part1();
            part1Answer.Should().Be(5);
        }

        [Fact]
        public void ShouldCalculatePart2Correctly()
        {
            var part2Answer = _day5.Part2();
            part2Answer.Should().Be(12);
        }

        [Fact]
        public void ShouldAssignCoordinatesCorrectly()
        {
            const string testLine = "6,4 -> 2,0";
            var coordinate = new Coordinates(testLine);

            coordinate.Should().NotBeNull();
            coordinate.X1.Should().Be(6);
            coordinate.Y1.Should().Be(4);
            coordinate.X2.Should().Be(2);
            coordinate.Y2.Should().Be(0);
        }
    }
}