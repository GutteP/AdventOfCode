namespace AoC._2023._17;

public class ClumsyCrucible : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<int[,], int>(Transformer, Solve), null);
    }

    private int[,] Transformer(string path)
    {
        return InputReader.ReadLines(path).Select(r => r.Select(c => (int)char.GetNumericValue(c))).Map2D();
    }

    private int Solve(int[,] map)
    {
        var result = AStarRunner.AStar(new Position<int>(0, 0), new Position<int>(map.GetLength(1) - 1, map.GetLength(0) - 1), 1, map);
        var weight = CalculateWeight(result, map);
        return weight;
    }
    private int CalculateWeight(List<Position<int>> path, int[,] map)
    {
        int weight = 0;
        foreach (var p in path)
        {
            weight += map[p.Y, p.X];
        }
        weight -= map[path[0].Y, path[0].X];
        return weight;
    }
}
