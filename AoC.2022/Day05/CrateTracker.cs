namespace AoC._2022.Day05;

public class CrateTracker : IAoCDay<string>
{
    public DayRunner<string> Runner()
    {
        return new DayRunner<string>(
            new Runner<(List<Stack<char>> stacks, List<(int move, int from, int to)> instructions), string>(Transformer, CrateTracker9000),
            new Runner<(List<Stack<char>> stacks, List<(int move, int from, int to)> instructions), string>(Transformer, CrateTracker9001)
            );
    }

    private (List<Stack<char>> stacks, List<(int move, int fromIndex, int toIndex)> instructions) Transformer(string path)
    {
        var input = InputReader.ReadLines(path).ToArray();

        List<(int move, int from, int to)> instructions = new();
        List<Stack<char>> stacks = new();

        int breakIndex = input.ToList().FindIndex(string.IsNullOrWhiteSpace);
        string[] rawStacks = input[..breakIndex];
        string[] rawInstructions = input[(breakIndex + 1)..];

        for (int i = rawStacks.Length - 2; i >= 0; i--)
        {
            List<string> content = rawStacks[i].SplitOn(Seperator.Space).Trim('[', ']');
            int stackIndex = 0;
            for (int j = 0; j < content.Count; j++)
            {
                if (string.IsNullOrEmpty(content[j]))
                {
                    j += 3;
                    stackIndex++;
                    continue;
                }

                if (stacks.ElementAtOrDefault(stackIndex) is null) stacks.Add(new Stack<char>());
                stacks[stackIndex].Push(content[j][0]);
                stackIndex++;
            }
        }

        foreach (string rawInstruction in rawInstructions)
        {
            List<string> instuction = rawInstruction.SplitOn(Seperator.Space);
            instructions.Add((int.Parse(instuction[1]), int.Parse(instuction[3]) - 1, int.Parse(instuction[5]) - 1));
        }

        return (stacks, instructions);
    }

    private string CrateTracker9000((List<Stack<char>> stacks, List<(int move, int fromIndex, int toIndex)> instructions) input)
    {
        foreach (var instruction in input.instructions)
        {
            for (int i = 0; i < instruction.move; i++)
            {
                input.stacks[instruction.toIndex].Push(input.stacks[instruction.fromIndex].Pop());
            }
        }

        string result = "";
        foreach (var item in input.stacks)
        {
            result += item.Pop();
        }

        return result;
    }
    private string CrateTracker9001((List<Stack<char>> stacks, List<(int move, int fromIndex, int toIndex)> instructions) input)
    {
        foreach (var ins in input.instructions)
        {
            Stack<char> temp = new();
            for (int i = 0; i < ins.move; i++)
            {
                temp.Push(input.stacks[ins.fromIndex].Pop());
            }
            while (temp.Any())
            {
                input.stacks[ins.toIndex].Push(temp.Pop());
            }
        }

        string result = "";
        foreach (var item in input.stacks)
        {
            result += item.Pop();
        }

        return result;
    }
}
