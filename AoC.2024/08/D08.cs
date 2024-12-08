
namespace AoC._2024;

public class D08
{
    public int? PartOne(string inputPath)
    {
        List<string> map = InputReader.ReadLines(inputPath);
        HashSet<(int X, int Y)> antinodes = new();
        foreach (var type in map.FrequencyTypes())
        {
            foreach (var (A, B) in map.EachPair(type))
            {
                foreach (var antinode in A.Antinodes(B))
                {
                    antinodes.Add(antinode);
                }
            }
        }
        return antinodes.ToList().Where(x => x.X < map.Count && x.X >= 0 && x.Y < map[0].Length && x.Y >= 0).Count();
    }


    public int? PartTwo(string inputPath)
    {
        List<string> map = InputReader.ReadLines(inputPath);
        HashSet<(int X, int Y)> antinodes = new();
        foreach (var type in map.FrequencyTypes())
        {
            foreach (var (A, B) in map.EachPair(type))
            {
                foreach (var antinode in A.AntinodesWithResonantHarmonics(B, map.Count, map[0].Length))
                {
                    antinodes.Add(antinode);
                }
            }
        }
        return antinodes.Count;
    }
}

public static class D08Extensions
{
    public static List<char> FrequencyTypes(this List<string> map)
    {
        HashSet<char> result = new HashSet<char>();
        foreach (var row in map)
        {
            foreach (char item in row)
            {
                if (item != '.') result.Add(item);
            }
        }
        return result.ToList();
    }

    public static IEnumerable<((int X, int Y) A, (int X, int Y) B)> EachPair(this List<string> map, char type)
    {
        List<(int X, int Y)> points = new();
        for (int x = 0; x < map.Count; x++)
        {
            for (int y = 0; y < map[x].Length; y++)
            {
                if (map[x][y] == type)
                {
                    points.Add((x, y));
                }
            }
        }

        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                yield return (points[i], points[j]);
            }
        }
    }

    private static (int X, int Y) ManhattanDistance(this (int X, int Y) A, (int X, int Y) B)
    {
        return (A.X - B.X, A.Y - B.Y);
    }

    public static List<(int X, int Y)> Antinodes(this (int X, int Y) a, (int X, int Y) b)
    {
        var d = a.ManhattanDistance(b);
        return [(a.X + d.X, a.Y + d.Y), (b.X - d.X, b.Y - d.Y)];
    }
    public static List<(int X, int Y)> AntinodesWithResonantHarmonics(this (int X, int Y) a, (int X, int Y) b, int xLim, int yLim)
    {
        var d = a.ManhattanDistance(b);
        List<(int X, int Y)> antinodes = [a, b];
        (int X, int Y) current = a;
        while (true)
        {
            (int X, int Y) anti = (current.X - d.X, current.Y - d.Y);
            if (anti.X < xLim && anti.X >= 0 && anti.Y < yLim && anti.Y >= 0)
            {
                antinodes.Add(anti);
                current = anti;
            }
            else break;
        }
        current = a;
        while (true)
        {
            (int X, int Y) anti = (current.X + d.X, current.Y + d.Y);
            if (anti.X < xLim && anti.X >= 0 && anti.Y < yLim && anti.Y >= 0)
            {
                antinodes.Add(anti);
                current = anti;
            }
            else break;
        }
        return antinodes;
    }


}