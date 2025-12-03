using AoC.Common;

namespace AoC._2025;

public class D02
{
    public long? PartOne(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<(long From, long To)> ranges = InputReader.ReadString(inputPath).SplitOn(Seperator.Comma)
            .Select(x => (long.Parse(x.Split('-', StringSplitOptions.TrimEntries)[0]), long.Parse(x.Split('-', StringSplitOptions.TrimEntries)[1]))).ToList();

        List<long> invalidIds = [];
        foreach (var range in ranges)
        {
            for (long i = range.From; i <= range.To; i++)
            {
                string v = i.ToString();
                if (v.Length % 2 == 0)
                {
                    if (v[..(v.Length / 2)] == v[(v.Length / 2)..])
                    {
                        invalidIds.Add(i);
                    }
                }
            }
        }

        return invalidIds.Sum();
    }

    public long? PartTwo(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<(long From, long To)> ranges = InputReader.ReadString(inputPath).SplitOn(Seperator.Comma)
            .Select(x => (long.Parse(x.Split('-', StringSplitOptions.TrimEntries)[0]), long.Parse(x.Split('-', StringSplitOptions.TrimEntries)[1]))).ToList();

        List<long> invalidIds = [];
        foreach (var range in ranges)
        {
            for (long i = range.From; i <= range.To; i++)
            {
                string v = i.ToString();
                if (HasPattern(v))
                {
                    invalidIds.Add(i);
                }
            }
        }

        return invalidIds.Sum();
    }

    private bool HasPattern(string v) 
    {
        if(v.Length < 2)
        {
            return false;
        }
        if (!SingleDigit(v))
        {
            if(v.Length > 3)
            {
                var maxSize = v.Length / 2;
                for (int i = 2; i <= maxSize; i++)
                {
                    if(PatternOfLength(v, i))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        return true;
    }

    private bool PatternOfLength(string v, int l)
    {
        if(v.Length % l == 0)
        {
            string first = v[..l];
            for (int i = l; i <= v.Length - l; i += l)
            {
                string p = v[i..(i + l)];
                if (p != first)
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    private bool SingleDigit(string v)
    {
        for (int i = 1; i < v.Length; i++)
        {
            if (v[0] != v[i]) return false;
        }
        return true;
    }
}
