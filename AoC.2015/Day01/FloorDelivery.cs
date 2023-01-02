namespace AoC._2015.Day01;

public class FloorDelivery : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<string, int>(Transformer, CountFloors), new Runner<string, int>(Transformer, FirstTimeOnB1));
    }

    private string Transformer(string path)
    {
        return InputReader.ReadLines(path).First();
    }

    private int CountFloors(string input)
    {
        int up = input.Where(x => x == '(').Count();
        return up - (input.Length - up);
    }

    private int FirstTimeOnB1(string input)
    {
        int floor = 0;
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '(') floor++;
            else floor--;
            if (floor == -1) return i + 1;
        }
        throw new Exception("Never on floor -1");

    }
}
