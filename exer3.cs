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
     * Complete the 'queensAttack' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER k
     *  3. INTEGER r_q
     *  4. INTEGER c_q
     *  5. 2D_INTEGER_ARRAY obstacles
     */
    
    public static bool inboard(int n, (int r, int c) pos){
        return (pos.r <= n && pos.r > 0) && (pos.c <= n && pos.c > 0);
    }
    
    
    public static int queensAttack(int n, int k, int r_q, int c_q, List<List<int>> obstacles)
    {
        (int r, int c) q = (r_q, c_q);
        bool[] directions_con = new bool[8];
        (int r, int c)[] dir = {(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)};
        for (int i = 0; i < directions_con.Length; i++) directions_con[i] = true;
        
        HashSet<(int r, int c)> obstacles_new = obstacles.Select(o => (o[0], o[1])).ToHashSet();
        
        int count = 0;
        
        int w = 1;
        
        while (directions_con.Any(b => b)) {
            for (int i = 0; i < dir.Length; i++) {
                if (directions_con[i]){
                    (int r, int c) pos = (q.r + (w * dir[i].r), q.c + (w * dir[i].c));
                    if (obstacles_new.Contains(pos) || !inboard(n, pos)) directions_con[i] = false;
                    else count++;
                }
            }
            w++;
        }
        return count;
        
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int k = Convert.ToInt32(firstMultipleInput[1]);

        string[] secondMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int r_q = Convert.ToInt32(secondMultipleInput[0]);

        int c_q = Convert.ToInt32(secondMultipleInput[1]);

        List<List<int>> obstacles = new List<List<int>>();

        for (int i = 0; i < k; i++)
        {
            obstacles.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(obstaclesTemp => Convert.ToInt32(obstaclesTemp)).ToList());
        }

        int result = Result.queensAttack(n, k, r_q, c_q, obstacles);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
