namespace AoC._2024;

public class D03
{
    public int? PartOne(string inputPath)
    {
        string inputString = InputReader.ReadString(inputPath);

        int sum = 0;
        foreach (Match m in Regex.Matches(inputString, "mul[(]\\d+,\\d+[)]"))
        {
            sum += Mul(m.Value);
        }
        return sum;
    }

    public int? PartTwo(string inputPath)
    {
        string inputString = InputReader.ReadString(inputPath);

        int sum = 0;
        bool dont = false;
        foreach (Match m in Regex.Matches(inputString, "mul[(]\\d+,\\d+[)]|do[(][)]|don't[(][)]"))
        {
            if (!dont && m.Value.StartsWith("mul"))
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
            return m.Value.Split(',').Select(x => int.Parse(x)).Aggregate((a, b) => a * b);
        }
        else throw new Exception();
    }
}