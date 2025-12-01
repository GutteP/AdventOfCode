using AoC.Common;
using FluentAssertions.Equivalency.Steps;
using System.Reflection.Metadata.Ecma335;

namespace AoC._2025;

public class D01
{
    public int? PartOne(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<int> rotationList = InputReader.ReadLines(inputPath).Select(x => x[0] == 'R' ? (int.Parse(x[1..])) : (-int.Parse(x[1..]))).ToList();

        int timesAtZero = 0;
        int currentPosition = 50;
        foreach (var rotation in rotationList)
        {
            currentPosition += rotation;
            while (currentPosition >= 100)
            {
                currentPosition -= 100;
            }
            while (currentPosition < 0)
            {
                currentPosition += 100;
            }
            if (currentPosition == 0)
            {
                timesAtZero++;
            }
        }
        return timesAtZero;
    }

    public int? PartTwo(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<(char Dir, int Steps)> rotationList = InputReader.ReadLines(inputPath).Select(x => (x[0], int.Parse(x[1..]))).ToList();

        int timesAtZero = 0;
        int current = 50;
        foreach (var rot in rotationList)
        {
            var step = () => current++;
            if(rot.Dir == 'L')
            {
                step = () => current--;
            }
            for (int i = 0; i < rot.Steps; i++)
            {
                step();
                if (current % 100 == 0)
                {
                    timesAtZero++;
                }
            }
        }
        return timesAtZero;
    }
}
