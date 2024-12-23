namespace AoC._2024;

public class D18
{
    public long PartOne(string inputPath, int xSize, int ySize, int simulationLength)
    {
        Stack<(int X, int Y)> bytes = new(InputReader.ReadLines(inputPath).Select(x => x.Split(',').Select(y => int.Parse(y)).ToList()).Select(x => (x[0], x[1])).Reverse());
        int[,] map = new int[xSize, ySize];



        for (int i = 0; i < simulationLength; i++)
        {
            (int X, int Y) b = bytes.Pop();
            map[b.X, b.Y] = -1;
        }
        map.CalculateDistances((0, 0));
        return map[xSize - 1, ySize - 1];
    }


    public string? PartTwo(string inputPath, int xSize, int ySize, int initialSeed)
    {
        Stack<(int X, int Y)> bytes = new(InputReader.ReadLines(inputPath).Select(x => x.Split(',').Select(y => int.Parse(y)).ToList()).Select(x => (x[0], x[1])).Reverse());
        int[,] map = new int[xSize, ySize];

        int i = 1;
        while (bytes.Count > 0)
        {
            (int X, int Y) b = bytes.Pop();
            map[b.X, b.Y] = -1;
            if (i > initialSeed)
            {
                if (!map.IsPassable((0, 0), new bool[xSize, ySize]))
                {
                    return $"{b.X},{b.Y}";
                }
            }
            i++;
        }

        return null;
    }
}


public static class D18Extensions
{
    public static void CalculateDistances(this int[,] map, (int X, int Y) current)
    {
        if (current.IsEnd(map))
        {
            return;
        }
        int currentDistance = map[current.X, current.Y];
        foreach (var n in map.Neighbors(current))
        {
            if (map[n.X, n.Y] == 0 || map[n.X, n.Y] > currentDistance + 1)
            {
                map[n.X, n.Y] = currentDistance + 1;
                CalculateDistances(map, n);
            }
        }
    }

    public static bool IsPassable(this int[,] map, (int X, int Y) current, bool[,] visited)
    {
        if (current.IsEnd(map))
        {
            return true;
        }
        if (visited[current.X, current.Y])
        {
            return false;
        }
        visited[current.X, current.Y] = true;
        foreach (var n in map.Neighbors(current))
        {
            if (IsPassable(map, n, visited))
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsStart(this (int X, int Y) b)
    {
        if (b.X == 0 && b.Y == 0) return true;
        return false;
    }
    public static bool IsEnd(this (int X, int Y) b, int[,] map)
    {
        if (b.X == map.GetLength(0) - 1 && b.Y == map.GetLength(1) - 1) return true;
        return false;
    }
    public static IEnumerable<(int X, int Y)> Neighbors(this int[,] map, (int X, int Y) c)
    {
        List<(int X, int Y)> neighbors = [(c.X - 1, c.Y), (c.X + 1, c.Y), (c.X, c.Y - 1), (c.X, c.Y + 1)];
        return neighbors.Where(x => !IsStart(x) && x.X >= 0 && x.X < map.GetLength(0) && x.Y >= 0 && x.Y < map.GetLength(1) && map[x.X, x.Y] != -1);
    }
}


