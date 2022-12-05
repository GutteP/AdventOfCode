namespace AoC._2022.Day3;

public class RucksackOrganaizer : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<List<int[]>, int>(Transformer, DuplicateFinder), new Runner<List<int[]>, int>(Transformer, BadgeIdentifier));
    }
    private List<int[]> Transformer(string path)
    {
        var rawInput = InputReader.ReadLines(path).Trim();
        List<int[]> output = new();
        foreach (string rucksack in rawInput)
        {
            int[] transformedRucksack = new int[rucksack.Length];
            for (int i = 0; i < rucksack.Length; i++)
            {
                int num = 0;
                if (char.IsLower(rucksack[i])) num = (char.ToUpper(rucksack[i])) - 64;
                else num = rucksack[i] - 64 + 26;
                transformedRucksack[i] = num;
            }
            output.Add(transformedRucksack);
        }
        return output;
    }
    private int DuplicateFinder(List<int[]> input)
    {
        int sum = 0;
        foreach (int[] rucksack in input)
        {
            sum += rucksack[0..(rucksack.Length / 2)].Intersect(rucksack[(rucksack.Length / 2)..]).First();
        }
        return sum;
    }

    private int BadgeIdentifier(List<int[]> rucksacks)
    {
        int sum = 0;
        while (rucksacks.Count >= 3)
        {
            int[][] group = rucksacks.Take(3).ToArray();
            rucksacks.RemoveRange(0, 3);
            sum += group[0].Intersect(group[1]).Intersect(group[2]).First();
        }
        return sum;
    }
}
