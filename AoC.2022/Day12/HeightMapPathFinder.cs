using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace AoC._2022.Day12
{
    public class HeightMapPathFinder : IAoCDay<int>
    {
        public DayRunner<int> Runner()
        {
            return new DayRunner<int>(
                new Runner<(HeightMapPosition Start, HeightMapPosition Goal, char[,] Map), int>(Transformer, SolvePartOne),
                new Runner<(HeightMapPosition Start, HeightMapPosition Goal, char[,] Map), int>(Transformer, SolvePartTwo));
        }

        private (HeightMapPosition Start, HeightMapPosition Goal, char[,] Map) Transformer(string path)
        {
            var map = InputReader.ReadLines(path).Map2D();
            HeightMapPosition start = null;
            HeightMapPosition goal = null;

            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == 'S') start = new(x, y);
                    if (map[y, x] == 'E') goal = new(x, y);
                }
                if (start != null && goal != null) break;
            }

            map[start.Y, start.X] = 'a';
            map[goal.Y, goal.X] = 'z';
            return (start, goal, map);
        }

        private int SolvePartOne((HeightMapPosition From, HeightMapPosition Goal, char[,] Map) input)
        {
            int?[,] distances = new int?[input.Map.GetLength(0), input.Map.GetLength(1)];
            distances[input.From.Y, input.From.X] = 0;

            List<HeightMapPosition> updated = new() { input.From };
            for (int i = 0; i < updated.Count; i++)
            {
                var result = CalculateDistances(updated[i], distances, input.Map);
                distances = result.Distances;
                updated.AddRange(result.Updated);
            }

            return distances[input.Goal.Y, input.Goal.X] ?? int.MaxValue;
        }
        private int SolvePartTwo((HeightMapPosition From, HeightMapPosition Goal, char[,] Map) input)
        {
            int min = int.MaxValue;
            foreach (var seaLevel in FindAll(input.Map, 'a'))
            {
                int r = SolvePartOne((seaLevel, input.Goal, input.Map));
                if (r < min) min = r;
            }
            return min;
        }

        private (int?[,] Distances, List<HeightMapPosition> Updated) CalculateDistances(HeightMapPosition from, int?[,] distances, char[,] map)
        {
            List<HeightMapPosition> updated = new();
            foreach (var n in from.PossibleMoves(map))
            {
                if (distances[n.Y, n.X] == null || distances[n.Y, n.X] > distances[from.Y, from.X] + 1)
                {
                    distances[n.Y, n.X] = distances[from.Y, from.X] + 1;
                    updated.Add(n);
                }
            }
            return (distances, updated);
        }

        private List<HeightMapPosition> FindAll(char[,] map, char c)
        {
            List<HeightMapPosition> result = new();
            for (int y = 0; y < map.GetLength(0); y++)
            {
                for (int x = 0; x < map.GetLength(1); x++)
                {
                    if (map[y, x] == 'a') result.Add(new(x, y));
                }
            }
            return result;
        }
    }

    public class HeightMapPosition : Position<int>
    {
        public HeightMapPosition(int x, int y) : base(x, y) { }


        public List<HeightMapPosition> PossibleMoves(char[,] map)
        {
            List<HeightMapPosition> possible = new();

            if (Y + 1 < map.GetLength(0) && map[Y + 1, X] <= map[Y, X] + 1)
            {
                possible.Add(new HeightMapPosition(X, Y + 1));
            }
            if (Y - 1 >= 0 && map[Y - 1, X] <= map[Y, X] + 1)
            {
                possible.Add(new HeightMapPosition(X, Y - 1));
            }
            if (X + 1 < map.GetLength(1) && map[Y, X + 1] <= map[Y, X] + 1)
            {
                possible.Add(new HeightMapPosition(X + 1, Y));
            }
            if (X - 1 >= 0 && map[Y, X - 1] <= map[Y, X] + 1)
            {
                possible.Add(new HeightMapPosition(X - 1, Y));
            }
            return possible;
        }
    }
}
