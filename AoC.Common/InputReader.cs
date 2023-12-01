namespace AoC.Common
{
    public static class InputReader
    {
        public static List<string> ReadLines(string fileName)
        {
            return File.ReadLines(fileName).ToList();
        }

        public static List<string> ReadStringAsLines(string input)
        {
            return input.Split("\n\r", StringSplitOptions.None).ToList();
        }
    }
}