using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Common.Test;

public class AStarTests
{
    [Fact]
    public void SimpleTest_1()
    {
        List<List<int>> toBeMap = new()
        {
            new(){ 0,5,5,1,1 },
            new(){ 1,1,1,0,1 },
            new(){ 0,1,1,1,0 },
            new(){ 0,0,1,1,0 },
            new(){ 8,0,0,0,1 },
        };
        var map = toBeMap.Map2D();
        var result = AStarRunner.AStar(new Position<int>(0, 0), new Position<int>(map.GetLength(1) - 1, map.GetLength(0) - 1), 1, map);
        CalculateWeight(result, map).Should().Be(2);
    }

    [Fact]
    public void SimpleTest_2()
    {
        List<List<int>> toBeMap = new()
        {
            new(){ 1,5,5,5,5,5,5,5,5 },
            new(){ 1,5,5,5,5,5,5,5,5 },
            new(){ 1,5,1,1,1,5,5,5,5 },
            new(){ 1,1,1,5,1,5,5,5,5 },
            new(){ 5,5,5,5,1,5,5,5,5 },
            new(){ 5,5,5,5,1,1,1,1,1 },
        };
        var map = toBeMap.Map2D();
        var result = AStarRunner.AStar(new Position<int>(0, 0), new Position<int>(map.GetLength(1) - 1, map.GetLength(0) - 1), 1, map);
        CalculateWeight(result, map).Should().Be(16);
    }

    private int CalculateWeight(List<Position<int>> path, int[,] map)
    {
        int weight = 0;
        foreach (var p in path)
        {
            weight += map[p.Y, p.X];
        }
        return weight;
    }
}
