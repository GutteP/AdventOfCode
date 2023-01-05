using System.Linq;

namespace AoC._2015.Day05
{
    public class NaughtyOrNice : IAoCDay<int>
    {
        public DayRunner<int> Runner()
        {
            return new DayRunner<int>(new Runner<List<string>, int>(Transformer, OldWay), new Runner<List<string>, int>(Transformer, NewWay));
        }

        private List<string> Transformer(string path)
        {
            return InputReader.ReadLines(path);
        }

        private int OldWay(List<string> words)
        {
            return words.Where(x => NiceWordsTheOldWay(x)).Count();
        }
        private int NewWay(List<string> words)
        {
            return words.Where(x => NiceWordsTheNewWay(x)).Count();
        }

        private bool NiceWordsTheOldWay(string word)
        {
            if (!HasVowels(word, 3)) return false;
            if (!HasDubbelChar(word)) return false;
            if (!NoForbiddenStrings(word)) return false;
            return true;

        }
        private bool NiceWordsTheNewWay(string word)
        {
            if (!HasTwoPairs(word)) return false;
            if (!HasDubbelCharWithDistance(word)) return false;

            return true;

        }
        private bool HasDubbelCharWithDistance(string word)
        {
            for (int i = 0; i < word.Length - 2; i++)
            {
                if (word[i] == word[i + 2]) return true;
            }
            return false;
        }
        private bool HasTwoPairs(string word)
        {
            var chunks1 = word.Chunk(2).Where(x => x.Count() == 2).ToList();
            var chunks2 = word.Substring(1).Chunk(2).Where(x => x.Count() == 2).ToList();
            List<string> zipped = new();
            for (int i = 0; i < chunks1.Count; i++)
            {
                zipped.Add($"{chunks1[i][0]}{chunks1[i][1]}");
                try
                {
                    zipped.Add($"{chunks2[i][0]}{chunks2[i][1]}");
                }
                catch (Exception) { }
            }

            for (int i = 0; i < zipped.Count - 2; i++)
            {
                for (int j = i + 2; j < zipped.Count; j++)
                {
                    if (zipped[i] == zipped[j]) return true;
                }
            }

            return false;
        }
        private bool NoForbiddenStrings(string word)
        {
            if (word.Contains("ab")) return false;
            if (word.Contains("cd")) return false;
            if (word.Contains("pq")) return false;
            if (word.Contains("xy")) return false;
            return true;
        }
        private bool HasDubbelChar(string word)
        {
            for (int i = 0; i < word.Length - 1; i++)
            {
                if (word[i] == word[i + 1]) return true;
            }
            return false;
        }
        private bool HasVowels(string word, int count)
        {
            int vowels = word.Where(x => x == 'a').Count();
            if (vowels >= count) return true;
            vowels += word.Where(x => x == 'e').Count();
            if (vowels >= count) return true;
            vowels += word.Where(x => x == 'i').Count();
            if (vowels >= count) return true;
            vowels += word.Where(x => x == 'o').Count();
            if (vowels >= count) return true;
            vowels += word.Where(x => x == 'u').Count();
            if (vowels >= count) return true;
            return false;
        }
    }
}
