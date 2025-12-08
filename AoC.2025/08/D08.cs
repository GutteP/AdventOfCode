using AoC.Common;
using System.Collections.Generic;

namespace AoC._2025;

public class D08
{
    public long? PartOne(string inputPath, int? option1 = null)
    {
        List<Position3D<long>> pos = InputReader.ReadLines(inputPath).Select(x => new Position3D<long>(long.Parse(x.Split(',', StringSplitOptions.TrimEntries)[0]),
            long.Parse(x.Split(',', StringSplitOptions.TrimEntries)[1]), long.Parse(x.Split(',', StringSplitOptions.TrimEntries)[2]))).ToList();

        Dictionary<string, long> distances = CalculateDistances(pos);
        List<(string Connection, long Distance)> minDistances = MinimumDistances(distances, option1 ?? 10);


        List<List<string[]>> circuits = FindCircuits(minDistances.Select(x => x.Connection.Split('-', StringSplitOptions.TrimEntries)).ToList());

        List<long> junc = [];
        foreach (var c in circuits)
        {
            HashSet<string> j = [];
            foreach (var k in c)
            {
                j.Add(k[0]);
                j.Add(k[1]);
            }
            junc.Add(j.Count);
        }

        junc = junc.OrderDescending().ToList();

        return junc[0] * junc[1] * junc[2];
    }

    public long? PartTwo(string inputPath)
    {
        List<Position3D<long>> pos = InputReader.ReadLines(inputPath).Select(x => new Position3D<long>(long.Parse(x.Split(',', StringSplitOptions.TrimEntries)[0]),
            long.Parse(x.Split(',', StringSplitOptions.TrimEntries)[1]), long.Parse(x.Split(',', StringSplitOptions.TrimEntries)[2]))).ToList();

        Dictionary<string, long> distances = CalculateDistances(pos);
        List<(string Connection, long Distance)> minDistances = MinimumDistances(distances, null);


        long r = LastConnected(minDistances.Select(x => x.Connection.Split('-', StringSplitOptions.TrimEntries)).ToList());

        return r;
    }

    public long LastConnected(List<string[]> distances)
    {
        Dictionary<string, string> connected = [];
        connected.Add(distances[0][0], string.Empty);
        connected.Add(distances[0][1], string.Empty);
        distances.RemoveAt(0);
        string l1 = string.Empty;
        string l2 = string.Empty;
        while (distances.Count > 0)
        {
            for (int i = 0; i < distances.Count; i++)
            {
                {
                    if (connected.ContainsKey(distances[i][0]) ^ connected.ContainsKey(distances[i][1]))
                    {
                        if (connected.ContainsKey(distances[i][0]))
                        {
                            l1 = distances[i][0];
                            l2 = distances[i][1];
                        }
                        else
                        {
                            l1 = distances[i][1];
                            l2 = distances[i][0];
                        }
                        connected.TryAdd(distances[i][0], string.Empty);
                        connected.TryAdd(distances[i][1], string.Empty);
                        distances.RemoveAt(i);
                        break;
                    }
                    else if (connected.ContainsKey(distances[i][0]) || connected.ContainsKey(distances[i][1]))
                    {
                        connected.TryAdd(distances[i][0], string.Empty);
                        connected.TryAdd(distances[i][1], string.Empty);
                        distances.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        return long.Parse(l1.Split(',', StringSplitOptions.TrimEntries)[0]) * long.Parse(l2.Split(',', StringSplitOptions.TrimEntries)[0]);
    }

    private List<List<string[]>> FindCircuits(List<string[]> connected)
    {
        List<List<string[]>> circuits = [];
        while (connected.Count > 0)
        {
            List<string[]> circuit = [];
            circuit.Add(connected[0]);
            connected.RemoveAt(0);
            while (true)
            {
                int count = circuit.Count;
                for (int i = 0; i < connected.Count; i++)
                {
                    if (circuit.Any(x => x[0] == connected[i][0] || x[1] == connected[i][0] || x[0] == connected[i][1] || x[1] == connected[i][1]))
                    {
                        circuit.Add(connected[i]);
                        connected.RemoveAt(i);
                        i--;
                    }
                }
                if (circuit.Count == count) break;
            }

            circuits.Add(circuit);
        }
        return circuits;
    }

    private List<(string, long)> MinimumDistances(Dictionary<string, long> distances, int? top)
    {
        IEnumerable<(string, long)> result = distances.Select(x => (x.Key, x.Value)).OrderBy(x => x.Item2);
        if (top != null)
        {
            result = result.Take(top.Value);
        }
        return result.ToList();
    }

    private Dictionary<string, long> CalculateDistances(List<Position3D<long>> positions)
    {
        Dictionary<string, long> distances = [];
        foreach (var a in positions)
        {
            foreach (var b in positions)
            {
                if (a.Equals(b)) continue;
                if (distances.ContainsKey($"{a}-{b}") || distances.ContainsKey($"{b}-{a}")) continue;
                long d = a.StraightLineDistance(b);
                distances.Add($"{a}-{b}", (int)d);
            }
        }
        return distances;
    }
}
