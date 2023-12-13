using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AoC._2023._13;

public class PointOfIncidence : IAoCDay<int>
{
    private const char Dot = '.';
    private const char Hash = '#';

    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<List<char[,]>, int>(Transformer, MirrorMap), new Runner<List<char[,]>, int>(Transformer, SmudgeMirrorMap));
    }

    private List<char[,]> Transformer(string path)
    {
        List<char[,]> maps = new();
        List<string> note = new();
        foreach (string row in InputReader.ReadLines(path))
        {
            if (string.IsNullOrWhiteSpace(row))
            {
                maps.Add(note.Map2D());
                note = new();
            }
            else note.Add(row.Trim());
        }
        if (note.Count > 0) maps.Add(note.Map2D());
        return maps;
    }

    private int MirrorMap(List<char[,]> maps)
    {
        List<int> sums = new();
        foreach (char[,] map in maps)
        {
            var r = FindReflection(map);
            if (r.Row != -1)
            {
                sums.Add((r.Row + 1) * 100);
                continue;
            }
            if (r.Column != -1)
            {
                sums.Add(r.Column + 1);
            }
            else throw new Exception("No reflection found");
        }
        return sums.Sum();
    }
    private int SmudgeMirrorMap(List<char[,]> maps)
    {
        List<(char[,] Map, int Row, int Column)> og = new();
        foreach (char[,] map in maps)
        {
            var r = FindReflection(map);
            og.Add((map, r.Row, r.Column));
        }
        List<int> sums = new();
        foreach (var o in og)
        {
            bool done = false;
            char[,] map = o.Map;
            (int Row, int Column) not = (o.Row, o.Column);
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = map[i, j] == Hash ? Dot : Hash;
                    var r = FindReflection(map, not);
                    if (r.Row != -1)
                    {
                        sums.Add((r.Row + 1) * 100);
                        done = true;
                        break;
                    }
                    if (r.Column != -1)
                    {
                        sums.Add(r.Column + 1);
                        done = true;
                        break;
                    }
                    map[i, j] = map[i, j] == Hash ? Dot : Hash;
                }
                if (done) break;
            }
        }

        if (sums.ToList().Count != maps.Count)
        {
            throw new Exception($"Hittade inte alla reflektioner, men summan blev: {sums.Sum()}");
        }

        return sums.Sum();
    }
    private (int Row, int Column) FindReflection(char[,] map, (int Row, int Column)? not = null)
    {
        int row = FindRowReflection(map, not);
        if ((row != -1 && not == null) || (row != -1 && not.Value.Row != row))
        {
            return (row, -1);
        }
        int column = FindColumnReflection(map, not);
        if ((column != -1 && not == null) || (column != -1 && not.Value.Column != column))
        {
            return (-1, column);
        }
        else if (not == null) throw new Exception("Ingen reflektion");
        return (-1, -1);
    }
    private int FindRowReflection(char[,] map, (int Row, int Column)? not)
    {
        for (var c = 0; c < map.GetLength(0) - 1; c++)
        {
            if (not != null && not.Value.Row == c) continue;
            if (IsRowsRefelections(map, c)) return c;
        }
        return -1;
    }

    private bool IsRowsRefelections(char[,] map, int start)
    {
        for (var c = 0; c < map.GetLength(0) + 1; c++)
        {
            if (start - c < 0 || start + 1 + c >= map.GetLength(0)) break;
            if (!IsRowsEquals(map, start - c, start + 1 + c)) return false;
        }
        return true;
    }

    private bool IsRowsEquals(char[,] map, int lower, int higher)
    {
        for (var placeInColumn = 0; placeInColumn < map.GetLength(1); placeInColumn++)
        {
            if (map[lower, placeInColumn] != map[higher, placeInColumn])
            {
                return false;
            }
        }
        return true;
    }

    private int FindColumnReflection(char[,] map, (int Row, int Column)? not)
    {
        for (var c = 0; c < map.GetLength(1) - 1; c++)
        {
            if (not != null && not.Value.Column == c) continue;
            if (IsColumnsRefelections(map, c)) return c;
        }
        return -1;
    }

    private bool IsColumnsRefelections(char[,] map, int start)
    {
        for (var c = 0; c < map.GetLength(1) + 1; c++)
        {
            if (start - c < 0 || start + 1 + c >= map.GetLength(1)) break;
            if (!IsColumnsEquals(map, start - c, start + 1 + c)) return false;
        }
        return true;
    }

    private bool IsColumnsEquals(char[,] map, int lower, int higher)
    {
        for (var placeInColumn = 0; placeInColumn < map.GetLength(0); placeInColumn++)
        {
            if (map[placeInColumn, lower] != map[placeInColumn, higher])
            {
                return false;
            }
        }
        return true;
    }
}
