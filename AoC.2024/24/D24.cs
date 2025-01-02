namespace AoC._2024;

public class D24
{
    public long PartOne(string inputPath)
    {
        (List<IGate> gates, Queue<(string To, bool Value)> queue) = InputReader.ReadLines(inputPath).ToGatesAndConnections();

        List<(string To, bool Value)> zGates = new();
        while (queue.Count > 0)
        {
            var (to, value) = queue.Dequeue();
            if (to.StartsWith('z'))
            {
                zGates.Add((to, value));
                continue;
            }

            var gate = gates.Where(x => x.Inputs.Contains(to));
            foreach (var g in gate)
            {
                g.Set(value);
                if (g.Ready)
                {
                    queue.Enqueue(g.Output());
                }
            }
        }
        return Convert.ToInt64(string.Concat(zGates.OrderByDescending(x => x.To).Select(x => x.Value ? 1 : 0)), 2);
    }

    public string? PartTwo(string inputPath)
    {
        (List<IGate> gates, Queue<(string To, bool Value)> queue) = InputReader.ReadLines(inputPath).ToGatesAndConnections();

        int numberOfBitesInZ = gates.Where(x => x.OutputName.StartsWith('z')).Count();
        int xyCount = queue.Count / 2;
        List<string> results = new();
        List<string> correctResults = new();

        for (int i = 0; i < 100; i++)
        {
            var shouldBe = "";
            while (shouldBe.Length != numberOfBitesInZ)
            {
                queue = D24Extensions.RandomXY(xyCount);
                (long x, long y, long sum) = queue.FindXYAndSum();
                shouldBe = Convert.ToString(sum, 2);
            }

            List<(string To, bool Value)> zGates = new();
            while (queue.Count > 0)
            {
                var (to, value) = queue.Dequeue();
                if (to.StartsWith('z'))
                {
                    zGates.Add((to, value));
                    continue;
                }

                var gate = gates.Where(x => x.Inputs.Contains(to));
                foreach (var g in gate)
                {
                    g.Set(value);
                    if (g.Ready)
                    {
                        queue.Enqueue(g.Output());
                    }
                }
            }
            string con = string.Concat(zGates.OrderByDescending(x => x.To).Select(x => x.Value ? 1 : 0));

            results.Add(con);
            correctResults.Add(shouldBe);
        }

        HashSet<int> errorIndexes = new();

        for (int i = 0; i < correctResults.Count; i++)
        {
            for (int j = 0; j < correctResults[i].Length; j++)
            {
                if (correctResults[i][j] != results[i][j])
                {
                    errorIndexes.Add(j);
                }
            }
        }

        return null;
    }
}

public static class D24Extensions
{
    public static Queue<(string To, bool Value)> RandomXY(int bitLength)
    {
        Random random = new();
        Queue<(string To, bool Value)> queue = new();
        for (int i = 0; i < bitLength; i++)
        {
            queue.Enqueue(($"x{i.ToString().PadLeft(2, '0')}", random.Next(0, 2) == 1));
            queue.Enqueue(($"y{i.ToString().PadLeft(2, '0')}", random.Next(0, 2) == 1));
        }
        return queue;
    }
    public static (long x, long y, long sum) FindXYAndSum(this IEnumerable<(string To, bool Value)> queue)
    {
        List<(string To, bool Value)> xGates = new();
        List<(string To, bool Value)> yGates = new();
        foreach (var (to, value) in queue)
        {
            if (to.StartsWith('x'))
            {
                xGates.Add((to, value));
            }
            else if (to.StartsWith('y'))
            {
                yGates.Add((to, value));
            }
        }
        long x = Convert.ToInt64(string.Concat(xGates.OrderByDescending(x => x.To).Select(x => x.Value ? 1 : 0)), 2);
        long y = Convert.ToInt64(string.Concat(yGates.OrderByDescending(x => x.To).Select(x => x.Value ? 1 : 0)), 2);
        long sum = x + y;
        return (x, y, sum);
    }
    public static (List<IGate> Gates, Queue<(string To, bool Value)> Queue) ToGatesAndConnections(this List<string> input)
    {
        List<IGate> gates = new();
        Queue<(string To, bool value)> queue = new();
        bool isGate = false;
        foreach (var row in input)
        {
            if (string.IsNullOrEmpty(row))
            {
                isGate = true;
                continue;
            }
            if (isGate)
            {
                var parts = row.Split(' ');
                switch (parts[1])
                {
                    case "AND":
                        gates.Add(new ANDGate([parts[0], parts[2]], parts[^1]));
                        break;
                    case "OR":
                        gates.Add(new ORGate([parts[0], parts[2]], parts[^1]));
                        break;
                    case "XOR":
                        gates.Add(new XORGate([parts[0], parts[2]], parts[^1]));
                        break;
                    default:
                        throw new Exception("Unknown gate");
                }
            }
            else
            {
                var parts = row.Split(": ");
                queue.Enqueue((parts[0], parts[1] == "1"));
            }
        }
        return (gates, queue);
    }
}

public interface IGate
{
    List<string> Inputs { get; }
    public bool? A { get; }
    public bool? B { get; }
    string OutputName { get; }

    public bool Ready { get; }
    public void Set(bool x);
    public (string, bool) Output();

}
public struct ANDGate : IGate
{
    public ANDGate(List<string> inputs, string outputName)
    {
        Inputs = inputs;
        OutputName = outputName;
    }
    public ANDGate(List<string> inputs, string outputName, bool a, bool b)
    {
        Inputs = inputs;
        OutputName = outputName;
        A = a;
        B = b;
    }
    public bool? A { get; private set; }

    public bool? B { get; private set; }
    public bool Ready
    {
        get { return A != null && B != null; }
    }

    public List<string> Inputs { get; }
    public string OutputName { get; }
    public (string, bool) Output()
    {
        if (!Ready) throw new Exception("Not ready");
        bool result = A!.Value & B!.Value;
        A = null;
        B = null;
        return (OutputName, result);
    }

    public void Set(bool x)
    {
        if (A == null) A = x;
        else if (B == null) B = x;
        else throw new Exception("Already set");
    }

    public override string ToString()
    {
        return $"{string.Join(" AND ", Inputs)} -> {OutputName}";
    }
}
public struct ORGate : IGate
{
    public ORGate(List<string> inputs, string outputName)
    {
        Inputs = inputs;
        OutputName = outputName;
    }
    public ORGate(List<string> inputs, string outputName, bool a, bool b)
    {
        Inputs = inputs;
        OutputName = outputName;
        A = a;
        B = b;
    }
    public bool? A { get; private set; }

    public bool? B { get; private set; }
    public bool Ready
    {
        get { return A != null && B != null; }
    }

    public List<string> Inputs { get; }
    public string OutputName { get; }
    public (string, bool) Output()
    {
        if (!Ready) throw new Exception("Not ready");
        bool result = A!.Value || B!.Value;
        A = null;
        B = null;
        return (OutputName, result);
    }

    public void Set(bool x)
    {
        if (A == null) A = x;
        else if (B == null) B = x;
        else throw new Exception("Already set");
    }

    public override string ToString()
    {
        return $"{string.Join(" OR ", Inputs)} -> {OutputName}";
    }
}
public struct XORGate : IGate
{
    public XORGate(List<string> inputs, string outputName)
    {
        Inputs = inputs;
        OutputName = outputName;
    }
    public XORGate(List<string> inputs, string outputName, bool a, bool b)
    {
        Inputs = inputs;
        OutputName = outputName;
        A = a;
        B = b;
    }
    public bool? A { get; private set; }

    public bool? B { get; private set; }
    public bool Ready
    {
        get { return A != null && B != null; }
    }

    public List<string> Inputs { get; }
    public string OutputName { get; }
    public (string, bool) Output()
    {
        if (!Ready) throw new Exception("Not ready");
        bool result = A!.Value ^ B!.Value;
        A = null;
        B = null;
        return (OutputName, result);
    }

    public void Set(bool x)
    {
        if (A == null) A = x;
        else if (B == null) B = x;
        else throw new Exception("Already set");
    }

    public override string ToString()
    {
        return $"{string.Join(" XOR ", Inputs)} -> {OutputName}";
    }
}


