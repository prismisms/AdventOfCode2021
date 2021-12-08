using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace AdventOfCode2021.Tests
{
    public class FileHandlerTests
    {
        [Fact]
        public void ShouldGetStringContentsByLine()
        {
            var result = FileHandler.GetFileContentsAsStrings("TestData/Day5.txt");

            // I could check every line but I'm lazy
            result.First().Should().Be("0,9 -> 5,9");
            result.Last().Should().Be("5,5 -> 8,2");
        }

        [Fact]
        public void ShouldGetIntsFromNewlineSeparatedFile()
        {
            var expectedResult = new List<int>
            {
                199,
                200,
                208,
                210,
                200,
                207,
                240,
                269,
                260,
                263
            };

            var actualResult = FileHandler.GetFileContentsAsNumbers("TestData/Day1.txt");

            actualResult.Should().NotBeEmpty()
                .And.HaveCount(expectedResult.Count)
                .And.ContainInOrder(expectedResult)
                .And.ContainItemsAssignableTo<int>();
        }

        [Fact]
        public void ShouldGetIntsFromCommaSeparatedSingleLine()
        {
            var expectedResult = new List<int>
            {
                3,
                4,
                3,
                1,
                2
            };

            var actualResult = FileHandler.GetNumbersFromCommaSeparatedSingleLine("TestData/Day6.txt");

            actualResult.Should().NotBeEmpty()
                .And.HaveCount(expectedResult.Count)
                .And.ContainInOrder(expectedResult)
                .And.ContainItemsAssignableTo<int>();
        }
    }
}
