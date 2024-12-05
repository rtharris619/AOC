using AOC_Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

        var dos = MatchDos(input);
        var donts = MatchDonts(input);

        var enabledInstructionIndices = dos.Select(x => x.Index).ToList();
        var disabledInstructionIndices = donts.Select(x => x.Index).ToList();

        for (var i = 0; i < enabledInstructionIndices.Count - 2; i++)
        {
            var intersections = disabledInstructionIndices.Where(disabled => disabled > enabledInstructionIndices[i] && 
                disabled < enabledInstructionIndices[i + 1]).ToList();

            Console.WriteLine("Start");
            Console.WriteLine($"{enabledInstructionIndices[i]} {enabledInstructionIndices[i + 1]}");
            Console.WriteLine(string.Join(',', intersections));
           
            if (intersections.Count == 0)
            {
                var range = (enabledInstructionIndices[i], enabledInstructionIndices[i + 1]);
                var subInput = input.Substring(range.Item1, range.Item2 - range.Item1);
                Console.WriteLine(subInput);

                foreach (Match match in GetMultiplications(subInput))
                {
                    result += MultiplyMatchedNumbers(match);
                }
            }

            if (intersections.Count == 1)
            {
                var range = (enabledInstructionIndices[i], intersections[0]);
                var subInput = input.Substring(range.Item1, range.Item2 - range.Item1);
                Console.WriteLine(subInput);

                foreach (Match match in GetMultiplications(subInput))
                {
                    result += MultiplyMatchedNumbers(match);
                }
            }

            if (intersections.Count > 1)
            {
                var range = (enabledInstructionIndices[i], intersections[0]);
                var subInput = input.Substring(range.Item1, range.Item2 - range.Item1);
                Console.WriteLine(subInput);

                foreach (Match match in GetMultiplications(subInput))
                {
                    result += MultiplyMatchedNumbers(match);
                }
            }

            Console.WriteLine("End");
        }

        var lastIndex = enabledInstructionIndices[enabledInstructionIndices.Count - 2];
        var length = enabledInstructionIndices[enabledInstructionIndices.Count - 1] - lastIndex;

        var lsubInput = input.Substring(lastIndex, length);
        Console.WriteLine(lsubInput);

        foreach (Match match in GetMultiplications(lsubInput))
        {
            result += MultiplyMatchedNumbers(match);
        }

        lastIndex = enabledInstructionIndices[enabledInstructionIndices.Count - 1];
        length = input.Length - lastIndex;

        lsubInput = input.Substring(lastIndex, length);
        Console.WriteLine(lsubInput);

        foreach (Match match in GetMultiplications(lsubInput))
        {
            result += MultiplyMatchedNumbers(match);
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
