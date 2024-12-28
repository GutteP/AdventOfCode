namespace AoC._2024;

public class D23
{
    public long PartOne(string inputPath, int cliqueSize)
    {
        List<(string A, string B)> connections = InputReader.ReadLines(inputPath).Select(x => (x.Split('-')[0], x.Split('-')[1])).ToList();
        List<string> distinctNodes = connections.DistinctNodes();
        List<Edge<int>> edges = connections.DistinctEdges(distinctNodes);

        Cliques cliqueFinder = new(edges, distinctNodes.Count, cliqueSize);
        List<List<int>> result = cliqueFinder.FindCliques(cliqueSize);
        List<List<int>> maybeWithChiefHistorian = result.MaybeWithChiefHistorian(distinctNodes);
        return maybeWithChiefHistorian.Count;
    }

    public long PartTwo(string inputPath, int interconnected)
    {

        return 0;
    }
}

public class Cliques
{
    private int[] _currentCliqueStore;
    private int[,] _matrix;
    private int _numberOfNodes;
    private int[] _countOfEdges;

    public Cliques(List<Edge<int>> edges, int numberOfNodes, int maxCliqueSize)
    {
        _currentCliqueStore = new int[maxCliqueSize + 1];
        _matrix = new int[numberOfNodes, numberOfNodes];
        _countOfEdges = new int[numberOfNodes + 1];
        _numberOfNodes = numberOfNodes;

        foreach (var edge in edges)
        {
            _matrix[edge.A, edge.B] = 1;
            _matrix[edge.B, edge.A] = 1;
            _countOfEdges[edge.A]++;
            _countOfEdges[edge.B]++;
        }
    }

    public List<List<int>> FindCliques(int targetCliqueSize, int startNode = 0, int currentCliqueSize = 1, List<List<int>>? collectedCliques = null)
    {
        if (collectedCliques == null)
        {
            collectedCliques = new();
        }

        for (int currentNode = startNode; currentNode <= _numberOfNodes - (targetCliqueSize - currentCliqueSize); currentNode++)
        {
            // Check if currentNode has the required number of edges
            if (targetCliqueSize < _countOfEdges[currentNode])
            {
                _currentCliqueStore[currentCliqueSize] = currentNode;

                if (IsClique(currentCliqueSize + 1))
                {
                    // If currentCliqueSize is still less than targetCliqueSize 
                    if (currentCliqueSize < targetCliqueSize)
                    {
                        FindCliques(targetCliqueSize, currentNode, currentCliqueSize + 1, collectedCliques);
                    }
                    else
                    {
                        collectedCliques.Add(CollectClique(currentCliqueSize + 1));
                    }
                }
            }
        }
        return collectedCliques;
    }

    private bool IsClique(int b)
    {
        // Run a loop for all the set of edges for the select node 
        for (int i = 1; i < b; i++)
        {
            for (int j = i + 1; j < b; j++)
            {
                if (_matrix[_currentCliqueStore[i], _currentCliqueStore[j]] == 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private List<int> CollectClique(int n)
    {
        List<int> clique = new();
        for (int i = 1; i < n; i++)
        {
            clique.Add(_currentCliqueStore[i]);
        }
        return clique;
    }
}


public static class D23Extensions
{
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

public struct Vertex<T> where T : struct
{
    public Vertex(T value)
    {
        Value = value;
    }
    public T Value { get; private set; }
}

public struct Edge<T> where T : struct
{
    public Edge(Vertex<T> a, Vertex<T> b)
    {
        A = a.Value;
        B = b.Value;
    }
    public Edge(T a, T b)
    {
        A = a;
        B = b;
    }
    public T A { get; set; }
    public T B { get; set; }

    public bool IsUndirectedEdge(Vertex<T> vertex)
    {
        return A.Equals(vertex.Value) || B.Equals(vertex.Value);
    }
}