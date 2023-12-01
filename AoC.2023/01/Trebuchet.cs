namespace AoC._2023._01;

public class Trebuchet : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<List<int>, int>(CalibrationValueExtractorOne, x => x.Sum()), new Runner<List<int>, int>(CalibrationValueExtractorTwo, x => x.Sum()));
    }
    private List<int> CalibrationValueExtractorOne(string path)
    {
        List<int> numbers = new();
        foreach (string row in InputReader.ReadLines(path))
        {
            string rowNumber = "";
            rowNumber += row.First(x => char.IsNumber(x));
            rowNumber += row.Last(x => char.IsNumber(x));
            if (rowNumber.Length != 2) throw new Exception("Not two digit number");
            if (int.TryParse(rowNumber, out int value))
            {
                numbers.Add(value);
            }
        }
        return numbers;
    }
    private static List<string> words = new() { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    private List<int> CalibrationValueExtractorTwo(string path)
    {
        List<int> numbers = new();
        foreach (string row in InputReader.ReadLines(path))
        {
            string rowNumber = "";

            for (int i = 0; i < row.Length; i++)
            {
                if (char.IsNumber(row[i]))
                {
                    rowNumber += row[i];
                    break;
                }

                string? word = words.FirstOrDefault(word => row[i..].StartsWith(word));
                if (!string.IsNullOrEmpty(word))
                {
                    rowNumber += (words.IndexOf(word) + 1).ToString();
                    break;
                }
            }
            for (int i = row.Length - 1; i >= 0; i--)
            {
                if (char.IsNumber(row[i]))
                {
                    rowNumber += row[i];
                    break;
                }

                string? word = words.FirstOrDefault(word => row[i..].StartsWith(word));
                if (!string.IsNullOrEmpty(word))
                {
                    rowNumber += (words.IndexOf(word) + 1).ToString();
                    break;
                }
            }
            if (rowNumber.Length != 2) throw new Exception("Not two digit number");
            if (int.TryParse(rowNumber, out int value))
            {
                numbers.Add(value);
            }
        }

        return numbers;
    }
}
