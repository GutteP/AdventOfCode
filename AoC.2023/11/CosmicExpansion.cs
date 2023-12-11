using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC._2023._11;

public class CosmicExpansion : IAoCDay<long>
{
    public DayRunner<long> Runner()
    {
        return new DayRunner<long>(new Runner<List<Position<long>>, long, long>(ReadStarMap, ShortestPath, 1), new Runner<List<Position<long>>, long, long>(ReadStarMap, ShortestPath, 999999));
    }

    public List<Position<long>> ReadStarMap(string path)
    {
        List<string> galaxy = InputReader.ReadLines(path);
        List<Position<long>> galaxyPositions = new();
        for(int y = 0;y < galaxy.Count; y++)
        {
            for(int x = 0;x < galaxy[y].Length; x++)
            {
                if (galaxy[y][x] != '.') galaxyPositions.Add(new Position<long>(x, y));
            }
        }
        return galaxyPositions;
    }

    public long ShortestPath(List<Position<long>> galaxys, long expansion)
    {
        galaxys = Expande(galaxys, expansion);
        long sum = 0;
        for (int i = 0; i < galaxys.Count; i++)
        {
            for (int j = i+1; j < galaxys.Count; j++)
            {
                var distance = galaxys[i].Distance(galaxys[j]);
                sum += (Math.Abs(distance.X) + Math.Abs(distance.Y));
            }
        }
        return sum;
    }

    private List<Position<long>> Expande(List<Position<long>> galaxys, long expansion) 
    {
        for (long y = 0; y <= galaxys.Max(g => g.Y); y++)
        {
            if(!galaxys.Any(g => g.Y == y))
            {
                for(int i = 0; i < galaxys.Count; i++)
                {
                    if (galaxys[i].Y > y) galaxys[i].Y += expansion;
                }
                y += expansion;
            }
        }
        for (long x = 0; x <= galaxys.Max(g => g.X); x++)
        {
            if (!galaxys.Any(g => g.X == x))
            {
                for (int i = 0; i < galaxys.Count; i++)
                {
                    if (galaxys[i].X > x) galaxys[i].X += expansion;
                }
                x += expansion;
            }
        }
        return galaxys;
    }
}

