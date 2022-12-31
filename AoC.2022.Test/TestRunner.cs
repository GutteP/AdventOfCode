namespace AoC._2022.Test;

public static class TestRunner
{
    public static void Test(this IAoCDay<int> day, string path, int? expectedPartOne, int? expectedPartTwo)
    {
        var runner = day.Runner();
        if (expectedPartOne is not null) runner.PartOne.Run(path).Should().Be(expectedPartOne);
        if (expectedPartTwo is not null) runner.PartTwo.Run(path).Should().Be(expectedPartTwo);
    }
    public static void Test(this IAoCDay<string> day, string path, string? expectedPartOne, string? expectedPartTwo)
    {
        var runner = day.Runner();
        if (expectedPartOne is not null) runner.PartOne.Run(path).Should().Be(expectedPartOne);
        if (expectedPartTwo is not null) runner.PartTwo.Run(path).Should().Be(expectedPartTwo);
    }
    public static void Test(this IAoCDay<double> day, string path, double? expectedPartOne, double? expectedPartTwo)
    {
        var runner = day.Runner();
        if (expectedPartOne is not null) runner.PartOne.Run(path).Should().Be(expectedPartOne);
        if (expectedPartTwo is not null) runner.PartTwo.Run(path).Should().Be(expectedPartTwo);
    }
    public static void Test(this IAoCDay<long> day, string path, long? expectedPartOne, long? expectedPartTwo)
    {
        var runner = day.Runner();
        if (expectedPartOne is not null) runner.PartOne.Run(path).Should().Be(expectedPartOne);
        if (expectedPartTwo is not null) runner.PartTwo.Run(path).Should().Be(expectedPartTwo);
    }
}
