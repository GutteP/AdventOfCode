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

        List<(long A, long B)> allSolutions = new();
        foreach (var game in games)
        {
            if (game.TryToCalculate(out (long X, long Y) solution))
            {
                allSolutions.Add(solution);
            }
        }
        return allSolutions.Select(s => s.A * 3 + s.B).Sum();
    }
}

public static class D13Extensions
{
    public static bool TryToCalculate(this ((int X, int Y) A, (int X, int Y) B, (long X, long Y) Prize) game, out (long X, long Y) solution)
    {
        // Button A: X + 26, Y + 66                         ===> ax, ay
        // Button B: X + 67, Y + 21                         ===> bx, by
        // Prize: X = 10000000012748, Y = 10000000012176    ===> px, py
        // y = ((px * ay)-(py*ax))/((bx*ay)-(by*ax))
        // x = (px - (bx*y))/ax

        double y = ((game.Prize.X * game.A.Y) - (game.Prize.Y * game.A.X)) / ((game.B.X * game.A.Y) - (game.B.Y * game.A.X));
        double x = (game.Prize.X - (game.B.X * y)) / game.A.X;

        solution = ((long)x, (long)y);

        return y % 1 == 0 && x % 1 == 0 && IsCorrect(game, solution);

        // Simultaneous equations by elimination
        // https://thirdspacelearning.com/gcse-maths/algebra/simultaneous-equations/
        //
        // Button A: X + 26, Y + 66                         ===> ax, ay
        // Button B: X + 67, Y + 21                         ===> bx, by
        // Prize: X = 10000000012748, Y = 10000000012176    ===> px, py
        // 
        // x* ax + y * bx = px
        // x* ay + y * by = py
        // 
        // Ställ upp
        // A) 26x + 67y = 10000000012748
        // B) 66x + 21y = 10000000012176
        // 
        // Multiplicera A med ay och B med ax
        // (26x * 66) +(67y * 66) = (10000000012748 * 66)
        // (66x * 26 + (21y * 26) = (10000000012176 * 26)
        //  
        // Då får vi:
        // 1716x + 4422y = 660000000841368
        // 1716x + 546y = 260000000316576
        //  
        // Förenkla:
        // (1716x - 1716x) +(4422y - 546y) = (660000000841368 - 260000000316576)
        // 0 + 3876y = 400000000524792
        // 3876y = 400000000524792
        //  
        // Ta fram y
        // y = 400000000524792 / 3876
        // y = 103199174542
        //  
        // Vi utgick från:
        // 26x + 67y = 10000000012748
        // 66x + 21y = 10000000012176
        //   
        // Sätt in y:
        // 26x + 67 * 103199174542 = 10000000012748
        // 66x + 21 * 103199174542 = 10000000012176
        //   
        // Då får vi:
        // 26x + 6914344694314 = 10000000012748
        // 66x + 2167182665382 = 10000000012176
        // 
        // Förenkla:
        // 26x = 10000000012748 - 6914344694314
        // 26x = 3085655318434
        // x = 3085655318434 / 26
        // x = 118679050709

        // x = 118679050709
        // y = 103199174542
    }

    public static bool IsCorrect(this ((int X, int Y) A, (int X, int Y) B, (long X, long Y) Prize) game, (long X, long Y) solution)
    {
        long tpx = game.A.X * solution.X + game.B.X * solution.Y;
        long tpy = game.A.Y * solution.X + game.B.Y * solution.Y;
        if (tpx == game.Prize.X && tpy == game.Prize.Y)
        {
            return true;
        }
        return false;
    }

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

