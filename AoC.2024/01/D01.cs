namespace AoC._2024;

public class D01
{
    public int? PartOne(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<string> input = InputReader.ReadLines(inputPath);
        (List<int> a, List<int> b) = SplitToIntLists(input);

        int diff = 0;
        foreach (var ab in a.Order().Zip(b.Order()))
        {
            diff += Math.Abs(ab.First - ab.Second);
        }

        return diff;
    }

    public int? PartTwo(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<string> input = InputReader.ReadLines(inputPath);
        (List<int> a, List<int> b) = SplitToIntLists(input);

        int score = 0;
        foreach (int n in a)
        {
            score += b.Count(x => x == n) * n;
        }
        return score;
    }

    private (List<int> a, List<int> b) SplitToIntLists(List<string> input)
    {
        List<int> a = new();
        List<int> b = new();
        foreach (string line in input)
        {
            string[] ab = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            a.Add(int.Parse(ab[0]));
            b.Add(int.Parse(ab[1]));
        }
        return (a, b);
    }
}