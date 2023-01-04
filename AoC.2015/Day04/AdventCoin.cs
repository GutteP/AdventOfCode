using System.Security.Cryptography;
using System.Text;

namespace AoC._2015.Day04;

public class AdventCoin : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<string, string, int>(Transformer, Solve, "00000"), new Runner<string, string, int>(Transformer, Solve, "000000"));
    }

    private string Transformer(string key)
    {
        return key;
    }

    private int Solve(string key, string startsWith)
    {
        byte[] hash;
        using (MD5 md5 = MD5.Create())
        {
            for (int i = 0; i < 10000000; i++)
            {
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes($"{key}{i}"));
                if (Convert.ToHexString(hash).StartsWith(startsWith))
                {
                    return i;
                }
            }
        }
        throw new Exception("Ingen resulterade i en hash som börjar med 00000");
    }
}
