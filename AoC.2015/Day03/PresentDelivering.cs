namespace AoC._2015.Day03;

public class PresentDelivering : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<List<Direction>, int>(Transformer, OneSanta), new Runner<List<Direction>, int>(Transformer, TwoSanta));
    }

    private List<Direction> Transformer(string path)
    {
        List<Direction> directions = new();
        foreach (char c in InputReader.ReadLines(path).First())
        {
            directions.Add(Direction.None.FromArrow(c));
        }
        return directions;
    }

    private int OneSanta(List<Direction> directions)
    {
        Position<int> current = new(0, 0);
        HashSet<string> positions = new() { current.ToString() };
        foreach (Direction dir in directions)
        {
            current.Move(dir, 1);
            positions.Add(current.ToString());
        }
        return positions.Count;
    }
    private int TwoSanta(List<Direction> directions)
    {
        Position<int> santa = new(0, 0);
        Position<int> roboSanta = new(0, 0);
        bool roboSantasTurn = false;
        HashSet<string> positions = new() { santa.ToString() };
        foreach (Direction dir in directions)
        {
            if (roboSantasTurn)
            {
                roboSanta.Move(dir, 1);
                positions.Add(roboSanta.ToString());
                roboSantasTurn = false;
            }
            else
            {
                santa.Move(dir, 1);
                positions.Add(santa.ToString());
                roboSantasTurn = true;
            }
        }
        return positions.Count;
    }
}
