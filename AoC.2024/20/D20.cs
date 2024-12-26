namespace AoC._2024;

public class D20
{
    public long PartOne(string inputPath, int min)
    {
        List<List<int>> map = InputReader.ReadLines(inputPath).Select(x => x.Select(c => c == '#' ? -2 : c != 'E' ? -1 : -3).ToList()).ToList();
        map = map.CalculateStepsLeft();
        List<(int X, int Y)> steps = map.Steps();
        List<int> savings = new();
        foreach (var step in steps)
        {
            foreach (var s in map.Savings(step))
            {
                if (s >= min) savings.Add(s);
            }
        }
        return savings.Count;
    }

    public long? PartTwo(string inputPath, int min)
    {
        List<List<int>> map = InputReader.ReadLines(inputPath).Select(x => x.Select(c => c == '#' ? -2 : c != 'E' ? -1 : -3).ToList()).ToList();
        map = map.CalculateStepsLeft();
        List<(int X, int Y)> steps = map.Steps();
        int cheats = 0;

        foreach (var step in steps)
        {
            foreach (var s in map.CheatsInSteps(step, 20))
            {
                if (s.Savings >= min)
                {
                    cheats++;
                }
            }
        }
        return cheats;
    }
}


public static class D20Extensions
{
    public static IEnumerable<((int X, int Y) Target, int Savings)> CheatsInSteps(this List<List<int>> map, (int X, int Y) current, int steps)
    {
        int currentValue = map[current.X][current.Y];
        IEnumerable<((int X, int Y) Move, int Steps)> moves = current.MovesInSteps(steps, map.Count - 1, map[0].Count - 1);
        foreach (var m in moves)
        {
            int v = map[current.X + m.Move.X][current.Y + m.Move.Y];
            if (v != -2 && v < currentValue) yield return ((current.X + m.Move.X, current.Y + m.Move.Y), currentValue - v - m.Steps);
        }
    }

    public static IEnumerable<((int X, int Y) Move, int Steps)> MovesInSteps(this (int X, int Y) current, int steps, int xMax, int yMax)
    {
        HashSet<((int X, int Y) Move, int steps)> moves = new();
        for (int i = 1; i <= steps; i++)
        {
            for (int j = 0; j <= steps - i; j++)
            {
                moves.Add(((i, j), i + j));
                moves.Add(((i, -j), i + j));
                moves.Add(((-i, j), i + j));
                moves.Add(((-i, -j), i + j));
                moves.Add(((j, i), i + j));
                moves.Add(((-j, i), i + j));
                moves.Add(((j, -i), i + j));
                moves.Add(((-j, -i), i + j));
            }
        }
        return moves.Where(m => current.X + m.Move.X < xMax && current.X + m.Move.X >= 0 && current.Y + m.Move.Y < yMax && current.Y + m.Move.Y >= 0);
    }
    public static IEnumerable<int> Savings(this List<List<int>> map, (int X, int Y) step)
    {
        if (step.X + 2 < map.Count && map[step.X + 1][step.Y] == -2 && map[step.X + 2][step.Y] >= 0 && map[step.X + 2][step.Y] < map[step.X][step.Y])
        {
            yield return map[step.X][step.Y] - map[step.X + 2][step.Y] - 2;
        }
        if (step.X - 2 >= 0 && map[step.X - 1][step.Y] == -2 && map[step.X - 2][step.Y] >= 0 && map[step.X - 2][step.Y] < map[step.X][step.Y])
        {
            yield return map[step.X][step.Y] - map[step.X - 2][step.Y] - 2;
        }
        if (step.Y + 2 < map[0].Count && map[step.X][step.Y + 1] == -2 && map[step.X][step.Y + 2] >= 0 && map[step.X][step.Y + 2] < map[step.X][step.Y])
        {
            yield return map[step.X][step.Y] - map[step.X][step.Y + 2] - 2;
        }
        if (step.Y - 2 >= 0 && map[step.X][step.Y - 1] == -2 && map[step.X][step.Y - 2] >= 0 && map[step.X][step.Y - 2] < map[step.X][step.Y])
        {
            yield return map[step.X][step.Y] - map[step.X][step.Y - 2] - 2;
        }
    }
    public static List<(int X, int Y)> Steps(this List<List<int>> map)
    {
        List<(int X, int Y)> steps = new();
        for (int x = 0; x < map.Count; x++)
        {
            for (int y = 0; y < map[x].Count; y++)
            {
                if (map[x][y] > 0)
                {
                    steps.Add((x, y));
                }
            }
        }
        return steps;
    }
    public static List<List<int>> CalculateStepsLeft(this List<List<int>> map)
    {
        (int X, int Y) current = (0, 0);
        (int X, int Y)? from = null;
        for (int x = 0; x < map.Count; x++)
        {
            for (int y = 0; y < map[x].Count; y++)
            {
                if (map[x][y] == -3)
                {
                    current = (x, y);
                }
            }
        }
        while (true)
        {
            if (from == null) map[current.X][current.Y] = 0;
            else
            {
                map[current.X][current.Y] = map[from.Value.X][from.Value.Y] + 1;
            }
            from = current;
            if (map[current.X + 1][current.Y] == -1)
            {
                current = (current.X + 1, current.Y);
            }
            else if (map[current.X - 1][current.Y] == -1)
            {
                current = (current.X - 1, current.Y);
            }
            else if (map[current.X][current.Y - 1] == -1)
            {
                current = (current.X, current.Y - 1);
            }
            else if (map[current.X][current.Y + 1] == -1)
            {
                current = (current.X, current.Y + 1);
            }
            else break;
        }
        return map;
    }
}


