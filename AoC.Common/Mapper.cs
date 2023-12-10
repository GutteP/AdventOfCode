namespace AoC.Common;

public static class Mapper
{

    public static T[,] Map2D<T>(this IEnumerable<IEnumerable<T>> input, bool acceptDefaults = false)
    {
        T[,] map = new T[input.Count(), input.First().Count()];
        for (int i = 0; i < input.Count(); i++)
        {
            for (int j = 0; j < input.ElementAt(i).Count(); j++)
            {
                try
                {
                    map[i, j] = input.ElementAt(i).ElementAt(j);
                }
                catch (Exception)
                {
                    if (acceptDefaults)
                    {
                        map[i, j] = default;
                    }
                    else throw;
                }
            }
        }
        return map;
    }

    public static Position<int>? FindFirst<T>(this T[,] map, T value)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j].Equals(value)) return new Position<int>(j, i);
            }
        }
        return default;
    }
}
