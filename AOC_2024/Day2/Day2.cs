using AOC_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
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
        var increasing = report[0] < report[1]; // 7, 8, 6, 4, 3
        increasing = increasing && report[0] < report[2];

        for (int i = 0; i < report.Count - 1; i+=2)
        {
            var canAdvanceTwoPlaces = i + 2 < report.Count;

            if (report[i] > report[i + 1] && increasing)
                if (canAdvanceTwoPlaces && report[i] > report[i + 2] && increasing)
                    return false;

            if (report[i] < report[i + 1] && !increasing)
                if (canAdvanceTwoPlaces && report[i] < report[i + 2] && !increasing)
                    return false;

            if (report[i] == report[i + 1])
                if (canAdvanceTwoPlaces && report[i] == report[i + 2])
                    return false;

            if (Math.Abs(report[i] - report[i + 1]) > 3)
                if (canAdvanceTwoPlaces && Math.Abs(report[i] - report[i + 2]) > 3)
                    return false;
        }

        return true;
    }

    //private bool IsSafeWithDampener(List<int> report)
    //{
    //    var increasing = report[0] < report[1];

    //    var leftPointer = 0;
    //    var rightPointer = 2;

    //    for (int i = 0; i < report.Count - 1; i++)
    //    {
    //        var canAdvanceTwoPlaces = i + 2 < report.Count;

    //        for (int j = leftPointer; j <= rightPointer; j++) // 7, 8, 6
    //        {
    //            if (report[j] > report[j + 1] && increasing)
    //                if (report[j] > report[j + 2] && increasing)
    //                    return false;
    //        }

    //        leftPointer += 1;
    //        rightPointer += 1;

    //        //if (report[i] > report[i + 1] && increasing)
    //        //    if (canAdvanceTwoPlaces && report[i] > report[i + 2] && increasing)
    //        //        return false;

    //        //if (report[i] < report[i + 1] && !increasing)
    //        //    if (canAdvanceTwoPlaces && report[i] < report[i + 2] && !increasing)
    //        //        return false;

    //        //if (report[i] == report[i + 1])
    //        //    if (canAdvanceTwoPlaces && report[i] == report[i + 2])
    //        //        return false;

    //        //if (Math.Abs(report[i] - report[i + 1]) > 3)
    //        //    if (canAdvanceTwoPlaces && Math.Abs(report[i] - report[i + 2]) > 3)
    //        //        return false;
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

        var part1Answer = SolvePart1(input);
        Console.WriteLine(part1Answer);

        var part2Answer = SolvePart2(input);
        Console.WriteLine(part2Answer);
    }
}
