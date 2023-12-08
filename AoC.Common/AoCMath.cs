namespace AoC.Common;

public static class AoCMath
{
    public static long LeastCommonMultiple(params long[] numbers)
    {
        List<long> work = numbers.ToList();
        while (work.Count > 1)
        {
            work[0] = LeastCommonMultiple(work[0], work[1]);
            work.RemoveAt(1);
        }
        return work[0];
    }

    public static long GreatestCommonDivisor(long a, long b)
    {
        while (b != 0L)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    public static long LeastCommonMultiple(long a, long b)
    {
        return a / GreatestCommonDivisor(a, b) * b;
    }
}
