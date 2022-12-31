namespace AoC._2022.Day17;

public class PyroclasticTetris : IAoCDay<double>
{
    public DayRunner<double> Runner()
    {
        return new DayRunner<double>(new Runner<char[], double, double>(Transformer, Play, 2022.0), new Runner<char[], double, double>(Transformer, Play, 1000000000000.0));
    }
    private char[] Transformer(string path)
    {
        return InputReader.ReadLines(path)[0].ToCharArray();
    }

    private double Play(char[] moves, double numberOfBlocks)
    {
        TetrisMap map = new(7);
        int n = 0;
        List<(double height, double i)> patterPool = new();
        for (double i = 0; i < numberOfBlocks; i++)
        {
            TetrisBlock block = map.Next();
            while (true)
            {
                if (n == moves.Length)
                {
                    patterPool.Add((map.TotalHeight() - patterPool.Select(x => x.Item1).Sum(), i - patterPool.Select(x => x.Item2).Sum()));
                    if (EnoughForPattern(patterPool))
                    {
                        (double heightToAdd, double iToAdd) pattern = ExtractPattern(patterPool);

                        int times = (int)((numberOfBlocks - (i + 1)) / pattern.iToAdd);
                        map.PatterHeight += times * pattern.heightToAdd;
                        i += times * pattern.iToAdd;
                    }

                    n = 0;
                }

                if (moves[n] == '<')
                {
                    block.Move(Direction.Left);
                    if (map.Hit(block))
                    {
                        block.Reverse();
                    }

                }
                else
                {
                    block.Move(Direction.Right);
                    if (map.Hit(block))
                    {
                        block.Reverse();
                    }
                }
                n++;

                block.Move(Direction.Down);
                if (map.Hit(block))
                {
                    block.Reverse();
                    map.Settle(block);
                    break;
                }
            }
        }
        return map.TotalHeight();
    }

    private bool EnoughForPattern(List<(double height, double i)> pool)
    {
        return pool.Count > 100 && pool.Where(x => x.height == pool.Last().height && x.i == pool.Last().i).Count() >= 8;
    }
    private (double heightToAdd, double iToAdd) ExtractPattern(List<(double height, double i)> pool)
    {
        int indexOfSecoundLast = pool.ToArray()[..(pool.Count - 1)].ToList().LastIndexOf(pool.Last());
        return (pool.ToArray()[(indexOfSecoundLast + 1)..].Sum(x => x.height), pool.ToArray()[(indexOfSecoundLast + 1)..].Sum(x => x.i));
    }
}


public class TetrisMap
{
    private int internalSize;
    private int n;
    private int currentHeight;
    public List<bool[]> Colums { get; set; }
    public TetrisMap(int width)
    {
        PatterHeight = 0;
        internalSize = 1000000;
        n = 0;
        Colums = new();
        for (int i = 0; i < width; i++)
        {
            Colums.Add(new bool[internalSize]);
        }
    }

    public double PatterHeight { get; set; }

    public TetrisBlock Next()
    {
        if (n == 0)
        {
            n++;
            return new StraightHorizontal(new Position<int>(2, currentHeight + 3));
        }
        if (n == 1)
        {
            n++;
            return new Plus(new Position<int>(2, currentHeight + 3));
        }
        if (n == 2)
        {
            n++;
            return new L(new Position<int>(2, currentHeight + 3));
        }
        if (n == 3)
        {
            n++;
            return new StraightVertical(new Position<int>(2, currentHeight + 3));
        }
        else
        {
            n = 0;
            return new Square(new Position<int>(2, currentHeight + 3));
        }
    }

    public bool Hit(TetrisBlock block)
    {
        foreach (var position in block.Shape)
        {
            if (position.X < 0 || position.X > Colums.Count - 1 || position.Y < 0) return true;
            if (Colums[position.X][position.Y]) return true;
        }
        return false;
    }

    public void Settle(TetrisBlock block)
    {
        foreach (var position in block.Shape)
        {
            if (position.Y > currentHeight - 1) currentHeight = position.Y + 1;
            Colums[position.X][position.Y] = true;
        }
    }

    public int Height()
    {
        return currentHeight;
    }
    public double TotalHeight()
    {
        return currentHeight + PatterHeight;
    }
}


public class TetrisBlock
{
    public List<Position<int>> Shape { get; set; }
    public Direction? LastDir { get; set; }

    public void Move(Direction direction)
    {
        foreach (Position<int> position in Shape)
        {
            position.Move(direction, 1);
        }
        LastDir = direction;
    }

    public void Reverse()
    {
        if (LastDir == null) throw new Exception("Never moved");
        switch (LastDir)
        {
            case Direction.Up:
                Move(Direction.Down);
                break;
            case Direction.Down:
                Move(Direction.Up);
                break;
            case Direction.Left:
                Move(Direction.Right);
                break;
            case Direction.Right:
                Move(Direction.Left);
                break;
            default:
                break;
        }
    }

}
public class StraightHorizontal : TetrisBlock
{
    public StraightHorizontal(Position<int> bottomLeft)
    {
        Shape = new()
        {
            bottomLeft,
            new Position<int>(bottomLeft.X + 1, bottomLeft.Y),
            new Position<int>(bottomLeft.X + 2, bottomLeft.Y),
            new Position<int>(bottomLeft.X + 3, bottomLeft.Y)
        };
    }
}
public class StraightVertical : TetrisBlock
{
    public StraightVertical(Position<int> bottomLeft)
    {
        Shape = new()
        {
            bottomLeft,
            new Position<int>(bottomLeft.X, bottomLeft.Y + 1),
            new Position<int>(bottomLeft.X, bottomLeft.Y + 2),
            new Position<int>(bottomLeft.X, bottomLeft.Y + 3)
        };
    }
}
public class L : TetrisBlock
{
    public L(Position<int> bottomLeft)
    {
        Shape = new()
        {
            bottomLeft,
            new Position<int>(bottomLeft.X + 1, bottomLeft.Y),
            new Position<int>(bottomLeft.X + 2, bottomLeft.Y),
            new Position<int>(bottomLeft.X + 2, bottomLeft.Y + 1),
            new Position<int>(bottomLeft.X + 2, bottomLeft.Y + 2)
        };
    }
}
public class Plus : TetrisBlock
{
    public Plus(Position<int> bottomLeft)
    {
        Shape = new()
        {
            new Position<int>(bottomLeft.X, bottomLeft.Y + 1),
            new Position<int>(bottomLeft.X + 1, bottomLeft.Y + 1),
            new Position<int>(bottomLeft.X + 1, bottomLeft.Y),
            new Position<int>(bottomLeft.X + 1, bottomLeft.Y + 2),
            new Position<int>(bottomLeft.X + 2, bottomLeft.Y + 1)
        };

    }
}
public class Square : TetrisBlock
{
    public Square(Position<int> bottomLeft)
    {
        Shape = new()
        {
            bottomLeft,
            new Position<int>(bottomLeft.X, bottomLeft.Y + 1),
            new Position<int>(bottomLeft.X + 1, bottomLeft.Y + 1),
            new Position<int>(bottomLeft.X + 1, bottomLeft.Y)
        };
    }
}

