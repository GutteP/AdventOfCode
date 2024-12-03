using System.Text.RegularExpressions;

namespace AoC._2024;

public class D03
{
    public int? PartOne(string inputPath, string? option1 = null, int? option2 = null)
    {
        string inputString = InputReader.ReadString(inputPath);

        int sum = 0;
        foreach (Match m in Regex.Matches(inputString, "mul[(]\\d+,\\d+[)]"))
        {
            sum += Mul(m.Value);
        }
        return sum;
    }

    public long? PartTwo(string inputPath, string? option1 = null, int? option2 = null)
    {
        string inputString = InputReader.ReadString(inputPath);

        long sum = 0;
        bool dont = false;
        foreach (Match m in Regex.Matches(inputString, "mul[(]\\d+,\\d+[)]|do[(][)]|don't[(][)]"))
        {
            if (m.Value.StartsWith("mul") && !dont)
            {
                sum += Mul(m.Value);
            }
            else if (m.Value.StartsWith("don"))
            {
                dont = true;
            }
            else if (m.Value.StartsWith("do"))
            {
                dont = false;
            }
        }
        return sum;
    }
    private int Mul(string mul)
    {
        Match m = Regex.Match(mul, "\\d+,\\d+");
        if (m.Success)
        {
            var ns = m.Value.Split(',').Select(x => int.Parse(x));
            return ns.First() * ns.Last();
        }
        else throw new Exception();
    }
}