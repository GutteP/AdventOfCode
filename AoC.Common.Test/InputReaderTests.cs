namespace AoC.Common.Test
{
    public class InputReaderTests
    {
        [Theory]
        [InlineData("TestData/common1Row.txt", 1)]
        [InlineData("TestData/common3Row.txt", 3)]

        public void Read(string file, int rows)
        {
            List<string> input = InputReader.ReadLines(file);
            Assert.True(input.Count == rows);
            for (int i = 0; i < input.Count; i++)
            {
                Assert.Equal($"En; test, rad-{i}", input[i]);
            }
        }
    }
}