namespace AoC._2022.TestDay2;

public class SubmarinePilot : IAoCDay
{
    public DayRunner Runner()
    {
        return new DayRunner(new Runner<List<List<string>>>(Transformer, Plotter), new Runner<List<List<string>>>(Transformer, AimPlotter));
    }

    private List<List<string>> Transformer(string path)
    {
        return InputReader.ReadLines(path).SplitOn(Seperator.Space).Trim();
    }

    private int Plotter(List<List<string>> commands)
    {
        int horizontalPosition = 0;
        int depth = 0;
        foreach (List<string> command in commands)
        {
            int steps = int.Parse(command[1]);
            if (command[0] == "forward")
            {
                horizontalPosition += steps;
                continue;
            }
            if (command[0] == "down")
            {
                depth += steps;
                continue;
            }
            if (command[0] == "up")
            {
                depth -= steps;
                continue;
            }
            throw new NotImplementedException(command[0]);
        }
        return horizontalPosition * depth;
    }
    private int AimPlotter(List<List<string>> commands)
    {
        int horizontalPosition = 0;
        int depth = 0;
        int aim = 0;
        foreach (List<string> command in commands)
        {
            int steps = int.Parse(command[1]);
            if (command[0] == "forward")
            {
                horizontalPosition += steps;
                depth += aim * steps;
                continue;
            }
            if (command[0] == "down")
            {
                aim += steps;
                continue;
            }
            if (command[0] == "up")
            {
                aim -= steps;
                continue;
            }
            throw new NotImplementedException(command[0]);
        }
        return horizontalPosition * depth;
    }
}
