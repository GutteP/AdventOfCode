using AoC._2015.Day01;
using AoC._2015.Day02;

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

    [Theory]
    [InlineData("Day02/t1.txt", 101, 48)]
    [InlineData("Day02/input.txt", 1588178, 3783758)]

    public void Day02_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new PresentWrappingOrder().Test(path, expectedPartOne, expectedPartTwo);
    }
}