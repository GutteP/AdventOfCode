using AoC.Common;

namespace AoC._2025;

public class D04
{
    public int? PartOne(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<char[]> map = InputReader.ReadLines(inputPath).Select(x => x.ToArray()).ToList();

        int count = 0;
        for (int x = 0; x < map.Count; x++)
        {
            for (int y = 0; y < map[x].Length; y++)
            {
                if (map[x][y] == '@' && CountAdjacent(map, new Position<int>(x, y), '@') < 4)
                {
                    count++;
                }
            }
        }
        return count;
    }

    public long? PartTwo(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<char[]> map = InputReader.ReadLines(inputPath).Select(x => x.ToArray()).ToList();

        int count = 0;
        while (true)
        {
            int c = 0;
            (map, c) = RemoveAccessible(map);
            if(c == 0)
            {
                break;
            }
            count += c;
        }
        
        return count;
    }

    private (List<char[]>, int) RemoveAccessible(List<char[]> map)
    {
        List<Position<int>> accessible = [];
        for (int x = 0; x < map.Count; x++)
        {
            for (int y = 0; y < map[x].Length; y++)
            {
                Position<int> p = new(x, y);
                if (map[x][y] == '@' && CountAdjacent(map, p, '@') < 4)
                {
                    accessible.Add(p);
                }
            }
        }
        foreach (var p in accessible)
        {
            map[p.X][p.Y] = '.';
        }
        return (map, accessible.Count);
    }

    private int CountAdjacent(List<char[]> map, Position<int> pos, char c)
    {
        int count = 0;
        var adjacents = pos.Neighbors(withDiagonals: true);
        foreach (var p in adjacents)
        {
            if (map.IsOnMap(p.X, p.Y) && map[p.X][p.Y] == c)
            {
                count++;
            }
        }
        return count;
    }
}
