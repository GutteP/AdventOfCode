using System.Diagnostics;

namespace AoC._2024;

public class D15
{
    List<string> Map = new();
    public long? PartOne(string inputPath)
    {
        List<string> input = InputReader.ReadLines(inputPath);
        Map = input.ToMap();
        var instructions = input.Instructions();
        (List<(int X, int Y)> boxesAndRobot, int robot) = input.BoxesAndRobot();

        int robotIndex = robot;
        foreach (var instruction in instructions)
        {
            if (TryMove(robotIndex, instruction, boxesAndRobot, out (int, int) p))
            {
                robotIndex = boxesAndRobot.IndexOf(p);
            }
        }
        boxesAndRobot.RemoveAt(robotIndex);
        return boxesAndRobot.Select(b => b.X * 100 + b.Y).Sum();
    }

    // Tar längre och längre tid, i slutet tar vissa 200ms. En uteliggare på över 8000ms
    public long? PartTwo(string inputPath)
    {
        List<string> input = InputReader.ReadLines(inputPath).SecondWarehouse();
        Map = input.ToMap();
        var instructions = input.Instructions();
        (List<Ob> boxesAndRobot, int robot) = input.BoxesAndRobotV2();
        Stopwatch stopwatch = new();
        int robotIndex = robot;
        List<long> times = new();
        foreach (var instruction in instructions)
        {
            stopwatch.Restart();
            if (TryMoveOb(robotIndex, instruction, boxesAndRobot, out List<(int Index, Ob Ob)> moved))
            {
                foreach (var (Index, Ob) in moved)
                {
                    boxesAndRobot[Index] = Ob;
                }
            }
            stopwatch.Stop();
            times.Add(stopwatch.ElapsedMilliseconds);
            if (stopwatch.ElapsedMilliseconds > 1000)
            {
            }
        }
        boxesAndRobot.RemoveAt(robotIndex);
        return boxesAndRobot.Select(b => b.ClosestEdge().X * 100 + b.ClosestEdge().Y).Sum();
    }
    public bool TryMoveOb(int currentIndex, char directions, List<Ob> boxesAndRobot, out List<(int Index, Ob Ob)> moved)
    {
        IEnumerable<(int X, int Y)> newPositions = boxesAndRobot[currentIndex].NewPositions(directions);
        moved = [];
        List<char> features = Map.Features(newPositions);

        if (features.Any(x => x == '#'))
        {
            return false;
        }
        IEnumerable<int> nextIndexes = boxesAndRobot.IndexesOf(newPositions);

        if (nextIndexes.Count() == 0)
        {
            moved.Add((currentIndex, boxesAndRobot[currentIndex].Move(directions)));
            return true;
        }

        bool possible = true;
        foreach (var nextIndex in nextIndexes)
        {
            if (!TryMoveOb(nextIndex, directions, boxesAndRobot, out List<(int Index, Ob Ob)> m))
            {
                possible = false;
                break;
            }
            moved.AddRange(m);
        }
        if (possible)
        {
            moved.Add((currentIndex, boxesAndRobot[currentIndex].Move(directions)));
            return true;
        }

        moved = [];
        return false;

    }
    public bool TryMove(int currentIndex, char directions, List<(int X, int Y)> boxesAndRobot, out (int X, int Y) newPosition)
    {
        newPosition = boxesAndRobot[currentIndex].NewPosition(directions);
        char feature = Map[newPosition.X][newPosition.Y];

        if (Map[newPosition.X][newPosition.Y] == '#')
        {
            return false;
        }
        int nextIndex = boxesAndRobot.IndexOf(newPosition);
        if (nextIndex == -1)
        {
            boxesAndRobot[currentIndex] = newPosition;
            return true;
        }
        if (TryMove(nextIndex, directions, boxesAndRobot, out (int, int) p))
        {
            boxesAndRobot[nextIndex] = p;
            boxesAndRobot[currentIndex] = newPosition;
            return true;
        }
        return false;

    }
}

public record Ob
{
    private IEnumerable<(int X, int Y)> positions;
    private readonly int count;
    public Ob(IEnumerable<(int X, int Y)> positions)
    {
        this.positions = positions;
        count = positions.Count();
    }

    public IEnumerable<(int X, int Y)> NewPositions(char dir)
    {
        switch (dir)
        {
            case '^':
                return positions.Select(x => x.NewPosition(dir));
            case '>':
                return [positions.MaxBy(p => p.Y).NewPosition(dir)];
            case 'v':
                return positions.Select(x => x.NewPosition(dir));
            case '<':
                return [positions.MinBy(p => p.Y).NewPosition(dir)];
            default:
                throw new NotImplementedException();
        }
    }

    public Ob Move(char dir)
    {
        return new Ob(positions.Select(x => x.NewPosition(dir)));
    }

    public bool IsInMultiple()
    {
        return count > 1;
    }
    public bool HitBy((int X, int Y) p)
    {
        return positions.Contains(p);
    }
    public (int X, int Y) ClosestEdge()
    {
        return positions.MinBy(p => p.Y);
    }
}

public static class D15Extensions
{
    public static IEnumerable<int> IndexesOf(this List<Ob> boxesAndRobot, IEnumerable<(int X, int Y)> p)
    {
        List<int> indexes = new();

        var hits = boxesAndRobot.Where(b => p.Any(p => b.HitBy(p)));
        foreach (var hit in hits)
        {
            indexes.Add(boxesAndRobot.IndexOf(hit));
        }
        return indexes.Distinct();
    }
    public static List<char> Features(this List<string> input, IEnumerable<(int X, int Y)> pos)
    {
        List<char> f = new();
        foreach (var (X, Y) in pos)
        {
            f.Add(input[X][Y]);
        }
        return f;
    }
    public static (int X, int Y) NewPosition(this (int X, int Y) p, char dir)
    {
        switch (dir)
        {
            case '^':
                return (p.X - 1, p.Y);
            case '>':
                return (p.X, p.Y + 1);
            case 'v':
                return (p.X + 1, p.Y);
            case '<':
                return (p.X, p.Y - 1);
            default:
                throw new NotImplementedException();
        }
    }

    public static (List<(int X, int Y)> Boxes, int Robot) BoxesAndRobot(this List<string> input)
    {
        List<(int X, int Y)> boxesAndRobot = new();
        (int X, int Y)? robot = null;
        for (int x = 0; x < input.Count; x++)
        {
            for (int y = 0; y < input[x].Length; y++)
            {
                if (input[x][y] == 'O')
                {
                    boxesAndRobot.Add((x, y));
                }
                if (input[x][y] == '@')
                {
                    boxesAndRobot.Add((x, y));
                    robot = (x, y);
                }
            }
        }
        return (boxesAndRobot, boxesAndRobot.IndexOf(robot!.Value));
    }
    public static (List<Ob> Boxes, int Robot) BoxesAndRobotV2(this List<string> input)
    {
        List<Ob> boxesAndRobot = new();
        int indexOfRobot = 0;
        for (int x = 0; x < input.Count; x++)
        {
            for (int y = 0; y < input[x].Length; y++)
            {
                if (input[x][y] == '[')
                {
                    boxesAndRobot.Add(new Ob([(x, y), (x, y + 1)]));
                }
                if (input[x][y] == '@')
                {
                    boxesAndRobot.Add(new Ob([(x, y)]));
                    indexOfRobot = boxesAndRobot.Count - 1;
                }
            }
        }
        return (boxesAndRobot, indexOfRobot);
    }
    public static List<string> ToMap(this List<string> input)
    {
        return input.Take(input.IndexOf(string.Empty) + 1).ToList();
    }
    public static string Instructions(this List<string> input)
    {
        return string.Concat(input.Skip(input.IndexOf(string.Empty) + 1));
    }

    public static List<string> SecondWarehouse(this List<string> input)
    {
        List<string> secondWarehouse = new();
        bool instructionTime = false;
        foreach (var line in input)
        {
            string newLine = "";
            if (line == string.Empty)
            {
                instructionTime = true;
            }
            if (!instructionTime)
            {
                foreach (char feature in line)
                {
                    switch (feature)
                    {
                        case '#':
                            newLine += "##";
                            break;
                        case 'O':
                            newLine += "[]";
                            break;
                        case '.':
                            newLine += "..";
                            break;
                        case '@':
                            newLine += "@.";
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                newLine = line;
            }
            secondWarehouse.Add(newLine);
        }
        return secondWarehouse;
    }
}

