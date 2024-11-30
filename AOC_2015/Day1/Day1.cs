using AOC_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2015.Day1
{
    internal class Day1
    {
        private void FindFloor(string input)
        {
            var result = input.Count(x => x == '(') - input.Count(x => x == ')');

            Console.WriteLine(result);
        }

        private void BasementPosition(string input)
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
                    basementPosition = i; 
                    break;
                }
            }

            Console.WriteLine(basementPosition + 1);
        }

        private void BasementPositionFunc(string input)
        {
            var up = '(';
            var down = ')';

            var basementPosition = input
                .Select((c, i) => new { c, i })
                .Aggregate(new { current = 0, position = (int?)null }, (acc, item) =>
                {
                    var current = acc.current + (item.c == up ? 1 : item.c == down ? -1 : 0);
                    var position = acc.position ?? (current == -1 ? item.i : null);

                    return new { current, position };
                }).position ?? int.MinValue;

            Console.WriteLine(basementPosition + 1);
        }

        private void BasementPositionTest()
        {
            var input = ")";
            BasementPosition(input);
        }

        public async Task Driver()
        {
            var puzzleInput = await new PuzzleDownloader().DownloadPuzzleInputAsync(2015, 1);

            FindFloor(puzzleInput);

            BasementPositionFunc(puzzleInput);

            // Answers: 74; 1795
        }
    }
}
