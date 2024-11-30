namespace AoC._2023._23;

public class ALongWalk : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<char[,], int>(Transformer, Icy), new Runner<char[,], int>(Transformer, Warm));
    }

    private char[,] Transformer(string path)
    {
        return InputReader.ReadLines(path).Map2D();
    }

    public Position<int> Goal { get; set; }
    public char[,] Map { get; set; }
    private const char WOOD = '#';
    private const char PATH = '.';


    private int Icy(char[,] map)
    {
        Map = map;
        Goal = new(map.GetLength(1) - 2, map.GetLength(0) - 1);
        Position<int> start = new(1, 0);
        var possible = RandomWalk(start, new HashSet<Position<int>>() { start });

        //foreach (var path in possible)
        //{
        //    string m = MapPrinter(path);
        //}

        int max = possible.Max(x => x.Count);
        return max-1;
    }
    private int Warm(char[,] map)
    {
        Map = map;
        Goal = new(map.GetLength(1) - 2, map.GetLength(0) - 1);
        Position<int> start = new(1, 0);
        var possible = RandomWalkGlobalWarming(start, new HashSet<Position<int>>() { start });

        //foreach (var path in possible)
        //{
        //    string m = MapPrinter(path);
        //}

        int max = possible.Max(x => x.Count);
        return max - 1;
    }

    public string MapPrinter(IEnumerable<Position<int>> positions)
    {
        char[,] mapCopy = (char[,])Map.Clone();
        foreach (var p in positions)
        {
            mapCopy[p.Y, p.X] = 'O';
        }
        string stringMap = "";
        for (int i = 0; i < mapCopy.GetLength(0); i++)
        {
            string r = "\n";
            for (int j = 0; j < mapCopy.GetLength(1); j++)
            {
                r += mapCopy[i, j];
            }
            stringMap += r;
        }
        return stringMap;
    }

    private IEnumerable<HashSet<Position<int>>> RandomWalk(Position<int> current, HashSet<Position<int>> visited)
    {
        List<HashSet<Position<int>>> result = new();
        if (current == Goal)
        {
            result.Add(visited);
            return result;
        }
        char currentTile = Map.Current(current);
        if (currentTile != PATH)
        {
            HashSet<Position<int>> newVisited = new(visited);
            Direction dir = Direction.None.FromArrow(currentTile);
            var moved = current.CopyAndMove(dir, 1);
            if (newVisited.Add(moved))
            {
                result.AddRange(RandomWalk(moved, newVisited));
                return result;
            }
            else return result;
        }
        else
        {
            var neighbors = current.Neighbors(false);
            foreach (var neighbor in neighbors)
            {
                HashSet<Position<int>> newVisited = new(visited);
                if (!Map.On(neighbor)) continue;
                if (Map.Current(neighbor) == WOOD) continue;
                if (!newVisited.Add(neighbor)) continue;
                result.AddRange(RandomWalk(neighbor, newVisited));
            }
            return result;
        }
    }
    private IEnumerable<HashSet<Position<int>>> RandomWalkGlobalWarming(Position<int> current, HashSet<Position<int>> visited)
    {
        List<HashSet<Position<int>>> result = new();
        if (current == Goal)
        {
            result.Add(visited);
            return result;
        }
        var neighbors = current.Neighbors(false);
        foreach (var neighbor in neighbors)
        {
            if (!Map.On(neighbor)) continue;
            if (Map.Current(neighbor) == WOOD) continue;
            if (visited.Contains(neighbor)) continue;
            HashSet<Position<int>> newVisited = new(visited)
            {
                neighbor
            };
            result.AddRange(RandomWalkGlobalWarming(neighbor, newVisited));
        }
        return result;
    }
}
