using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'countLuck' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts following parameters:
     *  1. STRING_ARRAY matrix
     *  2. INTEGER k
     */

    public static string countLuck(List<string> matrix, int k)
    {
        int n = matrix.Count;
        int m = matrix[0].Length;

        bool[,] visited = new bool[n, m];

        int startR = 0;
        int startC = 0;

        // Find M
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (matrix[i][j] == 'M')
                {
                    startR = i;
                    startC = j;
                }
            }
        }

        int waves = DFS(matrix, visited, startR, startC);

        return waves == k ? "Impressed" : "Oops!";
    }
    
    
    private static int DFS(List<string> matrix, bool[,] visited, int r, int c)
    {
        if (matrix[r][c] == '*')
            return 0;

        visited[r, c] = true;

        int n = matrix.Count;
        int m = matrix[0].Length;

        (int r, int c)[] dir = {
            (-1, 0),
            (1, 0),
            (0, -1),
            (0, 1)
        };

        List<(int r, int c)> options =
            new List<(int r, int c)>();

        // Find all available directions
        foreach (var d in dir)
        {
            int nr = r + d.r;
            int nc = c + d.c;

            if (nr < 0 || nr >= n ||
                nc < 0 || nc >= m)
                continue;

            if (matrix[nr][nc] != 'X' && !visited[nr, nc])
                options.Add((nr, nc));
        }

        // Search for the portkey
        foreach (var next in options)
        {
            if (visited[next.r, next.c])
                continue;

            int result = DFS(matrix, visited, next.r, next.c);

            if (result != -1)
            {
                // More than one direction = wand wave
                return result + (options.Count > 1 ? 1 : 0);
            }
        }

        return -1;
    }
    
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine().Trim());

        for (int tItr = 0; tItr < t; tItr++)
        {
            string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

            int n = Convert.ToInt32(firstMultipleInput[0]);

            int m = Convert.ToInt32(firstMultipleInput[1]);

            List<string> matrix = new List<string>();

            for (int i = 0; i < n; i++)
            {
                string matrixItem = Console.ReadLine();
                matrix.Add(matrixItem);
            }

            int k = Convert.ToInt32(Console.ReadLine().Trim());

            string result = Result.countLuck(matrix, k);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
