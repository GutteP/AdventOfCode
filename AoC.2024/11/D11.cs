namespace AoC._2024;

public class D11
{
    public long? PartOne(string inputPath, int blinks)
    {
        List<long> line = InputReader.ReadString(inputPath).Split(' ').Select(x => long.Parse(x)).ToList();
        for (int i = 0; i < blinks; i++)
        {
            line = line.Blink();
        }
        return line.Count;
    }


    public long? PartTwo(string inputPath, int blinks)
    {
        List<long> line = InputReader.ReadString(inputPath).Split(' ').Select(x => long.Parse(x)).ToList();
        long sum = 0;
        Dictionary<(long, int), long> memory = new();
        foreach (long number in line)
        {
            sum += number.DeepMemoryBlink(1, blinks, memory);
        }
        return sum;
    }
}

public static class D11Extensions
{
    public static List<long> Blink(this List<long> line)
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
                transformed.Add(long.Parse(string.Concat(line[i].ToString().Take(line[i].ToString().Length / 2))));
                transformed.Add(long.Parse(string.Concat(line[i].ToString().Skip(line[i].ToString().Length / 2))));
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
        if (memory.TryGetValue((number, depth), out long result))
        {
            return result;
        }
        if (depth > maxDepth)
        {
            memory.Add((number, depth), 1);
            return 1;
        }

        if (number == 0)
        {
            long r = DeepMemoryBlink(1, depth + 1, maxDepth, memory);
            memory[(number, depth)] = r;
            return r;
        }
        else if (number.ToString().Length % 2 == 0)
        {
            long first = long.Parse(string.Concat(number.ToString().Take(number.ToString().Length / 2)));
            long partSum = DeepMemoryBlink(first, depth + 1, maxDepth, memory);
            long last = long.Parse(string.Concat(number.ToString().Take(number.ToString().Length / 2)));
            partSum += DeepMemoryBlink(last, depth + 1, maxDepth, memory);
            memory[(number, depth)] = partSum;
            return partSum;
        }
        else
        {
            long mul = number * 2024;
            long r = DeepMemoryBlink(mul, depth + 1, maxDepth, memory);
            memory[(number, depth)] = r;
            return r;
        }
    }

    public static long DeepBlink(this long number, int depth, int maxDepth)
    {

        if (depth > maxDepth) return 1;
        long sum = 0;
        if (number == 0)
        {
            sum += DeepBlink(1, depth + 1, maxDepth);
        }
        else if (number.ToString().Length % 2 == 0)
        {
            sum += DeepBlink(long.Parse(string.Concat(number.ToString().Take(number.ToString().Length / 2))), depth + 1, maxDepth);
            sum += DeepBlink(long.Parse(string.Concat(number.ToString().Skip(number.ToString().Length / 2))), depth + 1, maxDepth);
        }
        else
        {
            sum += DeepBlink(number * 2024, depth + 1, maxDepth);
        }
        return sum;
    }
}

