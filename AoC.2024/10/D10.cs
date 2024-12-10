
using System.Collections.Generic;

namespace AoC._2024;

public class D10
{
    public long? PartOne(string inputPath)
    {
        List<string> map = InputReader.ReadLines(inputPath);

        int sumOfTrails = 0;
        for (int x = 0; x < map.Count; x++)
        {
            for (int y = 0; y < map[x].Length; y++)
            {
                sumOfTrails += map.Hike((x, y), new List<(int X, int Y)>()).RemoveDuplicates().Count();
            }
        }
        return sumOfTrails;
    }


    public long? PartTwo(string inputPath)
    {
        List<string> map = InputReader.ReadLines(inputPath);

        int sumOfTrails = 0;
        for (int x = 0; x < map.Count; x++)
        {
            for (int y = 0; y < map[x].Length; y++)
            {
                sumOfTrails += map.Hike((x, y), new List<(int X, int Y)>()).Count();
            }
        }
        return sumOfTrails;
    }
}

public static class D10Extensions
{
    public static List<List<(int X, int Y)>> Hike(this List<string> map, (int X, int Y) pos, List<(int X, int Y)> trail)
    {
        List<List<(int X, int Y)>> trails = new();
        if (pos.X < 0 || pos.Y < 0 || pos.X >= map.Count || pos.Y >= map[0].Length) return trails;
        if ( char.GetNumericValue(map[pos.X][pos.Y]) == trail.Count)
        {
            trail.Add(pos);
            if(trail.Count == 10)
            {
                trails.Add(trail);
                return trails;
            }
            trails.AddRange(map.Hike((pos.X+1, pos.Y), trail.Clone()));
            trails.AddRange(map.Hike((pos.X-1, pos.Y), trail.Clone()));
            trails.AddRange(map.Hike((pos.X, pos.Y+1), trail.Clone()));
            trails.AddRange(map.Hike((pos.X, pos.Y-1), trail.Clone()));

        }
        return trails;
    }

    public static List<(int X, int Y)> RemoveDuplicates(this List<List<(int X, int Y)>> trails)
    {
        HashSet<(int X, int Y)> unique = new();
        for (int i = 0; i < trails.Count; i++)
        {
            unique.Add(trails[i].Last());
        }
        return unique.ToList();
    }

    private static List<T> Clone<T>(this List<T> list)
    {
        List<T> clone = new();
        foreach (T item in list) 
        {
            clone.Add(item);
        }
        return clone;
    }
}

