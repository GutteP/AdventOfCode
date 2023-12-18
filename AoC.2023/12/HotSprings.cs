using System.IO;
using System.Reflection.Metadata.Ecma335;

namespace AoC._2023._12;

public class HotSprings : IAoCDay<long>
{
    public DayRunner<long> Runner()
    {
        return new DayRunner<long>(new Runner<List<string>, long>(Transformer, PartOne), new Runner<List<string>, long>(Transformer, PartTwo));
    }

    private List<string> Transformer(string path)
    {
        return InputReader.ReadLines(path);
    }

    private long PartOne(List<string> input)
    {
        List<SpringCondition> conditions = new();
        foreach (var row in input)
        {
            var sp = row.Split(' ');
            List<int> checksum = sp[1].Split(',').Select(int.Parse).ToList();
            conditions.Add(new(checksum, sp[0]));
        }
        Parallel.ForEach(conditions, condition =>
        {
            condition.CalculateValids();
        });
        return conditions.Sum(x => x.Valids);
    }
    private long PartTwo(List<string> input)
    {
        List<SpringCondition> conditions = new();
        foreach (var row in input)
        {

            var sp = row.Split(' ');
            string springs = "";
            string checksumString = "";
            for (int i = 0; i < 5; i++)
            {
                springs += sp[0];
                checksumString += sp[1];
                if (i != 4)
                {
                    springs += "?";
                    checksumString += ",";
                }
            }
            List<int> checksum = checksumString.Split(',').Select(int.Parse).ToList();
            conditions.Add(new(checksum, springs));
        }

        Parallel.ForEach(conditions, new ParallelOptions { MaxDegreeOfParallelism = 23 }, condition =>
        {
            condition.CalculateValids();
        });
        return conditions.Sum(x => x.Valids);
    }

    public class SpringCondition
    {
        private Dictionary<string, long> _dict;
        public SpringCondition(List<int> checksum, string springs)
        {
            Checksum = checksum;
            Springs = springs;
            _dict = new();
        }

        public void CalculateValids()
        {
            Valids = RValids(Springs, Checksum, 0, Checksum.Sum());
        }

        public long RValids(string str, List<int> checksum, int startFrom, int totalSum)
        {
            int si = startFrom - 1 < 0 ? 0 : startFrom - 1;
            string key = $"{str[(si)..]};{string.Join(',', checksum)};{startFrom};{str.Count(x => x == '#')}";
            if (!_dict.TryGetValue(key, out long knownSum))
            {
                long sum = 0;
                for (int i = startFrom; i + (checksum[0] - 1) < str.Length;)
                {
                    int firstValid = IndexOfValidPosition(str, checksum[0], i);
                    if (firstValid == -1) return sum;
                    bool exactMatch = IsExactMatch(str, firstValid, checksum[0]);
                    i = firstValid + 1;
                    string newStr = InsertInstring(str, firstValid, checksum[0]);
                    if (newStr.Count(x => x == '#') > totalSum) continue;
                    List<int> newChecksum = checksum[1..];
                    if (newChecksum.Count == 0)
                    {
                        sum++;
                        continue;
                    }
                    sum += RValids(newStr, newChecksum, firstValid + checksum[0], totalSum);
                    if (exactMatch) break;
                }
                if (sum > 0) _dict.Add(key, sum);
                return sum;
            }
            return knownSum;
        }

        private bool IsExactMatch(string str, int firstValid, int v)
        {
            return str.Substring(firstValid, v).All(x => x == '#');
        }

        public int IndexOfValidPosition(string s, int lengthToPlace, int startFrom = 0)
        {
            for (int i = startFrom; i < s.Length; i++)
            {
                if (i != 0 && s[i - 1] == '#') continue;
                if (i + lengthToPlace > s.Length) continue;
                if (s[i..(i + lengthToPlace)].Contains('.')) continue;
                if (i + lengthToPlace < s.Length && s[i + lengthToPlace] == '#') continue;
                return i;
            }
            return -1;
        }
        public string InsertInstring(string s, int index, int length)
        {
            var list = s.ToList();
            for (int i = 0; i < length; i++)
            {
                list[(index + i)] = '#';
            }
            return string.Concat(list);
        }

        public List<int> Checksum { get; set; }
        public string Springs { get; set; }
        public long Valids { get; set; }
        public long ValidSquare { get; set; }
    }
}
