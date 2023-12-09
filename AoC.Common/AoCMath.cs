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


    /// <summary>
    /// Hitta nästkommande nummer. Algoritm från 2023-09. Antar att den bara fungerar på "enkla" sekvenser.. 
    /// </summary>
    /// <param name="seq"></param>
    /// <returns>Nästa nummer</returns>
    public static int NextInSequence(IEnumerable<int> seq)
    {
        List<List<int>> work = AddDiff(seq.ToList());
        for (int i = work.Count - 2; i >= 0; i--)
        {
            work[i].Add(work[i][work[i].Count - 1] + work[i + 1][work[i + 1].Count - 1]);
        }
        return work[0][work[0].Count - 1];
    }
    /// <summary>
    /// Hitta föregående nummer. Algoritm från 2023-09. Antar att den bara fungerar på "enkla" sekvenser.. 
    /// </summary>
    /// <param name="seq"></param>
    /// <returns>Föregående nummer</returns>
    public static int PreviousInSequence(IEnumerable<int> seq)
    {
        List<List<int>> work = AddDiff(seq.ToList());
        for (int i = work.Count - 2; i >= 0; i--)
        {
            int prev = work[i][0] - work[i + 1][0];
            work[i].Insert(0, prev);
        }
        return work[0][0];
    }
    private static List<List<int>> AddDiff(List<int> seq)
    {
        List<List<int>> work = new();
        List<int> last = seq;
        while (!last.All(x => x == 0))
        {
            List<int> diff = new();
            for (int i = 0; i < last.Count - 1; i++)
            {
                diff.Add(last[i + 1] - last[i]);
            }
            work.Add(new() { last[0], last[last.Count - 1] });
            last = diff;
        }
        return work;
    }
}
