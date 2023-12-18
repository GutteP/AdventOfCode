using FluentAssertions;
using FluentAssertions.Equivalency;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Common;

/// <summary>
/// Skapad med hjälp av wikipedias pseudokod exempel https://en.wikipedia.org/wiki/A*_search_algorithm 2023-12-17
/// </summary>
public static class AStarRunner
{
    // A* finds a path from start to goal.
    // h is the heuristic function. h(n) estimates the cost to reach goal from node n.
    public static List<Position<int>> AStar(Position<int> start, Position<int> goal, int hMultiple, int[,] map)
    {
        // The set of discovered nodes that may need to be (re-)expanded.
        // Initially, only the start node is known.
        // This is usually implemented as a min-heap or priority queue rather than a hash-set.
        PriorityQueue<Position<int>, int> openSet = new();
        openSet.Enqueue(start, 0); // Kanske inte 0 ???

        // For node n, cameFrom[n] is the node immediately preceding it on the cheapest path from the start
        // to n currently known.
        Dictionary<string, Position<int>> cameFrom = new();

        // For node n, gScore[n] is the cost of the cheapest path from start to n currently known.
        Dictionary<string, int> gScore = new();
        gScore.Add(start.ToString(), 0);

        // For node n, fScore[n] := gScore[n] + h(n). fScore[n] represents our current best guess as to
        // how cheap a path could be from start to finish if it goes through n.
        Dictionary<string, int> fScore = new();
        int hScore = Heuristic(start, goal, hMultiple);
        fScore.Add(start.ToString(), hScore);

        while (openSet.Count > 0)
        {
            // This operation can occur in O(Log(N)) time if openSet is a min-heap or a priority queue
            Position<int> current = openSet.Dequeue();
            if (current == goal)
            {
                return ReconstructPath(cameFrom, current);
            }

            foreach (var neighbor in current.Neighbors(false))
            {
                if (!neighbor.InRange(0, map.GetLength(1) - 1, 0, map.GetLength(0) - 1)) continue;
                if (cameFrom.TryGetValue(current.ToString(), out Position<int> f))
                {
                    if (f == neighbor) continue;
                }

                // d(current,neighbor) is the weight of the edge from current to neighbor

                if (!gScore.TryGetValue(current.ToString(), out int currentGScore))
                {
                    currentGScore = int.MaxValue;
                }

                // tentative_gScore is the distance from start to the neighbor through current
                int tentative_gScore = currentGScore + map[neighbor.Y, neighbor.X];

                if (!gScore.TryGetValue(neighbor.ToString(), out int neighborGScore))
                {
                    neighborGScore = int.MaxValue;
                }
                if (tentative_gScore < neighborGScore)
                {

                    // This path to neighbor is better than any previous one. Record it!
                    if (cameFrom.ContainsKey(neighbor.ToString()))
                    {
                        cameFrom[neighbor.ToString()] = current;
                    }
                    else cameFrom.Add(neighbor.ToString(), current);

                    if (gScore.ContainsKey(neighbor.ToString()))
                    {
                        gScore[neighbor.ToString()] = tentative_gScore;
                    }
                    else gScore.Add(neighbor.ToString(), tentative_gScore);

                    int nHScore = Heuristic(neighbor, goal, hMultiple);
                    int nFScore = tentative_gScore + nHScore;
                    if (fScore.ContainsKey(neighbor.ToString()))
                    {
                        fScore[neighbor.ToString()] = tentative_gScore + nFScore;
                    }
                    else fScore.Add(neighbor.ToString(), tentative_gScore + nFScore);

                    if (!openSet.UnorderedItems.Select(x => x.Element).Contains(neighbor))
                    {
                        openSet.Enqueue(neighbor, nFScore);
                    }
                }
            }
        }
        throw new Exception("Open set is empty but goal was never reached");
    }

    private static int Heuristic(Position<int> a, Position<int> b, int multiple)
    {
        return (Math.Abs(a.DistanceX(b)) + Math.Abs(a.DistanceY(b))) * multiple;
    }
    private static List<Position<int>> ReconstructPath(Dictionary<string, Position<int>> cameFrom, Position<int> current)
    {
        List<Position<int>> total_path = new() { current };
        while (cameFrom.ContainsKey(current.ToString()))
        {
            current = cameFrom[current.ToString()];
            total_path.Insert(0, current);
        }
        return total_path;
    }

    private static bool IsToStright(int maxInStrightLine, Position<int> neighbor, Position<int> current, Dictionary<string, Position<int>> cameFrom)
    {
        if (current.X == neighbor.X)
        {
            bool toStright = true;
            Position<int> from = new(current.X, current.Y);
            for (int i = 0; i < maxInStrightLine - 1; i++)
            {
                if (cameFrom.TryGetValue(from.ToString(), out Position<int> next))
                {
                    if (from.X != next.X)
                    {
                        toStright = false;
                        break;
                    }
                    from = new(next.X, next.Y);
                }
                else
                {
                    toStright = false;
                    break;
                }
            }
            if (toStright) return true;
        }
        if (current.Y == neighbor.Y)
        {
            bool toStright = true;
            for (int i = 0; i < maxInStrightLine - 1; i++)
            {
                Position<int> from = new(current.X, current.Y);
                if (cameFrom.TryGetValue(from.ToString(), out Position<int> next))
                {
                    if (from.Y != next.Y)
                    {
                        toStright = false;
                        break;
                    }
                    from = new(next.X, next.Y);
                }
                else
                {
                    toStright = false;
                    break;
                }
            }
            if (toStright) return true; ;
        }
        return false;
    }
}
