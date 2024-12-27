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

    public int PartTwo(string inputPath, int evolutions)
    {
        List<List<long>> secretsLists = InputReader.ReadLines(inputPath).Select(x => new List<long> { long.Parse(x) }).ToList();
        for (int i = 0; i < evolutions; i++)
        {
            foreach (List<long> secrets in secretsLists)
            {
                secrets.Add(secrets.Last().Evolve());
            }
        }
        List<List<(int Price, string Sequence)>> priceSeqs = secretsLists.ToPriceSequences();
        List<(int Sum, string Sequence)> smashed = priceSeqs.SmashAndOrder();

        return smashed.First().Sum;
    }
}


public static class D22Extensions
{
    public static List<(int Sum, string Sequence)> SmashAndOrder(this List<List<(int Price, string Sequence)>> priceSeqs)
    {
        return priceSeqs.SelectMany(x => x).GroupBy(x => x.Sequence).Select(x => (x.Sum(y => y.Price), x.Key)).OrderByDescending(x => x.Item1).ToList();
    }

    public static List<List<(int Price, string Sequence)>> ToPriceSequences(this List<List<long>> secretsLists)
    {
        List<List<(int Price, string Sequence)>> result = new();
        foreach (List<long> secrets in secretsLists)
        {
            Dictionary<string, (int Price, string Sequence)> partResult = new();
            List<int> last3DiffsAndCurrent = new();
            int lastPrice = 0;
            for (int i = 0; i < secrets.Count; i++)
            {
                int price = (int)char.GetNumericValue(secrets[i].ToString().Last());
                int diff = i == 0 ? 0 : price - lastPrice;
                if (i != 0) last3DiffsAndCurrent.Add(diff);
                string? sequence = null;
                if (last3DiffsAndCurrent.Count == 4)
                {
                    sequence = string.Join(",", last3DiffsAndCurrent);
                    last3DiffsAndCurrent.RemoveAt(0);
                }

                if (sequence != null && !partResult.ContainsKey(sequence)) partResult.Add(sequence, (price, sequence));
                lastPrice = price;
            }
            result.Add(partResult.Values.ToList());
        }
        return result;

    }
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


