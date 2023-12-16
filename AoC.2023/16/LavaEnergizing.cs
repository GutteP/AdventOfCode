using AoC.Common;

namespace AoC._2023._16;

public class LavaEnergizing : IAoCDay<int>
{
    private const char FS = '/';
    private const char BS = (char)92;
    private const char DA = '-';
    private const char PIP = '|';
    private const char GR = '.';

    private Dictionary<string, bool> _visited;
    private List<string> map;

    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<List<string>, int>(Transformer, Solve), new Runner<List<string>, int>(Transformer, SolveMore));
    }

    private List<string> Transformer(string path)
    {
        List<string> map = new();
        foreach (string row in InputReader.ReadLines(path))
        {
            map.Add(row.Trim());
        }
        return map;
    }

    private int Solve(List<string> input)
    {
        map = input;
        return StartTravle(new(-1, 0), Direction.Right);
    }

    private int SolveMore(List<string> input)
    {
        map = input;
        int max = 0;
        for (int i = 0; i < map.Count; i++)
        {
            int energized = StartTravle(new(-1, i), Direction.Right);
            if (energized > max) max = energized;
            energized = StartTravle(new(map[0].Length, i), Direction.Left);
            if (energized > max) max = energized;
        }
        for (int i = 0; i < map[0].Length; i++)
        {
            int energized = StartTravle(new(i, -1), Direction.Down);
            if (energized > max) max = energized;
            energized = StartTravle(new(i, map.Count), Direction.Up);
            if (energized > max) max = energized;
        }

        return max;
    }

    private int NumberOfEnergized()
    {
        List<string> energized = new();
        foreach (string vis in _visited.Keys)
        {
            var sp = vis.Split(',');
            energized.Add(string.Join(',', sp[0..2]));
        }
        energized = energized.Distinct().ToList();
        return energized.Count;
    }

    private int StartTravle(Position<int> start, Direction direction)
    {
        _visited = new();
        Travel(start, direction);
        return NumberOfEnergized();
    }
    private void Travel(Position<int> position, Direction direction)
    {
        position.Move(direction, 1);
        if (!map.IsOnMap(position.X, position.Y)) return;
        string key = $"{position},{(int)direction}";
        if (_visited.ContainsKey(key)) return;
        _visited.Add(key, true);

        var dirs = NewDirections(direction, map[position.Y][position.X]);
        foreach (Direction dir in dirs)
        {
            Travel(new(position.X, position.Y), dir);
        }
    }

    private Direction[] NewDirections(Direction current, char changer)
    {
        if (changer == GR) return [current];
        if (current == Direction.Right) // ->
        {
            return changer switch
            {
                DA => [Direction.Right],
                PIP => [Direction.Up, Direction.Down],
                FS => [Direction.Up],
                BS => [Direction.Down],
                _ => throw new ArgumentException("Not a Arrow")
            };
        }
        if (current == Direction.Left) // <-
        {
            return changer switch
            {
                DA => [Direction.Left],
                PIP => [Direction.Up, Direction.Down],
                FS => [Direction.Down],
                BS => [Direction.Up],
                _ => throw new ArgumentException("Not a Arrow")
            };
        }
        if (current == Direction.Up) // ^
        {
            return changer switch
            {
                DA => [Direction.Left, Direction.Right],
                PIP => [Direction.Up],
                FS => [Direction.Right],
                BS => [Direction.Left],
                _ => throw new ArgumentException("Not a Arrow")
            };
        }
        if (current == Direction.Down) // v
        {
            return changer switch
            {
                DA => [Direction.Left, Direction.Right],
                PIP => [Direction.Down],
                FS => [Direction.Left],
                BS => [Direction.Right],
                _ => throw new ArgumentException("Not a Arrow")
            };
        }
        throw new Exception("Fel..");
    }
}


