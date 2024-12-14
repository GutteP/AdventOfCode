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

    [Theory]
    [InlineData("07/t1.txt", 3749L)]
    [InlineData("07/input.txt", 267566105056L)]
    public void Day07_1(string input, long expected)
    {
        new D07().PartOne(input).Should().Be(expected);
    }
    [Theory]
    [InlineData("07/t1.txt", 11387L)]
    [InlineData("07/input.txt", 116094961956019L)]
    public void Day07_2(string input, long expected)
    {
        new D07().PartTwo(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("08/t1.txt", 14)]
    [InlineData("08/input.txt", 313)]
    public void Day08_1(string input, int expected)
    {
        new D08().PartOne(input).Should().Be(expected);
    }
    [Theory]
    [InlineData("08/t1.txt", 34)]
    [InlineData("08/input.txt", 1064)]
    public void Day08_2(string input, int expected)
    {
        new D08().PartTwo(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("09/t1.txt", 1928L)]
    [InlineData("09/input.txt", 6349606724455L)]
    public void Day09_1(string input, long expected)
    {
        new D09().PartOne(input).Should().Be(expected);
    }
    [Theory]
    [InlineData("09/t1.txt", 2858L)]
    [InlineData("09/input.txt", 6376648986651L)]
    public void Day09_2(string input, long expected)
    {
        new D09().PartTwo(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("10/t1.txt", 36)]
    [InlineData("10/input.txt", 489L)]
    public void Day10_1(string input, long expected)
    {
        new D10().PartOne(input).Should().Be(expected);
    }
    [Theory]
    [InlineData("10/t1.txt", 81)]
    [InlineData("10/input.txt", 1086L)]
    public void Day10_2(string input, long expected)
    {
        new D10().PartTwo(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("11/t1.txt", 22L, 6)]
    [InlineData("11/t1.txt", 55312L, 25)]
    [InlineData("11/input.txt", 189167L, 25)]
    public void Day11_1(string input, long expected, int blinks)
    {
        new D11().PartOne(input, blinks).Should().Be(expected);
    }
    [Theory]
    [InlineData("11/t1.txt", 22L, 6)]
    [InlineData("11/t1.txt", 55312L, 25)]
    [InlineData("11/input.txt", 189167L, 25)]
    [InlineData("11/t1.txt", 65601038650482L, 75)]
    [InlineData("11/input.txt", 225253278506288L, 75)]
    public void Day11_2(string input, long expected, int blinks)
    {
        new D11().PartTwo(input, blinks).Should().Be(expected);
    }

    [Theory]
    [InlineData("12/t1.txt", 140L)]
    [InlineData("12/t2.txt", 772L)]
    [InlineData("12/t3.txt", 1930L)]
    [InlineData("12/input.txt", 1489582L)]
    public void Day12_1(string input, long expected)
    {
        new D12().PartOne(input).Should().Be(expected);
    }
    [Theory]
    [InlineData("12/t1.txt", 80)]
    [InlineData("12/t4.txt", 236)]
    [InlineData("12/t5.txt", 368)]
    [InlineData("12/t3.txt", 1206)]
    [InlineData("12/input.txt", 914966L)]
    public void Day12_2(string input, long expected)
    {
        new D12().PartTwo(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("13/t1.txt", 480)]
    [InlineData("13/input.txt", 37680)]
    public void Day13_1(string input, long expected)
    {
        new D13().PartOne(input).Should().Be(expected);
    }
    [Theory]
    [InlineData("13/t1.txt", 0)]
    [InlineData("13/input.txt", 0)]
    public void Day13_2(string input, long expected)
    {
        new D13().PartTwo(input).Should().Be(expected);
    }
}
