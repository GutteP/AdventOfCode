namespace AoC._2024;

public class D13
{
    public long? PartOne(string inputPath)
    {
        List<((int X, int Y) A, (int X, int Y) B, (int X, int Y) Prize)> games = InputReader.ReadLines(inputPath).ToGames();
        List<List<(int A, int B)>> allSolutions = new();
        foreach (var game in games)
        {
            var solutions = game.Check(100);
            if (solutions.Count > 0)
            {
                allSolutions.Add(solutions);
            }
        }
        return allSolutions.Select(x => x.Select(s => s.A * 3 + s.B).Min()).Sum();
    }


    public long? PartTwo(string inputPath)
    {
        List<string> map = InputReader.ReadLines(inputPath);

        return 0;
    }
}

//Button A: X+42, Y+24
//Button B: X+23, Y+55
//Prize: X=18849, Y=8875

public static class D13Extensions
{
    public static List<(int A, int B)> Check(this ((int X, int Y) A, (int X, int Y) B, (int X, int Y) Prize) game, int? max)
    {
        //decimal stopXA = (decimal)game.Prize.X / (decimal)game.A.X;
        //decimal stopYA = (decimal)game.Prize.Y / (decimal)game.A.Y;
        //decimal stopXB = (decimal)game.Prize.X / (decimal)game.B.X;
        //decimal stopYB = (decimal)game.Prize.Y / (decimal)game.B.Y;
        int stopXA = game.Prize.X / game.A.X;
        int stopYA = game.Prize.Y / game.A.Y;
        //int stopXB = game.Prize.X / game.B.X;
        //int stopYB = game.Prize.Y / game.B.Y;

        //int stopMXA = game.Prize.X % game.A.X;
        //int stopMYA = game.Prize.Y % game.A.Y;
        //int stopMXB = game.Prize.X % game.B.X;
        //int stopMYB = game.Prize.Y % game.B.Y;

        int stopA = Math.Min(stopXA, stopYA);
        List<(int A, int B)> solutions = new();
        for (int i = 0; i <= stopA; i++)
        {
            int a = stopA - i;
            int diffA = game.Prize.X - (game.A.X * a);
            if (diffA == 0)
            {
                if (game.A.Y * a == game.Prize.Y)
                {
                    if (max == null || a <= max)
                    {
                        solutions.Add((a, 0));
                    }
                }
                continue;
            }

            if (diffA % game.B.X == 0)
            {
                int b = diffA / game.B.X;
                if (game.A.Y * a + b * game.B.Y == game.Prize.Y)
                {
                    if (max == null || (a <= max && b <= max))
                    {
                        solutions.Add((a, b));
                    }
                }
            }

        }
        return solutions;

    }


    public static List<((int X, int Y) A, (int X, int Y) B, (int X, int Y) Prize)> ToGames(this List<string> input)
    {
        List<((int X, int Y) A, (int X, int Y) B, (int X, int Y) Prize)> games = new();
        (int X, int Y) a = (0, 0);
        (int X, int Y) b = (0, 0);
        (int X, int Y) prize = (0, 0);
        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                games.Add((a, b, prize));
                continue;
            }
            string[] parts = line.Split(" ");
            if (parts[1] == "A:")
            {
                a = (int.Parse(parts[2].Split('+')[1].Trim(',')), int.Parse(parts[3].Split('+')[1]));
            }
            else if (parts[1] == "B:")
            {
                b = (int.Parse(parts[2].Split('+')[1].Trim(',')), int.Parse(parts[3].Split('+')[1]));
            }
            else
            {
                prize = (int.Parse(parts[1].Split('=')[1].Trim(',')), int.Parse(parts[2].Split('=')[1]));
            }
        }
        games.Add((a, b, prize));
        return games;
    }
}

