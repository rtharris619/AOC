using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AOC_2015;

public class Day1Tests
{
    [Fact]
    public void TestFindFloor()
    {
        // Arrange
        var input = ")())())";
        var expected = -3;

        // Act
        var day1 = new Day1();
        var result = day1.FindFloor(input);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestFindBasementPosition()
    {
        // Arrange
        var input = "()())";
        var expected = 5;

        // Act
        var day1 = new Day1();
        var result = day1.FindBasementPosition(input);

        // Assert
        Assert.Equal(expected, result);
    }
}
