namespace AoC._2023._09
{
    public class MirageMaintenance : IAoCDay<int>
    {
        public DayRunner<int> Runner()
        {
            return new DayRunner<int>(new Runner<List<List<int>>, int>(Transformer, PartOne), new Runner<List<List<int>>, int>(Transformer, PartTwo));
        }

        private List<List<int>> Transformer(string path)
        {
            List<List<int>> readings = new();
            foreach (var line in InputReader.ReadLines(path))
            {
                readings.Add(line.SplitOn(Seperator.Space).Select(x => int.Parse(x)).ToList());
            }
            return readings;
        }

        private int PartOne(List<List<int>> input)
        {
            List<int> allNext = new();
            foreach (var seq in input)
            {
                allNext.Add(NextValue(seq));
            }
            return allNext.Sum();
        }
        private int PartTwo(List<List<int>> input)
        {
            List<int> allNext = new();
            foreach (var seq in input)
            {
                allNext.Add(PreviousValue(seq));
            }
            return allNext.Sum();
        }

        public int NextValue(List<int> seq)
        {
            List<List<int>> work = AddDiff(seq);
            for (int i = work.Count - 2; i >= 0; i--)
            {
                work[i].Add(work[i][work[i].Count - 1] + work[i + 1].Last());
            }
            return work[0].Last();
        }
        public int PreviousValue(List<int> seq)
        {
            List<List<int>> work = AddDiff(seq);
            for (int i = work.Count - 2; i >= 0; i--)
            {
                int prev = work[i][0] - work[i + 1][0];
                work[i].Insert(0, prev);
            }
            return work[0].First();
        }
        private List<List<int>> AddDiff(List<int> seq)
        {
            List<List<int>> work = new() { seq };
            while (!work.Last().All(x => x == 0))
            {
                List<int> diff = new();
                for (int i = 0; i < work.Last().Count - 1; i++)
                {
                    diff.Add(work.Last()[i + 1] - work.Last()[i]);
                }
                work.Add(diff);
            }
            return work;
        }
    }
}
