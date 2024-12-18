﻿using AOC_Shared;
using System.Data;

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

    private int CalculateCycles(List<(int Row, int Col)> visited, (int Row, int Col) guardStartingPosition)
    {
        var result = 0;

        var direction = Direction.Up;
        var guardPosition = guardStartingPosition;

        // for each item in visited check for a duplicate point then we have a cycle
        var set = new HashSet<(int Row, int Col, Direction direction)>();

        foreach (var node in visited)
        {
            if (node == guardStartingPosition) continue;

            if (set.Contains((node.Row, node.Col, direction)))
            {
                result++;
                continue;
            }

            set.Add((node.Row, node.Col, direction));

            while (true)
            {

                if (direction == Direction.Up)
                {
                    if (visited.Any(x => x.Row == node.Row && x.Col == node.Col)) // hit node
                    {
                        var nextCol = node.Col + 1;

                        if (visited.Any(x => x.Row == node.Row && x.Col == nextCol)) // can we go right
                        {
                            direction = Direction.Right;
                            guardPosition.Col++;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else // we haven't hit the node
                    {
                        var nextRow = guardPosition.Row - 1;

                        if (visited.Any(x => x.Row == nextRow && x.Col == guardPosition.Col))
                        {
                            guardPosition.Row--; // Keep going Up
                        }
                        else
                        {
                            // can't go up so go right
                            direction = Direction.Right;
                        }
                    }
                }

                if (direction == Direction.Down)
                {
                    if (visited.Any(x => x.Row == node.Row && x.Col == node.Col)) // hit node
                    {
                        var nextCol = node.Col - 1;

                        if (visited.Any(x => x.Row == node.Row && x.Col == nextCol)) // can we go left
                        {
                            direction = Direction.Left;
                            guardPosition.Col--;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else // we haven't hit the node
                    {
                        var nextRow = guardPosition.Row + 1;

                        if (visited.Any(x => x.Row == nextRow && x.Col == guardPosition.Col))
                        {
                            guardPosition.Row++; // Keep going down
                        }
                        else
                        {
                            // can't go down so go left
                            direction = Direction.Left;
                        }
                    }
                }

                if (direction == Direction.Left)
                {
                    if (visited.Any(x => x.Row == node.Row && x.Col == node.Col)) // hit node
                    {
                        var nextRow = node.Row - 1;

                        if (visited.Any(x => x.Row == nextRow && x.Col == node.Col)) // can we go up
                        {
                            direction = Direction.Up;
                            guardPosition.Row--;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else // we haven't hit the node
                    {
                        var nextCol = guardPosition.Col - 1;

                        if (visited.Any(x => x.Row == guardPosition.Col && x.Col == nextCol)) // can we go left
                        {
                            guardPosition.Col--; // Keep going left
                        }
                        else
                        {
                            // can't go left so go up
                            direction = Direction.Up;
                        }
                    }
                }

                if (direction == Direction.Right)
                {
                    if (visited.Any(x => x.Row == node.Row && x.Col == node.Col)) // hit node
                    {
                        var nextRow = node.Row + 1;

                        if (visited.Any(x => x.Row == nextRow && x.Col == node.Col)) // can we go down
                        {
                            direction = Direction.Down;
                            guardPosition.Row++;
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else // we haven't hit the node
                    {
                        var nextCol = guardPosition.Row + 1;

                        if (visited.Any(x => x.Row == guardPosition.Row && x.Col == nextCol)) // can we go right
                        {
                            guardPosition.Col++; // Keep going right
                        }
                        else
                        {
                            // can't go left so go up
                            direction = Direction.Up;
                        }
                    }
                }

                if (guardPosition.Row >= RowCount || guardPosition.Col >= ColCount)
                    break;

                if (guardPosition.Row < 0 || guardPosition.Col < 0)
                    break;
            }


        }

        return result;
    }


    private int CalculateNumberOfCycles(List<(int Row, int Col)> visited, (int Row, int Col) guardStartingPosition)
    {
        var cycles = 0;

        var direction = Direction.Up;
        var guardPosition = guardStartingPosition;

        var obstructions = new (int Row, int Col)[]
        {
            (guardPosition.Row - 1, guardPosition.Col - 1),
            //(guardPosition.Row - 2, guardPosition.Col)
        };

        foreach (var obstruction in obstructions) 
        {
            var seen = new HashSet<(int Row, int Col, Direction direction)>();

            while (true)
            {
                if (seen.Contains((guardPosition.Row, guardPosition.Col, direction)))
                {
                    cycles++;
                    break;
                }

                seen.Add((guardPosition.Row, guardPosition.Col, direction));

                if (direction == Direction.Up)
                {
                    if (obstruction.Row == guardPosition.Row && obstruction.Col == guardPosition.Col)
                    {
                        direction = Direction.Right;

                        // backtrack
                        guardPosition.Row++;

                        continue;
                    }

                    var nextRow = guardPosition.Row - 1;
                    if (!visited.Any(x => x.Row == nextRow && x.Col == guardPosition.Col))
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
                    if (obstruction.Row == guardPosition.Row && obstruction.Col == guardPosition.Col)
                    {
                        direction = Direction.Left;

                        // backtrack
                        guardPosition.Row--;

                        continue;
                    }

                    var nextRow = guardPosition.Row + 1;
                    if (!visited.Any(x => x.Row == nextRow && x.Col == guardPosition.Col))
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
                    if (obstruction.Row == guardPosition.Row && obstruction.Col == guardPosition.Col)
                    {
                        direction = Direction.Up;

                        // backtrack
                        guardPosition.Col++;

                        continue;
                    }

                    var nextCol = guardPosition.Col - 1;
                    if (!visited.Any(x => x.Col == nextCol && x.Row == guardPosition.Row))
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
                    if (obstruction.Row == guardPosition.Row && obstruction.Col == guardPosition.Col)
                    {
                        direction = Direction.Down;

                        // backtrack
                        guardPosition.Col--;

                        continue;
                    }

                    var nextCol = guardPosition.Col + 1;
                    if (!visited.Any(x => x.Col == nextCol && x.Row == guardPosition.Row))
                    {
                        direction = Direction.Down;
                    }
                    else
                    {
                        guardPosition.Col++;
                    }
                }
            }
        }

        return cycles;
    }

    public int SolvePart2(string input)
    {
        var data = ExtractData(input);

        var visited = CalculateRoute(data.Item1, data.Item2, Direction.Up, [data.Item1]);

        var cycles = CalculateNumberOfCycles(visited, data.Item1);

        return cycles;
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

        //input = @"
        //        ....#.....
        //        ....XXXXX#
        //        ....X...X.
        //        ..#.X...X.
        //        ..XXXXX#X.
        //        ..X.X.X.X.
        //        .#XX^XXXX.
        //        .XXXXXXX#.
        //        #XXXXXXX..
        //        ......#X..
        //";

        var part2Answer = SolvePart2(input);
        Console.WriteLine(part2Answer);
    }
}
