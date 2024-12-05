namespace AoC._2024;

public class D05
{
    public int? PartOne(string inputPath)
    {
        List<string> input = InputReader.ReadLines(inputPath);
        int split = input.IndexOf(string.Empty);
        List<(int A, int B)> rules = input.Take(split).Select(r => r.Split('|').Select(n => int.Parse(n))).Select(x => (x.First(), x.Last())).ToList();
        var prints = input.Skip(split + 1).Select(p => p.Split(',').Select(c => int.Parse(c)).ToList()).ToList();

        int sum = 0;
        foreach (var p in prints)
        {
            if (p.Evaluate(rules))
            {
                sum += p[(p.Count / 2)];
            }
        }

        return sum;
    }


    public int? PartTwo(string inputPath)
    {
        List<string> input = InputReader.ReadLines(inputPath);
        int split = input.IndexOf(string.Empty);
        List<(int A, int B)> rules = input.Take(split).Select(r => r.Split('|').Select(n => int.Parse(n))).Select(x => (x.First(), x.Last())).ToList();
        var prints = input.Skip(split + 1).Select(p => p.Split(',').Select(c => int.Parse(c)).ToList()).ToList();

        int sum = 0;
        foreach (var p in prints)
        {
            if (!p.Evaluate(rules))
            {
                var ordered = p.Order(rules);
                sum += ordered[(ordered.Count / 2)];
            }
        }

        return sum;
    }
}

public static class D05Extensions
{
    public static bool Evaluate(this List<int> p, List<(int A, int B)> rules)
    {
        for (int i = 0; i < p.Count; i++)
        {
            foreach (var rule in rules.Where(r => r.A == p[i] || r.B == p[i]))
            {
                if (rule.A == p[i])
                {
                    int index = p.IndexOf(rule.B);
                    if (index >= 0 && index < i) return false;
                }
                else if (rule.B == p[i])
                {
                    int index = p.IndexOf(rule.A, i);
                    if (index >= 0 && index > i) return false;
                }
            }
        }
        return true;
    }
    public static List<int> Order(this List<int> p, List<(int A, int B)> rules)
    {
        for (int i = 0; i < p.Count; i++)
        {
            foreach (var rule in rules.Where(r => r.A == p[i] || r.B == p[i]))
            {
                if (rule.A == p[i])
                {
                    int index = p.IndexOf(rule.B);
                    if (index >= 0 && index < i)
                    {
                        p.Insert(i + 1, p[index]);
                        p.RemoveAt(index);
                        i = 0;
                    }
                }
                else if (rule.B == p[i])
                {
                    int index = p.IndexOf(rule.A, i);
                    if (index >= 0 && index > i)
                    {
                        p.Insert(i, p[index]);
                        p.RemoveAt(index + 1);
                        i = 0;
                    }
                }
            }
        }

        return p;
    }

}