using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AOC_2024;

public class Day1Tests
{
    [Fact]
    public void TestCalculateTotalDistance()
    {
        // Arrange
        var left = new List<int> { 3, 4, 2, 1, 3, 3 };
        var right = new List<int> { 4, 3, 5, 3, 9, 3 };
        var expected = 11;

        // Act
        var day1 = new Day1();

        var result = day1.CalculateTotalDistance(left, right);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestCalculateTotalSimilarityScore()
    {
        // Arrange
        var left = new List<int> { 3, 4, 2, 1, 3, 3 };
        var right = new List<int> { 4, 3, 5, 3, 9, 3 };
        var expected = 31;

        // Act
        var day1 = new Day1();

        var result = day1.CalculateSimilarityScore(left, right);

        // Assert
        Assert.Equal(expected, result);
    }
}
