using AOC_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2024;

public class Day1
{
    public int CalculateTotalDistance(List<int> left, List<int> right)
    {
        left.Sort();
        right.Sort();

        var sumDistances = 0;

        for (int i = 0; i < left.Count; i++) 
        {
            var distance = Math.Abs(left[i] - right[i]);
            sumDistances += distance;            
        }

        return sumDistances;
    }

    public int CalculateSimilarityScore(List<int> left, List<int> right)
    {
        var totalSimilarityScore = 0;

        for (int i = 0; i < left.Count; i++)
        {
            var similarityScore = left[i] * right.Count(x => x.Equals(left[i]));
            totalSimilarityScore += similarityScore;
        }

        return totalSimilarityScore;
    }

    public (List<int>, List<int>) PopulateArrays(string input)
    {
        var leftList = new List<int>();
        var rightList = new List<int>();

        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        
        foreach (var line in lines)
        {
            var leftRight = line.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            leftList.Add(Convert.ToInt32(leftRight[0]));
            rightList.Add(Convert.ToInt32(leftRight[1]));
        }       

        return (leftList, rightList);
    }

    public async Task Driver()
    {
        var input = await new PuzzleInputDownloader().DownloadPuzzleInputAsync(2024, 1);

        var (leftList, rightList) = PopulateArrays(input);

        //var answerA = CalculateTotalDistance(leftList, rightList);
        //Console.WriteLine(answerA);

        var answerB = CalculateSimilarityScore(leftList, rightList);
        Console.WriteLine(answerB);
    }
}
