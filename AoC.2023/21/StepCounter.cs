using System.Collections.Concurrent;

namespace AoC._2023._21;

public class StepCounter : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<(bool[,], Position<int>), bool, int>(Transformer, Solve, false), new Runner<(bool[,], Position<int>), bool, int>(Transformer, Solve, true));
    }

    private (bool[,], Position<int>) Transformer(string path)
    {
        var input = InputReader.ReadLines(path);
        int y = input.IndexOf(input.First(x => x.Contains('S')));
        int x = input[y].IndexOf('S');

        Position<int> start = new(x, y);
        return (input.Select(x => x.Select(y => y != '#')).Map2D(), start);
    }
    private int _xMaxIndex = 0;
    private int _yMaxIndex = 0;
    private int Solve((bool[,], Position<int>) mapAndStart, bool partTwo)
    {
        Position<int> start = mapAndStart.Item2;
        bool[,] map = mapAndStart.Item1;
        _xMaxIndex = map.GetLength(1) - 1;
        _yMaxIndex = map.GetLength(0) - 1;

        IEnumerable<Position<int>> pP = new List<Position<int>>() { start };
        for (int i = 0; i < 64; i++)
        {
            ConcurrentBag<Position<int>> newPP = new();
            Parallel.ForEach(pP, new ParallelOptions { MaxDegreeOfParallelism = 23 }, p =>
            {
                var neighbors = p.Neighbors(false).Where(x => ValidNeighbor(x, map, partTwo));
                foreach (var n in neighbors)
                {
                    newPP.Add(n);
                }
            });
            pP = newPP.Distinct();
        }
        return pP.Count();
    }

    private bool ValidNeighbor(Position<int> p, bool[,] map, bool partTwo)
    {
        int x = p.X;
        int y = p.Y;
        if (partTwo)
        {
            while (x > _xMaxIndex)
            {
                x -= _xMaxIndex + 1;
            }
            while (x < 0)
            {
                x += _xMaxIndex + 1;
            }
            while (y > _yMaxIndex)
            {
                y -= _yMaxIndex + 1;
            }
            while (y < 0)
            {
                y += _yMaxIndex + 1;
            }
        }
        else
        {
            if (!p.InRange(_xMaxIndex, _yMaxIndex)) return false;
        }
        return map[y, x];
    }
}
