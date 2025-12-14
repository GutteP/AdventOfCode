using AoC.Common;

namespace AoC._2025;

public class D09
{
    public long? PartOne(string inputPath, int? option1 = null)
    {
        List<Position<long>> redTiles = InputReader.ReadLines(inputPath).Select(x => new Position<long>(long.Parse(x.Split(',', StringSplitOptions.TrimEntries)[0]), long.Parse(x.Split(',', StringSplitOptions.TrimEntries)[1]))).ToList();
        List<(long, Position<long>, Position<long>)> areas = Areas(redTiles);
        return areas.Max(x => x.Item1);
    }



    private List<(long, Position<long>, Position<long>)> Areas(List<Position<long>> redTiles)
    {
        List<(long, Position<long>, Position<long>)> areas = [];
        //foreach (var p1 in redTiles)
        //{
        //    foreach (var p2 in redTiles)
        //    {
        //        //if(areas.Any(x => x.Item2 == p2 && x.Item3 == p1))
        //        //{
        //        //    continue;
        //        //}
        //        var distance = p1.Distance(p2);
        //        areas.Add(((Math.Abs(distance.X) + 1) * (Math.Abs(distance.Y) + 1), p1,p2));
        //    }
        //}
        for (var i = 0; i < redTiles.Count; i++)
        {
            for (var j = i+1; j < redTiles.Count; j++)
            {
                var distance = redTiles[i].Distance(redTiles[j]);
                areas.Add(((Math.Abs(distance.X) + 1) * (Math.Abs(distance.Y) + 1), redTiles[i], redTiles[j]));
            }
        }
        return areas.OrderByDescending(x => x.Item1).ToList();
    }

    public long? PartTwo(string inputPath)
    {
        List<Position<long>> redTiles = InputReader.ReadLines(inputPath).Select(x => new Position<long>(long.Parse(x.Split(',', StringSplitOptions.TrimEntries)[0]), long.Parse(x.Split(',', StringSplitOptions.TrimEntries)[1]))).ToList();
        List<(long Area, Position<long> A, Position<long> B)> areas = Areas(redTiles);

        foreach (var a in areas)
        {
            if (a.Area < 1)
            {
                continue;
            }
            bool cornersInside = true;
            List<Position<long>> poly = [a.A, new Position<long>(a.A.X, a.B.Y), a.B, new Position<long>(a.B.X, a.A.Y)];

            foreach (var p in poly)
            {
                if(!redTiles.InclusiveInsidePolygon(p))
                {
                    cornersInside = false;
                    break;
                }
            }

            if (cornersInside)
            {
                bool fullPollyInside = true;
                var fullPoly = AddLines(poly);
                foreach (var p in fullPoly)
                {
                    if (!redTiles.InclusiveInsidePolygon(p))
                    {
                        fullPollyInside = false;
                        break;
                    }
                }
                if (fullPollyInside)
                {
                    return a.Area;
                }
            }
        }
        return null;
    }

    private List<Position<long>> AddLines(List<Position<long>> redTiles) 
    {
        List<Position<long>> redAndGreedTiles = [];

        for (int i = 0; i < redTiles.Count; i++)
        {
            int j = i + 1;
            if (i == redTiles.Count - 1)
            {
                j = 0;
            }

            if (redTiles[i].X < redTiles[j].X)
            {
                var greenTile = redTiles[i].Copy();
                redAndGreedTiles.Add(redTiles[i]);
                while (true)
                {
                    greenTile = greenTile.CopyAndMove(Direction.Right, 1);

                    if (redTiles[j].X == greenTile.X)
                    {
                        break;
                    }
                    redAndGreedTiles.Add(greenTile);
                }
            }
            else if (redTiles[i].X > redTiles[j].X)
            {
                var greenTile = redTiles[i].Copy();
                redAndGreedTiles.Add(redTiles[i]);
                while (true)
                {
                    greenTile = greenTile.CopyAndMove(Direction.Left, 1);

                    if (redTiles[j].X == greenTile.X)
                    {
                        break;
                    }
                    redAndGreedTiles.Add(greenTile);
                }
            }
            else if (redTiles[i].Y < redTiles[j].Y)
            {
                var greenTile = redTiles[i].Copy();
                redAndGreedTiles.Add(redTiles[i]);
                while (true)
                {
                    greenTile = greenTile.CopyAndMove(Direction.Down, 1);

                    if (redTiles[j].Y == greenTile.Y)
                    {
                        break;
                    }
                    redAndGreedTiles.Add(greenTile);
                }
            }
            else if (redTiles[i].Y > redTiles[j].Y)
            {
                var greenTile = redTiles[i].Copy();
                redAndGreedTiles.Add(redTiles[i]);
                while (true)
                {
                    greenTile = greenTile.CopyAndMove(Direction.Up, 1);

                    if (redTiles[j].Y == greenTile.Y)
                    {
                        break;
                    }
                    redAndGreedTiles.Add(greenTile);
                }
            }
        }
        return redAndGreedTiles;
    }
}
