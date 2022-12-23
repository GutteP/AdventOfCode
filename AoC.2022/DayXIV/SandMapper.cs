namespace AoC._2022.DayXIV;

public class SandMapper : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<SandMap, int>(CreateMap, CountDrops), new Runner<SandMap, int>(CreateMapWithFloor, CountDrops));
    }

    private SandMap CreateMap(string path)
    {
        var rockShapes = InputReader.ReadLines(path).SplitOn(Seperator.Arrow);
        SandMap map = new();
        foreach (var shape in rockShapes)
        {
            for (int i = 0; i < shape.Count - 1; i++)
            {
                var rA = shape[i].Split(',');
                (int h, int v) a = (int.Parse(rA[0]), int.Parse(rA[1]));
                var rB = shape[i + 1].Split(',');
                (int h, int v) b = (int.Parse(rB[0]), int.Parse(rB[1]));

                int hDiff = b.h - a.h;
                int vDiff = b.v - a.v;
                if (hDiff != 0 && vDiff != 0) throw new Exception("Diagonalt..");
                else if (hDiff > 0)
                {
                    for (int j = a.h; j <= b.h; j++)
                    {
                        map.Occupy(j, a.v);
                    }
                }
                else if (hDiff < 0)
                {
                    for (int j = a.h; j >= b.h; j--)
                    {
                        map.Occupy(j, a.v);
                    }
                }
                else if (vDiff > 0)
                {
                    for (int j = a.v; j <= b.v; j++)
                    {
                        map.Occupy(a.h, j);
                    }

                }
                else if (vDiff < 0)
                {
                    for (int j = a.v; j >= b.v; j--)
                    {
                        map.Occupy(a.h, j);
                    }
                }
                else throw new Exception("Både h- och vDiff var 0...");
            }
        }
        return map;
    }
    private SandMap CreateMapWithFloor(string path)
    {
        SandMap map = CreateMap(path);
        int floorV = map.VerticalMax + 2;
        int floorMinH = map.HorizontalMin - floorV;
        int floorMaxH = map.HorizontalMax + floorV;

        for (int i = floorMinH; i < floorMaxH; i++)
        {
            map.Occupy(i, floorV);
        }
        return map;
    }

    private int CountDrops(SandMap map)
    {
        int drops = 0;
        while (true)
        {
            if (map.SandDrop()) return drops;
            drops++;
        }
    }
}

public class SandMap
{
    Dictionary<string, bool> _spaces;
    public int HorizontalMin;
    public int HorizontalMax;
    public int VerticalMax;
    public SandMap()
    {
        _spaces = new();
        HorizontalMin = int.MaxValue;
        HorizontalMax = int.MinValue;
        VerticalMax = int.MinValue;
    }

    public void Occupy(int h, int v)
    {
        _spaces[$"{h},{v}"] = true;
        if (h > HorizontalMax) HorizontalMax = h;
        if (h < HorizontalMin) HorizontalMin = h;
        if (v > VerticalMax) VerticalMax = v;

    }

    public bool SandDrop(int h = 500, int v = 0)
    {
        if (h > HorizontalMax) return true;
        if (h < HorizontalMin) return true;
        if (v > VerticalMax) return true;

        if (_spaces.TryGetValue($"{h},{v}", out bool value) && value)
        {
            if (h == 500 && v == 0) return true;
            return SideStep(h, v);
        }
        return SandDrop(h, v + 1);
    }

    private bool SideStep(int h, int v)
    {
        if (h > HorizontalMax) return true;
        if (h < HorizontalMin) return true;
        if (v > VerticalMax) return true;

        if (!_spaces.TryGetValue($"{h - 1},{v}", out bool left) || !left)
        {
            return SandDrop(h - 1, v);
        }
        if (!_spaces.TryGetValue($"{h + 1},{v}", out bool right) || !right)
        {
            return SandDrop(h + 1, v);
        }
        _spaces[$"{h},{v - 1}"] = true;
        return false;
    }
}
