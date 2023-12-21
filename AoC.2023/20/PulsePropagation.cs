namespace AoC._2023._20;

public class PulsePropagation : IAoCDay<long>
{
    public DayRunner<long> Runner()
    {
        return new DayRunner<long>(new Runner<Dictionary<string, IPropagator>, long>(Transformer, Solve), new Runner<Dictionary<string, IPropagator>, long>(Transformer, PartTwo));
    }

    private Dictionary<string, IPropagator> Transformer(string path)
    {
        var input = InputReader.ReadLines(path);
        List<IPropagator> propagators = new();
        propagators.Add(new Broadcaster(input.Single(x => x.StartsWith("broadcaster")).Split(" -> ")[1].Split(", ").Select(x => x.Trim())));
        foreach (var row in input.Where(x => x[0] == '%'))
        {
            var sp = row.Split(" -> ");
            string name = sp[0][1..];
            var d = sp[1].Split(", ").Select(x => x.Trim());
            propagators.Add(new FlipFlop(sp[0][1..], sp[1].Split(", ").Select(x => x.Trim())));

        }
        foreach (var row in input.Where(x => x[0] == '&'))
        {
            var sp = row.Split(" -> ");
            string name = sp[0][1..];
            var d = sp[1].Split(", ").Select(x => x.Trim());
            propagators.Add(new Conjunction(sp[0][1..], sp[1].Split(", ").Select(x => x.Trim())));
        }
        foreach (Conjunction con in propagators.Where(x => x is Conjunction))
        {
            con.SetConnected(propagators.Where(x => x.Destinations.Contains(con.Name)).Select(x => x.Name));
        }
        Dictionary<string, IPropagator> propagatorDict = new();
        propagators.ForEach(x => propagatorDict.Add(x.Name, x));
        return propagatorDict;
    }

    private long Solve(Dictionary<string, IPropagator> propagators)
    {
        int low = 0;
        int high = 0;
        Queue<(string destination, int pulse, string from)> queue = new();
        IPropagator broadcaster = propagators["broadcaster"];
        for (int i = 0; i < 1000; i++)
        {
            low++;
            broadcaster.Receive(("broadcaster", 0, "button"));
            foreach (var pulse in broadcaster.Send())
            {
                if (pulse.pulse == 0) low++;
                else high++;
                queue.Enqueue(pulse);
            };
            while (queue.Count > 0)
            {
                var pulse = queue.Dequeue();
                if (propagators.TryGetValue(pulse.destination, out IPropagator propagator))
                {
                    propagator.Receive(pulse);
                    foreach (var newPuls in propagator.Send())
                    {
                        if (newPuls.pulse == 0) low++;
                        else high++;
                        queue.Enqueue(newPuls);
                    };
                }
            }
        }

        return low * high;
    }
    private const string RX = "rx";
    private long PartTwo(Dictionary<string, IPropagator> propagators)
    {
        long i = 0;
        Queue<(string destination, int pulse, string from)> queue = new(100);
        IPropagator broadcaster = propagators["broadcaster"];
        while (true)
        {
            i++;
            int rx = 0;
            broadcaster.Receive(("broadcaster", 0, "button"));
            foreach (var pulse in broadcaster.Send())
            {
                queue.Enqueue(pulse);
            };
            while (queue.Count > 0)
            {
                var pulse = queue.Dequeue();
                if (propagators.TryGetValue(pulse.destination, out IPropagator propagator))
                {
                    propagator.Receive(pulse);
                    foreach (var newPuls in propagator.Send())
                    {
                        queue.Enqueue(newPuls);
                    };
                }
                else
                {
                    if (pulse.destination == RX)
                    {
                        rx++;
                    }
                }
            }
            if (rx == 1) return i;
        }
        throw new Exception($"rx not found, i: {i}");
    }


    interface IPropagator
    {
        string Name { get; set; }
        void Receive((string destination, int pulse, string from) signal);
        IEnumerable<(string destination, int pulse, string from)> Send();
        List<string> Destinations { get; set; }
    }

    public class Broadcaster : IPropagator
    {
        private int lastReceivedPuls = 0;
        public Broadcaster(IEnumerable<string> destinations)
        {
            Destinations = destinations.ToList();
        }
        public string Name { get; set; } = "broadcaster";
        public List<string> Destinations { get; set; }

        public void Receive((string destination, int pulse, string from) signal)
        {
            lastReceivedPuls = signal.pulse;
        }

        public IEnumerable<(string destination, int pulse, string from)> Send()
        {
            return Destinations.Select(x => (x, lastReceivedPuls, Name));
        }
    }
    public class FlipFlop : IPropagator
    {
        private bool On = false;
        private int toSend = 0;
        public FlipFlop(string name, IEnumerable<string> destinations)
        {
            Name = name;
            Destinations = destinations.ToList();
        }
        public string Name { get; set; }
        public List<string> Destinations { get; set; }
        public void Receive((string destination, int pulse, string from) signal)
        {
            if (signal.pulse == 0)
            {
                On = !On;
                toSend = On ? 1 : 0;
            }
            else toSend = -1;
        }

        public IEnumerable<(string destination, int pulse, string from)> Send()
        {
            if (toSend == -1) return new List<(string destination, int pulse, string from)>();
            return Destinations.Select(x => (x, toSend, Name));
        }
    }
    public class Conjunction : IPropagator
    {
        private Dictionary<string, int> memory;
        public Conjunction(string name, IEnumerable<string> destinations)
        {
            Name = name;
            Destinations = destinations.ToList();
        }
        public string Name { get; set; }
        public List<string> Destinations { get; set; }
        public void SetConnected(IEnumerable<string> connected)
        {
            memory = new();
            foreach (string con in connected)
            {
                memory.Add(con, 0);
            }
        }
        public void Receive((string destination, int pulse, string from) signal)
        {
            if (memory == null) throw new Exception($"Connected is not set in Conjunction {Name}");
            if (signal.from == null) throw new Exception($"Conjunction {Name} must receive sender.");
            memory[signal.from] = signal.pulse;
        }

        public IEnumerable<(string destination, int pulse, string from)> Send()
        {
            if (memory == null) throw new Exception($"Connected is not set in Conjunction {Name}");
            if (memory.Values.All(x => x == 1)) return Destinations.Select(x => (x, 0, Name));
            return Destinations.Select(x => (x, 1, Name));
        }
    }
}
