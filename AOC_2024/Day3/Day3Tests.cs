using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AOC_2024;

public class Day3Tests
{
    [Fact]
    public void TestPart1()
    {
        var input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";
        var expected = 161;

        var day3 = new Day3();
        var result = day3.SolvePart1(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestPart2()
    {
        var input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";
        var expected = 48;
    }
}
