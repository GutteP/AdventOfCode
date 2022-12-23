using AoC._2022.Day1;
using AoC._2022.Day2;
using AoC._2022.Day3;
using AoC._2022.Day4;
using AoC._2022.Day5;
using AoC._2022.Day6;
using AoC._2022.Day7;
using AoC._2022.Day8;
using AoC._2022.Day9;
using AoC._2022.DayX;
using AoC._2022.DayXI;
using AoC._2022.DayXII;
using AoC._2022.DayXIII;
using AoC._2022.DayXIV;
using AoC._2022.DayXV;

namespace AoC._2022.Test;

public class Day1ToXTests
{
    [Theory]
    [InlineData("Day1/t1.txt", 24000, 45000)]
    [InlineData("Day1/input.txt", 67658, 200158)]

    public void Day1_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new CalorieCounter().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day2/t1.txt", 15, 12)]
    [InlineData("Day2/input.txt", 14069, 12411)]

    public void Day2_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new RPSScoreCalculator().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("Day3/t1.txt", 157, 70)]
    [InlineData("Day3/input.txt", 7821, 2752)]

    public void Day3_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new RucksackOrganaizer().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day4/t1.txt", 2, 4)]
    [InlineData("Day4/input.txt", 509, 870)]
    public void Day4_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new SectionChecker().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day5/t1.txt", "CMZ", "MCD")]
    [InlineData("Day5/input.txt", "TLFGBZHCN", "QRQFHFWCL")]
    public void Day5_Test(string path, string? expectedPartOne, string? expectedPartTwo)
    {
        new CrateTracker().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day6/t1.txt", 7, 19)]
    [InlineData("Day6/t2.txt", 5, 23)]
    [InlineData("Day6/t3.txt", 6, 23)]
    [InlineData("Day6/t4.txt", 10, 29)]
    [InlineData("Day6/t5.txt", 11, 26)]
    [InlineData("Day6/input.txt", 1542, 3153)]
    public void Day6_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new PacketProcessor().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day7/t1.txt", 95437, 24933642)]
    [InlineData("Day7/input.txt", 1334506, 7421137)]
    public void Day7_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new CommandLineInterpreter().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day8/t1.txt", 21, 8)]
    [InlineData("Day8/input.txt", 1695, 287040)]
    public void Day8_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new GroveEvaluator().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("Day9/t1.txt", 13, 1)]
    [InlineData("Day9/t2.txt", 88, 36)]
    [InlineData("Day9/input.txt", 6311, 2482)]
    public void Day9_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new RopeSimulator().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("DayX/t1.txt", 13140, null)]
    [InlineData("DayX/input.txt", 14160, null)] // Part Two: RJERPEFC
    public void DayX_Test(string path, int? expectedPartOne, int? expectedPartTwo)
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
    [InlineData("DayXII/t1.txt", 31, 29)]
    [InlineData("DayXII/input.txt", 468, 459)]
    public void DayXII_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new HeightMapPathFinder().Test(path, expectedPartOne, expectedPartTwo);
    }

    [Theory]
    [InlineData("DayXIII/t1.txt", 13, 140)]
    [InlineData("DayXIII/input.txt", 4734, 21836)]
    public void DayXIII_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new PacketOrderChecker().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("DayXIV/t1.txt", 24, 93)]
    [InlineData("DayXIV/input.txt", 832, 27601)]
    public void DayXIV_Test(string path, int? expectedPartOne, int? expectedPartTwo)
    {
        new SandMapper().Test(path, expectedPartOne, expectedPartTwo);
    }
    [Theory]
    [InlineData("DayXV/t1.txt", 26.0, 56000011.0)]
    [InlineData("DayXV/input.txt", 6425133.0, 10996191429555.0)]
    public void DayXV_Test(string path, double? expectedPartOne, double? expectedPartTwo)
    {
        new BeconSensors().Test(path, expectedPartOne, expectedPartTwo);
    }
}
