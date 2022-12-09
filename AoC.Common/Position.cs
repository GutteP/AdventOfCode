using System.Numerics;

namespace AoC.Common;

public class Position<T> where T : INumber<T>
{
    public Position(T x, T y)
    {
        X = x;
        Y = y;
    }

    public T X { get; protected set; }
    public T Y { get; protected set; }

    public void Move(Direction direction, T steps)
    {
        switch (direction)
        {
            case Direction.Up:
                Y += steps;
                break;
            case Direction.Right:
                X += steps;
                break;
            case Direction.Down:
                Y -= steps;
                break;
            case Direction.Left:
                X -= steps;
                break;
            default:
                break;
        }
    }

    public (T X, T Y) Distance(Position<T> b)
    {
        T x = DistanceX(b);
        T y = DistanceY(b);
        return (x, y);
    }
    public T DistanceX(Position<T> b)
    {
        return b.X - X;
    }
    public T DistanceY(Position<T> b)
    {
        return b.Y - Y;
    }

    public override string ToString()
    {
        return $"{X},{Y}";
    }
}
