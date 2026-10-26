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
     * Complete the 'connectedCell' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts 2D_INTEGER_ARRAY matrix as parameter.
     */

    public static int connectedCell(List<List<int>> matrix)
    {
        int n = matrix.Count;
        int m = matrix[0].Count;

        bool[,] visited = new bool[n, m];

        (int r, int c)[] dir = {
            (-1, -1), (-1, 0), (-1, 1),
            (0, -1),           (0, 1),
            (1, -1),  (1, 0),  (1, 1)
        };

        int maxRegion = 0;

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                if (matrix[i][j] == 0 || visited[i, j])
                    continue;

                Queue<(int r, int c)> q =
                    new Queue<(int r, int c)>();

                q.Enqueue((i, j));
                visited[i, j] = true;

                int count = 0;

                while (q.Count > 0)
                {
                    (int r, int c) pos = q.Dequeue();
                    count++;

                    foreach (var d in dir)
                    {
                        int r = pos.r + d.r;
                        int c = pos.c + d.c;

                        if (r < 0 || r >= n ||
                            c < 0 || c >= m)
                            continue;

                        if (matrix[r][c] == 1 && !visited[r, c])
                        {
                            visited[r, c] = true;
                            q.Enqueue((r, c));
                        }
                    }
                }

                maxRegion = Math.Max(maxRegion, count);
            }
        }

        return maxRegion;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        int m = Convert.ToInt32(Console.ReadLine().Trim());

        List<List<int>> matrix = new List<List<int>>();

        for (int i = 0; i < n; i++)
        {
            matrix.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(matrixTemp => Convert.ToInt32(matrixTemp)).ToList());
        }

        int result = Result.connectedCell(matrix);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
