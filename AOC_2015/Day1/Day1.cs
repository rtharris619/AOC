using AOC_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2015;

public class Day1
{
    public int FindFloor(string input)
    {
        var result = input.Count(x => x == '(') - input.Count(x => x == ')');

        return result;
    }

    public int FindBasementPosition(string input)
    {
        var basementPosition = int.MinValue;
        var current = 0;
        var up = '(';
        var down= ')';

        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] == up)
            {
                current += 1;
            }
            if (input[i] == down)
            {
                current -= 1;
            }

            if (current == -1)
            {
                basementPosition = i + 1;
                break;
            }
        }

        return basementPosition;
    }

    public int FindBasementPositionFunc(string input)
    {
        var up = '(';
        var down = ')';

        var basementPosition = input
            .Select((c, i) => new { c, i })
            .Aggregate(new { current = 0, position = (int?)null }, (acc, item) =>
            {
                var current = acc.current + (item.c == up ? 1 : item.c == down ? -1 : 0);
                var position = acc.position ?? (current == -1 ? item.i + 1 : null);

                return new { current, position };
            }).position ?? int.MinValue;

        return basementPosition;
    }

    public async Task Driver()
    {
        var puzzleInput = await new PuzzleInputDownloader().DownloadPuzzleInputAsync(2015, 1);

        var floor = FindFloor(puzzleInput);
        Console.WriteLine(floor);

        var position = FindBasementPositionFunc(puzzleInput);
        Console.WriteLine(position);

        // Answers: 74; 1795
    }
}
