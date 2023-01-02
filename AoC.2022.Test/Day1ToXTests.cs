using AoC._2022.Day01;
using AoC._2022.Day02;
using AoC._2022.Day03;
using AoC._2022.Day04;
using AoC._2022.Day05;
using AoC._2022.Day06;
using AoC._2022.Day07;
using AoC._2022.Day08;
using AoC._2022.Day09;
using AoC._2022.Day10;
using AoC._2022.Day12;
using AoC._2022.Day13;
using AoC._2022.Day14;
using AoC._2022.Day15;
using AoC._2022.Day17;
using AoC._2022.Day18;
using AoC._2022.Day19;
using AoC._2022.Day20;
using AoC._2022.Day21;

namespace AoC._2022.Test;

public class Day1ToXTests
{
    [Theory]
    [InlineData("Day01/t1.txt", 24000, 45000)]
    [InlineData("Day01/input.txt", 67658, 200158)]

    public void Day01_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new CalorieCounter().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day02/t1.txt", 15, 12)]
    [InlineData("Day02/input.txt", 14069, 12411)]

    public void Day02_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new RPSScoreCalculator().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("Day03/t1.txt", 157, 70)]
    [InlineData("Day03/input.txt", 7821, 2752)]

    public void Day03_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new RucksackOrganaizer().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day04/t1.txt", 2, 4)]
    [InlineData("Day04/input.txt", 509, 870)]
    public void Day04_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new SectionChecker().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day05/t1.txt", "CMZ", "MCD")]
    [InlineData("Day05/input.txt", "TLFGBZHCN", "QRQFHFWCL")]
    public void Day05_Test(string path, string? expectedPartOne, string? expectedPartTwo)
    {
        new CrateTracker().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day06/t1.txt", 7, 19)]
    [InlineData("Day06/t2.txt", 5, 23)]
    [InlineData("Day06/t3.txt", 6, 23)]
    [InlineData("Day06/t4.txt", 10, 29)]
    [InlineData("Day06/t5.txt", 11, 26)]
    [InlineData("Day06/input.txt", 1542, 3153)]
    public void Day06_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new PacketProcessor().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day07/t1.txt", 95437, 24933642)]
    [InlineData("Day07/input.txt", 1334506, 7421137)]
    public void Day07_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new CommandLineInterpreter().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day08/t1.txt", 21, 8)]
    [InlineData("Day08/input.txt", 1695, 287040)]
    public void Day08_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new GroveEvaluator().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day09/t1.txt", 13, 1)]
    [InlineData("Day09/t2.txt", 88, 36)]
    [InlineData("Day09/input.txt", 6311, 2482)]
    public void Day09_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new RopeSimulator().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("Day10/t1.txt", 13140, null)]
    [InlineData("Day10/input.txt", 14160, null)] // Part Two: RJERPEFC
    public void Day10_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new HandheldRepair().Test(path, expectedPartOne, expectedPartTwo);
    }
    //[Theory]
    //[InlineData("DayXI/t1.txt", 10605.0, null)] //2713310158.0
    //[InlineData("DayXI/t1.txt", null, 2713310158.0)] //2713310158.0
    //[InlineData("DayXI/input.txt", 54054.0, null)]
    //public void DayXI_Test(string path, double? expectedPartOne, double? expectedPartTwo)
    //{
    //    new MonkeyKeepAway().Test(path, expectedPartOne, expectedPartTwo);
    //}

    [Theory]
    [InlineData("Day12/t1.txt", 31, 29)]
    [InlineData("Day12/input.txt", 468, 459)]
    public void Day12_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new HeightMapPathFinder().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day13/t1.txt", 13, 140)]
    [InlineData("Day13/input.txt", 4734, 21836)]
    public void Day13_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new PacketOrderChecker().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("Day14/t1.txt", 24, 93)]
    [InlineData("Day14/input.txt", 832, 27601)]
    public void Day14_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new SandMapper().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("Day15/t1.txt", 26.0, 56000011.0)]
    [InlineData("Day15/input.txt", 6425133.0, 10996191429555.0)]
    public void Day15_Test(string path, double? expectedPartOne, double? expectedPartTwo)
    {
        new BeconSensors().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("Day17/t1.txt", 3068.0, 1514285714288.0)] // 1514285714288.0
    [InlineData("Day17/input.txt", 3209.0, 1580758017509.0)]

    public void Day17_Test(string path, double? expectedPartOne, double? expectedPartTwo)
    {
        new PyroclasticTetris().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("Day18/t1.txt", 64, 58)]
    [InlineData("Day18/input.txt", 4580, 2610)]

    public void Day18_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new LavaDroplet().Test(path, expectedPartOne, expectedPartTwo);
    }

    //[Theory]
    //[InlineData("Day19/t1.txt", 33, null)]
    //[InlineData("Day19/input.txt", 1, null)]

    //public void Day19_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    //{
    //    new RobotBlueprintTester().Test(path, expectedPartOne, expectedPartTwo);
    //}

    [Theory]
    [InlineData("Day20/t1.txt", 3L, 1623178306L)]
    [InlineData("Day20/input.txt", 19070L, 14773357352059L)]

    public void Day20_Test(string path, long? expectedPartOne, long? expectedPartTwo)
    {
        new CoordinatesDecoder().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day21/t1.txt", 152D, 301D)]
    [InlineData("Day21/input.txt", 256997859093114D, 3952288690726D)]

    public void Day21_Test(string path, double? expectedPartOne, double? expectedPartTwo)
    {
        new MonkeyGame().Test(path, expectedPartOne, expectedPartTwo);
    }
}
