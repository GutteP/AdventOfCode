using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace AoC.Common;

public static class AoCShapes
{
    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="polygon"></param>
    /// <returns></returns>
    public static int AreaOfSimpelPolygon(this List<Position<int>> polygon, bool includeOutline)
    {
        polygon.Add(polygon[0]);
        var area = Math.Abs(polygon.Take(polygon.Count - 1)
           .Select((p, i) => (polygon[i + 1].X - p.X) * (polygon[i + 1].Y + p.Y))
           .Sum() / 2);

        //Inkludera själva polygonen
        int outline = 0;
        if (includeOutline)
        {
            for (int i = 0; i < polygon.Count - 1; i++)
            {
                outline += (Math.Abs(polygon[i].DistanceX(polygon[i + 1])) + Math.Abs(polygon[i].DistanceY(polygon[i + 1])));
            }
            if (outline > 0) area += (outline / 2)+1;
        }
        return area;
    }
    public static long AreaOfSimpelPolygon(this List<Position<long>> polygon, bool includeOutline)
    {
        polygon.Add(polygon[0]);
        var area = Math.Abs(polygon.Take(polygon.Count - 1)
           .Select((p, i) => (polygon[i + 1].X - p.X) * (polygon[i + 1].Y + p.Y))
           .Sum() / 2);

        //Inkludera själva polygonen
        long outline = 0;
        if (includeOutline)
        {
            for (int i = 0; i < polygon.Count-1; i++)
            {
                outline += (Math.Abs(polygon[i].DistanceX(polygon[i + 1])) + Math.Abs(polygon[i].DistanceY(polygon[i + 1])));
            }
            if (outline > 0) area += (outline / 2)+1;
        }

        return area;
    }
    private static T GSum<T>(IEnumerable<T> collection) where T : IBinaryInteger<T>
    {
        T sum = T.Zero;
        foreach ( var item in collection)
        {
            sum += item;
        }
        return sum;
    }
    /// <summary>
    /// Kollar om x,y är i en polygon bestående av en lista av array med x,y, HELTAl ska användas i den här implementationen
    /// </summary>
    /// <remarks> Anpassad lösning from https://stackoverflow.com/a/14998816 </remarks>
    /// <typeparam name="T"></typeparam>
    /// <param name="polygon">List av x,y som bildar en polygon, antar att den sista sitter ihop med den första i listan</param>
    /// <param name="x">x i positionen du vill kolla</param>
    /// <param name="y">y i positionen du vill kolla</param>
    /// <returns>Sant om x,y är i polygonen</returns>
    public static bool InsidePolygon<T>(List<T[]> polygon, T x, T y) where T : IBinaryInteger<T>
    {
        bool result = false;
        int j = polygon.Count - 1;
        for (int i = 0; i < polygon.Count; i++)
        {
            if (polygon[i][1] < y && polygon[j][1] >= y ||
                polygon[j][1] < y && polygon[i][1] >= y)
            {
                if (polygon[i][0] + (y - polygon[i][1]) /
                   (polygon[j][1] - polygon[i][1]) *
                   (polygon[j][0] - polygon[i][0]) < x)
                {
                    result = !result;
                }
            }
            j = i;
        }
        return result;
    }

    /// <summary>
    /// Kollar om positionen är i en polygon bestående av andra positioner, HELTAL ska användas i den här implementationen
    /// </summary>
    /// <remarks> Anpassad lösning from https://stackoverflow.com/a/14998816 </remarks>
    /// <typeparam name="T"></typeparam>
    /// <param name="polygon">List av positioner som bildar en polygon, antar att den sista sitter ihop med den första i listan</param>
    /// <param name="position">Position som du vill veta om den är i polygonen</param>
    /// <returns>Sant om positinen är i polygonen</returns>
    public static bool InsidePolygon<T>(this List<Position<T>> polygon, Position<T> position) where T : IBinaryInteger<T>
    {
        bool result = false;
        int j = polygon.Count - 1;
        for (int i = 0; i < polygon.Count; i++)
        {
            if (polygon[i].Y < position.Y && polygon[j].Y >= position.Y ||
                polygon[j].Y < position.Y && polygon[i].Y >= position.Y)
            {
                if (polygon[i].X + (position.Y - polygon[i].Y) /
                   (polygon[j].Y - polygon[i].Y) *
                   (polygon[j].X - polygon[i].X) < position.X)
                {
                    result = !result;
                }
            }
            j = i;
        }
        return result;
    }

    public static bool InclusiveInsidePolygon<T>(this List<Position<T>> polygon, Position<T> point) where T : IBinaryInteger<T>
    {
        bool result = false;
        var a = polygon.Last();
        foreach (var b in polygon)
        {
            if ((b.X == point.X) && (b.Y == point.Y))
                return true;

            if ((b.Y == a.Y) && (point.Y == a.Y))
            {
                if ((a.X <= point.X) && (point.X <= b.X))
                    return true;

                if ((b.X <= point.X) && (point.X <= a.X))
                    return true;
            }

            if ((b.Y < point.Y) && (a.Y >= point.Y) || (a.Y < point.Y) && (b.Y >= point.Y))
            {
                if (b.X + (point.Y - b.Y) / (a.Y - b.Y) * (a.X - b.X) <= point.X)
                    result = !result;
            }
            a = b;
        }
        return result;
    }
}
