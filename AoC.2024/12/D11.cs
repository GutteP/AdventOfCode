namespace AoC._2024;

public class D12
{
    public long? PartOne(string inputPath)
    {
        List<string> map = InputReader.ReadLines(inputPath);

        List<(char Type, HashSet<(int X, int Y)> Region, int Perimeter)> regions = new();
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (regions.Any(x => x.Region.Contains((i, j)))) continue;
                var region = (i, j).CompleteRegion(new HashSet<(int X, int Y)>(), map[i][j], map);
                regions.Add((map[i][j], region, region.Perimeter().Count));
            }
        }
        return regions.Select(x => x.Region.Count * x.Perimeter).Sum();
    }


    public long? PartTwo(string inputPath)
    {
        List<string> map = InputReader.ReadLines(inputPath);

        List<(char Type, HashSet<(int X, int Y)> Region, int PerimeterSides)> regions = new();
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (regions.Any(x => x.Region.Contains((i, j)))) continue;
                var region = (i, j).CompleteRegion(new HashSet<(int X, int Y)>(), map[i][j], map);
                regions.Add((map[i][j], region, region.Perimeter().CountOfSides()));
            }
        }
        return regions.Select(x => x.Region.Count * x.PerimeterSides).Sum();
    }
}

public static class D12Extensions
{
    public static HashSet<(int X, int Y)> CompleteRegion(this (int X, int Y) p, HashSet<(int X, int Y)> region, char type, List<string> map)
    {
        if (region.Contains(p))
        {
            return region;
        }
        if (p.X < 0 || p.Y < 0 || p.X >= map.Count || p.Y >= map[0].Length)
        {
            return region;
        }
        if (map[p.X][p.Y] != type)
        {
            return region;
        }

        region.Add(p);

        (p.X + 1, p.Y).CompleteRegion(region, type, map);
        (p.X, p.Y + 1).CompleteRegion(region, type, map);
        (p.X - 1, p.Y).CompleteRegion(region, type, map);
        (p.X, p.Y - 1).CompleteRegion(region, type, map);

        return region;
    }

    public static HashSet<(int Side, (int X, int Y) P)> Perimeter(this HashSet<(int X, int Y)> region)
    {
        HashSet<(int Side, (int X, int Y) P)> perimeter = new();

        foreach (var p in region)
        {
            if (!region.Contains((p.X + 1, p.Y))) perimeter.Add((1, (p.X + 1, p.Y)));
            if (!region.Contains((p.X, p.Y + 1))) perimeter.Add((2, (p.X, p.Y + 1)));
            if (!region.Contains((p.X - 1, p.Y))) perimeter.Add((3, (p.X - 1, p.Y)));
            if (!region.Contains((p.X, p.Y - 1))) perimeter.Add((4, (p.X, p.Y - 1)));
        }

        return perimeter;
    }
    public static int CountOfSides(this HashSet<(int Side, (int X, int Y) P)> perimeter)
    {

        List<(int Side, List<(int X, int Y)> PerimiterSide)> distinctPerimetersSides = new();
        foreach (var p in perimeter)
        {
            if (distinctPerimetersSides.Any(x => x.Side == p.Side && x.PerimiterSide.Contains(p.P))) continue;
            distinctPerimetersSides.Add((p.Side, perimeter.Where(x => x.Side == p.Side).Select(x => x.P).WalkPerimeterSide(p.P, new List<(int X, int Y)>(), p.Side)));
        }

        return distinctPerimetersSides.Count;
    }
    public static List<(int X, int Y)> WalkPerimeterSide(this IEnumerable<(int X, int Y)> perimetersWithSide, (int X, int Y) p, List<(int X, int Y)> perimeter, int side)
    {
        if (!perimeter.Contains(p) && perimetersWithSide.Contains(p)) perimeter.Add(p);
        else return perimeter;

        if (side == 1 || side == 3)
        {
            perimetersWithSide.WalkPerimeterSide((p.X, p.Y + 1), perimeter, side);
            perimetersWithSide.WalkPerimeterSide((p.X, p.Y - 1), perimeter, side);
        }
        else if (side == 2 || side == 4)
        {
            perimetersWithSide.WalkPerimeterSide((p.X + 1, p.Y), perimeter, side);
            perimetersWithSide.WalkPerimeterSide((p.X - 1, p.Y), perimeter, side);
        }
        return perimeter;
    }
}

