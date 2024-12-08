using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AOC_2024;

public class Day5Tests
{
    private string GetInput()
    {
        return @"
            47|53
            97|13
            97|61
            97|47
            75|29
            61|13
            75|53
            29|13
            97|29
            53|29
            61|53
            97|53
            61|29
            47|13
            75|47
            97|75
            47|61
            75|61
            47|29
            75|13
            53|13

            75,47,61,53,29
            97,61,53,29,13
            75,29,13
            75,97,47,61,53
            61,13,29
            97,13,75,29,47
        ";
    }

    [Fact]
    public void TestPart1()
    {
        var expected = 143;

        var puzzle = new Day5();
        var result = puzzle.SolvePart1(GetInput());

        Assert.Equal(expected, result);
    }

    [Fact]
    public void TestPart2()
    {
        var expected = 123;

        var puzzle = new Day5();
        var result = puzzle.SolvePart2(GetInput());

        Assert.Equal(expected, result);
    }
}
