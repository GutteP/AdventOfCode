using System.Reflection.Metadata.Ecma335;

namespace AoC.Common;

public enum Direction
{
    None, Up, Right, Down, Left
}

public static class DirectionCreator
{
    public static Direction FromArrow(this Direction dir, char arrow)
    {
        return arrow switch
        {
            '>' => Direction.Right,
            'v' => Direction.Down,
            '<' => Direction.Left,
            '^' => Direction.Up,
            _ => throw new ArgumentException("Not a Arrow")
        };
    }
}