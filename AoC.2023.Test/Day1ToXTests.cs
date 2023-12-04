using AoC._2023._01;
using AoC._2023._02;
using AoC._2023._03;
using AoC._2023._04;
using AoC.Common;

namespace AoC._2023.Test;

public class Day1ToXTests
{
    [Theory]
    [InlineData("01/t1.txt", 142, null)]
    [InlineData("01/t2.txt", null, 281)]
    [InlineData("01/input.txt", 53651, 53894)]

    public void Day01_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new Trebuchet().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("02/t1.txt", 8, 2286)]
    [InlineData("02/input.txt", 1853, 72706)]

    public void Day02_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new CubeConundrum().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("03/t1.txt", 4361, 467835)]
    [InlineData("03/input.txt", 528819, 80403602)]

    public void Day03_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new GearRatios().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("04/t1.txt", 13, 30)]
    [InlineData("04/input.txt", 19135, 5704953)]

    public void Day04_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new Scratchcards().Test(path, expectedPartOne, expectedPartTwo);
    }
}