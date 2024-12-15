namespace AoC._2024;

public class D14
{
    public long? PartOne(string inputPath, int xSize, int ySize)
    {
        List<BRRobot> robots = InputReader.ReadLines(inputPath).MakeRobots(xSize, ySize);
        for (int i = 0; i < 100; i++)
        {
            robots.Step();
        }
        long safetyFactor = robots.CountAndMultiplyQuadrants(xSize, ySize);
        return safetyFactor;
    }


    public long? PartTwo(string inputPath, int xSize, int ySize)
    {
        // Se AoC.2024.14.2 projektet för lösning...
        return 0;
    }
}

//p=0,4 v=3,-3

public record BRRobot
{
    public int X { get; set; }
    public int Y { get; set; }
    public int VX { get; set; }
    public int VY { get; set; }

    public int XSize { get; set; }
    public int YSize { get; set; }


    public BRRobot(string input, int xSize, int ySize)
    {

        string[] s1 = input.Split(' ');
        string[] s2 = s1[0].Split('=');
        string[] s3 = s2[1].Split(',');
        X = int.Parse(s3[0]);
        Y = int.Parse(s3[1]);
        string[] s4 = s1[1].Split('=');
        string[] s5 = s4[1].Split(',');
        VX = int.Parse(s5[0]);
        VY = int.Parse(s5[1]);
        XSize = xSize;
        YSize = ySize;
    }

    public void Step()
    {
        X += VX;
        while (X < 0 || X >= XSize)
        {
            if (X < 0)
            {
                X = XSize + X;
            }
            else
            {
                X = X - XSize;
            }
        }

        Y += VY;
        while (Y < 0 || Y >= YSize)
        {
            if (Y < 0)
            {
                Y = YSize + Y;
            }
            else
            {
                Y = Y - YSize;
            }
        }
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}

public static class D14Extensions
{
    public static long CountAndMultiplyQuadrants(this List<BRRobot> robots, int xSize, int ySize)
    {
        int xHalf = xSize / 2;
        int yHalf = ySize / 2;

        long ul = robots.Where(r => r.X < xHalf && r.Y < yHalf).Count();
        long ur = robots.Where(r => r.X > xHalf && r.Y < yHalf).Count();
        long ll = robots.Where(r => r.X < xHalf && r.Y > yHalf).Count();
        long lr = robots.Where(r => r.X > xHalf && r.Y > yHalf).Count();

        return ul * ur * ll * lr;
    }

    public static void Step(this List<BRRobot> robots)
    {
        foreach (BRRobot robot in robots)
        {
            robot.Step();
        }
    }

    public static List<BRRobot> MakeRobots(this List<string> input, int xSize, int ySize)
    {
        List<BRRobot> robots = new();
        foreach (string s in input)
        {
            robots.Add(new BRRobot(s, xSize, ySize));
        }
        return robots;
    }

    public static void Print(this List<BRRobot> robots, int xSize, int ySize)
    {
        Console.Clear();
        for (int y = 0; y < ySize; y++)
        {
            string row = "";
            for (int x = 0; x < xSize; x++)
            {
                if (robots.Any(r => r.X == x && r.Y == y))
                {
                    row += "#";
                }
                else
                {
                    row += ".";
                }
            }
            Console.WriteLine(row);
        }
    }
}

