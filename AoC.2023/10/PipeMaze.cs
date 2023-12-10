using AoC.Common;

namespace AoC._2023._10;

public class PipeMaze : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<char[,], int>(Transformer, Loop), new Runner<char[,], int>(Transformer, Inside));
    }

    private char[,] Transformer(string path)
    {
        return InputReader.ReadLines(path).Map2D(false);
    }

    private int Loop(char[,] map)
    {
        var (next, dir) = FindFirstNextAndDirection(map);
        int steps = 1;
        while (map[next.Y, next.X] != 'S')
        {
            Direction newDir = dir.FromPipe(map[next.Y, next.X]);
            next.Move(newDir, 1);
            dir = newDir;
            steps++;
        }
        if (steps % 2 != 0)
        {

        }
        return steps / 2;
    }

    private int Inside(char[,] map)
    {
        var (next, dir) = FindFirstNextAndDirection(map);

        List<int[]> loop = new() { new int[2] { next.X, next.Y } };
        while (map[next.Y, next.X] != 'S')
        {
            Direction newDir = dir.FromPipe(map[next.Y, next.X]);
            next.Move(newDir, 1);
            dir = newDir;
            loop.Add([next.X, next.Y]);
        }

        int inside = 0;
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (loop.Any(p => p[0] == x && p[1] == y)) continue;
                if (AoCShapes.InsidePolygon(loop, x, y)) inside++;
            }
        }
        return inside;
    }

    private (Position<int> Next, Direction Direction) FindFirstNextAndDirection(char[,] map)
    {
        Position<int> first = map.FindFirst('S');
        if (first == null) throw new Exception("Hittader ingen start..");
        Position<int> next = default;
        Direction dir = Direction.None;
        if (Direction.Down.FromPipe(map[first.Y + 1, first.X]) != Direction.None)
        {
            next = first with { Y = first.Y + 1 };
            dir = Direction.Down;
        }
        else if (Direction.Right.FromPipe(map[first.Y, first.X + 1]) != Direction.None)
        {
            next = first with { X = first.X + 1 };
            dir = Direction.Right;
        }
        else if (Direction.Up.FromPipe(map[first.Y - 1, first.X]) != Direction.None)
        {
            next = first with { Y = first.Y - 1 };
            dir = Direction.Up;
        }
        else if (Direction.Left.FromPipe(map[first.Y, first.X - 1]) != Direction.None)
        {
            next = first with { X = first.X - 1 };
            dir = Direction.Left;
        }
        return (next, dir);
    }
}
