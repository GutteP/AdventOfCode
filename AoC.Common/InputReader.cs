namespace AoC.Common
{
    public static class InputReader
    {
        public static List<string> ReadLines(string fileName)
        {
            return File.ReadLines(fileName).ToList();
        }
        public static string ReadString(string fileName)
        {
            return File.ReadAllText(fileName);
        }

        public static List<string> ReadStringAsLines(string input)
        {
            return input.Split("\n\r", StringSplitOptions.None).ToList();
        }

    }
}