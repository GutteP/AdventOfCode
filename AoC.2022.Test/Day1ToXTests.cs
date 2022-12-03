using AoC._2022.Day1;
using AoC._2022.Day2;
using AoC._2022.Day3;

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
    [InlineData("Day2/t1.txt", 15, 2752)]
    [InlineData("Day2/input.txt", 14069, 12411)]

    public void DayTwoTest(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new RPSScoreCalculator().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("Day3/t1.txt", 157, 70)]
    [InlineData("Day3/input.txt", 7821, 2752)]

    public void DayThreeTest(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new RucksackOrganaizer().Test(path, expectedPartOne, expectedPartTwo);
    }

}
