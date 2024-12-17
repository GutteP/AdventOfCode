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
            if(TryMove(robotIndex, instruction, boxesAndRobot, out (int, int) p))
            {
                robotIndex = boxesAndRobot.IndexOf(p);
            }
        }
        boxesAndRobot.RemoveAt(robotIndex);
        return boxesAndRobot.Select(b => b.X * 100 + b.Y).Sum();
    }


    public long? PartTwo(string inputPath)
    {
        return 0;
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
        if(TryMove(nextIndex, directions, boxesAndRobot, out (int, int) p))
        {
            boxesAndRobot[nextIndex] = p;
            boxesAndRobot[currentIndex] = newPosition;
            return true;
        }
        return false;
        
    }
}

public static class D15Extensions
{
    public static (int X, int Y) NewPosition(this (int X, int Y) p, char dir)
    {
        switch (dir)
        {
            case '^':
                return (p.X - 1, p.Y);
            case '>':
                return (p.X, p.Y+1);
            case 'v':
                return (p.X + 1, p.Y);
            case '<':
                return (p.X, p.Y-1);
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
                if(input[x][y] == 'O')
                {
                    boxesAndRobot.Add((x, y));
                }
                if(input[x][y] == '@')
                {
                    boxesAndRobot.Add((x, y));
                    robot = (x, y);
                }
            }
        }
        return (boxesAndRobot, boxesAndRobot.IndexOf(robot!.Value));
    }
    public static List<string> ToMap(this List<string> input)
    {
        return input.Take(input.IndexOf(string.Empty) + 1).ToList();
    }
    public static string Instructions(this List<string> input)
    {
        return string.Concat(input.Skip(input.IndexOf(string.Empty) + 1));
    }
}

