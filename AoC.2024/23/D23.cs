using AoC.Common.Graphing;

namespace AoC._2024;

public class D23
{
    public long PartOne(string inputPath, int cliqueSize)
    {
        List<(string A, string B)> connections = InputReader.ReadLines(inputPath).Select(x => (x.Split('-')[0], x.Split('-')[1])).ToList();
        List<string> distinctNodes = connections.DistinctNodes();
        List<Edge<int>> edges = connections.DistinctEdges(distinctNodes);

        CliquesOfSize cliqueFinder = new(edges, distinctNodes.Count, cliqueSize);
        List<List<int>> result = cliqueFinder.FindCliques(cliqueSize);
        List<List<int>> maybeWithChiefHistorian = result.MaybeWithChiefHistorian(distinctNodes);

        return maybeWithChiefHistorian.Count;
    }

    public string? PartTwo(string inputPath)
    {
        List<(string A, string B)> connections = InputReader.ReadLines(inputPath).Select(x => (x.Split('-')[0], x.Split('-')[1])).ToList();
        List<string> distinctNodes = connections.DistinctNodes();
        List<Edge<int>> edges = connections.DistinctEdges(distinctNodes);

        MaxCliqueFinder maxCliqueFinder = new(edges, distinctNodes.Count);

        List<int> maxClique = maxCliqueFinder.MaxCliques();
        List<string> letterResult = maxClique.BackToLetters(distinctNodes);

        return string.Join(',', letterResult.Order());
    }
}

public static class D23Extensions
{
    public static List<string> BackToLetters(this List<int> clique, List<string> distinctNodes)
    {
        List<string> letterClique = new();
        foreach (var node in clique)
        {
            letterClique.Add(distinctNodes[node]);
        }
        return letterClique;
    }
    public static List<List<int>> MaybeWithChiefHistorian(this List<List<int>> result, List<string> distinctNodes)
    {
        List<List<int>> mabyWithChiefHistorian = new();
        foreach (var clique in result)
        {
            if (clique.Any(x => distinctNodes[x].StartsWith("t")))
            {
                mabyWithChiefHistorian.Add(clique);
            }
        }
        return mabyWithChiefHistorian;
    }
    public static List<Edge<int>> DistinctEdges(this List<(string A, string B)> connections, List<string> distinctNodes)
    {
        List<Edge<int>> edges = new();
        foreach (var connection in connections)
        {
            int a = distinctNodes.IndexOf(connection.A);
            int b = distinctNodes.IndexOf(connection.B);
            if (edges.Any(x => (x.A == a && x.B == b) || (x.B == a && x.A == b)))
            {
                continue;
            }
            edges.Add(new Edge<int>(a, b));
        }
        return edges;
    }
    public static List<string> DistinctNodes(this List<(string A, string B)> connections)
    {
        HashSet<string> nodes = new();
        foreach ((string A, string B) connection in connections)
        {
            nodes.Add(connection.A);
            nodes.Add(connection.B);
        }
        return nodes.ToList();
    }
}
