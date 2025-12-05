using AoC.Common;

namespace AoC._2025;

public class D05
{
    public int? PartOne(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<string> input = InputReader.ReadLines(inputPath);
        List<long> ingredients = [];
        List<AoCRange> ranges = [];
        bool partOne = true;
        foreach (string line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                partOne = false;
                continue;
            }
            if(partOne)
            {
                string[] parts = line.Split('-', StringSplitOptions.RemoveEmptyEntries);
                long from = long.Parse(parts[0]);
                long to = long.Parse(parts[1]);
                ranges.Add(AoCRange.CreateFromTo(from, to));
            }
            else
            {
                ingredients.Add(long.Parse(line));
            }
        }

        int fresh = 0;
        foreach (var ing in ingredients)
        {
            if (ranges.Any(r => r.In(ing)))
            {
                fresh++;
            }
        }


        return fresh;
    }

    public long? PartTwo(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<string> input = InputReader.ReadLines(inputPath);
        List<AoCRange> ranges = [];
        foreach (string line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                break; ;
            }
            string[] parts = line.Split('-', StringSplitOptions.RemoveEmptyEntries);
            long from = long.Parse(parts[0]);
            long to = long.Parse(parts[1]);
            ranges.Add(AoCRange.CreateFromTo(from, to));
        }

        ranges = AoCRange.Merge(ranges);

        return ranges.Sum(x => x.Count());
    }
}
