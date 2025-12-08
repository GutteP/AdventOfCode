using AoC.Common;
using System.Collections.Generic;

namespace AoC._2025;

public class D07
{
    public int? PartOne(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<string> map = InputReader.ReadLines(inputPath);
        
        Position<int> start = new (map[0].IndexOf('S'), 0);
        int splits = Run(map, start, new HashSet<string>());

        return splits;
    }

    private int Run(List<string> map, Position<int> a, HashSet<string> record)
    {
        int splits = 0;
        if (!record.Add($"{a.Y},{a.X}"))
        {
            return 0;
        }
        a.Move(Direction.Down, 1);
        try
        {
            if (map[a.Y][a.X] == '^')
            {
                splits++;
                splits += Run(map, a.CopyAndMove(Direction.Left, 1), record);
                splits += Run(map, a.CopyAndMove(Direction.Right, 1), record);
            }
            else
            {
                splits += Run(map, a, record);
            }
        }
        catch (ArgumentOutOfRangeException e)
        {
            return 0;
        }
        return splits;
    }

    public long? PartTwo(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<string> map = InputReader.ReadLines(inputPath);

        Position<int> start = new(map[0].IndexOf('S'), 0);
        long splits = RunWithMemo(map, start, new Dictionary<string, long>());

        return splits+1;
    }


    private long RunWithMemo(List<string> map, Position<int> a, Dictionary<string, long> memory)
    {
        long splits = 0;
        a.Move(Direction.Down, 1);
        if (memory.TryGetValue($"{a.Y},{a.X}", out long s))
        {
            return s;
        }
        try
        {
            if (map[a.Y][a.X] == '^')
            {
                splits++;
                splits += RunWithMemo(map, a.CopyAndMove(Direction.Left, 1), memory);
                splits += RunWithMemo(map, a.CopyAndMove(Direction.Right, 1), memory);              
            }
            else
            {
                splits += RunWithMemo(map, a, memory);
            }
        }
        catch (ArgumentOutOfRangeException e)
        {
            return 0;
        }
        memory.TryAdd($"{a.Y},{a.X}", splits);
        return splits;
    }
}
