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
        return null;
    }
}

public static class D24Extensions
{
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

    public bool Ready { get; }
    public bool Done { get; }
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
    public bool Done { get; private set; }
    public (string, bool) Output()
    {
        if (!Ready) throw new Exception("Not ready");
        Done = true;
        return (OutputName, A!.Value && B!.Value);
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
    public bool Done { get; private set; }
    public (string, bool) Output()
    {
        if (!Ready) throw new Exception("Not ready");
        Done = true;
        return (OutputName, A!.Value || B!.Value);
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
    public bool Done { get; private set; }
    public (string, bool) Output()
    {
        if (!Ready) throw new Exception("Not ready");
        Done = true;
        return (OutputName, A!.Value ^ B!.Value);
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


