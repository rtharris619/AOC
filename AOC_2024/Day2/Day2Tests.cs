using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AOC_2024;

public class Day2Tests
{
    [Fact]
    public void TestPart1()
    {
        var input = "7 6 4 2 1\n1 2 7 8 9\n9 7 6 2 1\n1 3 2 4 5\r\n8 6 4 4 1\n1 3 6 7 9";
        var expected = 2;

        var day2 = new Day2();
        var result = day2.SolvePart1(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestPart2()
    {
        var input = "7 6 4 2 1\n1 2 7 8 9\n9 7 6 2 1\n1 3 2 4 5\r\n8 6 4 4 1\n1 3 6 7 9";
        var expected = 4;

        var day2 = new Day2();
        var result = day2.SolvePart2(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestPart2b()
    {
        var input = "7 8 6 4 3";
        var expected = 1;

        var day2 = new Day2();
        var result = day2.SolvePart2(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestPart2c()
    {
        var input = "1 2 7 4 5";
        var expected = 1;

        var day2 = new Day2();
        var result = day2.SolvePart2(input);

        Assert.Equal(expected, result);
    }
}
