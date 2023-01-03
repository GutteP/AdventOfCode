namespace AoC._2015.Day02;

public class PresentWrappingOrder : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<List<int[]>, int>(Transformer, AmountOfPaper), new Runner<List<int[]>, int>(Transformer, AmountOfRibbon));
    }

    private List<int[]> Transformer(string path)
    {
        List<int[]> presents = new();
        foreach (string present in InputReader.ReadLines(path))
        {
            presents.Add(present.Split('x').ToIntArray());
        }
        return presents;
    }

    private int AmountOfPaper(List<int[]> presents)
    {
        int squareFeetOfPaperToOrder = 0;
        foreach (int[] present in presents)
        {
            int a = 2 * (present[0] * present[1]);
            int b = 2 * (present[1] * present[2]);
            int c = 2 * (present[2] * present[0]);
            squareFeetOfPaperToOrder += a + b + c;
            squareFeetOfPaperToOrder += (a <= b && a <= c ? a : b <= a && b <= c ? b : c) / 2;
        }

        return squareFeetOfPaperToOrder;
    }
    private int AmountOfRibbon(List<int[]> presents)
    {
        int feetOfRibbonToOrder = 0;
        foreach (int[] present in presents)
        {
            int[] orderd = present.OrderBy(x => x).ToArray();
            feetOfRibbonToOrder += orderd[0] + orderd[0] + orderd[1] + orderd[1];
            feetOfRibbonToOrder += present.Product();
        }

        return feetOfRibbonToOrder;
    }
}
