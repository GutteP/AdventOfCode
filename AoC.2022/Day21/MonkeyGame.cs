namespace AoC._2022.Day21;

public class MonkeyGame : IAoCDay<double>
{
    public DayRunner<double> Runner()
    {
        return new DayRunner<double>(new Runner<Dictionary<string, MonkeyGameTask>, double>(Transformer, Solve), new Runner<Dictionary<string, MonkeyGameTask>, double>(Transformer, FindTheEqualizer));
    }

    private Dictionary<string, MonkeyGameTask> Transformer(string path)
    {
        Dictionary<string, MonkeyGameTask> tasks = new();
        foreach (string rTask in InputReader.ReadLines(path))
        {
            string[] sTask = rTask.Split(' ');
            if (sTask.Length == 2)
            {
                string name = sTask[0].Trim(':');
                tasks.Add(name, new MonkeyGameTask(name, int.Parse(sTask[1])));
            }
            else if (sTask.Length == 4)
            {
                string name = sTask[0].Trim(':');
                tasks.Add(name, new MonkeyGameTask(name, sTask[1], sTask[2][0], sTask[3]));
            }
            else throw new Exception("Konstig längds på den raden..");
        }
        foreach (var key in tasks.Keys)
        {
            if (tasks[key].AFrom != null)
            {
                if (tasks[tasks[key].AFrom].Result != null)
                {
                    tasks[key].A = tasks[tasks[key].AFrom].Result;
                }
                else
                {
                    tasks[tasks[key].AFrom].WaitingForResult.Add(key);
                }
            }
            if (tasks[key].BFrom != null)
            {
                if (tasks[tasks[key].BFrom].Result != null)
                {
                    tasks[key].B = tasks[tasks[key].BFrom].Result;
                }
                else
                {
                    tasks[tasks[key].BFrom].WaitingForResult.Add(key);
                }
            }
        }
        return tasks;
    }

    private double Solve(Dictionary<string, MonkeyGameTask> tasks)
    {
        foreach (string key in ReadyToYell(tasks))
        {
            double number = tasks[key].Yell();
            foreach (var waiting in tasks[key].WaitingForResult)
            {
                if (tasks[waiting].AFrom == key)
                {
                    tasks[waiting].A = number;
                }
                if (tasks[waiting].BFrom == key)
                {
                    tasks[waiting].B = number;
                }
            }
        }

        if (tasks["root"].Result != null) return tasks["root"].Result.Value;
        else return Solve(tasks);
    }

    private double FindTheEqualizer(Dictionary<string, MonkeyGameTask> tasks)
    {
        tasks["root"].Operator = '-';
        long from = 0;
        long to = 10000000000000;

        while (true)
        {
            List<(double diff, long number)> values = new();
            foreach (long num in Spread.Gradient(from, to, 100))
            {
                tasks = ChangeHumansNumber(tasks, num);
                var clone = CloneTasks(tasks);
                if (Solve(clone) == 0)
                {
                    return num;
                }
                values.Add((Math.Abs(clone["root"].A.Value - clone["root"].B.Value), num));
            }
            values = values.OrderBy(x => x.diff).ToList();
            if (values[1].number < values[0].number)
            {
                from = values[1].number;
                to = values[2].number;
            }
            else
            {
                from = values[2].number;
                to = values[1].number;
            }
        }

        throw new Exception("Hittade inget nummber som resulterade i 0");
    }

    private Dictionary<string, MonkeyGameTask> CloneTasks(Dictionary<string, MonkeyGameTask> tasks)
    {
        Dictionary<string, MonkeyGameTask> clone = new();
        foreach (string key in tasks.Keys)
        {
            clone.Add(key, tasks[key].Clone());
        }
        return clone;
    }

    private Dictionary<string, MonkeyGameTask> ChangeHumansNumber(Dictionary<string, MonkeyGameTask> tasks, double number)
    {
        tasks["humn"].Result = number;
        foreach (string key in tasks.Keys)
        {
            if (tasks[key].AFrom == "humn")
            {
                tasks[key].A = number;
            }
            if (tasks[key].BFrom == "humn")
            {
                tasks[key].B = number;
            }
        }
        return tasks;
    }

    private List<string> ReadyToYell(Dictionary<string, MonkeyGameTask> tasks)
    {
        List<string> result = new();
        foreach (string key in tasks.Keys)
        {
            if (tasks[key].Result == null)
            {
                if (tasks[key].Ready())
                {
                    result.Add(key);
                }
            }
        }
        return result;
    }
}

public class MonkeyGameTask
{
    public MonkeyGameTask(string name, double result)
    {
        Name = name;
        Result = result;
        WaitingForResult = new();
    }
    public MonkeyGameTask(string name, string aFrom, char op, string bFrom)
    {
        Name = name;
        AFrom = aFrom;
        Operator = op;
        BFrom = bFrom;
        WaitingForResult = new();
    }
    public string Name { get; set; }
    public double? Result { get; set; }
    public string AFrom { get; set; }
    public double? A { get; set; }
    public string BFrom { get; set; }
    public double? B { get; set; }
    public List<string> WaitingForResult { get; set; }
    public char Operator { get; set; }

    public bool Ready()
    {
        if (Result != null) throw new Exception("Has result..");
        return A != null && B != null;
    }

    public double Yell()
    {
        if (A == null || B == null) throw new Exception("Cant yell...");
        if (Operator == '+')
        {
            Result = A + B;
        }
        else if (Operator == '-')
        {
            Result = A - B;
        }
        else if (Operator == '/')
        {
            Result = A / B;
        }
        else if (Operator == '*')
        {
            Result = A * B;
        }
        else throw new Exception($"{Name} has invalid operator: {Operator}");

        return Result.Value;
    }

    public MonkeyGameTask Clone()
    {
        MonkeyGameTask t = null;
        if (Result != null) t = new(Name, Result.Value);
        else t = new(Name, AFrom, Operator, BFrom);
        t.A = A;
        t.B = B;
        t.WaitingForResult = WaitingForResult;
        return t;
    }
}
