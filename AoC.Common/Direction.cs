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

    public static Direction FromPipe(this Direction from, char pipe)
    {
        if (from == Direction.Down)
        {
            return pipe switch
            {
                '|' => Direction.Down,
                'L' => Direction.Right,
                'J' => Direction.Left,
                _ => Direction.None
            };
        }
        else if (from == Direction.Up)
        {
            return pipe switch
            {
                '|' => Direction.Up,
                '7' => Direction.Left,
                'F' => Direction.Right,
                _ => Direction.None
            };
        }
        else if (from == Direction.Left)
        {
            return pipe switch
            {
                '-' => Direction.Left,
                'L' => Direction.Up,
                'F' => Direction.Down,
                _ => Direction.None
            };
        }
        else if (from == Direction.Right)
        {
            return pipe switch
            {
                '-' => Direction.Right,
                'J' => Direction.Up,
                '7' => Direction.Down,
                _ => Direction.None
            };
        }
        else throw new ArgumentException("Not a Arrow");
    }
}