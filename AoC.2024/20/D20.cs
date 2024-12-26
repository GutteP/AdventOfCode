namespace AoC._2024;

public class D20
{
    public long PartOne(string inputPath, int min)
    {
        List<List<int>> map = InputReader.ReadLines(inputPath).Select(x => x.Select(c => c == '#' ? -2 : c != 'E' ? -1 : -3).ToList()).ToList();
        map = map.CalculateStepsLeft();
        string print = map.MapPrint();
        List<(int X, int Y)> steps = map.Steps();
        List<int> savings = new();
        foreach (var step in steps)
        {
            foreach (var s in map.Savings(step))
            {
                if(s >= min) savings.Add(s);
            }
        }
        return savings.Count;
    }

    public long? PartTwo(string inputPath)
    {
        return 0;
    }
}


public static class D20Extensions
{
    public static IEnumerable<int> Savings(this List<List<int>> map, (int X, int Y) step)
    {
        if(step.X + 2 < map.Count && map[step.X + 1][step.Y] == -2 && map[step.X + 2][step.Y] >= 0 && map[step.X + 2][step.Y] < map[step.X][step.Y])
        {
            yield return map[step.X][step.Y] - map[step.X + 2][step.Y] - 2;
        }
        if (step.X - 2 >= 0 && map[step.X - 1][step.Y] == -2 && map[step.X - 2][step.Y] >= 0 && map[step.X - 2][step.Y] < map[step.X][step.Y])
        {
            yield return map[step.X][step.Y] - map[step.X - 2][step.Y] - 2;
        }
        if (step.Y + 2 < map[0].Count && map[step.X][step.Y+1] == -2 && map[step.X][step.Y+2] >= 0 && map[step.X][step.Y+2] < map[step.X][step.Y])
        {
            yield return map[step.X][step.Y] - map[step.X][step.Y+2] - 2;
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
                current = (current.X+1, current.Y);
            }
            else if (map[current.X - 1][current.Y] == -1)
            {
                current = (current.X - 1, current.Y);
            }
            else if (map[current.X][current.Y - 1] == -1)
            {
                current = (current.X, current.Y-1);
            }
            else if (map[current.X][current.Y + 1] == -1)
            {
                current = (current.X, current.Y+1);
            }
            else break;
        }
        return map;
    }

    public static string MapPrint(this List<List<int>> map)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in map)
        {
            string line = "";
            foreach (var item2 in item)
            {
                line += item2 == -2 ? '#' : item2.ToString().Last();
            }
            sb.AppendLine(line);
        }
        return sb.ToString();
    }
}


