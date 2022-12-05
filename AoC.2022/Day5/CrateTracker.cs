namespace AoC._2022.Day5;

public class CrateTracker/* : IAoCDay*/
{
    //public DayRunner Runner()
    //{
    //    return new DayRunner<string>(new Runner<(List<Stack<char>> stacks, List<(int move, int from, int to)> instructions), string>(Transformer, Track), null)
    //}

    public (List<Stack<char>> stacks, List<(int move, int from, int to)> instructions) Transformer(string path)
    {
        var input = InputReader.ReadLines(path);

        List<(int move, int from, int to)> instructions = new();
        List<Stack<char>> stacks = new();
        List<string> tempStacks = new();

        bool stacksDone = false;
        foreach (string row in input)
        {
            if (string.IsNullOrEmpty(row))
            {
                stacksDone = true;
                continue;
            }
            if (stacksDone)
            {
                List<string> instuction = row.SplitOn(Seperator.Space);
                instructions.Add((int.Parse(instuction[1]), int.Parse(instuction[3]), int.Parse(instuction[5])));
            }
            else
            {
                tempStacks.Add(row);
            }
        }
        tempStacks.Reverse();
        for (int i = 0; i < tempStacks[0].SplitOn(Seperator.Space).Trim().RemoveEmpty().Count; i++)
        {
            stacks.Add(new Stack<char>());
        }

        tempStacks.RemoveAt(0);

        foreach (string s in tempStacks)
        {
            List<string> content = s.SplitOn(Seperator.Space).Trim('[', ']');
            int numOfempty = 0;
            int j = 0;
            for (int i = 0; i < content.Count; i++)
            {
                if (string.IsNullOrEmpty(content[i]))
                {
                    numOfempty++;
                    if (numOfempty == 4)
                    {
                        j++;
                        numOfempty = 0;
                    }
                    continue;
                }
                stacks[j].Push(content[i][0]);
                j++;
            }
        }

        return (stacks, instructions);
    }

    public string Track((List<Stack<char>> stacks, List<(int move, int from, int to)> instructions) input)
    {
        foreach ((int move, int from, int to) ins in input.instructions)
        {
            for (int i = 0; i < ins.move; i++)
            {
                char temp = input.stacks[ins.from - 1].Pop();
                input.stacks[ins.to - 1].Push(temp);
            }
        }
        string result = "";
        foreach (var item in input.stacks)
        {
            result += item.Pop();
        }

        return result;
    }
    public string Track9001((List<Stack<char>> stacks, List<(int move, int from, int to)> instructions) input)
    {
        foreach ((int move, int from, int to) ins in input.instructions)
        {
            Stack<char> temp = new();
            for (int i = 0; i < ins.move; i++)
            {
                temp.Push(input.stacks[ins.from - 1].Pop());

            }
            while (temp.Any())
            {
                input.stacks[ins.to - 1].Push(temp.Pop());
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
