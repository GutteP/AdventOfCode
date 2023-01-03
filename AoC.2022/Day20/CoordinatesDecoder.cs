namespace AoC._2022.Day20
{
    public class CoordinatesDecoder : IAoCDay<long>
    {
        public DayRunner<long> Runner()
        {
            return new DayRunner<long>(new Runner<List<int>, long>(Transformer, SimpleDecoder), new Runner<List<int>, long>(Transformer, AdvancedDecoder));
        }

        private List<int> Transformer(string path)
        {
            return InputReader.ReadLines(path).ToIntList();
        }

        private long SimpleDecoder(List<int> numbers)
        {
            List<(long num, int org)> ledger = CreateLedger(numbers, 1);

            ledger = Mix(ledger);

            long result = FindCoordinates(ledger);
            return result;
        }

        private long AdvancedDecoder(List<int> numbers)
        {
            List<(long num, int org)> ledger = CreateLedger(numbers, 811589153);

            for (int i = 0; i < 10; i++)
            {
                ledger = Mix(ledger);
            }

            long result = FindCoordinates(ledger);
            return result;
        }

        private List<(long num, int org)> Mix(List<(long num, int org)> ledger)
        {
            for (int i = 0; i < ledger.Count; i++)
            {
                int index = ledger.IndexOf(ledger.Where(x => x.org == i).First());
                var num = ledger[index];
                ledger.RemoveAt(index);
                long newIndex = index + num.num;
                if (newIndex == ledger.Count)
                {
                    ledger.Add(num);
                }
                else
                {
                    ledger.Insert(newIndex.LoopAround(ledger.Count), num);
                }
            }
            return ledger;
        }

        private List<(long num, int org)> CreateLedger(List<int> numbers, long multiplier)
        {
            List<(long num, int org)> ledger = new();
            for (int i = 0; i < numbers.Count; i++)
            {
                ledger.Add((numbers[i] * multiplier, i));
            }
            return ledger;
        }

        public long FindCoordinates(List<(long num, int org)> ledger)
        {
            List<long> mixedCoordinates = new();
            foreach (var num in ledger)
            {
                mixedCoordinates.Add(num.num);
            }

            int index = mixedCoordinates.IndexOf(0) + 1000;
            while (index > mixedCoordinates.Count - 1)
            {
                index -= mixedCoordinates.Count;
            }
            long r0 = mixedCoordinates[index];
            index += 1000;
            while (index > mixedCoordinates.Count - 1)
            {
                index -= mixedCoordinates.Count;
            }
            long r1 = mixedCoordinates[index];
            index += 1000;
            while (index > mixedCoordinates.Count - 1)
            {
                index -= mixedCoordinates.Count;
            }
            long r2 = mixedCoordinates[index];
            return r0 + r1 + r2;
        }
    }
}
