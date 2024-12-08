using AOC_Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2024;

public class Day5
{

    public ILookup<int, int> GetPageOrderingRules(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Where(x => x.Contains('|'))
            .ToList();

        var tempList = new List<KeyValuePair<int, int>>();

        for (int i = 0; i < lines.Count; i++)
        {
            var item = lines[i].Trim().Split('|');
            tempList.Add(new KeyValuePair<int, int>(
                Convert.ToInt32(item[0]),
                Convert.ToInt32(item[1])));
        }

        var lookup = tempList.ToLookup(pair => pair.Key, pair => pair.Value);

        return lookup;
    }

    public List<List<int>> GetUpdates(string input)
    {
        var updates = new List<List<int>>();
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Where(x => x.Contains(','))
            .ToList();

        for (int i = 0; i < lines.Count; i++)
        {
            var items = lines[i].Trim().Split(',');
            var update = items.ToList().Select(x => Convert.ToInt32(x)).ToList();
            updates.Add(update);
        }

        return updates;
    }

    public int GetTotalUpdates(ILookup<int, int> pageOrderingRules, List<List<int>> updates)
    {
        var totalUpdates = 0;

        for (int i = 0; i < updates.Count; i++)
        {
            var valid = true;

            for (int j = 0; j < updates[i].Count; j++)
            {
                var numberToCheck = updates[i][j]; // 75
                var rules = pageOrderingRules.FirstOrDefault(x => x.Key == numberToCheck);

                if (rules == null) continue;

                foreach (var rule in rules) // 47, 61, 53, 29
                {
                    var updateIndex = updates[i].IndexOf(rule);
                    if (updateIndex < j && updateIndex != -1)
                    {
                        valid = false;
                        break;
                    }
                }

                if (!valid)
                {
                    break;
                }
            }

            if (valid)
            {
                int midPointIndex = updates[i].Count / 2;
                totalUpdates += updates[i][midPointIndex];
            }
        }

        return totalUpdates;
    }

    public int GetTotalUnorderedUpdates(ILookup<int, int> pageOrderingRules, List<List<int>> updates)
    {
        var totalUpdates = 0;

        for (int i = 0; i < updates.Count; i++)
        {
            var incorrectUpdate = false;

            // 97,13,75,29,47 -> 97,75,47,29,13

            for (int j = 0; j < updates[i].Count; j++)
            {
                var numberToCheck = updates[i][j];

                var rules = pageOrderingRules.FirstOrDefault(x => x.Key == numberToCheck);

                if (rules == null) continue;

                foreach (var rule in rules)
                {
                    var ruleInUpdatesIndex = updates[i].IndexOf(rule);
                    if (ruleInUpdatesIndex != -1 && ruleInUpdatesIndex < j)
                    {
                        updates[i][j] = rule;
                        updates[i][ruleInUpdatesIndex] = numberToCheck;
                        j = ruleInUpdatesIndex;

                        incorrectUpdate = true;
                    }
                }
            }

            if (incorrectUpdate)
            {
                int midPointIndex = updates[i].Count / 2;
                totalUpdates += updates[i][midPointIndex];

                Console.WriteLine(string.Join(',', updates[i]));
            }
        }

        return totalUpdates;
    }

    public int SolvePart1(string input)
    {
        var pageOrderingRules = GetPageOrderingRules(input);
        var updates = GetUpdates(input);

        return GetTotalUpdates(pageOrderingRules, updates);
    }

    public int SolvePart2(string input)
    {
        var pageOrderingRules = GetPageOrderingRules(input);
        var updates = GetUpdates(input);

        return GetTotalUnorderedUpdates(pageOrderingRules, updates);
    }

    public async Task Driver()
    {
        var input = await new PuzzleInputDownloader().DownloadPuzzleInputAsync(2024, 5);

        //Console.WriteLine(input);

        //var part1Answer = SolvePart1(input);
        //Console.WriteLine(part1Answer);

        var part2Answer = SolvePart2(input);
        Console.WriteLine(part2Answer);
    }
}
