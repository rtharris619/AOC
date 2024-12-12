using AOC_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2024;

public class Day7
{
    public enum Operator
    {
        Add, Multiply
    }

    public class Node
    {
        public Int64 Value;
        public Node? Left, Right;
    }

    public Node NewNode(Node root, Int64 value)
    {
        if (root == null)
        {
            return new Node { Value = value };
        }

        root.Left = NewNode(root.Left, value);
        root.Right = NewNode(root.Right, value);

        return root;
    }

    public Int64 Calculate(Node root, Int64 testValue, Operator? op = null)
    {
        if (root == null)
        {
            if (op == Operator.Add)
                return 0;
            else
                return 1;
        }

        var addResult = root.Value + Calculate(root.Left, testValue, Operator.Add);

        var multiplyResult = root.Value * Calculate(root.Right, testValue, Operator.Multiply);

        if (op == Operator.Add)
            if (addResult >= testValue)
                return addResult;
            else
                return root.Value + Calculate(root.Left, testValue, Operator.Add);

        if (op == Operator.Multiply)
            if (multiplyResult >= testValue)
                return root.Value * Calculate(root.Right, testValue, Operator.Multiply);

        return op == Operator.Add ? 0 : 1;
    }


    public record Equation
    {
        public Equation(Int64 testValue, List<Int64> numbers)
        {
            TestValue = testValue;
            Numbers = numbers;
        }

        public Int64 TestValue { get; init; }
        public List<Int64> Numbers { get; init; }
    }

    private List<Equation> ExtractData(string input)
    {
        var equations = new List<Equation>();
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
           .ToList();

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            if (trimmedLine.Length == 0) continue;

            var lineSplit = trimmedLine.Split(':');

            var testValue = Int64.Parse(lineSplit[0].Trim());
            var numbers = lineSplit[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)
                                      .Select(Int64.Parse)
                                      .ToList();

            equations.Add(new Equation(testValue, numbers));
        }

        return equations;
    }

    private Int64 GetTotalCalibration(List<Equation> equations)
    {
        var totalCalibration = 0;

        foreach (var equation in equations)
        {
            var testValue = equation.TestValue;

            var numbers = equation.Numbers;

            Node? root = null;

            for (var i = 0; i < numbers.Count; i++)
            {
                root = NewNode(root, numbers[i]);
            }

            var result = Calculate(root, testValue, Operator.Add);

            if (result == testValue)
            {

            }
        }

        return totalCalibration;
    }

    public Int64 SolvePart1(string input)
    {
        var data = ExtractData(input);

        var result = GetTotalCalibration(data);

        return result;
    }

    public async Task Driver()
    {
        var input = await new PuzzleInputDownloader().DownloadPuzzleInputAsync(2024, 7);

        input = @"
                190: 10 19
                3267: 81 40 27
                83: 17 5
                156: 15 6
                7290: 6 8 6 15
                161011: 16 10 13
                192: 17 8 14
                21037: 9 7 18 13
                292: 11 6 16 20
        ";

        //Console.WriteLine(input);

        var part1Answer = SolvePart1(input);
        Console.WriteLine(part1Answer);
    }
}
