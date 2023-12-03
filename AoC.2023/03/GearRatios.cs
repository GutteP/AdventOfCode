namespace AoC._2023._03;

public class GearRatios : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<List<EnginePart>, int>(SchematicReader, x => x.Sum(y => y.Number)), new Runner<List<EnginePart>, int>(SchematicReader, GearRatioSum));
    }

    public List<EnginePart> SchematicReader(string path)
    {
        List<string> schematic = InputReader.ReadLines(path);
        List<EnginePart> parts = new();
        for (int i = 0; i < schematic.Count; i++)
        {
            List<Position<int>> numberPositions = new();
            for (int j = 0; j < schematic[i].Length; j++)
            {
                if (char.IsNumber(schematic[i][j]))
                {
                    numberPositions.Add(new(i, j));
                }
                if (numberPositions.Count > 0 && (!char.IsNumber(schematic[i][j]) || j + 1 == schematic[i].Length))
                {
                    var adjacentSymbolPlace = AdjacentSymbol(schematic, numberPositions);

                    if (adjacentSymbolPlace != null)
                    {
                        string number = string.Concat(numberPositions.Select(position => schematic[position.X][position.Y]));
                        parts.Add(new(int.Parse(number), schematic[adjacentSymbolPlace.X][adjacentSymbolPlace.Y], adjacentSymbolPlace));
                    }
                    numberPositions = new();
                }
            }
        }
        return parts;
    }

    public int GearRatioSum(List<EnginePart> parts)
    {
        int sum = 0;
        foreach (var gear in parts.Where(x => x.Symbol == '*').GroupBy(x => x.SymbolPosition).Where(x => x.Count() == 2).Select(x => x.Select(y => y.Number)))
        {
            sum += gear.Product();
        }
        return sum;
    }

    private Position<int> AdjacentSymbol(List<string> schematic, List<Position<int>> num)
    {
        List<Position<int>> adjacentPositions = new Positions<int>(num).Neighbors(true);
        foreach (var position in adjacentPositions)
        {
            if (position.InRange(schematic.Count - 1, schematic[0].Length - 1))
            {
                if (schematic[position.X][position.Y] != '.' && !char.IsNumber(schematic[position.X][position.Y]))
                {
                    return position;
                }
            }
        }
        return null;
    }


    public record EnginePart(int Number, char Symbol, Position<int> SymbolPosition);
}
