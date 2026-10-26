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
     * Complete the 'isValid' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts STRING s as parameter.
     */

    public static string isValid(string s)
    {
        if (s.Length <= 2) return "YES";
        Dictionary<char, int> res = new Dictionary<char, int>();
        foreach (char ch in s){
            try {
                res[ch] += 1;
            } catch (Exception) {
                res.Add(ch, 1);
            }
        }
        if (res.Values.Distinct().Count() <= 1) return "YES";
        if (res.Values.Distinct().Count() > 2) return "NO";
        
        List<char> keys = res.Where(x => x.Value == 1).Select(x => x.Key).ToList();
        if (keys.Count == 1) {
            res.Remove(keys[0]);
        } else {
            char max = res.MaxBy(kvp => kvp.Value).Key;
            res[max] -= 1;
        }
        
        if (res.Values.Distinct().Count() <= 1) return "YES";
        return "NO";
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string s = Console.ReadLine();

        string result = Result.isValid(s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
