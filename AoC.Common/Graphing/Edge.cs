namespace AoC.Common.Graphing;

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