using FluentAssertions;
using System;
using System.Security;

namespace AoC._2024;

public class D07
{
    public long? PartOne(string inputPath)
    {
        List<(long Sum, List<int> Values)> input = InputReader.ReadLines(inputPath).Select(x => (long.Parse(x.Split(':', StringSplitOptions.TrimEntries)[0]), x.Split(':', StringSplitOptions.TrimEntries)[1].Split(' ').Select(y => int.Parse(y)).ToList())).ToList();
        long total = 0;
        foreach (var calc in input)
        {
            if(D07Extensions.Check(calc, 0, true, 0L) || D07Extensions.Check(calc, 0, false, 0L))
            {
                total += calc.Sum;
            }
        }
        return total;
    }


    public long? PartTwo(string inputPath)
    {
        List<(long Sum, List<int> Values)> input = InputReader.ReadLines(inputPath).Select(x => (long.Parse(x.Split(':', StringSplitOptions.TrimEntries)[0]), x.Split(':', StringSplitOptions.TrimEntries)[1].Split(' ').Select(y => int.Parse(y)).ToList())).ToList();
        long total = 0;
        foreach (var calc in input)
        {
            if (D07Extensions.CheckV2(calc, 0, 1, 0) || D07Extensions.CheckV2(calc, 0, 2, 0) || D07Extensions.CheckV2(calc, 0, 3, 0))
            {
                total += calc.Sum;
            }
        }
        return total;
    }
}

public static class D07Extensions
{
    public static bool Check((long Sum, List<int> Values) calc, int index, bool add, long total)
    {
        if (add && index < calc.Values.Count)
        {
            total = total + calc.Values[index];
            if (total > calc.Sum) return false;
            return Check(calc, index + 1, true, total) || Check(calc, index + 1, false, total);
        }
        else if(!add && index < calc.Values.Count)
        {
            total = total == 0 ? 1 : total * calc.Values[index];
            if (total > calc.Sum) return false;
            return Check(calc, index + 1, true, total) || Check(calc, index + 1, false, total);
        }
        else
        {
            if(calc.Sum == total) return true;
            return false;
        }
        
    }
    public static bool CheckV2((long Sum, List<int> Values) calc, int index, int op, long total)
    {
        if (op == 1 && index < calc.Values.Count)
        {
            total = total + calc.Values[index];
            if (total > calc.Sum) return false;
            return CheckV2(calc, index + 1, 1, total) || CheckV2(calc, index + 1, 2, total) || CheckV2(calc, index + 1, 3, total);
        }
        else if (op == 2 && index < calc.Values.Count)
        {
            total = total == 0 ? 1 : total * calc.Values[index];
            if (total > calc.Sum) return false;
            return CheckV2(calc, index + 1, 1, total) || CheckV2(calc, index + 1, 2, total) || CheckV2(calc, index + 1, 3, total);
        }
        else if (op == 3 && index < calc.Values.Count)
        {
            total = long.Parse($"{total}{calc.Values[index]}");
            if (total > calc.Sum) return false;
            return CheckV2(calc, index + 1, 1, total) || CheckV2(calc, index + 1, 2, total) || CheckV2(calc, index + 1, 3, total);
        }
        else
        {
            if (calc.Sum == total) return true;
            return false;
        }

    }

}