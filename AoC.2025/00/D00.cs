using AoC.Common;

namespace AoC._2025;

public class D00
{
    public int? PartOne(string input, string? option1 = null, int? option2 = null)
    {
        if (int.TryParse(input, out int result))
        {
            return result;
        }
        else return null;
    }

    public int? PartTwo(string inputPath, string? option1 = null, int? option2 = null)
    {
        var input = InputReader.ReadLines(inputPath);

        if (int.TryParse(input[0], out int result))
        {
            return result * 2;
        }
        else return null;
    }
}