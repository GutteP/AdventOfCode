namespace AoC._2022.Day10;

public class HandheldRepair : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<List<(VideoSystem.InsType Ins, int Num)>, int>(Transformer, PartOne), null);
    }

    public List<(VideoSystem.InsType Ins, int Num)> Transformer(string path)
    {
        var input = InputReader.ReadLines(path);

        List<(VideoSystem.InsType Ins, int Num)> result = new();
        foreach (var ins in input)
        {
            var splittedInstruction = ins.SplitOn(Seperator.Space);
            if (splittedInstruction[0] == "addx")
            {
                result.Add((VideoSystem.InsType.AddX, int.Parse(splittedInstruction[1])));
            }
            else result.Add((VideoSystem.InsType.Noop, 0));
        }
        return result;
    }

    private int PartOne(List<(VideoSystem.InsType Ins, int Num)> instructions)
    {
        VideoSystem sys = new();
        sys.Process(instructions);
        int sum = sys.CycleValues[20] + sys.CycleValues[60] + sys.CycleValues[100] + sys.CycleValues[140] + sys.CycleValues[180] + sys.CycleValues[220];

        return sum;
    }

}


public class VideoSystem
{
    public VideoSystem()
    {
        Cycle = 0;
        X = 1;
        CycleValues = new List<int> { 0 };
        CRT = new() { "", "", "", "", "", "" };
    }
    public List<string> CRT { get; set; }
    public List<int> CycleValues { get; private set; }
    public int Cycle { get; private set; }
    public int X { get; private set; }

    public void Process((InsType Ins, int Num) instruction)
    {
        if (instruction.Ins == InsType.Noop)
        {
            AddCycle();
        }
        else
        {
            AddCycle();
            AddCycle();
            X += instruction.Num;
        }
    }
    public void Process(IEnumerable<(InsType Ins, int Num)> instructions)
    {
        foreach (var ins in instructions)
        {
            Process(ins);
        }
    }

    private void AddCycle()
    {
        Cycle++;
        Draw();
        CycleValues.Add(Cycle * X);
    }

    private void Draw()
    {
        var dp = DrawPosition();
        if (X == dp.Pos || X + 1 == dp.Pos || X - 1 == dp.Pos)
        {
            CRT[dp.Row] += "#";
        }
        else CRT[dp.Row] += ".";
    }

    private (int Row, int Pos) DrawPosition()
    {
        int r = 0;
        int p = Cycle - 1;
        while (p > 39)
        {
            p -= 40;
            r++;
        }
        return (r, p);
    }


    public enum InsType
    {
        AddX,
        Noop
    }
}
