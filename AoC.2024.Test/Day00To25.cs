namespace AoC._2024.Test;

public class Day00To25
{
    [Theory]
    [InlineData("1", 1)]
    [InlineData("2", 2)]
    public void Day00_1(string input, int expected)
    {
        new D00().PartOne(input).Should().Be(expected);
    }
    [Theory]
    [InlineData("00/t1.txt", 2)]
    [InlineData("00/t2.txt", 4)]
    public void Day00_2(string input, int expected)
    {
        new D00().PartTwo(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("01/t1.txt", 11)]
    [InlineData("01/input.txt", 1110981)]
    public void Day01_1(string input, int expected)
    {
        new D01().PartOne(input).Should().Be(expected);
    }
    [Theory]
    [InlineData("01/t1.txt", 31)]
    [InlineData("01/input.txt", 24869388)]
    public void Day01_2(string input, int expected)
    {
        new D01().PartTwo(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("02/t1.txt", 2)]
    [InlineData("02/input.txt", 334)]
    public void Day02_1(string input, int expected)
    {
        new D02().PartOne(input).Should().Be(expected);
    }
    [Theory]
    [InlineData("02/t1.txt", 4)]
    [InlineData("02/input.txt", 400)]
    public void Day02_2(string input, int expected)
    {
        new D02().PartTwo(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("03/t1.txt", 161)]
    [InlineData("03/input.txt", 178886550)]
    public void Day03_1(string input, int expected)
    {
        new D03().PartOne(input).Should().Be(expected);
    }
    [Theory]
    [InlineData("03/t2.txt", 48)]
    [InlineData("03/input.txt", 87163705)]
    public void Day03_2(string input, int expected)
    {
        new D03().PartTwo(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("04/t1.txt", 18)]
    [InlineData("04/input.txt", 2644)]
    public void Day04_1(string input, int expected)
    {
        new D04().PartOne(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("04/t1.txt", 9)]
    [InlineData("04/input.txt", 1952)]
    public void Day04_2(string input, int expected)
    {
        new D04().PartTwo(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("05/t1.txt", 143)]
    [InlineData("05/input.txt", 4905)]
    public void Day05_1(string input, int expected)
    {
        new D05().PartOne(input).Should().Be(expected);
    }
    [Theory]
    [InlineData("05/t1.txt", 123)]
    [InlineData("05/input.txt", 6204)]
    public void Day05_2(string input, int expected)
    {
        new D05().PartTwo(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("06/t1.txt", 41)]
    [InlineData("06/input.txt", 4696)]
    public void Day06_1(string input, int expected)
    {
        new D06().PartOne(input).Should().Be(expected);
    }
    [Theory]
    [InlineData("06/t1.txt", 6)]
    [InlineData("06/input.txt", 1443)]
    public void Day06_2(string input, int expected)
    {
        new D06().PartTwo(input).Should().Be(expected);
    }
}
