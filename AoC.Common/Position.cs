﻿using System.Numerics;
using System.Transactions;

namespace AoC.Common;
public record Position<T> where T : IBinaryInteger<T>
{
    public Position(T x, T y)
    {
        X = x;
        Y = y;
    }

    public T X { get; set; }
    public T Y { get; set; }

    public List<Position<T>> Neighbors(bool withDiagonals)
    {
        List<Position<T>> neighbors = new();
        neighbors.Add(new(X - T.One, Y));
        neighbors.Add(new(X + T.One, Y));
        neighbors.Add(new(X, Y - T.One));
        neighbors.Add(new(X, Y + T.One));
        if (withDiagonals)
        {
            neighbors.Add(new(X - T.One, Y - T.One));
            neighbors.Add(new(X + T.One, Y - T.One));
            neighbors.Add(new(X - T.One, Y + T.One));
            neighbors.Add(new(X + T.One, Y + T.One));
        }
        return neighbors;
    }
    public bool InRange(T xLower, T xUpper, T yLower, T yUpper)
    {
        if (X < xLower) return false;
        if (X > xUpper) return false;
        if (Y < yLower) return false;
        if (Y > yUpper) return false;
        return true;
    }
    public bool InRange(T upper)
    {
        return InRange(T.Zero, upper, T.Zero, upper);
    }
    public bool InRange(T xUpper, T yUpper)
    {
        return InRange(T.Zero, xUpper, T.Zero, yUpper);
    }

    public void Move(Direction direction, T steps)
    {
        switch (direction)
        {
            case Direction.Up:
                Y -= steps;
                break;
            case Direction.Right:
                X += steps;
                break;
            case Direction.Down:
                Y += steps;
                break;
            case Direction.Left:
                X -= steps;
                break;
            default:
                break;
        }
    }
    public Position<T> CopyAndMove(Direction direction, T steps)
    {
        Position<T> copy = new Position<T>(X, Y);
        copy.Move(direction, steps);
        return copy;
    }

    public Position<T> Copy()
    {
        return new Position<T>(X, Y);
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

public class Positions<T> where T : IBinaryInteger<T>
{
    private readonly IEnumerable<Position<T>> _positions;

    public Positions(IEnumerable<Position<T>> positions)
    {
        _positions = positions ?? throw new ArgumentNullException(nameof(positions));
    }

    public List<Position<T>> Neighbors(bool withDiagonals)
    {
        HashSet<Position<T>> adjacent = new();
        foreach (var position in _positions)
        {
            foreach (var neighbor in position.Neighbors(withDiagonals))
            {
                adjacent.Add(neighbor);
            }
        }
        foreach (var p in _positions)
        {
            adjacent.Remove(p);
        }
        return adjacent.ToList();
    }
}

public static class MapPositionHelper
{
    public static TMap Current<TMap>(this TMap[,] map, Position<int> position)
    {
        return map[position.Y, position.X];
    }
    public static TMap Current<TMap>(this TMap[,] map, Position<long> position)
    {
        return map[position.Y, position.X];
    }
    public static bool On<TMap>(this TMap[,] map, Position<int> position)
    {
        if (position.Y < 0 || position.Y >= map.GetLength(0)) return false;
        if (position.X < 0 || position.X >= map.GetLength(1)) return false;
        return true;
    }
}
