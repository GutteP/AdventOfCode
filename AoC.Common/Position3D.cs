using System.Numerics;

namespace AoC.Common;

public sealed record Position3D<T> where T : IBinaryInteger<T>
{
    public Position3D(T x, T y, T z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public T X { get; private set; }
    public T Y { get; private set; }
    public T Z { get; private set; }

    public void Move(Direction3D direction, T steps)
    {
        switch (direction)
        {
            case Direction3D.X:
                Y += steps;
                break;
            case Direction3D.Y:
                X += steps;
                break;
            case Direction3D.Z:
                Z += steps;
                break;
            default:
                break;
        }
    }
    public Position3D<T> CopyAndMove(Direction3D direction, T steps)
    {
        Position3D<T> copy = new Position3D<T>(X, Y, Z);
        copy.Move(direction, steps);
        return copy;
    }

    public Position3D<T> Copy()
    {
        return new Position3D<T>(X, Y, Z);
    }

    public override string ToString()
    {
        return $"{X},{Y},{Z}";
    }
}
public enum Direction3D
{
    X,
    Y,
    Z
}
