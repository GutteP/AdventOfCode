namespace AoC._2022.Day02;

public class RPSScoreCalculator : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<List<List<string>>, int>(Transformer, ScoreCounter), new Runner<List<List<string>>, int>(Transformer, StrategyScoreCounter));
    }

    private List<List<string>> Transformer(string path)
    {
        return InputReader.ReadLines(path).SplitOn(Seperator.Space).Trim();
    }

    private int ScoreCounter(List<List<string>> input)
    {
        int score = 0;
        foreach (List<string> match in input)
        {
            RPS rps = match[1].ToRPS();
            score += (int)rps;

            RPS winner = rps.Play(match[0].ToRPS());
            if (winner == RPS.None) score += 3;
            else if (winner == rps) score += 6;
        }
        return score;
    }

    private int StrategyScoreCounter(List<List<string>> input)
    {
        int score = 0;
        foreach (List<string> match in input)
        {
            if (match[1] == "X") score += 0;
            if (match[1] == "Y") score += 3;
            if (match[1] == "Z") score += 6;
            score += (int)match[0].ToRPS().Strategy(match[1]);
        }
        return score;
    }
}

public static class RPSPlayer
{
    public static RPS Strategy(this RPS opponent, string desiredResult)
    {
        if (desiredResult == "Y") return opponent;
        if (desiredResult == "Z")
        {
            if (opponent == RPS.Rock) return RPS.Paper;
            if (opponent == RPS.Paper) return RPS.Scissors;
            if (opponent == RPS.Scissors) return RPS.Rock;
        }
        if (opponent == RPS.Rock) return RPS.Scissors;
        if (opponent == RPS.Paper) return RPS.Rock;
        return RPS.Paper;
    }
    public static RPS ToRPS(this string a)
    {
        switch (a)
        {
            case "A": return RPS.Rock;
            case "B": return RPS.Paper;
            case "C": return RPS.Scissors;
            case "X": return RPS.Rock;
            case "Y": return RPS.Paper;
            case "Z": return RPS.Scissors;

            default:
                throw new NotImplementedException();
        }
    }
    public static RPS Play(this RPS a, RPS b)
    {
        if (a == RPS.Rock && b == RPS.Rock) return RPS.None;
        if (a == RPS.Rock && b == RPS.Paper) return RPS.Paper;
        if (a == RPS.Rock && b == RPS.Scissors) return RPS.Rock;
        if (a == RPS.Paper && b == RPS.Rock) return RPS.Paper;
        if (a == RPS.Paper && b == RPS.Paper) return RPS.None;
        if (a == RPS.Paper && b == RPS.Scissors) return RPS.Scissors;
        if (a == RPS.Scissors && b == RPS.Rock) return RPS.Rock;
        if (a == RPS.Scissors && b == RPS.Paper) return RPS.Scissors;
        if (a == RPS.Scissors && b == RPS.Scissors) return RPS.None;
        throw new NotImplementedException();
    }
}

public enum RPS
{
    None = 0,
    Rock = 1,
    Paper = 2,
    Scissors = 3
}
