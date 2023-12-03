namespace AoC._2022.Day09;

public class RopeSimulator : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(
            new Runner<List<(Direction Direction, int Steps)>, int>(Transformer, TwoKnots),
            new Runner<List<(Direction Direction, int Steps)>, int>(Transformer, TenKnots));
    }

    private List<(Direction Direction, int Steps)> Transformer(string path)
    {
        List<(Direction Direction, int Steps)> input = new();
        foreach (string row in InputReader.ReadLines(path))
        {
            var s = row.SplitOn(Seperator.Space).Trim();
            switch (s[0])
            {
                case "U":
                    input.Add((Direction.Up, int.Parse(s[1])));
                    break;
                case "R":
                    input.Add((Direction.Right, int.Parse(s[1])));
                    break;
                case "D":
                    input.Add((Direction.Down, int.Parse(s[1])));
                    break;
                case "L":
                    input.Add((Direction.Left, int.Parse(s[1])));
                    break;
                default:
                    throw new ArgumentException("Där gick något fel..");
            }
        }
        return input;
    }

    private int TwoKnots(List<(Direction Direction, int Steps)> input)
    {
        Knot head = new(0, 0);
        Knot tail = new(0, 0);

        HashSet<string> visitedAtLeastOnce = new();
        foreach (var move in input)
        {
            for (int i = 0; i < move.Steps; i++)
            {
                head.Move(move.Direction, 1);
                visitedAtLeastOnce.Add(tail.Follow(head));
            }
        }
        return visitedAtLeastOnce.Count;
    }
    private int TenKnots(List<(Direction Direction, int Steps)> input)
    {
        Knot head = new(0, 0);
        Knot one = new(0, 0);
        Knot two = new(0, 0);
        Knot three = new(0, 0);
        Knot four = new(0, 0);
        Knot five = new(0, 0);
        Knot six = new(0, 0);
        Knot seven = new(0, 0);
        Knot eight = new(0, 0);
        Knot tail = new(0, 0);

        HashSet<string> visitedAtLeastOnce = new();
        foreach (var move in input)
        {
            for (int i = 0; i < move.Steps; i++)
            {
                head.Move(move.Direction, 1);
                one.Follow(head);
                two.Follow(one);
                three.Follow(two);
                four.Follow(three);
                five.Follow(four);
                six.Follow(five);
                seven.Follow(six);
                eight.Follow(seven);
                visitedAtLeastOnce.Add(tail.Follow(eight));
            }
        }
        return visitedAtLeastOnce.Count;
    }
}
public record Knot : Position<int>
{
    public Knot(int x, int y) : base(x, y) { }

    public string Follow(Knot b)
    {
        if (Adjacent(b)) return ToString();

        (int X, int Y) distance = Distance(b);
        if (Math.Abs(distance.X) >= 1 && Math.Abs(distance.Y) >= 1)
        {
            if (distance.X > 0) X++;
            if (distance.X < 0) X--;

            if (distance.Y > 0) Y++;
            if (distance.Y < 0) Y--;
        }
        else if (distance.X > 1) X++;
        else if (distance.X < -1) X--;
        else if (distance.Y > 1) Y++;
        else if (distance.Y < -1) Y--;

        return ToString();
    }
    private bool Adjacent(Knot b)
    {
        return Math.Abs(DistanceX(b)) <= 1 && Math.Abs(DistanceY(b)) <= 1;
    }
}
