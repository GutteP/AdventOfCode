namespace AoC._2024;

public class D02
{
    public int? PartOne(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<string> input = InputReader.ReadLines(inputPath);
        List<List<int>> reports = SplitToIntLists(input);

        int safe = 0;
        foreach (List<int> report in reports)
        {
            if (IsDecreasingSafe(report) || IsIncreasingSafe(report))
            {
                safe++;
            }
        }
        return safe;
    }

    public int? PartTwo(string inputPath, string? option1 = null, int? option2 = null)
    {
        List<string> input = InputReader.ReadLines(inputPath);
        List<List<int>> reports = SplitToIntLists(input);

        int safe = 0;
        foreach (List<int> report in reports)
        {
            bool isSafe = false;
            isSafe = IsDecreasingSafe(report) || IsIncreasingSafe(report);
            if (!isSafe)
            {
                for (int i = 0; i < report.Count; i++)
                {
                    int removed = report[i];
                    report.RemoveAt(i);
                    isSafe = IsDecreasingSafe(report) || IsIncreasingSafe(report);
                    if (!isSafe)
                    {
                        report.Insert(i, removed);
                    }
                    else break;
                }
            }
            if (isSafe)
            {
                safe++;
                continue;
            }
        }

        return safe;
    }

    private List<List<int>> SplitToIntLists(List<string> input)
    {
        List<List<int>> reports = new();
        foreach (string line in input)
        {
            string[] stringReport = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            List<int> report = new();
            foreach (string r in stringReport)
            {
                report.Add(int.Parse(r));
            }
            reports.Add(report);
        }
        return reports;
    }

    private bool IsDecreasingSafe(IEnumerable<int> report)
    {
        int? c = null;
        bool isSafe = true;
        foreach (int n in report)
        {
            if (c == null)
            {
                c = n;
                continue;
            }
            if (c - 1 != n && c - 2 != n && c - 3 != n)
            {
                isSafe = false;
                break;
            }
            else c = n;
        }
        return isSafe;
    }
    private bool IsIncreasingSafe(IEnumerable<int> report)
    {
        int? c = null;
        bool isSafe = true;
        foreach (int n in report)
        {
            if (c == null)
            {
                c = n;
                continue;
            }
            if (c + 1 != n && c + 2 != n && c + 3 != n)
            {
                isSafe = false;
                break;
            }
            else c = n;
        }
        return isSafe;
    }
}