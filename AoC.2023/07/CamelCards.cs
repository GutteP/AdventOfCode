using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AoC._2023._07.CamelCards;

namespace AoC._2023._07;

public class CamelCards : IAoCDay<long>
{
    public DayRunner<long> Runner()
    {
        return new DayRunner<long>(new Runner<List<CamelCardHand>, long>(ReadInputPartOne, TotalWinnings), new Runner<List<CamelCardHand>, long>(ReadInputPartTwo, TotalWinnings));
    }

    public List<CamelCardHand> ReadInputPartOne(string path)
    {
        List<CamelCardHand> cards = new();
        foreach (var stringCard in InputReader.ReadLines(path))
        {
            var sp = stringCard.Trim().SplitOn(Seperator.Space).Select(x => x.Trim()).ToList();
            cards.Add(new(sp[0], int.Parse(sp[1]), false));
        }
        cards = cards.Order().ToList();
        return cards;
    }
    public List<CamelCardHand> ReadInputPartTwo(string path)
    {
        List<CamelCardHand> hands = new();
        foreach (var stringCard in InputReader.ReadLines(path))
        {
            var sp = stringCard.Trim().SplitOn(Seperator.Space).Select(x => x.Trim()).ToList();
            hands.Add(new(sp[0], int.Parse(sp[1]), true));
        }
        hands = hands.Order().ToList();
        return hands;
    }

    public long TotalWinnings(List<CamelCardHand> hands)
    {
        long sum = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            sum += hands[i].Value * (i + 1);
        }
        return sum;
    }

    public record CamelCardHand : IComparable<CamelCardHand>
    {
        public CamelCardHand(string inputString, int value, bool partTwo)
        {
            if (inputString.Length != 5) throw new ArgumentException();
            PartTwo = partTwo;
            Counts = new();
            Cards = inputString.ToList().Select(x => CardValue(x)).ToList();
            for (int i = 0; i < Cards.Count; i++)
            {
                if (Cards[i] == 1) continue;
                if (Cards[..i].Contains(Cards[i])) continue;
                Counts.Add(Cards.Count(x => x == Cards[i]));
            }
            Counts = Counts.OrderByDescending(x => x).ToList();
            NumberOfJokers = Cards.Count(x => x == 1);
            CardType = TypePoints();
            Value = value;
        }
        public List<int> Cards { get; set; }
        public List<int> Counts { get; set; }
        public int CardType { get; set; }
        public int Value { get; set; }
        public bool PartTwo { get; }
        public int NumberOfJokers { get; set; }

        private int TypePoints()
        {
            if (Counts.Count <= 1) return 7;
            if (Counts[0] + NumberOfJokers >= 5) return 7;
            if (Counts[0] + NumberOfJokers == 4) return 6;
            if ((Counts.Count >= 2 && Counts[0] == 3 && Counts[1] == 2) ||
                (Counts.Count >= 2 && Counts[0] == 2 && Counts[1] == 2 && NumberOfJokers == 1)) return 5;
            if (Counts[0] + NumberOfJokers == 3) return 4;
            if (Counts[0] == 2 && Counts[1] == 2) return 3;
            if (Counts[0] + NumberOfJokers == 2) return 2;
            if (Counts[0] + NumberOfJokers == 1) return 1;
            throw new Exception("Hit ska vi inte komma");
        }

        public int CompareTo(CamelCardHand? other)
        {
            if (other == null) throw new Exception("other null");

            if (CardType != other.CardType) return CardType > other.CardType ? 1 : -1;
            else
            {
                for (int i = 0; i < Cards.Count; i++)
                {
                    if (Cards[i] == other.Cards[i]) continue;
                    int r = Cards[i] > other.Cards[i] ? 1 : -1;
                    return r;
                }
            }
            return 0;
        }
        private int CardValue(char card)
        {
            if (card == 'A') return 14;
            if (card == 'K') return 13;
            if (card == 'Q') return 12;
            if (card == 'J' && PartTwo) return 1;
            if (card == 'J') return 11;
            if (card == 'T') return 10;
            if (card == '9') return 9;
            if (card == '8') return 8;
            if (card == '7') return 7;
            if (card == '6') return 6;
            if (card == '5') return 5;
            if (card == '4') return 4;
            if (card == '3') return 3;
            if (card == '2') return 2;
            throw new Exception("Ojoj");
        }

        public override string ToString()
        {
            return string.Join(',', Cards) + " - " + CardType + " - " + Value;
        }
    }
}
