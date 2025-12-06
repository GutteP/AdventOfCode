using AoC.Common;
using System.Collections.Generic;

namespace AoC._2025;

public class D06
{
    public long? PartOne(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<string[]> input = InputReader.ReadLines(inputPath).Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToList();
        string[] operations = input.Last();
        List<int[]> numbers = input[..(input.Count - 1)].Select(x => x.ToIntArray()).ToList();

        long sum = 0;
        for (int i = 0; i < operations.Length; i++)
        {
            if (operations[i] == "*")
            {
                sum += Mul(numbers, i);
            }
            else
            {
                sum += Add(numbers, i);
            }
        }

        return sum;
    }
    private long Mul(List<int[]> numbers, int colomn)
    {
        long sum = numbers[0][colomn];
        for (int i = 1; i < numbers.Count; i++)
        {
            sum *= numbers[i][colomn];
        }
        return sum;
    }
    private long Add(List<int[]> numbers, int colomn)
    {
        long sum = numbers[0][colomn];
        for (int i = 1; i < numbers.Count; i++)
        {
            sum += numbers[i][colomn];
        }
        return sum;
    }


    public long? PartTwo(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<(List<long> Numbers, char Op)> math = CephalopodMathTranslator(inputPath);

        long sum = 0;
        foreach (var m in math)
        {
            if(m.Op == '*')
            {
                sum += m.Numbers.Product();
            }
            else
            {
                sum += m.Numbers.Sum();
            }
        }

        return sum;
    }

    private List<(List<long>, char)> CephalopodMathTranslator(string inputPath)
    {
        List<string> input = InputReader.ReadLines(inputPath);

        List<(List<long>, char)> math = [];

        List<long> working = [];
        for (int i = input[0].Length - 1; i >= 0; i--)
        {
            string num = string.Empty;
            char op = ' ';
            for (int j = 0; j < input.Count; j++)
            {
                if (char.IsNumber(input[j][i]))
                {
                    num += input[j][i];
                }
                else if (input[j][i] == '+' || input[j][i] == '*')
                {
                    op = input[j][i];
                }
            }
            if (int.TryParse(num, out int n))
            {
                working.Add(n);
            }
            num = string.Empty;
            if(op != ' ')
            {
                math.Add((working, op));
                working = [];
            }
        }
        return math;
    }

}
