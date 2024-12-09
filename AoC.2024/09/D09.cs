
namespace AoC._2024;

public class D09
{
    public long? PartOne(string inputPath)
    {
        string map = InputReader.ReadString(inputPath);
        List<int?> defragmented = map.Expand().Defragment();
        return defragmented.Checksum();
    }


    public long? PartTwo(string inputPath)
    {
        string map = InputReader.ReadString(inputPath);
        List<(int Id, int Spaces)> defragmented = map.SmartExpand().SmartDefragment();
        return defragmented.Readable().SmartChecksum();
    }
}

public static class D09Extensions
{
    public static List<(int Id, int Spaces)> SmartExpand(this string map)
    {
        List<(int Id, int Spaces)> expanded = new();
        bool space = false;
        int id = 0;
        foreach (char c in map)
        {
            if (space)
            {
                expanded.Add((-1, (int)char.GetNumericValue(c)));
                space = false;
            }
            else
            {
                expanded.Add((id, (int)char.GetNumericValue(c)));
                space = true;
                id++;
            }
        }
        return expanded;
    }

    public static List<(int Id, int Spaces)> SmartDefragment(this List<(int Id, int Spaces)> expanded)
    {
        for (int j = expanded.Count - 1; j >= 0; j--)
        {
            if (expanded[j].Id != -1)
            {
                for (int i = 0; i < j; i++)
                {
                    if (expanded[i].Id == -1 && expanded[j].Spaces <= expanded[i].Spaces)
                    {
                        var toMove = expanded[j];
                        expanded[j] = (-1, toMove.Spaces);
                        int left = expanded[i].Spaces - toMove.Spaces;
                        expanded[i] = toMove;
                        if (left > 0)
                        {
                            expanded.Insert(i + 1, (-1, left));
                            j++;
                        }
                        int beforeCount = expanded.Count;
                        expanded = expanded.Smash(j);
                        break;
                    }
                }
            }
        }
        return expanded;
    }
    public static List<(int Id, int Spaces)> Smash(this List<(int Id, int Spaces)> expanded, int maxIndex)
    {
        for (int i = 0; i < maxIndex - 1; i++)
        {
            if (expanded[i].Id == -1 && expanded[i + 1].Id == -1)
            {
                expanded[i] = (-1, expanded[i].Spaces + expanded[i + 1].Spaces);
                expanded.RemoveAt(i + 1);
                i = 0;
            }
        }
        return expanded;
    }
    public static List<int?> Readable(this List<(int Id, int Spaces)> expanded)
    {
        List<int?> readable = new();
        foreach (var (id, spaces) in expanded)
        {
            if (id == -1)
            {
                for (int i = 0; i < spaces; i++)
                {
                    readable.Add(null);
                }
            }
            else
            {
                for (int i = 0; i < spaces; i++)
                {
                    readable.Add(id);
                }
            }
        }
        return readable;
    }
    public static long SmartChecksum(this List<int?> defragmented)
    {
        long sum = 0;
        for (int i = 0; i < defragmented.Count; i++)
        {
            if (defragmented[i] != null)
            {
                sum += (i * defragmented[i]!.Value);
            }
        }
        return sum;
    }

    public static List<int?> Expand(this string map)
    {
        List<int?> expanded = new();
        bool space = false;
        int id = 0;
        foreach (char c in map)
        {
            if (space)
            {
                for (int i = 0; i < (int)char.GetNumericValue(c); i++)
                {
                    expanded.Add(null);
                }
                space = false;
            }
            else
            {
                for (int i = 0; i < (int)char.GetNumericValue(c); i++)
                {
                    expanded.Add(id);
                }
                space = true;
                id++;
            }
        }
        return expanded;
    }

    public static List<int?> Defragment(this List<int?> expanded)
    {
        int last = expanded.Count - 1;
        for (int i = 0; i < last; i++)
        {
            if (expanded[i] == null)
            {
                int? toInject = expanded[last];
                while (toInject == null)
                {
                    last--;
                    toInject = expanded[last];
                }
                if (last > i)
                {
                    expanded[i] = toInject;
                    expanded[last] = null;
                }

            }
        }
        return expanded;
    }

    public static long Checksum(this List<int?> defragmented)
    {
        long sum = 0;
        for (int i = 0; i < defragmented.Count; i++)
        {
            if (defragmented[i] == null)
            {
                break;
            }
            sum += (i * defragmented[i]!.Value);
        }
        return sum;
    }
}

