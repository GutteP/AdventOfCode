namespace AoC.Common.Test;

public class ToIntTests
{
    [Fact]
    public void ToIntTest()
    {
        var input = InputReader.ReadLines("TestData/commonToInt.txt");
        List<List<int>> list = input.SplitOn(Seperator.Comma).Trim(' ').RemoveEmpty().ToInt();
        for (int i = 0; i < list.Count; i++)
        {
            Assert.True(list[i].Count == 4);
            Assert.True(list[i].Where(x => x == i).Count() == 4);
        }
        Assert.Equal(3, list.Count);
    }
    [Fact]
    public void ToIntTabTest()
    {
        var input = InputReader.ReadLines("TestData/2017i2.txt");
        List<List<int>> list = input.SplitOn(Seperator.Tab).Trim(' ').RemoveEmpty().ToInt();
        Assert.Equal(16, list.Count);
        foreach (var item in list)
        {
            Assert.Equal(16, item.Count);
        }

    }
}
