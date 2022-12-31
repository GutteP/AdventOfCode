using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Common;

public static class Spread
{
    public static IEnumerable<long> Gradient(long from, long to, int count)
    {
        List<long> gradient = new();
        if (to - from < count)
        {
            for (int i = 0; i <= to - from; i++)
            {
                gradient.Add(from + i);
            }
            return gradient;
        }
        long step = (to - from) / count;
        for (int i = 0; i <= count; i++)
        {
            gradient.Add(from + (step * i));
        }
        return gradient;
    }
}
