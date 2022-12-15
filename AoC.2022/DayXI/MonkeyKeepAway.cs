namespace AoC._2022.DayXI;

public class MonkeyKeepAway : IAoCDay<double>
{
    public DayRunner<double> Runner()
    {
        return new DayRunner<double>(new Runner<List<Monkey>, int, bool, double>(Transformer, MonkeyBusiness, 20, true), new Runner<List<Monkey>, int, bool, double>(Transformer, MonkeyBusiness, 10000, false));
    }

    private List<Monkey> Transformer(string path)
    {
        var rawMonkeys = InputReader.ReadLines(path);
        List<Monkey> monkeys = new();
        for (int i = 0; i < rawMonkeys.Count; i += 7)
        {
            Monkey monkey = new()
            {
                Id = int.Parse(rawMonkeys[i].SplitOn(Seperator.Space)[1][0].ToString()),
                Items = rawMonkeys[i + 1].Trim().Split(' ')[2..].ToList().Trim(',').ToDouble(),
                Operation = rawMonkeys[i + 2].Trim().SplitOn(Seperator.Space)[4] == "*" ? OperationMulti : OperationAdd,
                OperationVar = rawMonkeys[i + 2].Trim().SplitOn(Seperator.Space)[5] == "old" ? null : double.Parse(rawMonkeys[i + 2].Trim().SplitOn(Seperator.Space)[5]),
                Test = int.Parse(rawMonkeys[i + 3].Trim().SplitOn(Seperator.Space)[3]),
                IfTrue = int.Parse(rawMonkeys[i + 4].Trim().SplitOn(Seperator.Space)[5]),
                IfFalse = int.Parse(rawMonkeys[i + 5].Trim().SplitOn(Seperator.Space)[5]),
                InspectCount = 0
            };
            monkeys.Add(monkey);
        }
        return monkeys;
    }

    private double OperationMulti(double x, double factor)
    {
        return x * factor;
        //return factor;
        //return (int)(Math.Sqrt(x) * Math.Sqrt(factor));
    }
    private double OperationAdd(double x, double add)
    {
        return x + add;
    }

    private double MonkeyBusiness(List<Monkey> monkeys, int times, bool manageableWorryLevel)
    {
        List<int> inspections = new() { 1, 20, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000 };
        List<(double, double, double, double)> inspectionR = new();
        for (int i = 0; i < times; i++)
        {
            if (inspections.Contains(i))
            {
                inspectionR.Add((monkeys[0].InspectCount, monkeys[1].InspectCount, monkeys[2].InspectCount, monkeys[3].InspectCount));
            }
            for (int monkeyId = 0; monkeyId < monkeys.Count; monkeyId++)
            {
                var trows = monkeys[monkeyId].Turn(manageableWorryLevel);
                foreach ((int Id, int Item) t in trows)
                {
                    monkeys[t.Id].Items.Add(t.Item);
                }
            }

        }
        monkeys = monkeys.OrderByDescending(x => x.InspectCount).ToList();
        var r = monkeys[0].InspectCount * monkeys[1].InspectCount;
        return r;
    }
}

public class Monkey
{
    public int Id { get; set; }
    public List<double> Items { get; set; }
    public Func<double, double, double> Operation { get; set; }
    public double? OperationVar { get; set; }
    public int Test { get; set; }
    public int IfTrue { get; set; }
    public int IfFalse { get; set; }
    public double InspectCount { get; set; }

    public List<(int to, double item)> Turn(bool manageableWorryLevel)
    {
        List<(int to, double item)> throws = new();
        int c = Items.Count;
        while (Items.Any())
        {
            InspectCount++;
            double f = OperationVar.HasValue ? OperationVar.Value : Items[0];
            double mItem = Operation(Items[0], f);
            if (manageableWorryLevel) mItem = Math.Floor(mItem / 3);

            if ((int)mItem % Test == 0) throws.Add((IfTrue, mItem));
            else throws.Add((IfFalse, mItem));
            Items.RemoveAt(0);
        }
        if (throws.Count != c)
        {

        }
        return throws;
    }
}
