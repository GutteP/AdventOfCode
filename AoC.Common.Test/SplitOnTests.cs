namespace AoC.Common.Test;

public class SplitOnTests
{



    [Theory]
    [InlineData("En,???, separerad,rad ", Seperator.Comma)]
    [InlineData("En.???. separerad.rad ", Seperator.Dot)]
    [InlineData("En;???; separerad;rad ", Seperator.Semicolon)]
    public void SplitOnStringTest(string row, Seperator seperator)
    {
        List<string> output = row.SplitOn(seperator);
        Assert.Equal(4, output.Count);
        Assert.Equal("En", output[0]);
        Assert.Equal("???", output[1]);
        Assert.Equal(" separerad", output[2]);
        Assert.Equal("rad ", output[3]);
    }
    [Fact]
    public void SplitOnStringSpaceTest()
    {
        string row = "En ??? separerad rad";
        List<string> output = row.SplitOn(Seperator.Space);
        Assert.Equal(4, output.Count);
        Assert.Equal("En", output[0]);
        Assert.Equal("???", output[1]);
        Assert.Equal("separerad", output[2]);
        Assert.Equal("rad", output[3]);
    }
    [Fact]
    public void SplitOnStringTabTest()
    {
        string row = InputReader.ReadLines("TestData/commonTab.txt")[0];
        List<string> output = row.SplitOn(Seperator.Tab);
        Assert.Equal(4, output.Count);
        Assert.Equal("En", output[0]);
        Assert.Equal("???", output[1]);
        Assert.Equal(" separerad", output[2]);
        Assert.Equal("rad ", output[3]);
    }
    [Fact]
    public void SplitOnStringNewLineTest()
    {
        string row = @"En
???
 separerad
rad ";
        List<string> output = row.SplitOn(Seperator.NewLine);
        Assert.Equal(4, output.Count);
        Assert.Equal("En", output[0]);
        Assert.Equal("???", output[1]);
        Assert.Equal(" separerad", output[2]);
        Assert.Equal("rad ", output[3]);
    }
    [Fact]
    public void SplitOnListTest()
    {
        List<string> list = new List<string> { "En,komma,separerad,rad ", "En ,annan,komma,separerad,rad" };
        var output = list.SplitOn(Seperator.Comma);
        Assert.Equal("En", output[0][0]);
        Assert.Equal("rad ", output[0].Last());
        Assert.True(output[0].Count == 4);
        Assert.Equal("En ", output[1][0]);
        Assert.Equal("rad", output[1].Last());
        Assert.True(output[1].Count == 5);

    }
}
