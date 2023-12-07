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
        return new DayRunner<long>(new Runner<List<Card>, long>(ReadInput, PartOne), null);
    }

    public List<Card> ReadInput(string path)
    {
        List<Card> cards = new();
        foreach (var stringCard in InputReader.ReadLines(path))
        {
            var sp = stringCard.Trim().SplitOn(Seperator.Space).Select(x => x.Trim()).ToList();
            cards.Add(new(sp[0], int.Parse(sp[1])));
        }
        cards = cards.Order().ToList();
        return cards;
    }

    public long PartOne(List<Card> cards)
    {
        long sum = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            sum += cards[i].Value * (i + 1);
        }
        return sum;
    }

    public record Card : IComparable<Card>
    {
        public Card(string inputString, int value)
        {
            if(inputString.Length != 5) throw new ArgumentException();
            Counts = new();
            Cards = inputString.ToList().Select(x => CardValue(x)).ToList();
            for (int i = 0; i < Cards.Count; i++)
            {
                if (Cards[..i].Contains(Cards[i])) continue;
                Counts.Add(Cards.Count(x => x == Cards[i]));
            }
            Counts = Counts.OrderByDescending(x => x).ToList();
            CardType = TypePoints();
            Value = value;
        }
        public List<int> Cards { get; set; }
        public List<int> Counts { get; set; }
        public int CardType { get; set; }
        public int Value { get; set; }

        private int TypePoints()
        {
            if (Counts[0] == 5) return 7;
            if (Counts[0] == 4) return 6;
            if (Counts[0] == 3 && Counts[1] == 2) return 5;
            if (Counts[0] == 3) return 4;
            if (Counts[0] == 2 && Counts[1] == 2) return 3;
            if (Counts[0] == 2) return 2;
            if (Counts[0] == 1) return 1;
            throw new Exception("Hit ska vi inte komma");
        }

        public int CompareTo(Card? other)
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
            return string.Join(',', Cards) + " - " +CardType+ " - " + Value;
        }
    }
}
