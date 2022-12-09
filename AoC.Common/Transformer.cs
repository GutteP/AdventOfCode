namespace AoC.Common;
public static class Transformer
{
    public static List<List<int>> ToNumericValue(this List<string> input)
    {
        List<List<int>> result = new();
        foreach (string s in input)
        {
            result.Add(s.ToNumericValue());
        }
        return result;
    }
    public static List<int> ToNumericValue(this string input)
    {
        List<int> result = new();
        foreach (char c in input)
        {
            result.Add((int)char.GetNumericValue(c));
        }
        return result;
    }
    public static List<List<int>> ToInt(this List<List<string>> input)
    {
        List<List<int>> output = new();
        foreach (List<string> list in input)
        {
            output.Add(list.ToInt());
        }
        return output;
    }
    public static List<int> ToInt(this List<string> input)
    {
        List<int> output = new();
        foreach (string str in input)
        {
            output.Add(int.Parse(str));
        }
        return output;
    }
    public static List<List<string>> RemoveEmpty(this List<List<string>> input)
    {
        List<List<string>> output = new();
        foreach (var item in input)
        {
            List<string> l = item.RemoveEmpty();
            if (l.Any()) output.Add(l);
        }
        return output;
    }
    public static List<string> RemoveEmpty(this List<string> input)
    {
        List<string> output = new();
        foreach (var item in input)
        {
            if (!string.IsNullOrEmpty(item)) output.Add(item);
        }
        return output;
    }
    public static List<List<string>> Trim(this List<List<string>> input, params char[] chars)
    {
        List<List<string>> output = new();
        foreach (List<string> row in input)
        {
            output.Add(row.Trim(chars));
        }
        return output;
    }
    public static List<string> Trim(this List<string> input, params char[] chars)
    {
        List<string> output = new();
        foreach (string row in input)
        {
            output.Add(row.Trim(chars));
        }
        return output;
    }
    public static List<List<string>> SplitOn(this List<string> input, Seperator seperator)
    {
        List<List<string>> output = new();
        foreach (string line in input)
        {
            output.Add(line.SplitOn(seperator));
        }
        return output;
    }

    public static List<string> SplitOn(this string input, Seperator seperator)
    {
        return input.Split(seperator.SeperatorToChar()).ToList();
    }

    private static string SeperatorToChar(this Seperator seperator)
    {
        switch (seperator)
        {
            case Seperator.Comma: return ",";
            case Seperator.Space: return " ";
            case Seperator.Dot: return ".";
            case Seperator.Semicolon: return ";";
            case Seperator.NewLine: return "\r\n";
            case Seperator.Tab: return "\t";
            case Seperator.Dash: return "-";
            default: throw new NotImplementedException();
        }
    }
}