namespace AoC._2024;

public class D19
{
    public long PartOne(string inputPath)
    {
        (List<string> towels, List<string> patterns) = InputReader.ReadLines(inputPath).ToTowelsAndPatterns();
        Dictionary<string, bool> memory = new();
        int possible = 0;
        foreach (var pattern in patterns) 
        {
            if (pattern.IsPossible(towels, memory)) possible++;
        }
        return possible;
    }

    public long? PartTwo(string inputPath)
    {
        (List<string> towels, List<string> patterns) = InputReader.ReadLines(inputPath).ToTowelsAndPatterns();
        Dictionary<string, long> memory = new();
        long possibilities = 0;
        foreach (var pattern in patterns)
        {
            possibilities += pattern.Possibilities(towels, memory, 0);
        }
        return possibilities;
    }
}


public static class D19Extensions
{
    public static bool IsPossible(this string pattern, List<string> towels, Dictionary<string, bool> memory)
    {
        if (pattern.Length == 0)
        {
            return true;
        }
        if (memory.TryGetValue(pattern, out bool possible))
        {
            return possible;
        }
        foreach (string towel in towels)
        {
            if (pattern.StartsWith(towel))
            {
                if (pattern[towel.Length..].IsPossible(towels, memory))
                {
                    memory.Add(pattern, true);
                    return true;
                }
            }
        }
        memory.Add(pattern, false);
        return false;
    }

    public static long Possibilities(this string pattern, List<string> towels, Dictionary<string, long> memory, long possibilities)
    {
        if (pattern.Length == 0)
        {
            return possibilities + 1;
        }
        foreach (string towel in towels)
        {
            if (pattern.StartsWith(towel))
            {
                if(memory.TryGetValue(pattern[towel.Length..], out long v2))
                {
                    possibilities += v2;
                    continue;
                }
                long newPossibilities = pattern[towel.Length..].Possibilities(towels, memory, possibilities);
                if (newPossibilities > possibilities)
                {
                    memory.Add(pattern[towel.Length..], newPossibilities - possibilities);
                    possibilities = newPossibilities;
                }
                else
                {
                    memory.Add(pattern[towel.Length..], 0);
                }
            }
        }
        
        return possibilities;
    }

    public static (List<string> Towels, List<string> Patterns) ToTowelsAndPatterns(this List<string> input)
    {
        List<string> towels = new();
        List<string> patterns = new();
        bool pattern = false;
        foreach (string l in input) 
        {
            if(string.IsNullOrEmpty(l))
            {
                pattern = true;
                continue;
            }
            if (pattern) 
            {
                patterns.Add(l.Trim());
            }
            else
            {
                towels = l.Split(", ").Select(x => x.Trim()).OrderByDescending(x => x.Length).ToList();
            }
        }
        return (towels, patterns);
    }
}


