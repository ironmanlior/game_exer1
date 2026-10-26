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
     * Complete the 'printShortestPath' function below.
     *
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER i_start
     *  3. INTEGER j_start
     *  4. INTEGER i_end
     *  5. INTEGER j_end
     */

    public static void printShortestPath(int n, int i_start, int j_start, int i_end, int j_end)
    {
    // Print the distance along with the sequence of moves.
        (int r, int c)[] dir = {
            (-2, -1), // UL
            (-2,  1), // UR
            ( 0,  2), // R
            ( 2,  1), // LR
            ( 2, -1), // LL
            ( 0, -2)  // L
        };

        string[] names = {
            "UL", "UR", "R", "LR", "LL", "L"
        };

        bool[,] visited = new bool[n, n];
        (int r, int c)[,] parent = new (int, int)[n, n];
        string[,] moves = new string[n, n];

        Queue<(int r, int c)> q =
            new Queue<(int r, int c)>();

        q.Enqueue((i_start, j_start));
        visited[i_start, j_start] = true;

        while (q.Count > 0)
        {
            (int r, int c) pos = q.Dequeue();

            if (pos.r == i_end && pos.c == j_end)
                break;

            for (int i = 0; i < 6; i++)
            {
                int r = pos.r + dir[i].r;
                int c = pos.c + dir[i].c;

                if (r < 0 || r >= n ||
                    c < 0 || c >= n)
                    continue;

                if (visited[r, c])
                    continue;

                visited[r, c] = true;
                parent[r, c] = pos;
                moves[r, c] = names[i];

                q.Enqueue((r, c));
            }
        }

        if (!visited[i_end, j_end])
        {
            Console.WriteLine("Impossible");
            return;
        }

        List<string> path = new List<string>();

        (int r, int c) current = (i_end, j_end);

        while (current.r != i_start ||
            current.c != j_start)
        {
            path.Add(moves[current.r, current.c]);
            current = parent[current.r, current.c];
        }

        path.Reverse();

        Console.WriteLine(path.Count);
        Console.WriteLine(string.Join(" ", path));
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int i_start = Convert.ToInt32(firstMultipleInput[0]);

        int j_start = Convert.ToInt32(firstMultipleInput[1]);

        int i_end = Convert.ToInt32(firstMultipleInput[2]);

        int j_end = Convert.ToInt32(firstMultipleInput[3]);

        Result.printShortestPath(n, i_start, j_start, i_end, j_end);
    }
}
