using FluentAssertions;
using System.Security;

namespace AoC._2024;

public class D06
{
    public int? PartOne(string inputPath)
    {
        List<string> input = InputReader.ReadLines(inputPath);
        List<string> visited = new();
        (int A, int B, int Dir)? current = input.FindStart();
        visited.Add($"{current.Value.A},{current.Value.B}");
        while (true)
        {
            current = input.Walk(current.Value);
            if (current == null) 
            {
                break;
            }
            visited.Add($"{current.Value.A},{current.Value.B}");
        }


        return visited.Distinct().Count();
    }


    public int? PartTwo(string inputPath)
    {
        List<string> input = InputReader.ReadLines(inputPath);
        
        int count = 0;
        for (int i = 0; i < input.Count; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == '.')
                {
                    HashSet<string> visited = new();
                    (int A, int B, int Dir)? current = input.FindStart();
                    visited.Add($"{current.Value.A},{current.Value.B},{current.Value.Dir}");

                    while (true)
                    {
                        current = input.Walk(current.Value, (i, j));
                        if (current == null)
                        {
                            break;
                        }
                        if (!visited.Add($"{current.Value.A},{current.Value.B},{current.Value.Dir}"))
                        {
                            count++;
                            break;
                        }
                    }
                }
            }
        }

        return count;
    }
}

public static class D06Extensions
{
    public static (int A, int B, int D)? Walk(this List<string> map, (int A, int B, int Dir) current, (int A, int B)? newBlock = null)
    {
        try
        {
            if (current.Dir == 3)
            {
                if(map[current.A + 1][current.B] != '#' && !Blocked((current.A+1,current.B), newBlock))
                {
                    return (current.A+1,  current.B, current.Dir);
                }
                else
                {
                    current.Dir = 4;
                    return Walk(map, current, newBlock);
                }

            }
            if (current.Dir == 2)
            {
                if (map[current.A][current.B+1] != '#' && !Blocked((current.A, current.B+1), newBlock))
                {
                    return (current.A, current.B+1, current.Dir);
                }
                else
                {
                    current.Dir = 3;
                    return Walk(map, current, newBlock);
                }
            }
            if (current.Dir == 1)
            {
                if (map[current.A-1][current.B] != '#' && !Blocked((current.A - 1, current.B), newBlock))
                {
                    return (current.A-1, current.B, current.Dir);
                }
                else
                {
                    current.Dir = 2;
                    return Walk(map, current, newBlock);
                }
            }
            if (current.Dir == 4)
            {
                if (map[current.A][current.B-1] != '#' && !Blocked((current.A, current.B-1), newBlock))
                {
                    return (current.A, current.B-1, current.Dir);
                }
                else
                {
                    current.Dir = 1;
                    return Walk(map, current, newBlock);
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        catch (Exception)
        {
            return null;
        }
        
    }

    public static bool Blocked((int A, int B) current, (int A, int B)? newBlock)
    {
        if (newBlock == null) return false;
        if (newBlock.Value.A == current.A && newBlock.Value.B == current.B) return true;
        return false;
    }

    public static (int A, int B, int Dir) FindStart(this List<string> map)
    {
        for (int i = 0; i < map.Count; i++)
        {
            for (global::System.Int32 j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == '^') return (i, j, 1);
            }
        }
        throw new InvalidOperationException();
    }

}