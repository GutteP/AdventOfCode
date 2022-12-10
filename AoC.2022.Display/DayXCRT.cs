using AoC._2022.DayX;

namespace AoC._2022.Display;

public static class DayXCRT
{
    public static void Display(string path)
    {
        HandheldRepair day = new();
        List<(VideoSystem.InsType Ins, int Num)> instructions = day.Transformer(path);
        VideoSystem sys = new VideoSystem();
        sys.Process(instructions);
        Console.WriteLine(sys.CRT[0]);
        Console.WriteLine(sys.CRT[1]);
        Console.WriteLine(sys.CRT[2]);
        Console.WriteLine(sys.CRT[3]);
        Console.WriteLine(sys.CRT[4]);
        Console.WriteLine(sys.CRT[5]);
        Console.WriteLine();
    }
}
