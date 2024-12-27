namespace AoC._2024;

public class D22
{
    public long PartOne(string inputPath, int evolutions)
    {
        List<long> secrets = InputReader.ReadLines(inputPath).Select(long.Parse).ToList();
        for (int i = 0; i < evolutions; i++)
        {
            secrets = secrets.Select(s => s.Evolve()).ToList();
        }
        return secrets.Sum();
    }

    public long? PartTwo(string inputPath)
    {
        return 0;
    }
}


public static class D22Extensions
{
    public static long Evolve(this long secret)
    {
        secret = secret.Mix(secret * 64).Prune();
        secret = secret.Mix(secret / 32).Prune();
        return secret.Mix(secret * 2048).Prune();
    }
    public static long Mix(this long secret, long product)
    {
        return secret ^ product;
    }
    public static long Prune(this long secret)
    {
        return secret % 16777216;
    }
}


