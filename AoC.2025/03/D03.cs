using AoC.Common;

namespace AoC._2025;

public class D03
{
    public int? PartOne(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<int[]> banks = InputReader.ReadLines(inputPath).Select(x => x.Select(y => (int)char.GetNumericValue(y)).ToArray()).ToList();

        List<int> bankValues = [];
        foreach (var bank in banks)
        {
            string num = string.Empty;
            int firstIndex = bank.IndexOf(bank.Max());
            int lastIndex = -1;
            
            if(firstIndex == bank.Length - 1)
            {
                lastIndex = firstIndex;
                firstIndex = bank[..(bank.Length-1)].IndexOf(bank[..(bank.Length - 1)].Max());
                num = bank[firstIndex].ToString();
                num += bank[lastIndex].ToString();
            }
            else
            {
                num = bank[firstIndex].ToString();
                var rest = bank[(firstIndex+1)..];
                lastIndex = rest.IndexOf(rest.Max());
                num += rest[lastIndex].ToString();

            }
            bankValues.Add(int.Parse(num));
        }
        return bankValues.Sum();
    }

    public long? PartTwo(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<int[]> banks = InputReader.ReadLines(inputPath).Select(x => x.Select(y => (int)char.GetNumericValue(y)).ToArray()).ToList();

        List<long> bankValues = [];
        foreach (var bank in banks)
        {
            string num = string.Empty;
            int lastIndex = 0;
            for (int i = 11; i >= 0; i--)
            {
                var bankPart = bank[lastIndex..(bank.Length - i)];
                int maxIndex = bankPart.IndexOf(bankPart.Max());
                num += bankPart[maxIndex];
                lastIndex += maxIndex + 1;
            }
            bankValues.Add(long.Parse(num));
        }
        return bankValues.Sum();
    }

    
}
