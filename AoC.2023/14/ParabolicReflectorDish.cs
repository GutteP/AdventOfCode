using AoC.Common;
using System.Collections.Generic;

namespace AoC._2023._14;

public class ParabolicReflectorDish : IAoCDay<int>
{
    private const char RRock = 'O';
    private const char SRock = '#';
    private const int PartTwoCycles = 1000000000;

    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<List<string>, int>(Transformer, Solve), new Runner<List<string>, int>(Transformer, SpinCycles));
    }

    private List<string> Transformer(string path)
    {
        return InputReader.ReadLines(path);
    }

    private int Solve(List<string> map)
    {
        List<Position<int>> rRocks = ExtractRoundRockPositions(map);
        Tilt(rRocks, map, Direction.Up);
        return rRocks.Sum(x => map.Count - x.Y);
    }

    private int SpinCycles(List<string> map)
    {
        List<Position<int>> rRocks = ExtractRoundRockPositions(map);

        Dictionary<string, int> dict = new();
        int i = 0;
        List<string> keys = null;
        List<int> values = null;
        int last = -1;
        int fromIndex = -1;
        int lastIndex = -1;
        while (i < PartTwoCycles)
        {
            string from = string.Join(';', rRocks.OrderBy(x => x.X).ThenBy(x => x.Y).Select(x => x.ToString()));
            if (!dict.TryGetValue(from, out int outRocks))
            {
                Cycle(rRocks, map);
                dict.Add(from, rRocks.Sum(x => map.Count - x.Y));
                i++;
            }
            else
            {
                keys = dict.Keys.ToList();
                values = dict.Values.ToList();
                fromIndex = keys.IndexOf(from);
                break;
            }
        }
        int max = keys.Count - (fromIndex);
        while (i < PartTwoCycles)
        {
            if (i + max < PartTwoCycles)
            {
                i += max;
                last = max;
            }
            else
            {
                last = (PartTwoCycles - i) - 1;
                i = PartTwoCycles;
            }
            lastIndex = fromIndex + last;
        }
        return values[lastIndex];
    }

    private void Cycle(List<Position<int>> rRocks, List<string> map)
    {
        Tilt(rRocks, map, Direction.Up);
        Tilt(rRocks, map, Direction.Left);
        Tilt(rRocks, map, Direction.Down);
        Tilt(rRocks, map, Direction.Right);
    }

    private void Tilt(List<Position<int>> rRocks, List<string> map, Direction direction)
    {
        if (direction == Direction.Up || direction == Direction.Down)
        {
            Parallel.ForEach(rRocks.GroupBy(x => x.X), group =>
            {
                ParallelTilt(group, map, direction);
            });
        }
        else
        {
            Parallel.ForEach(rRocks.GroupBy(x => x.Y), group =>
            {
                ParallelTilt(group, map, direction);
            });
        }
    }
    private void ParallelTilt(IGrouping<int, Position<int>> group, List<string> map, Direction direction)
    {
        foreach (var r in OrderByDirection(group, direction))
        {
            while (true)
            {
                if (direction == Direction.Up && r.Y == 0) break;
                if (direction == Direction.Down && r.Y == map.Count - 1) break;
                if (direction == Direction.Left && r.X == 0) break;
                if (direction == Direction.Right && r.X == map[0].Length - 1) break;

                r.Move(direction, 1);
                if (map[r.Y][r.X] != SRock && group.Count(x => x == r) == 1) continue;
                else
                {
                    r.Move(direction.Opposite(), 1);
                    break;
                }
            }
        }
    }

    private List<Position<int>> ExtractRoundRockPositions(List<string> map)
    {
        List<Position<int>> rRocks = new();
        for (int i = 0; i < map.Count; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == RRock)
                {
                    rRocks.Add(new(j, i));
                }
            }
        }
        return rRocks;
    }

    private IEnumerable<Position<int>> OrderByDirection(IEnumerable<Position<int>> rRocks, Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return rRocks.OrderBy(x => x.Y);
            case Direction.Down:
                return rRocks.OrderByDescending(x => x.Y);
            case Direction.Left:
                return rRocks.OrderBy(x => x.X);
            case Direction.Right:
                return rRocks.OrderByDescending(x => x.X);
            default:
                return rRocks;
        }
    }
}
