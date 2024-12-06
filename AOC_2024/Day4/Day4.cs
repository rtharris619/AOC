using AOC_Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC_2024;

public class Day4
{
    public const string WORD_SEARCH = "XMAS";

    private List<List<char>> GetGrid(string input)
    {
        var grid = new List<List<char>>();
        var rows = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        foreach (var row in rows)
        {
            var subGrid = new List<char>();
            foreach (var column in row)
            {
                subGrid.Add(column);
            }
            grid.Add(subGrid);
        }

        return grid;
    }

    private bool WordSearchHorizontal(List<List<char>> grid, int row, int col)
    {
        var canTraverse = col <= grid[row].Count - WORD_SEARCH.Length;

        if (!canTraverse) return false;

        var wordPosition = 0;
        for (var i = col; i < WORD_SEARCH.Length + col; i++)
        {
            if (grid[row][i] != WORD_SEARCH[wordPosition]) return false;
            wordPosition++;
        }

        return true;
    }

    private bool WordSearchBackwards(List<List<char>> grid, int row, int col)
    {
        var canTraverse = col >= WORD_SEARCH.Length - 1;

        if (!canTraverse) return false;

        var wordPosition = 0;
        for (var i = col; i > col - WORD_SEARCH.Length; i--)
        {
            if (grid[row][i] != WORD_SEARCH[wordPosition]) return false;
            wordPosition++;
        }

        return true;
    }

    private bool WordSearchVertical(List<List<char>> grid, int row, int col)
    {
        var canTraverse = row <= grid.Count - WORD_SEARCH.Length;

        if (!canTraverse) return false;

        var wordPosition = 0;
        for (var i = row; i < WORD_SEARCH.Length + row; i++)
        {
            if (grid[i][col] != WORD_SEARCH[wordPosition]) return false;
            wordPosition++;
        }

        return true;
    }

    private bool WordSearchVerticalBackwards(List<List<char>> grid, int row, int col)
    {
        var canTraverse = row >= WORD_SEARCH.Length - 1;

        if (!canTraverse) return false;

        var wordPosition = 0;
        for (var i = row; i > row - WORD_SEARCH.Length; i--)
        {
            if (grid[i][col] != WORD_SEARCH[wordPosition]) return false;
            wordPosition++;
        }

        return true;
    }

    private bool WordSearchDiagonalUpRight(List<List<char>> grid, int row, int col)
    {
        var canTraverse = row >= WORD_SEARCH.Length - 1 && col <= grid[row].Count - WORD_SEARCH.Length;
        if (!canTraverse) return false;

        var wordPosition = 0;
        
        for (var i = row; i > row - WORD_SEARCH.Length; i--)
        {
            if (grid[i][col + wordPosition] != WORD_SEARCH[wordPosition]) return false;
            wordPosition++;
        }
        
        return true;
    }

    private bool WordSearchDiagonalUpLeft(List<List<char>> grid, int row, int col)
    {
        var canTraverse = row >= WORD_SEARCH.Length - 1 && col >= WORD_SEARCH.Length - 1;
        if (!canTraverse) return false;

        var wordPosition = 0;

        for (var i = row; i > row - WORD_SEARCH.Length; i--)
        {
            if (grid[i][col - wordPosition] != WORD_SEARCH[wordPosition]) return false;
            wordPosition++;
        }

        return true;
    }

    private bool WordSearchDiagonalDownRight(List<List<char>> grid, int row, int col)
    {
        var canTraverse = row <= grid.Count - WORD_SEARCH.Length && col <= grid[row].Count - WORD_SEARCH.Length;
        if (!canTraverse) return false;

        var wordPosition = 0;

        for (var i = row; i < WORD_SEARCH.Length + row; i++)
        {
            if (grid[i][col + wordPosition] != WORD_SEARCH[wordPosition]) return false;
            wordPosition++;
        }

        return true;
    }

    private bool WordSearchDiagonalDownLeft(List<List<char>> grid, int row, int col)
    {
        var canTraverse = row <= grid.Count - WORD_SEARCH.Length && col >= WORD_SEARCH.Length - 1;
        if (!canTraverse) return false;

        var wordPosition = 0;

        for (var i = row; i < WORD_SEARCH.Length + row; i++)
        {
            if (grid[i][col - wordPosition] != WORD_SEARCH[wordPosition]) return false;
            wordPosition++;
        }

        return true;
    }

    private int FindXmas(List<List<char>> grid)
    {
        var result = 0;
        for (int row = 0; row < grid.Count; row++)
        {
            for (int col = 0; col < grid[row].Count; col++)
            {
                if (grid[row][col] != 'X') continue;

                result += WordSearchHorizontal(grid, row, col) ? 1 : 0;
                result += WordSearchBackwards(grid, row, col) ? 1 : 0;
                result += WordSearchVertical(grid, row, col) ? 1 : 0;
                result += WordSearchVerticalBackwards(grid, row, col) ? 1 : 0;

                result += WordSearchDiagonalUpRight(grid, row, col) ? 1 : 0;
                result += WordSearchDiagonalUpLeft(grid, row, col) ? 1 : 0;
                result += WordSearchDiagonalDownRight(grid, row, col) ? 1 : 0;
                result += WordSearchDiagonalDownLeft(grid, row, col) ? 1 : 0;
            }
        }
        return result;
    }

    private bool WordSearchMas(List<List<char>> grid, int row, int col)
    {
        var canTraverse = 
            row <= grid.Count - 2 && 
            row >= 1 &&
            col <= grid[row].Count - 2 &&
            col >= 1;

        if (!canTraverse) return false;

        var topLeft = grid[row - 1][col - 1];
        var topRight = grid[row - 1][col + 1];

        var bottomLeft = grid[row + 1][col - 1];
        var bottomRight = grid[row + 1][col + 1];

        if ((topLeft == 'M' && bottomRight == 'S') || (topLeft == 'S' && bottomRight == 'M'))
        {
            if ((topRight == 'M' && bottomLeft == 'S') || (topRight == 'S' && bottomLeft == 'M'))
            {
                return true;
            }
        }

        return false;
    }

    private int FindMas(List<List<char>> grid)
    {
        var result = 0;
        for (int row = 0; row < grid.Count; row++)
        {
            for (int col = 0; col < grid[row].Count; col++)
            {
                if (grid[row][col] != 'A') continue;

                result += WordSearchMas(grid, row, col) ? 1 : 0;
            }
        }
        return result;
    }

    public int SolvePart1(string input)
    {
        var grid = GetGrid(input);

        var result = FindXmas(grid);

        return result;
    }

    public int SolvePart2(string input)
    {
        var grid = GetGrid(input);

        var result = FindMas(grid);

        return result;
    }

    public async Task Driver()
    {
        var input = await new PuzzleInputDownloader().DownloadPuzzleInputAsync(2024, 4);
        //Console.WriteLine(input);

        //var part1Answer = SolvePart1(input);
        //Console.WriteLine(part1Answer);

        var part2Answer = SolvePart2(input);
        Console.WriteLine(part2Answer);
    }
}
