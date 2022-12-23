using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Common;

public static class Ranges
{
    public static bool Overlap(this (int a, int b) r1, (int a, int b) r2)
    {
        if (r1.a > r1.b || r2.a > r2.b)
        {
            throw new Exception("a bigger or equal to b");
        }


        if (r1.b + 1 < r2.a || r1.a - 1 > r2.b)
        {
            return false;
        }
        return true;
    }

    public static (int a, int b) CombineOverlaping(this (int a, int b) r1, (int a, int b) r2)
    {
        if (r2.a < r1.a) r1.a = r2.a;
        if (r2.b > r1.b) r1.b = r2.b;
        return r1;
    }
    public static List<(int a, int b)> CombineRanges(this List<(int a, int b)> l)
    {
        l = l.OrderBy(x => x.a).ToList();
        for (int i = 0; i < l.Count - 1;)
        {
            if (l[i].Overlap(l[i + 1]))
            {
                l[i] = l[i].CombineOverlaping(l[i + 1]);
                l.RemoveAt(i + 1);
            }
            else i++;
        }
        return l;
    }
}
