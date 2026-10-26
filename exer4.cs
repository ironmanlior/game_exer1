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
    static int[,] prefix;
    static long[] factorial;
    static long[] inverseFactorial;

    const long MOD = 1000000007;
    
    
    static long modPow(long a, long b)
    {
        long result = 1;

        while (b > 0)
        {
            if (b % 2 == 1)
                result = result * a % MOD;

            a = a * a % MOD;
            b /= 2;
        }

        return result;
    }
    /*
     * Complete the 'initialize' function below.
     *
     * The function accepts STRING s as parameter.
     */

    public static void initialize(string s)
    {
    // This function is called once before all queries.
        int n = s.Length;

        prefix = new int[n + 1, 26];
        factorial = new long[n + 1];
        inverseFactorial = new long[n + 1];

        // 1. Count each letter using prefix sums
        for (int i = 1; i <= n; i++)
        {
            for (int j = 0; j < 26; j++)
            {
                prefix[i, j] = prefix[i - 1, j];
            }

            prefix[i, s[i - 1] - 'a']++;
        }

        // 2. Calculate factorials
        factorial[0] = 1;

        for (int i = 1; i <= n; i++)
        {
            factorial[i] = factorial[i - 1] * i % MOD;
        }

        // 3. Calculate inverse factorials
        inverseFactorial[n] = modPow(factorial[n], MOD - 2);

        for (int i = n; i > 0; i--)
        {
            inverseFactorial[i - 1] =
                inverseFactorial[i] * i % MOD;
        }
    }

    /*
     * Complete the 'answerQuery' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER l
     *  2. INTEGER r
     */

    public static int answerQuery(int l, int r)
    {
    // Return the answer for this query modulo 1000000007.
        int pairs = 0;
        int odd = 0;
        long result = 1;

        for (int i = 0; i < 26; i++)
        {
            int count = prefix[r, i] - prefix[l - 1, i];

            int p = count / 2;

            pairs += p;
            odd += count % 2;

            result = result * inverseFactorial[p] % MOD;
        }

        result = result * factorial[pairs] % MOD;
        result = result * Math.Max(1, odd) % MOD;

        return (int)result;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string s = Console.ReadLine();

        Result.initialize(s);

        int q = Convert.ToInt32(Console.ReadLine().Trim());

        for (int qItr = 0; qItr < q; qItr++)
        {
            string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

            int l = Convert.ToInt32(firstMultipleInput[0]);

            int r = Convert.ToInt32(firstMultipleInput[1]);

            int result = Result.answerQuery(l, r);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
