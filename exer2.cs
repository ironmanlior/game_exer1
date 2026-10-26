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
     * Complete the 'bomberMan' function below.
     *
     * The function is expected to return a STRING_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. STRING_ARRAY grid
     */
    
    public static List<string> clean(List<string> grid){
        char[][] fullgrid = grid.Select(s => s.Replace('.', 'O').ToCharArray()).ToArray();
        
        Queue<(int X, int Y)> q = new Queue<(int X, int Y)>();
        
        for (int i = 0; i < grid.Count; i++){
            for (int j = 0; j < grid[i].Length; j++) {
                if (grid[i][j].ToString() == "O") q.Enqueue((i, j));
                while (q.Count != 0) {
                    
                    (int X, int Y) target = q.Dequeue();
                    
                    try {
                        if (grid[target.X][target.Y].ToString() == "O"){
                            q.Enqueue((target.X + 1, target.Y));
                            q.Enqueue((target.X - 1, target.Y));
                            q.Enqueue((target.X, target.Y + 1));
                            q.Enqueue((target.X, target.Y - 1));
                        }
                        
                        fullgrid[target.X][target.Y] = '.';
                        
                        char[] ch  = grid[target.X].ToCharArray();
                        ch[target.Y] = '.';
                        grid[target.X] = new string(ch);
                    } catch (IndexOutOfRangeException) {}
                    catch (ArgumentOutOfRangeException) {}
                    
                }
            }
        }
        
        return fullgrid.Select(row => new string(row)).ToList();;
    }
    
    public static List<string> bomberMan(int n, List<string> grid)
    {
        if (n == 1) {
            return grid;
        }
        if (n % 2 == 0) {
            return grid.ConvertAll(s => s.Replace(".", "O"));
        }
        // List<string> grid1 = grid.ToList();
        
        if (n % 4 == 3)
            return clean(grid);

        return clean(clean(grid));
        
        // for (int i = 3; i <= n; i += 2){
        //     grid1 = clean(grid1);
        // }
        // return grid1;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int r = Convert.ToInt32(firstMultipleInput[0]);

        int c = Convert.ToInt32(firstMultipleInput[1]);

        int n = Convert.ToInt32(firstMultipleInput[2]);

        List<string> grid = new List<string>();

        for (int i = 0; i < r; i++)
        {
            string gridItem = Console.ReadLine();
            grid.Add(gridItem);
        }

        List<string> result = Result.bomberMan(n, grid);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
