namespace AoC._2022.TestDay1;

public class SleighKeysFinder : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<int[], int, int>(Transformer, NumberOfLarger, 1), new Runner<int[], int, int>(Transformer, NumberOfLarger, 3));
    }

    private int[] Transformer(string path)
    {
        return InputReader.ReadLines(path).Trim().ToIntList().ToArray();
    }

    private int NumberOfLarger(int[] depths, int window = 1)
    {
        int sum = 0;
        for (int i = 0; i < depths.Length - window; i++)
        {
            var ra = depths[i..(i + window)].Sum();
            if (Larger(depths[i..(i + window)].Sum(), depths[(i + 1)..(i + window + 1)].Sum())) sum += 1;
        }
        return sum;
    }
    private bool Larger(int a, int b)
    {
        return b > a;
    }
}



