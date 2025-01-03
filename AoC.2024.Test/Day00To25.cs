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
    [InlineData("13/t1.txt", 875318608908L)]
    [InlineData("13/input.txt", 87550094242995L)]
    public void Day13_2(string input, long expected)
    {
        new D13().PartTwo(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("14/t1.txt", 12, 11, 7)]
    [InlineData("14/t2.txt", 0, 11, 7)]
    [InlineData("14/input.txt", 208437768L, 101, 103)]
    public void Day14_1(string input, long expected, int xSize, int ySize)
    {
        new D14().PartOne(input, xSize, ySize).Should().Be(expected);
    }

    // Dag 14 del 2 löstes med hjälp av Aoc.2024.14.2 projektet.

    [Theory]
    [InlineData("15/t1.txt", 2028)]
    [InlineData("15/t2.txt", 10092)]
    [InlineData("15/input.txt", 1485257L)]
    public void Day15_1(string input, long expected)
    {
        new D15().PartOne(input).Should().Be(expected);
    }
    [Theory]
    [InlineData("15/t3.txt", 618L)]
    [InlineData("15/t2.txt", 9021)]
    //[InlineData("15/input.txt", 1475512L)] //16 minuter....
    public void Day15_2(string input, long expected)
    {
        new D15().PartTwo(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("16/t1.txt", 7036)]
    [InlineData("16/t2.txt", 11048)]
    [InlineData("16/input.txt", 99460L)]
    public void Day16_1(string input, long expected)
    {
        new D16().PartOne(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("16/t1.txt", 45)]
    [InlineData("16/t2.txt", 64)]
    //[InlineData("16/input.txt", 0)] Tar för lång tid
    public void Day16_2(string input, long expected)
    {
        new D16().PartTwo(input).Should().Be(expected);
    }
    [Theory]
    [InlineData("17/t1.txt", "4,6,3,5,6,3,5,2,1,0")]
    [InlineData("17/t2.txt", "0,1,2")]
    [InlineData("17/t3.txt", "4,2,5,6,7,7,7,7,3,1,0")]
    [InlineData("17/t4.txt", "5,7,3,0")]
    [InlineData("17/input.txt", "7,0,3,1,2,6,3,7,1")]
    public void Day17_1(string input, string expected)
    {
        new D17().PartOne(input).Should().Be(expected);
    }

    [Theory]
    //[InlineData("17/t4.txt", 117440L)]
    //[InlineData("17/input.txt", 109020013201563L)] // Tar 30 min att köra.. 
    public void Day17_2(string input, long expected)
    {
        new D17().PartTwo(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("18/t1.txt", 22, 7, 7, 12)]
    [InlineData("18/input.txt", 446L, 71, 71, 1024)]
    public void Day18_1(string input, long expected, int xSize, int ySize, int simulationLength)
    {
        new D18().PartOne(input, xSize, ySize, simulationLength).Should().Be(expected);
    }
    [Theory]
    [InlineData("18/t1.txt", "6,1", 7, 7, 12)]
    [InlineData("18/input.txt", "39,40", 71, 71, 1024)]
    public void Day18_2(string input, string expected, int xSize, int ySize, int simulationLength)
    {
        new D18().PartTwo(input, xSize, ySize, simulationLength).Should().Be(expected);
    }

    [Theory]
    [InlineData("19/t1.txt", 6)]
    [InlineData("19/input.txt", 283)]
    public void Day19_1(string input, long expected)
    {
        new D19().PartOne(input).Should().Be(expected);
    }
    [Theory]
    [InlineData("19/t1.txt", 16)]
    [InlineData("19/input.txt", 615388132411142L)]
    public void Day19_2(string input, long expected)
    {
        new D19().PartTwo(input).Should().Be(expected);
    }

    [Theory]
    [InlineData("20/t1.txt", 44, 0)]
    [InlineData("20/input.txt", 1507, 100)]
    public void Day20_1(string input, long expected, int min)
    {
        new D20().PartOne(input, min).Should().Be(expected);
    }
    [Theory]
    [InlineData("20/t1.txt", 285, 50)]
    [InlineData("20/input.txt", 1037936, 100)]
    public void Day20_2(string input, long expected, int min)
    {
        new D20().PartTwo(input, min).Should().Be(expected);
    }

    [Theory]
    [InlineData("22/t1.txt", 37327623L, 2000)]
    [InlineData("22/input.txt", 20411980517L, 2000)]
    public void Day22_1(string input, long expected, int evolutions)
    {
        new D22().PartOne(input, evolutions).Should().Be(expected);
    }
    [Theory]
    [InlineData("22/t1.txt", 24, 2000)]
    [InlineData("22/t2.txt", 23, 2000)]
    [InlineData("22/t3.txt", 6, 10)]
    [InlineData("22/input.txt", 2362, 2000)] // -2,1,-1,2
    public void Day22_2(string input, int expected, int evolutions)
    {
        new D22().PartTwo(input, evolutions).Should().Be(expected);
    }

    [Theory]
    [InlineData("23/t1.txt", 7, 3)]
    [InlineData("23/t1.txt", 1, 4)]
    [InlineData("23/input.txt", 1046L, 3)]
    [InlineData("23/input.txt", 1, 13)]
    public void Day23_1(string input, long expected, int interconnected)
    {
        new D23().PartOne(input, interconnected).Should().Be(expected);
    }
    [Theory]
    [InlineData("23/t1.txt", "co,de,ka,ta")]
    [InlineData("23/input.txt", "de,id,ke,ls,po,sn,tf,tl,tm,uj,un,xw,yz")]
    public void Day23_2(string input, string expected)
    {
        new D23().PartTwo(input).Should().Be(expected);
    }
    [Theory]
    [InlineData("24/t1.txt", 4)]
    [InlineData("24/t2.txt", 2024)]
    [InlineData("24/input.txt", 36035961805936L)]
    public void Day24_1(string input, long expected)
    {
        new D24().PartOne(input).Should().Be(expected);
    }
    //[Theory]
    //[InlineData("24/input.txt", "")]
    //[InlineData("24/t3.txt", "")]
    //[InlineData("24/t4.txt", "")]
    //[InlineData("24/t5.txt", "")]
    //public void Day24_2(string input, string expected)
    //{
    //    new D24().PartTwo(input).Should().Be(expected);
    //}

}

