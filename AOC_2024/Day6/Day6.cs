using AOC_Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC_2024;

public class Day6
{
    public enum Direction
    {
        Up, Right, Down, Left
    }

    public int RowCount;
    public int ColCount;

    public ((int Row, int Col), List<(int Row, int Col)>) ExtractData(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .ToList();

        (int Row, int Col) guardPosition = (0, 0);

        var obstacles = new List<(int Row, int Col)>();

        var rowCount = 0;

        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            if (trimmedLine.Length == 0) continue;

            var obstaclesIndices = trimmedLine
                .Select((ch, index) => new { ch, index }) // Pair character with index
                .Where(pair => pair.ch == '#')         // Filter by target character
                .Select(pair => pair.index)              // Select indices
                .ToList();

            foreach (var col in obstaclesIndices)
            {
                obstacles.Add((rowCount, col));
            }

            if (trimmedLine.Any(x => x == '^'))
            {
                guardPosition = (rowCount, trimmedLine.IndexOf('^'));
                ColCount = trimmedLine.Length;
            }

            rowCount++;
        }

        RowCount = rowCount;

        return (guardPosition, obstacles);
    }

    // Only works for small datasets due to Stack Overflow.
    private List<(int Row, int Col)> CalculateRouteRecursive(
        (int Row, int Col) guardPosition, 
        List<(int Row, int Col)> obstacles, 
        Direction direction, 
        List<(int Row, int Col)> visited)
    {
        if (guardPosition.Row >= RowCount || guardPosition.Col >= ColCount)
            return visited;

        if (guardPosition.Row < 0 || guardPosition.Col < 0)
            return visited;

        if (!visited.Any(x => x == guardPosition))
        {
            visited.Add(guardPosition);
        }

        if (direction == Direction.Up)
        {
            var nextRow = guardPosition.Row - 1;
            if (obstacles.Any(x => x.Row == nextRow && x.Col == guardPosition.Col))
            {
                direction = Direction.Right;
            }
            else
            {
                guardPosition.Row--;
            }
        }

        if (direction == Direction.Down)
        {
            var nextRow = guardPosition.Row + 1;
            if (obstacles.Any(x => x.Row == nextRow && x.Col == guardPosition.Col))
            {
                direction = Direction.Left;
            }
            else
            {
                guardPosition.Row++;
            }
        }

        if (direction == Direction.Left)
        {
            var nextCol = guardPosition.Col - 1;
            if (obstacles.Any(x => x.Col == nextCol && x.Row == guardPosition.Row))
            {
                direction = Direction.Up;
            }
            else
            {
                guardPosition.Col--;
            }
        }

        if (direction == Direction.Right)
        {
            var nextCol = guardPosition.Col + 1;
            if (obstacles.Any(x => x.Col == nextCol && x.Row == guardPosition.Row))
            {
                direction = Direction.Down;
            }
            else
            {
                guardPosition.Col++;
            }
        }

        return CalculateRouteRecursive(guardPosition, obstacles, direction, visited);
    }

    private List<(int Row, int Col)> CalculateRoute(
        (int Row, int Col) guardPosition,
        List<(int Row, int Col)> obstacles,
        Direction direction,
        List<(int Row, int Col)> visited)
    {
        while (true)
        {
            if (!visited.Any(x => x == guardPosition))
            {
                visited.Add(guardPosition);
            }

            if (direction == Direction.Up)
            {
                var nextRow = guardPosition.Row - 1;
                if (obstacles.Any(x => x.Row == nextRow && x.Col == guardPosition.Col))
                {
                    direction = Direction.Right;
                }
                else
                {
                    guardPosition.Row--;
                }
            }

            if (direction == Direction.Down)
            {
                var nextRow = guardPosition.Row + 1;
                if (obstacles.Any(x => x.Row == nextRow && x.Col == guardPosition.Col))
                {
                    direction = Direction.Left;
                }
                else
                {
                    guardPosition.Row++;
                }
            }

            if (direction == Direction.Left)
            {
                var nextCol = guardPosition.Col - 1;
                if (obstacles.Any(x => x.Col == nextCol && x.Row == guardPosition.Row))
                {
                    direction = Direction.Up;
                }
                else
                {
                    guardPosition.Col--;
                }
            }

            if (direction == Direction.Right)
            {
                var nextCol = guardPosition.Col + 1;
                if (obstacles.Any(x => x.Col == nextCol && x.Row == guardPosition.Row))
                {
                    direction = Direction.Down;
                }
                else
                {
                    guardPosition.Col++;
                }
            }

            if (guardPosition.Row >= RowCount || guardPosition.Col >= ColCount)
                break;

            if (guardPosition.Row < 0 || guardPosition.Col < 0)
                break;
        }

        return visited;
    }

    public int SolvePart1(string input)
    {
        var data = ExtractData(input);

        var result = CalculateRoute(data.Item1, data.Item2, Direction.Up, [data.Item1]);

        return result.Count;
    }

    public int SolvePart2(string input)
    {
        var data = ExtractData(input);

        var result = CalculateRoute(data.Item1, data.Item2, Direction.Up, [data.Item1]);

        return result.Count;
    }

    public async Task Driver()
    {
        var input = await new PuzzleInputDownloader().DownloadPuzzleInputAsync(2024, 6);

        input = @"
                ....#.....
                .........#
                ..........
                ..#.......
                .......#..
                ..........
                .#..^.....
                ........#.
                #.........
                ......#...
        ";

        //Console.WriteLine(input);

        //var part1Answer = SolvePart1(input);
        //Console.WriteLine(part1Answer);

        var part2Answer = SolvePart2(input);
        Console.WriteLine(part2Answer);
    }
}
