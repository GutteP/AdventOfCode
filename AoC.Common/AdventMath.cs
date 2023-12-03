using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Common;

public static class AdventMath
{
    public static T Product<T>(this IEnumerable<T> numbers) where T : INumber<T>
    {
        T result = T.Zero;
        result++;
        foreach (T n in numbers)
        {
            result = result * n;
        }
        return result;
    }
}
