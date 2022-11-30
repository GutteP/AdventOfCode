using AoC._2022.TestDay1;
using AoC._2022.TestDay2;

namespace AoC._2022.Test;

public class TestDaysTests
{
    [Theory]
    [InlineData("TestDay1/t1.txt", 7, 5)]
    [InlineData("TestDay1/input.txt", 1298, 1248)]
    public void TestDayOne(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new SleighKeysFinder().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("TestDay2/t1.txt", 150, 900)]
    [InlineData("TestDay2/input.txt", 1690020, 1408487760)]
    public void TestDayTwo(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new SubmarinePilot().Test(path, expectedPartOne, expectedPartTwo);
    }
}
