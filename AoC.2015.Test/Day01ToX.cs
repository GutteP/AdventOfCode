using AoC._2015.Day01;
using AoC._2015.Day02;
using AoC._2015.Day03;
using AoC._2015.Day04;
using AoC._2015.Day05;

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

    [Theory]
    [InlineData("Day03/t1.txt", 4, 3)]
    [InlineData("Day03/input.txt", 2592, 2360)]

    public void Day03_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new PresentDelivering().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("abcdef", 609043, 6742839)]
    [InlineData("pqrstuv", 1048970, 5714438)]
    [InlineData("yzbqklnj", 282749, 9962624)]

    public void Day04_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new AdventCoin().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day05/t1.txt", 2, null)]
    [InlineData("Day05/t2.txt", null, 2)]
    [InlineData("Day05/input.txt", 258, 53)]

    public void Day05_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new NaughtyOrNice().Test(path, expectedPartOne, expectedPartTwo);
    }
}