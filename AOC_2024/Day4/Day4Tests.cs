using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AOC_2024;

public class Day4Tests
{
    private string GetInput()
    {
        return "MMMSXXMASM\n" +
            "MSAMXMSMSA\n" +
            "AMXSXMAAMM\n" +
            "MSAMASMSMX\n" +
            "XMASAMXAMM\n" +
            "XXAMMXXAMA\n" +
            "SMSMSASXSS\n" +
            "SAXAMASAAA\n" +
            "MAMMMXMMMM\n" +
            "MXMXAXMASX";
    }

    [Fact]
    public void TestPart1()
    {
        var expected = 18;

        var puzzle = new Day4();
        var result = puzzle.SolvePart1(GetInput());

        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestPart2()
    {
        var expected = 9;

        var puzzle = new Day4();
        var result = puzzle.SolvePart2(GetInput());

        Assert.Equal(expected, result);
    }
}
