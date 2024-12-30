namespace AoC.Common.Graphing;

/// <summary>
/// Hitta den största klicken. 
/// Anpassad från https://www.geeksforgeeks.org/maximal-clique-problem-recursive-solution/
/// </summary>
public class MaxCliqueFinder
{
    int _numberOfNodes;
    int _maxCliqueSize;
    private int[] _cliqueStore;
    private int[,] _graph;
    private int[] _countOfEdges;

    public MaxCliqueFinder(List<Edge<int>> edges, int numberOfNodes)
    {
        _maxCliqueSize = numberOfNodes + 1;
        _numberOfNodes = numberOfNodes;
        _graph = new int[_maxCliqueSize, _maxCliqueSize];
        _countOfEdges = new int[_maxCliqueSize];
        _cliqueStore = new int[_maxCliqueSize];
        for (int i = 0; i < edges.Count; i++)
        {
            _graph[edges[i].A, edges[i].B] = 1;
            _graph[edges[i].B, edges[i].A] = 1;
            _countOfEdges[edges[i].A]++;
            _countOfEdges[edges[i].B]++;
        }
    }

    public List<int> MaxCliques(int firstNode = 0, int l = 1)
    {
        int currentMax = 0;
        List<int> maxClique = new();

        // Check if any vertices from i can be inserted
        for (int j = firstNode; j <= _numberOfNodes; j++)
        {
            _cliqueStore[l] = j;

            if (IsClique(l + 1))
            {
                // Update max
                if (l > currentMax)
                {
                    maxClique = CollectClique(l + 1);
                    currentMax = l;
                }

                // Check if another edge can be added
                var temp = MaxCliques(j, l + 1);
                if (temp.Count > currentMax)
                {
                    maxClique = temp;
                    currentMax = temp.Count;
                }
            }
        }
        return maxClique;
    }
    private bool IsClique(int b)
    {
        for (int i = 1; i < b; i++)
        {
            for (int j = i + 1; j < b; j++)
            {
                if (_graph[_cliqueStore[i], _cliqueStore[j]] == 0)
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
            clique.Add(_cliqueStore[i]);
        }
        return clique;
    }
}
