using AoC.Common;

namespace AoC._2025;

public class D09
{
    public long? PartOne(string inputPath, int? option1 = null)
    {
        List<Position<long>> redTiles = InputReader.ReadLines(inputPath).Select(x => new Position<long>(long.Parse(x.Split(',', StringSplitOptions.TrimEntries)[0]), long.Parse(x.Split(',', StringSplitOptions.TrimEntries)[1]))).ToList();


        List<(long,string)> areas = [];
        foreach (var p1 in redTiles)
        {
            foreach (var p2 in redTiles) 
            {
                var distance = p1.Distance(p2);
                areas.Add(((Math.Abs(distance.X)+1) * (Math.Abs(distance.Y)+1), $"{p1},{p2}"));
            }
        }
        areas = areas.OrderByDescending(x => x.Item1).ToList();
        return areas.Max(x => x.Item1);
    }

    public long? PartTwo(string inputPath)
    {
        var input = InputReader.ReadLines(inputPath);

        return null;
    }
}
