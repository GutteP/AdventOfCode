namespace AoC._2024;

public class D16
{
    public long? PartOne(string inputPath)
    {
        List<string> map = InputReader.ReadLines(inputPath);
        ((int X, int Y) start, (int X, int Y) end) = map.StartAndEnd();

        Dictionary<(int X, int Y), long> visited = new();
        map.Walk(start, 0, 4, end, visited);

        return visited[end];
    }


    public long? PartTwo(string inputPath)
    {
        List<string> map = InputReader.ReadLines(inputPath);
        ((int X, int Y) start, (int X, int Y) end) = map.StartAndEnd();

        Dictionary<(int X, int Y), long> visited = new();
        map.Walk(start, 0, 4, end, visited);
        long minScore = visited[end];
        foreach (var key in visited.Keys)
        {
            if (visited[key] > minScore)
            {
                visited.Remove(key);
            }
        }
        var result = map.WalkWithLimit(start, 0, 4, end, visited, minScore, []);

        var t = result.SelectMany(x => x).Select(y => y).Distinct().ToList();
        return t.Count;
    }
}


public static class D16Extensions
{
    public static void Walk(this List<string> map, (int X, int Y) current, long currentScore, int dir, (int X, int Y) end, Dictionary<(int X, int Y), long> visited)
    {
        if (visited.TryGetValue(end, out long score))
        {
            if (currentScore >= score) return;
        }
        if (map[current.X][current.Y] == '#')
        {
            return;
        }
        if (visited.ContainsKey(current))
        {
            if (visited[current] > currentScore)
            {
                visited[current] = currentScore;
            }
            else return;
        }
        else
        {
            visited.Add(current, currentScore);
        }
        if (current == end)
        {
            return;
        }

        var nextSteps = NextSteps(current, dir);
        foreach (var next in nextSteps)
        {
            map.Walk(next.Position, currentScore + next.Increase, next.Direction, end, visited);
        }
    }

    public static List<List<(int X, int Y)>> WalkWithLimit(this List<string> map, (int X, int Y) current, long currentScore, int dir, (int X, int Y) end, Dictionary<(int X, int Y), long> visited, long limit, List<(int X, int Y)> newVisited)
    {
        if (currentScore > limit)
        {
            return [];
        }
        if (current == end)
        {
            return [[current]];
        }
        List<List<(int X, int Y)>> result = [];
        var nextSteps = NextSteps(current, dir).Where(x => visited.ContainsKey(x.Position) && !newVisited.Contains(x.Position));
        newVisited.Add(current);
        foreach (var next in nextSteps)
        {
            var r = map.WalkWithLimit(next.Position, currentScore + next.Increase, next.Direction, end, visited, limit, newVisited[..]);
            if (r.Count > 0)
            {
                foreach (var l in r)
                {
                    l.Add(current);
                }
                result.AddRange(r);
            }
        }
        return result;
    }

    public static List<((int X, int Y) Position, int Direction, int Increase)> NextSteps((int X, int Y) current, int dir)
    {
        if (dir == 1)
        {
            return [((current.X - 1, current.Y), 1, 1), ((current.X, current.Y + 1), 2, 1001), ((current.X, current.Y - 1), 4, 1001)];
        }
        if (dir == 2)
        {
            return [((current.X, current.Y + 1), 2, 1), ((current.X - 1, current.Y), 1, 1001), ((current.X + 1, current.Y), 3, 1001)];
        }
        if (dir == 3)
        {
            return [((current.X + 1, current.Y), 3, 1), ((current.X, current.Y - 1), 4, 1001), ((current.X, current.Y + 1), 2, 1001)];
        }
        if (dir == 4)
        {
            return [((current.X, current.Y - 1), 4, 1), ((current.X + 1, current.Y), 3, 1001), ((current.X - 1, current.Y), 1, 1001)];
        }
        throw new Exception("Invalid direction");
    }

    public static ((int X, int Y) Start, (int X, int Y) End) StartAndEnd(this List<string> input)
    {
        (int X, int Y)? start = null;
        (int X, int Y)? end = null;

        for (int x = 0; x < input.Count; x++)
        {
            if (start != null && end != null) break;
            for (int y = 0; y < input[x].Length; y++)
            {
                if (input[x][y] == 'S')
                {
                    start = (x, y);
                }
                if (input[x][y] == 'E')
                {
                    end = (x, y);
                }
            }
        }
        return (start!.Value, end!.Value);
    }
}

