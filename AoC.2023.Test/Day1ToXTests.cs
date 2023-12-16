using AoC._2023._01;
using AoC._2023._02;
using AoC._2023._03;
using AoC._2023._04;
using AoC._2023._05;
using AoC._2023._06;
using AoC._2023._07;
using AoC._2023._08;
using AoC._2023._09;
using AoC._2023._10;
using AoC._2023._11;
using AoC._2023._12;
using AoC._2023._13;
using AoC._2023._14;
using AoC._2023._15;
using AoC._2023._16;
using AoC.Common;
using FluentAssertions;

namespace AoC._2023.Test;

public class Day1ToXTests
{
    [Theory]
    [InlineData("01/t1.txt", 142, null)]
    [InlineData("01/t2.txt", null, 281)]
    [InlineData("01/input.txt", 53651, 53894)]

    public void Day01_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new Trebuchet().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("02/t1.txt", 8, 2286)]
    [InlineData("02/input.txt", 1853, 72706)]

    public void Day02_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new CubeConundrum().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("03/t1.txt", 4361, 467835)]
    [InlineData("03/input.txt", 528819, 80403602)]

    public void Day03_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new GearRatios().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("04/t1.txt", 13, 30)]
    [InlineData("04/input.txt", 19135, 5704953)]

    public void Day04_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new Scratchcards().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("05/t1.txt", 35, 46)]
    [InlineData("05/input.txt", 178159714, 100165128, Skip = "true")] // 1h körtid typ

    public void Day05_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new SeedMapping().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("06/t1.txt", 288, 71503)]
    [InlineData("06/input.txt", 449820, 42250895)]

    public void Day06_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new BoatRacing().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("07/t1.txt", 6440L, 5905L)]
    [InlineData("07/input.txt", 253910319L, 254083736L)]

    public void Day07_Test(string path, long? expectedPartOne, long? expectedPartTwo)
    {
        new CamelCards().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("08/t1.txt", 2, null)]
    [InlineData("08/t2.txt", 6, null)]
    [InlineData("08/t3.txt", null, 6L)]
    [InlineData("08/input.txt", 17141, 10818234074807L)]
    public void Day08_Test(string path, int? expectedPartOne, long? expectedPartTwo)
    {
        new HauntedWasteland().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("09/t1.txt", 114, 2)]
    [InlineData("09/input.txt", 1934898178, 1129)]
    public void Day09_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new MirageMaintenance().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("10/t1.txt", 4, 1)]
    [InlineData("10/t2.txt", 8, 1)]
    [InlineData("10/input.txt", 6757, 523)]
    public void Day10_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new PipeMaze().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("11/t1.txt", 374, 82000210L)]
    [InlineData("11/input.txt", 9605127, 458191688761L)]
    public void Day11_Test(string path, int? expectedPartOne, long? expectedPartTwo)
    {
        new CosmicExpansion().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    //[InlineData("12/t1.txt", null, 525152L)]
    [InlineData("12/t1.txt", 21, null)]
    [InlineData("12/t2.txt", 10, null)]
    //[InlineData("12/t2.txt", null, 506250L)]
    [InlineData("12/t3.txt", 2, null)]
    [InlineData("12/t4.txt", 4, null)]
    [InlineData("12/t5.txt", 4, null)]
    [InlineData("12/input.txt", 7090, null)]
    //[InlineData("12/input.txt", null, 1L)]

    public void Day12_Test(string path, int? expectedPartOne, long? expectedPartTwo)
    {
        new HotSprings().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("13/t1.txt", 405, 400)]
    [InlineData("13/input.txt", 32723, 34536)]
    public void Day13_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new PointOfIncidence().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("14/t1.txt", 136, 64)]
    [InlineData("14/input.txt", 109424, 102509)]
    public void Day14_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new ParabolicReflectorDish().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("15/t1.txt", 52, null)]
    [InlineData("15/t2.txt", 1320, 145)]
    [InlineData("15/input.txt", 510273, 212449)]
    public void Day15_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new LensLibrary().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("16/t1.txt", 46, 51)]
    [InlineData("16/input.txt", 7543, 8231)]
    public void Day16_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new LavaEnergizing().Test(path, expectedPartOne, expectedPartTwo);
    }
}