namespace AoC.Common.Graphing;

/// <summary>
/// Hitta klickar av en viss storlek.
/// Anpassad från https://www.geeksforgeeks.org/find-all-cliques-of-size-k-in-an-undirected-graph/
/// </summary>
public class CliquesOfSize
{
    private int[] _currentCliqueStore;
    private int[,] _matrix;
    private int _numberOfNodes;
    private int[] _countOfEdges;

    public CliquesOfSize(List<Edge<int>> edges, int numberOfNodes, int maxCliqueSize)
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
            if (targetCliqueSize <= _countOfEdges[currentNode])
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
