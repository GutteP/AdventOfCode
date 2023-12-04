namespace AoC._2023._04;

public class Scratchcards : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<List<Scratchcard>, int>(ScratchcardReader, PartOne), new Runner<List<Scratchcard>, int>(ScratchcardReader, PartTwo));
    }

    public List<Scratchcard> ScratchcardReader(string path)
    {
        List<Scratchcard> scratchcards = new();
        foreach (var cardString in InputReader.ReadLines(path))
        {
            var spl = cardString.SplitOn(Seperator.Colon);
            spl = spl.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
            int cardNumber = int.Parse(spl[0].SplitOn(Seperator.Space).Where(x => !string.IsNullOrWhiteSpace(x)).ToList()[1]);
            var winningAndYours = spl[1].SplitOn(Seperator.Pipe);
            List<int> winningNumbers = winningAndYours[0].Trim().SplitOn(Seperator.Space).Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x)).ToList();
            List<int> yourNumbers = winningAndYours[1].Trim().SplitOn(Seperator.Space).Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x)).ToList();
            scratchcards.Add(new(cardNumber, winningNumbers, yourNumbers, 1));
        }
        return scratchcards;
    }

    public int PartOne(List<Scratchcard> cards)
    {
        int points = 0;
        foreach (var card in cards)
        {
            int cardPoints = 0;
            var overlap = card.WinningNumbers.Intersect(card.YourNumbers);
            for (int i = 0; i < overlap.Count(); i++)
            {
                if(i == 0) cardPoints++;
                else
                {
                    cardPoints = cardPoints * 2;
                }
            }
            points += cardPoints;
        }
        return points;
    }

    public int PartTwo(List<Scratchcard> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            var overlap = cards[i].WinningNumbers.Intersect(cards[i].YourNumbers);
            for (int j = 0; j < overlap.Count(); j++)
            {
                int index = i + 1 + j;
                if (index >= cards.Count) break;
                cards[index].Count += cards[i].Count;
            }
        }
        return cards.Sum(x => x.Count);
    }

    public record Scratchcard
    {
        public Scratchcard(int cardNumber, List<int> winningNumbers, List<int> yourNumbers, int count)
        {
            CardNumber = cardNumber;
            WinningNumbers = winningNumbers;
            YourNumbers = yourNumbers;
            Count = count;
        }

        public int CardNumber { get; set; }
        public List<int> WinningNumbers { get; set; }
        public List<int> YourNumbers { get; set; }
        public int Count { get; set; }
    };
}
