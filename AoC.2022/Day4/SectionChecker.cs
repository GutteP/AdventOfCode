namespace AoC._2022.Day4;

public class SectionChecker : IAoCDay
{
    public DayRunner Runner()
    {
        return new DayRunner(new Runner<List<(List<int> a, List<int> b)>>(Transformer, TotalOverlap), new Runner<List<(List<int> a, List<int> b)>>(Transformer, AnyOverlap));
    }

    private List<(List<int> a, List<int> b)> Transformer(string path)
    {
        List<(List<int> a, List<int> b)> result = new();
        foreach (List<string> pair in InputReader.ReadLines(path).Trim().SplitOn(Seperator.Comma))
        {
            int aFrom = int.Parse(pair[0].SplitOn(Seperator.Dash)[0]);
            int aTo = int.Parse(pair[0].SplitOn(Seperator.Dash)[1]);

            int bFrom = int.Parse(pair[1].SplitOn(Seperator.Dash)[0]);
            int bTo = int.Parse(pair[1].SplitOn(Seperator.Dash)[1]);

            result.Add((Enumerable.Range(aFrom, aTo - aFrom + 1).ToList(), Enumerable.Range(bFrom, bTo - bFrom + 1).ToList()));
        }
        return result;
    }

    private int TotalOverlap(List<(List<int> a, List<int> b)> input)
    {
        int totalOverlap = 0;
        foreach (var pair in input)
        {
            int unionCount = pair.a.Union(pair.b).Count();
            if (unionCount == pair.a.Count || unionCount == pair.b.Count) totalOverlap++;
        }
        return totalOverlap;
    }
    private int AnyOverlap(List<(List<int> a, List<int> b)> input)
    {
        int anyOverlap = 0;
        foreach (var pair in input)
        {
            int unionCount = pair.a.Union(pair.b).Count();
            if (unionCount != pair.a.Count + pair.b.Count) anyOverlap++;
        }
        return anyOverlap;
    }
}
