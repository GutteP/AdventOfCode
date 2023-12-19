using System.Reflection.Metadata.Ecma335;
using static AoC._2023._19.Aplenty;

namespace AoC._2023._19;

public class Aplenty : IAoCDay<long>
{
    public DayRunner<long> Runner()
    {
        return new DayRunner<long>(new Runner<(Dictionary<string, AplentyWorkflow> Workflows, List<AplentyPart> Parts), long>(Transformer, Solve), new Runner<(Dictionary<string, AplentyWorkflow> Workflows, List<AplentyPart> Parts), long>(Transformer, PartTwo));
    }

    private (Dictionary<string, AplentyWorkflow> Workflows, List<AplentyPart> Parts) Transformer(string path)
    {
        Dictionary<string, AplentyWorkflow> workflows = new();
        List<AplentyPart> parts = new();

        bool ratingsTurn = false;
        foreach (var row in InputReader.ReadLines(path))
        {
            if (string.IsNullOrWhiteSpace(row))
            {
                ratingsTurn = true;
                continue;
            }
            if (!ratingsTurn)
            {

                var sp = row.Split('{');
                List<(bool More, char Prop, int V, string Y, string N)> wfParts = new();
                var fsp = sp[1].Split(',');
                foreach (var part in fsp.Select(x => x.Trim().Trim('}').Trim('{')))
                {
                    int colonI = part.IndexOf(':');
                    if (colonI != -1)
                    {
                        int lessI = part.IndexOf('<');
                        if (lessI != -1)
                        {
                            wfParts.Add((false, part[0], int.Parse(part[2..colonI]), part[(colonI + 1)..], string.Empty));
                        }
                        else
                        {
                            wfParts.Add((true, part[0], int.Parse(part[2..colonI]), part[(colonI + 1)..], string.Empty));
                        }
                    }
                    else
                    {
                        wfParts.Add((false, 'q', 0, string.Empty, part));
                    }
                }
                workflows.Add(sp[0], new(sp[0], wfParts));
            }
            else if (ratingsTurn)
            {
                var sp = row.Split(',');
                parts.Add(new(int.Parse(sp[0].Split('=')[1]), int.Parse(sp[1].Split('=')[1]), int.Parse(sp[2].Split('=')[1]), int.Parse(sp[3].Split('=')[1].Trim('}'))));
            }
        }

        return (workflows, parts);
    }

    private long Solve((Dictionary<string, AplentyWorkflow> Workflows, List<AplentyPart> Parts) wfAndP)
    {
        Dictionary<string, AplentyWorkflow> wfs = wfAndP.Workflows;
        List<AplentyPart> Parts = wfAndP.Parts;

        List<AplentyPart> accepted = new();
        foreach (var part in Parts)
        {
            string current = "in";
            while (true)
            {
                var r = wfs[current].Run(part);
                if (r == "A")
                {
                    accepted.Add(part);
                    break;
                }
                if (r == "R") break;
                current = r;
            }
        }
        return accepted.Select(x => x.S + x.X + x.M + x.A).Sum();
    }
    private string A = "A";
    private string R = "R";
    private long PartTwo((Dictionary<string, AplentyWorkflow> Workflows, List<AplentyPart> Parts) wfAndP)
    {
        List<Outcome> outcomes = new();
        foreach (var wf in wfAndP.Workflows.Values)
        {
            outcomes.AddRange(wf.Outcomes());
        }
        var r = RWays("in", outcomes);

        long xmas = 0;
        foreach (var rList in r)
        {
            Outcome limits = new();
            foreach (var o in rList)
            {
                if (o.xMax < limits.xMax) limits.xMax = o.xMax;
                if (o.xMin > limits.xMin) limits.xMin = o.xMin;
                if (o.mMax < limits.mMax) limits.mMax = o.mMax;
                if (o.mMin > limits.mMin) limits.mMin = o.mMin;
                if (o.aMax < limits.aMax) limits.aMax = o.aMax;
                if (o.aMin > limits.aMin) limits.aMin = o.aMin;
                if (o.sMax < limits.sMax) limits.sMax = o.sMax;
                if (o.sMin > limits.sMin) limits.sMin = o.sMin;
            }
            long x = (limits.xMax - limits.xMin);
            long m = (limits.mMax - limits.mMin);
            long a = (limits.aMax - limits.aMin);
            long s = (limits.sMax - limits.sMin);
            long result = x * m * a * s;
            xmas += result;
        }
        return xmas;
    }
    private IEnumerable<IEnumerable<Outcome>> RWays(string first, IEnumerable<Outcome> allOutcomes)
    {
        List<IEnumerable<Outcome>> results = new();

        foreach (Outcome outcome in allOutcomes.Where(x => x.From == first))
        {
            var firstL = new List<Outcome> { outcome };
            if (outcome.To == A)
            {
                results.Add(firstL);
            }
            else
            {
                results.AddRange(RWays(firstL, allOutcomes));
            }
        }
        return results;
    }
    private IEnumerable<IEnumerable<Outcome>> RWays(IEnumerable<Outcome> previous, IEnumerable<Outcome> allOutcomes)
    {
        List<IEnumerable<Outcome>> results = new();
        foreach (Outcome outcome in allOutcomes.Where(x => x.From == previous.Last().To))
        {
            var l = previous.ToList();
            l.Add(outcome);
            if (outcome.To == A)
            {
                results.Add(l);
            }
            else
            {
                results.AddRange(RWays(l, allOutcomes));
            }
        }
        return results;
    }

    public class AplentyWorkflow
    {
        public AplentyWorkflow(string name, List<(bool More, char Prop, int V, string Y, string N)> wf)
        {
            Name = name;
            Wf = wf;
        }
        public string Name { get; set; }
        public List<(bool More, char Prop, int V, string Y, string N)> Wf { get; set; }

        public List<Outcome> Outcomes()
        {
            List<Outcome> outcomes = new();
            for (int i = Wf.Count; i >= 0; i--)
            {
                var o = Outcome.Create(Wf[..i], Name);
                if (o != default) outcomes.Add(o);
            }
            return outcomes;
        }

        public string Run(AplentyPart part)
        {
            string r = null;
            foreach (var wfp in Wf)
            {
                if (!string.IsNullOrEmpty(wfp.N))
                {
                    r = wfp.N;
                }
                else if (wfp.More)
                {
                    r = More(PartProp(part, wfp.Prop), wfp.V, wfp.Y);
                }
                else if (!wfp.More)
                {
                    r = Less(PartProp(part, wfp.Prop), wfp.V, wfp.Y);
                }

                if (r != null) return r;
            }
            return r;
        }

        private int PartProp(AplentyPart part, char p)
        {
            return p switch
            {
                'x' => part.X,
                'm' => part.M,
                'a' => part.A,
                's' => part.S,
                _ => throw new ArgumentException("Not a property")
            };
        }
        private string Less(int pp, int value, string yes)
        {
            return pp < value ? yes : null;
        }
        private string More(int pp, int value, string yes)
        {
            return pp > value ? yes : null;
        }
    }

    public class Outcome
    {

        public static Outcome Create(IEnumerable<(bool More, char Prop, int V, string Y, string N)> wParts, string from)
        {
            string A = "A";
            string R = "R";

            Outcome outcome = new Outcome();
            outcome.From = from;
            int i = 0;
            foreach (var wPart in wParts)
            {
                if (!string.IsNullOrEmpty(wPart.N))
                {
                    if (wPart.N == R) return null;
                    outcome.To = wPart.N;
                }
                else if (i == wParts.Count() - 1)
                {
                    switch (wPart.Prop)
                    {
                        case 'x':
                            if (wPart.More) outcome.xMin = wPart.V > outcome.xMin ? wPart.V : outcome.xMin;
                            else outcome.xMax = wPart.V < outcome.xMax ? wPart.V : outcome.xMax;
                            break;
                        case 'm':
                            if (wPart.More) outcome.mMin = wPart.V > outcome.mMin ? wPart.V : outcome.mMin;
                            else outcome.mMax = wPart.V < outcome.mMax ? wPart.V : outcome.mMax;
                            break;
                        case 'a':
                            if (wPart.More) outcome.aMin = wPart.V > outcome.aMin ? wPart.V : outcome.aMin;
                            else outcome.aMax = wPart.V < outcome.aMax ? wPart.V : outcome.aMax;
                            break;
                        case 's':
                            if (wPart.More) outcome.sMin = wPart.V > outcome.sMin ? wPart.V : outcome.sMin;
                            else outcome.sMax = wPart.V < outcome.sMax ? wPart.V : outcome.sMax;
                            break;
                        default:
                            break;
                    }
                    if (wPart.Y == R) return null;
                    outcome.To = wPart.Y;
                }
                else
                {
                    switch (wPart.Prop)
                    {
                        case 'x':
                            if (!wPart.More) outcome.xMin = wPart.V > outcome.xMin ? wPart.V : outcome.xMin;
                            else outcome.xMax = wPart.V < outcome.xMax ? wPart.V : outcome.xMax;
                            break;
                        case 'm':
                            if (!wPart.More) outcome.mMin = wPart.V > outcome.mMin ? wPart.V : outcome.mMin;
                            else outcome.mMax = wPart.V < outcome.mMax ? wPart.V : outcome.mMax;
                            break;
                        case 'a':
                            if (!wPart.More) outcome.aMin = wPart.V > outcome.aMin ? wPart.V : outcome.aMin;
                            else outcome.aMax = wPart.V < outcome.aMax ? wPart.V : outcome.aMax;
                            break;
                        case 's':
                            if (!wPart.More) outcome.sMin = wPart.V > outcome.sMin ? wPart.V : outcome.sMin;
                            else outcome.sMax = wPart.V < outcome.sMax ? wPart.V : outcome.sMax;
                            break;
                        default:
                            break;
                    }
                }

                i++;
            }
            return outcome;
        }
        public string From { get; set; }
        public string To { get; set; }
        public int xMin { get; set; } = 1;
        public int xMax { get; set; } = 4000;
        public int mMin { get; set; } = 1;
        public int mMax { get; set; } = 4000;
        public int aMin { get; set; } = 1;
        public int aMax { get; set; } = 4000;
        public int sMin { get; set; } = 1;
        public int sMax { get; set; } = 4000;

    }

    public record AplentyPart(int X, int M, int A, int S);
}
