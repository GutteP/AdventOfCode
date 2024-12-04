namespace AoC._2024;

public class D04
{
    public int? PartOne(string inputPath)
    {
        List<string> input = InputReader.ReadLines(inputPath);
        int count = 0;
        for (int a = 0; a < input.Count; a++)
        {
            for (int b = 0; b < input[a].Length; b++)
            {
                if (input.HorizontalXmas(a, b))
                {
                    count++;
                }
                if (input.VerticalXmas(a, b))
                {
                    count++;
                }
                if (input.DiaDownXmas(a, b))
                {
                    count++;
                }
                if (input.DiaUpXmas(a, b))
                {
                    count++;
                }
            }
        }

        return count;
    }


    public int? PartTwo(string inputPath)
    {
        List<string> input = InputReader.ReadLines(inputPath);
        int count = 0;
        for (int a = 0; a < input.Count; a++)
        {
            for (int b = 0; b < input[a].Length; b++)
            {
                if (input.XMas(a, b))
                {
                    count++;
                }
            }
        }

        return count;
    }
}

public static class D04Extensions
{

    public static bool XMas(this List<string> m, int a, int b)
    {
        if (m.Count > a + 2 && m[a].Length > b + 2)
        {
            List<string> xmas = new();
            xmas.Add(m[a].Substring(b, 3));
            xmas.Add(m[a + 1].Substring(b, 3));
            xmas.Add(m[a + 2].Substring(b, 3));
            return xmas.IsXmas();

        }
        return false;
    }
    private static bool IsXmas(this List<string> xmas)
    {
        return xmas.DiaDownXmasShort(0, 0) && xmas.DiaUpXmasShort(2, 0);
    }

    public static bool HorizontalXmas(this List<string> m, int a, int b)
    {
        if (m.Count > a && m[a].Length > b + 3)
        {
            string w = string.Concat(m[a][b], m[a][b + 1], m[a][b + 2], m[a][b + 3]);
            if (w == "XMAS" || w == "SAMX")
            {
                return true;
            }
        }

        return false;
    }
    public static bool VerticalXmas(this List<string> m, int a, int b)
    {
        if (m.Count > a + 3 && m[a].Length > b)
        {
            string w = string.Concat(m[a][b], m[a + 1][b], m[a + 2][b], m[a + 3][b]);
            if (w == "XMAS" || w == "SAMX")
            {
                return true;
            }
        }

        return false;
    }
    public static bool DiaDownXmas(this List<string> m, int a, int b)
    {
        if (m.Count > a + 3 && m[a].Length > b + 3)
        {
            string w = string.Concat(m[a][b], m[a + 1][b + 1], m[a + 2][b + 2], m[a + 3][b + 3]);
            if (w == "XMAS" || w == "SAMX")
            {
                return true;
            }
        }

        return false;
    }
    public static bool DiaUpXmas(this List<string> m, int a, int b)
    {
        if (a - 3 >= 0 && m[a].Length > b + 3)
        {
            string w = string.Concat(m[a][b], m[a - 1][b + 1], m[a - 2][b + 2], m[a - 3][b + 3]);
            if (w == "XMAS" || w == "SAMX")
            {
                return true;
            }
        }

        return false;
    }
    public static bool DiaDownXmasShort(this List<string> m, int a, int b)
    {
        string w = string.Concat(m[a][b], m[a + 1][b + 1], m[a + 2][b + 2]);
        if (w == "MAS" || w == "SAM")
        {
            return true;
        }

        return false;
    }
    public static bool DiaUpXmasShort(this List<string> m, int a, int b)
    {
        string w = string.Concat(m[a][b], m[a - 1][b + 1], m[a - 2][b + 2]);
        if (w == "MAS" || w == "SAM")
        {
            return true;
        }

        return false;
    }
}