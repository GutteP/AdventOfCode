using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Common;

public static class DistanceExtension
{
    public static int ManhattanDistance(this Position<int> a, Position<int> b)
    {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }

    /// <summary>
    /// Euclidean distance or straight-line distance
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int StraightLineDistance(this Position3D<int> a, Position3D<int> b)
    {
        int deltaX = a.X - b.X;
        int deltaY = a.Y - b.Y;
        int deltaZ = a.Z - b.Z;
        return (int)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
    }
    public static long StraightLineDistance(this Position3D<long> a, Position3D<long> b)
    {
        long deltaX = a.X - b.X;
        long deltaY = a.Y - b.Y;
        long deltaZ = a.Z - b.Z;
        return (long)Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
    }
}


