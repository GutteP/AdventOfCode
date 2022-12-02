using AoC._2022.Day1;
using AoC._2022.Day2;

namespace AoC._2022.Test;

public class Day1ToXTests
{
    [Theory]
    [InlineData("Day1/t1.txt", 24000, 45000)]
    [InlineData("Day1/input.txt", 67658, 200158)]

    public void DayOneTest(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new CalorieCounter().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day2/t1.txt", 15, 12)]
    [InlineData("Day2/input.txt", 14069, 12411)]

    public void DayTwoTest(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new RPSScoreCalculator().Test(path, expectedPartOne, expectedPartTwo);
    }

}
