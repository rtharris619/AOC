using AOC_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2024;

public class Day2
{
    private bool IsSafe(List<int> report)
    {
        var increasing = report[0] < report[1];

        for (int i = 0; i < report.Count - 1; i++)
        {
            if (report[i] > report[i + 1] && increasing)
                return false;

            if (report[i] < report[i + 1] && !increasing) 
                return false;

            if (report[i] == report[i + 1])
                return false;

            if (Math.Abs(report[i] - report[i + 1]) > 3)
                return false;
        }

        return true;
    }

    private bool IsSafeWithDampener(List<int> report)
    {
        var set = new List<bool>();

        for (int i = 1; i < report.Count - 1; i++)
        {
            var subset = report.Skip(i).Take(report.Count - i + 1).ToList();
            //Console.WriteLine(subset);
            var safe = IsSafe(subset);
            set.Add(safe);
        }

        Console.WriteLine(string.Join(',', set));
        //Console.WriteLine(set.Any(x => x == true));

        return set.Any(x => x == true);
    }

    //private bool IsSafeWithDampener(List<int> report)
    //{
    //    var increasing = report[0] < report[1]; // 1, 2, 7, 4, 5
    //    increasing = increasing && report[0] < report[2];

    //    for (int i = 0; i < report.Count - 1; i++)
    //    {
    //        var canAdvanceTwoPlaces = i + 2 < report.Count;

    //        if (report[i] > report[i + 1] && increasing)
    //            if (canAdvanceTwoPlaces && report[i] > report[i + 2] && increasing)
    //                return false;

    //        if (report[i] < report[i + 1] && !increasing)
    //            if (canAdvanceTwoPlaces && report[i] < report[i + 2] && !increasing)
    //                return false;

    //        if (report[i] == report[i + 1])
    //            if (canAdvanceTwoPlaces && report[i] == report[i + 2])
    //                return false;

    //        if (Math.Abs(report[i] - report[i + 1]) > 3)
    //            if (canAdvanceTwoPlaces && Math.Abs(report[i] - report[i + 2]) > 3)
    //                return false;
    //    }

    //    return true;
    //}

    private List<List<int>> ExtractReports(string input)
    {
        var reports = new List<List<int>>();
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            var report = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => Convert.ToInt32(x)).ToList();

            if (report == null || report.Count == 0) throw new Exception("No luck");

            reports.Add(report);
        }

        return reports;
    }

    public int SolvePart1(string input)
    {
        var reports = ExtractReports(input);
        var safeReportSum = 0;

        foreach (var report in reports)
        {
            safeReportSum += IsSafe(report) ? 1 : 0;
        }

        return safeReportSum;
    }

    public int SolvePart2(string input)
    {
        var reports = ExtractReports(input);
        var safeReportSum = 0;

        foreach (var report in reports)
        {
            safeReportSum += IsSafeWithDampener(report) ? 1 : 0;
        }

        return safeReportSum;
    }

    public async Task Driver()
    {
        var input = await new PuzzleInputDownloader().DownloadPuzzleInputAsync(2024, 2);

        //var part1Answer = SolvePart1(input);
        //Console.WriteLine(part1Answer);

        var part2Answer = SolvePart2(input);
        Console.WriteLine(part2Answer);
    }
}
