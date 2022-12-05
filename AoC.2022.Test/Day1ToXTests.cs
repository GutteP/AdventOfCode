using AoC._2022.Day1;
using AoC._2022.Day2;
using AoC._2022.Day3;
using AoC._2022.Day4;
using AoC._2022.Day5;

namespace AoC._2022.Test;

public class Day1ToXTests
{
    [Theory]
    [InlineData("Day1/t1.txt", 24000, 45000)]
    [InlineData("Day1/input.txt", 67658, 200158)]

    public void Day1Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new CalorieCounter().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day2/t1.txt", 15, 12)]
    [InlineData("Day2/input.txt", 14069, 12411)]

    public void Day2Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new RPSScoreCalculator().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("Day3/t1.txt", 157, 70)]
    [InlineData("Day3/input.txt", 7821, 2752)]

    public void Day3Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new RucksackOrganaizer().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day4/t1.txt", 2, 4)]
    [InlineData("Day4/input.txt", 509, 870)]
    public void Day4Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new SectionChecker().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day5/t1.txt", "CMZ", "MCD")]
    [InlineData("Day5/input.txt", "TLFGBZHCN", "QRQFHFWCL")]
    public void Day5Test(string path, string? expectedPartOne, string? expectedPartTwo)
    {
        new CrateTracker().Test(path, expectedPartOne, expectedPartTwo);
    }

}
