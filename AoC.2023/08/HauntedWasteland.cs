using FluentAssertions.Equivalency;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;

namespace AoC._2023._08;

public class HauntedWasteland : IAoCDay<long>
{
    public DayRunner<long> Runner()
    {
        return new DayRunner<long>(new Runner<(string Ins, Dictionary<string, List<string>> Map), string, long>(Transformer, PartOne, "AAA"), new Runner<(string Ins, Dictionary<string, List<string>> Map), long>(Transformer, PartTwo));
    }

    private (string Ins, Dictionary<string, List<string>> Map) Transformer(string path)
    {
        List<string> input = InputReader.ReadLines(path);
        string instructions = input[0];
        Dictionary<string, List<string>> map = new();
        for (int i = 2; i < input.Count; i++)
        {
            List<string> nodes = new() { input[i][7..10], input[i][12..15] };
            map.Add(input[i][0..3], nodes);
        }
        return (instructions, map);
    }

    private long PartOne((string Ins, Dictionary<string, List<string>> Map) insAndMap, string currentNode)
    {
        string instructions = insAndMap.Ins;
        Dictionary<string, List<string>> map = insAndMap.Map;
        long steps = 0;
        while (!currentNode.EndsWith('Z'))
        {
            for (int i = 0; i < instructions.Length; i++)
            {
                if (instructions[i] == 'L') currentNode = map[currentNode][0];
                else if (instructions[i] == 'R') currentNode = map[currentNode][1];
                else throw new Exception($"Något hände instructions[{i}]");

                steps++;
                if (currentNode.EndsWith('Z')) break;
            }
        }
        return steps;
    }

    private long PartTwo((string Ins, Dictionary<string, List<string>> Map) insAndMap)
    {
        IEnumerable<long> steps = insAndMap.Map.Keys.Where(x => x.EndsWith('A')).Select(x => PartOne(insAndMap, x));
        long lcm = AoCMath.LeastCommonMultiple(steps.ToArray());
        return lcm;
    }
}
