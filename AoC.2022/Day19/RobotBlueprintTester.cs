namespace AoC._2022.Day19;

public class RobotBlueprintTester : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<List<RobotBlueprint>, int, int>(Transformer, Solve, 24), null);
    }

    private List<RobotBlueprint> Transformer(string path)
    {
        List<RobotBlueprint> blueprints = new();
        foreach (string rawBlueprint in InputReader.ReadLines(path))
        {
            blueprints.Add(new RobotBlueprint(rawBlueprint));
        }
        return blueprints;
    }

    private int Solve(List<RobotBlueprint> blueprints, int numberOfMinutes)
    {
        RobotFactory factory = new(blueprints[1], numberOfMinutes);
        for (int i = 0; i < numberOfMinutes; i++)
        {
            factory.Tick();
        }

        return factory.Geodes;
    }
}

public class RobotFactory
{

    public RobotFactory(RobotBlueprint blueprint, int ticks)
    {
        Blueprint = blueprint;
        Ticks = ticks;
        OreRobots++;
    }

    public RobotBlueprint Blueprint { get; set; }
    public int Ticks { get; set; }
    public int OreRobots { get; set; }
    public int ClayRobots { get; set; }
    public int ObsidianRobots { get; set; }
    public int GeodeRobots { get; set; }
    public int Ore { get; set; }
    public int Clay { get; set; }
    public int Obsidian { get; set; }
    public int Geodes { get; set; }
    public int Building { get; set; }
    public void Tick()
    {
        FactoryStart();
        CollectMaterial();
        FactoryEnd();
        Ticks--;
    }



    private void CollectMaterial()
    {
        Ore += OreRobots;
        Clay += ClayRobots;
        Obsidian += ObsidianRobots;
        Geodes += GeodeRobots;
    }
    private void FactoryStart()
    {
        if (Ore >= Blueprint.GeodeRobot.Ore && Obsidian >= Blueprint.GeodeRobot.Obsidian)
        {
            Building = 4;
            Ore -= Blueprint.GeodeRobot.Ore;
            Obsidian -= Blueprint.GeodeRobot.Obsidian;
            return;
        }

        double geodeWeight = ClaculateGeodeWeight();
        double currentObsidianNeed = (Blueprint.ObsidianFactor * OreRobots) - ObsidianRobots;
        double currentClayNeed = (Blueprint.ClayFactor * OreRobots) - ClayRobots;
        double currentOreNeed = 1;

        if (geodeWeight > currentObsidianNeed && geodeWeight > currentClayNeed && geodeWeight > currentOreNeed)
        {
            if (Ore >= Blueprint.GeodeRobot.Ore && Obsidian >= Blueprint.GeodeRobot.Obsidian)
            {
                Building = 4;
                Ore -= Blueprint.GeodeRobot.Ore;
                Obsidian -= Blueprint.GeodeRobot.Obsidian;
            }
            return;
        }

        if (currentObsidianNeed > currentClayNeed && currentObsidianNeed > currentOreNeed)
        {
            if (Ore >= Blueprint.ObsidianRobot.Ore && Clay >= Blueprint.ObsidianRobot.Clay)
            {
                Building = 3;
                Ore -= Blueprint.ObsidianRobot.Ore;
                Clay -= Blueprint.ObsidianRobot.Clay;
            }
            return;
        }

        if (currentClayNeed > currentOreNeed)
        {
            if (Ore >= Blueprint.ClayRobot.Ore)
            {
                Building = 2;
                Ore -= Blueprint.ClayRobot.Ore;
            }
            return;
        }

        if (Ore >= Blueprint.OreRobot.Ore)
        {
            Building = 1;
            Ore -= Blueprint.OreRobot.Ore;
        }
        return;

    }
    private double ClaculateGeodeWeight()
    {
        if (ObsidianRobots == 0) return 0;
        int oreTicks = (int)Math.Ceiling((double)(Blueprint.GeodeRobot.Ore - Ore) / (double)OreRobots);
        int obsidianTicks = (int)Math.Ceiling((double)(Blueprint.GeodeRobot.Obsidian - Obsidian) / (double)ObsidianRobots);
        int ticks = oreTicks > obsidianTicks ? oreTicks : obsidianTicks;
        return 5 - ticks;
    }
    private void FactoryStartOld()
    {
        bool missingOreGeode = false;
        bool missingOreObsidian = false;
        bool missingOreClay = false;
        bool missingClay = false;
        bool missingObsidian = false;

        if (Blueprint.GeodeRobot.Obsidian > Obsidian) missingObsidian = true;
        if (Blueprint.GeodeRobot.Ore > Ore) missingOreGeode = true;
        if (!missingObsidian && !missingOreGeode)
        {
            Building = 4;
            Ore -= Blueprint.GeodeRobot.Ore;
            Obsidian -= Blueprint.GeodeRobot.Obsidian;
            return;
        }
        if (missingObsidian)
        {

            if (Blueprint.ObsidianRobot.Clay > Clay) missingClay = true;
            if (Blueprint.ObsidianRobot.Ore > Ore) missingOreObsidian = true;
            if (!missingOreObsidian && !missingClay)
            {
                Building = 3;
                Ore -= Blueprint.ObsidianRobot.Ore;
                Clay -= Blueprint.ObsidianRobot.Clay;
                return;
            }
        }
        if (missingClay)
        {

            if (Blueprint.ClayRobot.Ore > Ore) missingOreClay = true;
            if (!missingOreClay)
            {
                Building = 2;
                Ore -= Blueprint.ClayRobot.Ore;
                return;
            }
        }
        if (missingOreGeode || missingOreObsidian || missingOreClay)
        {
            if (Blueprint.OreRobot.Ore <= Ore)
            {
                Building = 1;
                Ore -= Blueprint.OreRobot.Ore;
                return;
            }
        }
    }
    private void FactoryEnd()
    {
        if (Building == 0) return;
        if (Building == 1) OreRobots++;
        if (Building == 2) ClayRobots++;
        if (Building == 3) ObsidianRobots++;
        if (Building == 4) GeodeRobots++;
        Building = 0;
    }
}

public class RobotBlueprint
{
    public RobotBlueprint(string blueprint)
    {
        var a = blueprint.Split(':');
        var b = a[0].Split(' ');
        Id = int.Parse(b[1]);
        var c = a[1].Split('.');
        var d = c[0].Split(' ');
        OreRobot = new RobotCost { Ore = int.Parse(d[5]) };
        var e = c[1].Split(' ');
        ClayRobot = new RobotCost { Ore = int.Parse(e[5]) };
        var f = c[2].Split(' ');
        ObsidianRobot = new RobotCost { Ore = int.Parse(f[5]), Clay = int.Parse(f[8]) };
        var g = c[3].Split(' ');
        GeodeRobot = new RobotCost { Ore = int.Parse(g[5]), Obsidian = int.Parse(g[8]) };
        ClayFactor = (double)ObsidianRobot.Clay / (double)((double)(OreRobot.Ore + ClayRobot.Ore + ObsidianRobot.Ore + GeodeRobot.Ore) / 4);
        ObsidianFactor = (double)GeodeRobot.Obsidian / (double)((double)(OreRobot.Ore + ClayRobot.Ore + ObsidianRobot.Ore + GeodeRobot.Ore) / 4);

    }
    public int Id { get; set; }
    public RobotCost OreRobot { get; set; }
    public RobotCost ClayRobot { get; set; }
    public RobotCost ObsidianRobot { get; set; }
    public RobotCost GeodeRobot { get; set; }
    public double ClayFactor { get; set; }
    public double ObsidianFactor { get; set; }

    public class RobotCost
    {
        public int Ore { get; set; }
        public int Clay { get; set; }
        public int Obsidian { get; set; }
    }
}
