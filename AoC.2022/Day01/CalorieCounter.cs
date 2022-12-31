namespace AoC._2022.Day01;

public class CalorieCounter : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<List<int>, int, int>(Transformer, Solver, 1), new Runner<List<int>, int, int>(Transformer, Solver, 3));
    }

    private List<int> Transformer(string path)
    {
        List<int> result = new();

        List<int> nisse = new();
        foreach (string row in InputReader.ReadLines(path))
        {
            if (string.IsNullOrEmpty(row))
            {
                result.Add(nisse.Sum());
                nisse = new();
                continue;
            }
            nisse.Add(int.Parse(row));
        }
        if (nisse.Any()) result.Add(nisse.Sum());

        return result;
    }

    private int Solver(List<int> input, int numberToCount)
    {
        return input.OrderByDescending(x => x).ToArray()[0..numberToCount].ToList().Sum();
    }
}
