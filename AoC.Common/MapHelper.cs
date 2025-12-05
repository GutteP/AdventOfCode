namespace AoC.Common;

public static class MapHelper
{
    public static bool IsOnMap(this List<string> map, int x, int y)
    {
        if (y < 0 || y >= map.Count) return false;
        if (x < 0 || x >= map[y].Length) return false;
        return true;
    }
    public static bool IsOnMap(this List<char[]> map, int x, int y)
    {
        if (y < 0 || y >= map.Count) return false;
        if (x < 0 || x >= map[y].Length) return false;
        return true;
    }
}
