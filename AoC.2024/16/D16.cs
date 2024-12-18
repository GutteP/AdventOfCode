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
        List<string> input = InputReader.ReadLines(inputPath);
        return 0;
    }
}


public static class D16Extensions
{
    public static void Walk(this List<string> map, (int X, int Y) current, long currentScore, int dir, (int X, int Y) end, Dictionary<(int X, int Y), long> visited)
    {
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

        var nextSterps = NextSteps(current, dir);
        map.Walk(nextSterps.Forward.Position, currentScore + 1, nextSterps.Forward.Direction, end, visited);
        map.Walk(nextSterps.Right.Position, currentScore + 1001, nextSterps.Right.Direction, end, visited);
        map.Walk(nextSterps.Left.Position, currentScore + 1001, nextSterps.Left.Direction, end, visited);
    }

    public static (((int X, int Y) Position, int Direction) Forward, ((int X, int Y) Position, int Direction) Right, ((int X, int Y) Position, int Direction) Left) NextSteps((int X, int Y) current, int dir)
    {
        if (dir == 1)
        {
            return (((current.X - 1, current.Y), 1), ((current.X, current.Y + 1), 2), ((current.X, current.Y - 1), 4));
        }
        if (dir == 2)
        {
            return (((current.X, current.Y + 1), 2), ((current.X - 1, current.Y), 1), ((current.X + 1, current.Y), 3));
        }
        if (dir == 3)
        {
            return (((current.X + 1, current.Y), 3), ((current.X, current.Y - 1), 4), ((current.X, current.Y + 1), 2));
        }
        if (dir == 4)
        {
            return (((current.X, current.Y - 1), 4), ((current.X + 1, current.Y), 3), ((current.X - 1, current.Y), 1));
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

