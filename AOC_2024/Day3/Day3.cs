using AOC_Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AOC_2024;

public class Day3
{
    private MatchCollection GetMultiplications(string input)
    {
        string pattern = @"mul\([0-9]+,[0-9]+\)";
        var regex = new Regex(pattern);

        return Regex.Matches(
            input, 
            pattern,
            RegexOptions.None,
            TimeSpan.FromSeconds(1));
    }

    private int MultiplyMatchedNumbers(Match match)
    {
        var multiples = Regex.Matches(match.Value, "[0-9]+");

        return Convert.ToInt32(multiples[0]?.Value) * Convert.ToInt32(multiples[1]?.Value);
    }

    private MatchCollection MatchDos(string input)
    {
        string pattern = @"do\(\)";
        var regex = new Regex(pattern);

        return Regex.Matches(
            input,
            pattern,
            RegexOptions.None,
            TimeSpan.FromSeconds(1));
    }

    private MatchCollection MatchDonts(string input)
    {
        string pattern = @"don't\(\)";
        var regex = new Regex(pattern);

        return Regex.Matches(
            input,
            pattern,
            RegexOptions.None,
            TimeSpan.FromSeconds(1));
    }

    public int SolvePart2(string input)
    {
        var result = 0;

        foreach (Match match in MatchDos(input))
        {
            Console.WriteLine($"{match.Index}: {match.Value}");
        }

        foreach (Match match in MatchDonts(input))
        {
            Console.WriteLine($"{match.Index}: {match.Value}");
        }

        return result;
    }

    public int SolvePart1(string input)
    {
        var result = 0;       

        foreach (Match match in GetMultiplications(input))
        {
            result += MultiplyMatchedNumbers(match);
        }

        return result;
    }

    public async Task Driver()
    {
        var input = await new PuzzleInputDownloader().DownloadPuzzleInputAsync(2024, 3);

        //var part1Answer = SolvePart1(input);
        //Console.WriteLine(part1Answer);

        var part2Answer = SolvePart2(input);
        Console.WriteLine(part2Answer);
    }
}
