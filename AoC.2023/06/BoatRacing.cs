using System.Diagnostics;

namespace AoC._2023._06
{
    public class BoatRacing : IAoCDay<long>
    {
        public DayRunner<long> Runner()
        {
            return new DayRunner<long>(new Runner<List<(int time, int distance)>, long>(Transformer, PartOne), new Runner<List<(int time, int distance)>, long>(Transformer, PartTwo));
        }

        private List<(int time, int distance)> Transformer(string path)
        {
            var input = InputReader.ReadLines(path);
            List<int> times = input[0].SplitOn(Seperator.Space).Where(x => int.TryParse(x, out int v)).Select(x => int.Parse(x)).ToList();
            List<int> distances = input[1].SplitOn(Seperator.Space).Where(x => int.TryParse(x, out int v)).Select(x => int.Parse(x)).ToList();

            List<(int time, int distance)> races = new();
            for (int i = 0; i < times.Count; i++)
            {
                races.Add((times[i], distances[i]));
            }
            return races;
        }

        private long PartOne(List<(int time, int distance)> races)
        {
            List<int> winningHoldTimes = new();

            foreach (var race in races)
            {
                int num = 0;
                for (int i = 1; i <= race.time; i++)
                {
                    if ((race.time - i) * i > race.distance) num++;
                    else if (num > 0) break;
                }
                winningHoldTimes.Add(num);
            }

            return winningHoldTimes.Product();
        }
        private long PartTwo(List<(int time, int distance)> races)
        {
            long time = long.Parse(string.Concat(races.Select(x => x.time.ToString())));
            long distance = long.Parse(string.Concat(races.Select(x => x.distance.ToString())));

            long num = 0;
            for (long i = 1; i <= time; i++)
            {
                if ((time - i) * i > distance) num++;
                else if (num > 0) break;
            }

            return num;
        }
    }
}
