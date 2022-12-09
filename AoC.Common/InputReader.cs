namespace AoC.Common
{
    public static class InputReader
    {
        public static List<string> ReadLines(string fileName)
        {
            return File.ReadLines(fileName).ToList();
        }
    }
}