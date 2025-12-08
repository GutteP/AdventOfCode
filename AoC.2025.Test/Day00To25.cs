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

    [Theory]
    [InlineData("05/t1.txt", 3)]
    [InlineData("05/input.txt", 782)]
    public void Day05_1(string input, int expected)
    {
        new D05().PartOne(input).ShouldBe(expected);
    }

    [Theory]
    [InlineData("05/t1.txt", 14)]
    [InlineData("05/input.txt", 353863745078671L)]
    public void Day05_2(string input, long expected)
    {
        new D05().PartTwo(input).ShouldBe(expected);
    }

    [Theory]
    [InlineData("06/t1.txt", 4277556)]
    [InlineData("06/input.txt", 5335495999141L)]
    public void Day06_1(string input, long expected)
    {
        new D06().PartOne(input).ShouldBe(expected);
    }

    [Theory]
    [InlineData("06/t1.txt", 3263827)]
    [InlineData("06/input.txt", 10142723156431L)]
    public void Day06_2(string input, long expected)
    {
        new D06().PartTwo(input).ShouldBe(expected);
    }

    [Theory]
    [InlineData("07/t1.txt", 21)]
    [InlineData("07/input.txt", 1518)]
    public void Day07_1(string input, int expected)
    {
        new D07().PartOne(input).ShouldBe(expected);
    }

    [Theory]
    [InlineData("07/t1.txt", 40)]
    [InlineData("07/input.txt", 25489586715621L)]
    public void Day07_2(string input, long expected)
    {
        new D07().PartTwo(input).ShouldBe(expected);
    }

    [Theory]
    [InlineData("08/t1.txt", 40, 10)]
    [InlineData("08/input.txt", 133574L, 1000)]
    public void Day08_1(string input, int expected, int connections)
    {
        new D08().PartOne(input, connections).ShouldBe(expected);
    }

    [Theory]
    [InlineData("08/t1.txt", 25272)]
    [InlineData("08/input.txt", 2435100380L)]
    public void Day08_2(string input, long expected)
    {
        new D08().PartTwo(input).ShouldBe(expected);
    }
}
