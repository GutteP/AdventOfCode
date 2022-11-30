namespace AoC._2022.Test;

public static class TestRunner
{
    public static void Test(this IAoCDay day, string path, int? expectedPartOne, int? expectedPartTwo)
    {
        var runner = day.Runner();
        if (expectedPartOne is not null) runner.PartOne.Run(path).Should().Be(expectedPartOne);
        if (expectedPartTwo is not null) runner.PartTwo.Run(path).Should().Be(expectedPartTwo);
    }
}
