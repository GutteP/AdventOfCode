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
                allNext.Add(AoCMath.NextInSequence(seq));
            }
            return allNext.Sum();
        }
        private int PartTwo(List<List<int>> input)
        {
            List<int> allNext = new();
            foreach (var seq in input)
            {
                allNext.Add(AoCMath.PreviousInSequence(seq));
            }
            return allNext.Sum();
        }
    }
}
