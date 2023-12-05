namespace AoC._2023._05;

public class SeedMapping : IAoCDay<long>
{
    public DayRunner<long> Runner()
    {
        return new DayRunner<long>(new Runner<List<MapMap>, long>(Transformer, Solve), new Runner<List<MapMap>, long>(Transformer, Solve2));
    }

    private List<MapMap> Transformer(string path)
    {
        List<long> seeds = new();
        List<MapMap> maps = new();
        MapMap map = default;
        foreach (string line in InputReader.ReadLines(path))
        {

            if (seeds.Count == 0)
            {
                seeds = line.SplitOn(Seperator.Colon)[1].Trim().SplitOn(Seperator.Space).Select(x => long.Parse(x)).ToList();
            }
            else if (string.IsNullOrWhiteSpace(line))
            {
                if (map != null) maps.Add(map);
            }
            else if (line.Contains(':'))
            {
                map = new();
            }
            else
            {
                //Ranges
                List<long> numbers = line.SplitOn(Seperator.Space).Select(x => long.Parse(x)).ToList();
                map.AddRanges(numbers[1], numbers[0], numbers[2]);
            }
        }
        if (map != null) maps.Add(map);
        maps[0].Seeds = seeds;
        return maps;
    }

    private long Solve(List<MapMap> maps)
    {
        List<long> seeds = maps[0].Seeds;
        List<(long seed, long location)> seedToLocation = new();
        foreach (long seed in seeds)
        {
            long currentNumber = seed;
            foreach (MapMap map in maps)
            {
                currentNumber = map.Map(currentNumber);
            }
            seedToLocation.Add((seed, currentNumber));
        }

        return seedToLocation.Min(x => x.location);
    }
    private long Solve2(List<MapMap> maps)
    {
        List<long> seeds = maps[0].Seeds;
        long currentMin = long.MaxValue;
        try
        {
            for (int i = 0; i < seeds.Count - 1; i += 2)
            {
                for (int j = 0; j < seeds[i + 1] - 1; j++)
                {
                    long currentNumber = seeds[i] + j;
                    foreach (MapMap map in maps)
                    {
                        currentNumber = map.Map(currentNumber);
                    }

                    if (currentNumber < currentMin) currentMin = currentNumber;
                }
            }
        }
        catch (Exception)
        {
            return currentMin - (currentMin * 2);
        }

        return currentMin;
    }

    public class MapMap
    {
        public MapMap()
        {
            Sources = new();
            Destinations = new();
            Lengths = new();
        }
        public void AddRanges(long source, long destination, long length)
        {
            Sources.Add(source);
            Destinations.Add(destination);
            Lengths.Add(length);
        }

        public List<long> Sources { get; }
        public List<long> Destinations { get; }
        public List<long> Lengths { get; set; }
        public List<long> Seeds { get; set; }
        public long Map(long number)
        {
            for (int i = 0; i < Sources.Count; i++)
            {
                if (number >= Sources[i] && number <= Sources[i] + Lengths[i] - 1)
                {
                    long diff = number - Sources[i];
                    return Destinations[i] + diff;
                }
            }
            return number;
        }
    }
}
