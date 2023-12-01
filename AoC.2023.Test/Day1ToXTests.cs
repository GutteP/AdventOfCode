using AoC._2023._01;
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
}