namespace AoC._2024;

public class D11
{
    public long? PartOne(string inputPath, int blinks)
    {
        List<long> line = InputReader.ReadString(inputPath).Split(' ').Select(x => long.Parse(x)).ToList();
        for (int i = 0; i < blinks; i++)
        {
            line = line.NaiveBlink();
        }
        return line.Count;
    }


    public long? PartTwo(string inputPath, int blinks)
    {
        List<long> line = InputReader.ReadString(inputPath).Split(' ').Select(x => long.Parse(x)).ToList();
        long sum1 = 0;
        Dictionary<(long, int), long> memory = new();
        foreach (long number in line)
        {
            long v = number.DeepMemoryBlink(1, blinks, memory);
            sum1 += v;
        }
        return sum1;
    }
}

public static class D11Extensions
{
    public static List<long> NaiveBlink(this List<long> line)
    {
        List<long> transformed = new();
        for (int i = 0; i < line.Count; i++)
        {
            if (line[i] == 0)
            {
                transformed.Add(1);
            }
            else if (line[i].ToString().Length % 2 == 0)
            {
                var (first, last) = line[i].SplitInHalf();
                transformed.Add(first);
                transformed.Add(last);
            }
            else
            {
                transformed.Add(line[i] * 2024);
            }
        }
        return transformed;
    }

    public static long DeepMemoryBlink(this long number, int depth, int maxDepth, Dictionary<(long, int), long> memory)
    {
        if (depth > maxDepth)
        {
            return 1;
        }
        if (memory.TryGetValue((number, depth), out long precalculatedResult))
        {
            return precalculatedResult;
        }

        long result = 0;
        if (number == 0)
        {
            result = DeepMemoryBlink(1, depth + 1, maxDepth, memory);
            memory[(number, depth)] = result;
        }
        else if (number.ToString().Length % 2 == 0)
        {
            var (first, last) = number.SplitInHalf();
            result = DeepMemoryBlink(first, depth + 1, maxDepth, memory) + DeepMemoryBlink(last, depth + 1, maxDepth, memory);
            memory[(number, depth)] = result;
        }
        else
        {
            long mul = number * 2024;
            result = DeepMemoryBlink(mul, depth + 1, maxDepth, memory);
            memory[(number, depth)] = result;
        }
        return result;
    }

    private static (long, long) SplitInHalf(this long number)
    {
        return (long.Parse(string.Concat(number.ToString().Take(number.ToString().Length / 2))),
                long.Parse(string.Concat(number.ToString().Skip(number.ToString().Length / 2))));
    }
}

