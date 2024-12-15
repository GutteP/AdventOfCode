namespace AoC._2024;

public class D13
{
    public long? PartOne(string inputPath)
    {
        List<((int X, int Y) A, (int X, int Y) B, (long X, long Y) Prize)> games = InputReader.ReadLines(inputPath).ToGames();
        List<List<(long A, long B)>> allSolutions = new();
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
        List<((int X, int Y) A, (int X, int Y) B, (long X, long Y) Prize)> games = InputReader.ReadLines(inputPath).ToGames(partTwo: true);
        List<List<(long A, long B)>> allSolutions = new();
        foreach (var game in games)
        {
            var solutions = game.Check(null);
            if (solutions.Count > 0)
            {
                allSolutions.Add(solutions);
            }
        }
        return allSolutions.Select(x => x.Select(s => s.A * 3 + s.B).Min()).Sum();
    }
}

public static class D13Extensions
{
    public static List<(long A, long B)> Check(this ((int X, int Y) A, (int X, int Y) B, (long X, long Y) Prize) game, int? max)
    {
        long stopXA = game.Prize.X / game.A.X;
        long stopYA = game.Prize.Y / game.A.Y;

        long stopA = Math.Min(stopXA, stopYA);
        List<(long A, long B)> solutions = new();
        for (long i = 0; i <= stopA; i++)
        {
            long a = stopA - i;
            long diffA = game.Prize.X - (game.A.X * a);

            if (diffA % game.B.X == 0)
            {
                long b = diffA / game.B.X;
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

    public static List<((int X, int Y) A, (int X, int Y) B, (long X, long Y) Prize)> ToGames(this List<string> input, bool partTwo = false)
    {
        List<((int X, int Y) A, (int X, int Y) B, (long X, long Y) Prize)> games = new();
        (int X, int Y) a = (0, 0);
        (int X, int Y) b = (0, 0);
        (long X, long Y) prize = (0, 0);
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
                if (partTwo)
                {
                    prize.X += 10000000000000;
                    prize.Y += 10000000000000;
                }
            }
        }
        games.Add((a, b, prize));
        return games;
    }
}

