namespace AoC._2022.Day6;

public class PacketProcessor : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<string, int, int>(Receiver, SteamProcessing, 4), new Runner<string, int, int>(Receiver, SteamProcessing, 14));
    }

    private string Receiver(string path)
    {
        return InputReader.ReadLines(path).First();
    }

    private int SteamProcessing(string stream, int numberOfCharacters)
    {
        for (int i = numberOfCharacters; i < stream.Length; i++)
        {
            if (stream[(i - numberOfCharacters)..i].Distinct().Count() == numberOfCharacters) return i;
        }
        throw new ArgumentException("Not an elf communication stream");
    }
}
