namespace AoC.Common.Graphing;

public struct Vertex<T> where T : struct
{
    public Vertex(T value)
    {
        Value = value;
    }
    public T Value { get; private set; }
}
