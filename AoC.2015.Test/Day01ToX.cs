using AoC._2015.Day01;

namespace AoC._2015.Test;

public class Day01ToX
{
    [Theory]
    [InlineData("Day01/t1.txt", -3, 1)]
    [InlineData("Day01/t2.txt", -1, 5)]
    [InlineData("Day01/input.txt", 232, 1783)]

    public void Day01_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new FloorDelivery().Test(path, expectedPartOne, expectedPartTwo);
    }
}