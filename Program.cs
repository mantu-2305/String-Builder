using System;
using System.Collections.Generic;
using System.Text;

internal class Solution
{
    private static IList<string> substrings(string s)
    {
        int length = s.Length;
        IList<string> list = new List<string>();

        for (int i = 0; i < length; i++)
        {
            for (int j = i + 1; j <= length; j++)
            {
                list.Add(s.Substring(i, j - i));
            }
        }
        return list;
    }

    private static int solve(string text, int A, int C)
    {
        int length = text.Length;
        StringBuilder sb = new StringBuilder(length);
        ISet<string> set = new HashSet<string>();

        int cost = 0;
        int len = 0;
        for (int i = 0; i < length; i++)
        {
            len = sb.Length;
            if (len == 0)
            {
                sb.Append(text[0]);
                set.Add(sb.ToString());
                cost += A;
                continue;
            }

            while (len > 0)
            {
                int sum = i + len;
                int end = (sum > length) ? length : sum;
                string lookAhead = text.Substring(i, end - i);
                if (set.Contains(lookAhead))
                {
                    if (lookAhead.Length == 1 && A <= C)
                    {
                        cost += A;
                    }
                    else
                    {
                        cost += C;
                    }

                    i += len - 1;
                    sb.Append(lookAhead);
                    foreach (string sub in substrings(sb.ToString()))
                    {
                        set.Add(sub);
                    }
                    break;
                }
                else
                {
                    if (lookAhead.Length == 1)
                    {
                        cost += A;
                        sb.Append(lookAhead);
                        foreach (string sub in substrings(sb.ToString()))
                        {
                            set.Add(sub);
                        }
                        break;
                    }
                    len--;
                }
            }
        } 
        //return sb.toString();
        return cost;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine(solve("a", 4, 5)); // 4 A
        Console.WriteLine(solve("ab", 4, 5)); // 8 A + A
        Console.WriteLine(solve("aba", 4, 3)); // 11 A + A + C
        Console.WriteLine(solve("aabaacaba", 4, 5)); // 26 A + A + A + C + A + C
        Console.WriteLine(solve("bacbacacb", 8, 9)); // 42 A + A + A + C + C
        Console.ReadLine();
    }
}
