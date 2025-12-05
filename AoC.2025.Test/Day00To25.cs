using Shouldly;

namespace AoC._2025.Test;

public class Day00To25
{
    [Theory]
    [InlineData("1", 1)]
    [InlineData("2", 2)]
    public void Day00_1(string input, int expected)
    {
        new D00().PartOne(input).ShouldBe(expected);
    }
    [Theory]
    [InlineData("00/t1.txt", 2)]
    [InlineData("00/t2.txt", 4)]
    public void Day00_2(string input, int expected)
    {
        new D00().PartTwo(input).ShouldBe(expected);
    }

    [Theory]
    [InlineData("01/t1.txt", 3)]
    [InlineData("01/input.txt", 1145)]
    public void Day01_1(string input, int expected)
    {
        new D01().PartOne(input).ShouldBe(expected);
    }

    [Theory]
    [InlineData("01/t1.txt", 6)]
    [InlineData("01/t2.txt", 21)]
    [InlineData("01/input.txt", 6561)]
    public void Day01_2(string input, int expected)
    {
        new D01().PartTwo(input).ShouldBe(expected);
    }

    [Theory]
    [InlineData("02/t1.txt", 1227775554)]
    [InlineData("02/input.txt", 15873079081L)]
    public void Day02_1(string input, long expected)
    {
        new D02().PartOne(input).ShouldBe(expected);
    }

    [Theory]
    [InlineData("02/t1.txt", 4174379265)]
    [InlineData("02/input.txt", 22617871034)]
    public void Day02_2(string input, long expected)
    {
        new D02().PartTwo(input).ShouldBe(expected);
    }

    [Theory]
    [InlineData("03/t1.txt", 357)]
    [InlineData("03/input.txt", 17193)]
    public void Day03_1(string input, int expected)
    {
        new D03().PartOne(input).ShouldBe(expected);
    }

    [Theory]
    [InlineData("03/t1.txt", 3121910778619L)]
    [InlineData("03/input.txt", 171297349921310L)]
    public void Day03_2(string input, long expected)
    {
        new D03().PartTwo(input).ShouldBe(expected);
    }

    [Theory]
    [InlineData("04/t1.txt", 13)]
    [InlineData("04/input.txt", 1320)]
    public void Day04_1(string input, int expected)
    {
        new D04().PartOne(input).ShouldBe(expected);
    }

    [Theory]
    [InlineData("04/t1.txt", 43)]
    [InlineData("04/input.txt", 8354)]
    public void Day04_2(string input, int expected)
    {
        new D04().PartTwo(input).ShouldBe(expected);
    }
}
