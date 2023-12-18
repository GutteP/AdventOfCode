using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC._2023._18;

public class LavaductLagoon : IAoCDay<long>
{
    public DayRunner<long> Runner()
    {
        return new DayRunner<long>(new Runner<List<DigInstruction>, long>(Transformer, Solve), new Runner<List<DigInstruction>, long>(Transformer, PartTwo));
    }

    private List<DigInstruction> Transformer(string path)
    {
        List<DigInstruction> instructions = new();
        foreach (string row in InputReader.ReadLines(path))
        {
            var sp = row.Trim().Split(' ');
            instructions.Add(new(DirFromLetter(sp[0][0]), int.Parse(sp[1]), sp[2]));
        }
        return instructions;
    }

    private long Solve(List<DigInstruction> instructions)
    {
        List<Position<int>> polygon = new() { new(0, 0) };
        foreach(DigInstruction dI in instructions)
        {
            polygon.Add(polygon.Last().CopyAndMove(dI.Direction, dI.Steps));
        }
        polygon.RemoveAt(polygon.Count - 1);

        int area = AoCShapes.AreaOfSimpelPolygon(polygon, true);
        return area;
    }
    private long PartTwo(List<DigInstruction> instructions)
    {
        List<Position<long>> polygon = new() { new(0, 0) };
        foreach (DigInstruction dI in instructions)
        {
            int steps = Convert.ToInt32(dI.Color[2..7], 16);
            Direction dir = DirFromNum(dI.Color[7]);
            polygon.Add(polygon.Last().CopyAndMove(dir, steps));
        }
        polygon.RemoveAt(polygon.Count - 1);
        long area = AoCShapes.AreaOfSimpelPolygon(polygon, true);
        return area;
    }
    public bool PartOfOutline(Position<int> p, List<Position<int>[]> lines)
    {
        foreach (var line in lines)
        {
            var xOrdered = line.OrderBy(x => x.X);
            if (p.Y == line[0].Y && p.Y == line[1].Y && p.X >= xOrdered.ElementAt(0).X && p.X <= xOrdered.ElementAt(1).X)
            {
                return true;
            }
            var yOrdered = line.OrderBy(y => y.Y);
            if (p.X == line[0].X && p.X == line[1].X && p.Y >= yOrdered.ElementAt(0).Y && p.Y <= yOrdered.ElementAt(1).Y)
            {
                return true;
            }
        }
        return false;
    }
    public Direction DirFromLetter(char letter)
    {
        return letter switch
        {
            'R' => Direction.Right,
            'D' => Direction.Down,
            'L' => Direction.Left,
            'U' => Direction.Up,
            _ => throw new ArgumentException("Not a dir letter")
        };
    }
    public Direction DirFromNum(char num)
    {
        return num switch
        {
            '0' => Direction.Right,
            '1' => Direction.Down,
            '2' => Direction.Left,
            '3' => Direction.Up,
            _ => throw new ArgumentException("Not a dir letter")
        };
    }

    public record DigInstruction(Direction Direction, int Steps, string Color);
}
