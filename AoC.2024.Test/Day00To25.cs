﻿namespace AoC._2024.Test;

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
}
