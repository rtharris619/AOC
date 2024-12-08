using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AOC_2024;

public class Day6Tests
{
    private string GetTestInput()
    {
        return @"
                ....#.....
                .........#
                ..........
                ..#.......
                .......#..
                ..........
                .#..^.....
                ........#.
                #.........
                ......#...
        ";
    }

    [Fact]
    public void TestPart1()
    {
        var expected = 41;

        var puzzle = new Day6();
        var result = puzzle.SolvePart1(GetTestInput());

        Assert.Equal(expected, result);
    }
}
