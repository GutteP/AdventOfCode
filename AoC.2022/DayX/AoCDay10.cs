namespace AoC._2022.DayX
{
    public class AoCDay10 : IAoCDay<int>
    {
        public DayRunner<int> Runner()
        {
            return new DayRunner<int>(new Runner<List<string>, int>(Transformer, Solve), null);
        }

        private List<string> Transformer(string path)
        {
            return InputReader.ReadLines(path);
        }

        private int Solve(List<string> input)
        {


            return 1;
        }
    }
}
