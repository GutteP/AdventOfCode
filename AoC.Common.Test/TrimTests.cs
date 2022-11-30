namespace AoC.Common.Test;

public class TrimTests
{
    [Fact]
    public void TrimList()
    {
        List<string> input = InputReader.ReadLines("TestData/commonTrimList.txt");
        input = input.Trim(' ', ',');
        Assert.Equal("Hej", input[0]);
        Assert.Equal("jag", input[1]);
        Assert.Equal("heter", input[2]);
        Assert.Equal("Kalle", input[3]);
    }
    [Fact]
    public void TrimListList()
    {
        List<string> input = InputReader.ReadLines("TestData/commonTrimListList.txt");
        List<List<string>> splitted = input.SplitOn(Seperator.Comma);
        splitted = splitted.Trim(' ');
        Assert.Equal("Hej", splitted[0][0]);
        Assert.Equal("jag", splitted[0][1]);
        Assert.Equal("heter", splitted[0][2]);
        Assert.Equal("Kalle", splitted[0][3]);
        Assert.Equal("", splitted[0][4]);
        Assert.Equal("Hej", splitted[1][0]);
        Assert.Equal("jag", splitted[1][1]);
        Assert.Equal("heter", splitted[1][2]);
        Assert.Equal("Kalle", splitted[1][3]);
        Assert.Equal("", splitted[1][4]);
        Assert.Equal("", splitted[2][0]);
        Assert.Equal("", splitted[2][1]);
        Assert.Equal("", splitted[2][2]);
        Assert.Equal("", splitted[2][3]);
        Assert.Equal("", splitted[2][4]);
    }
    [Fact]
    public void TrimListListRemoveEmpty()
    {
        List<string> input = InputReader.ReadLines("TestData/commonTrimListList.txt");
        List<List<string>> splitted = input.SplitOn(Seperator.Comma);
        splitted = splitted.Trim(' ');
        splitted = splitted.RemoveEmpty();
        Assert.Equal("Hej", splitted[0][0]);
        Assert.Equal("jag", splitted[0][1]);
        Assert.Equal("heter", splitted[0][2]);
        Assert.Equal("Kalle", splitted[0][3]);
        Assert.Equal(4, splitted[0].Count);
        Assert.Equal("Hej", splitted[1][0]);
        Assert.Equal("jag", splitted[1][1]);
        Assert.Equal("heter", splitted[1][2]);
        Assert.Equal("Kalle", splitted[1][3]);
        Assert.Equal(4, splitted[0].Count);
        Assert.Equal(2, splitted.Count);

    }
}
