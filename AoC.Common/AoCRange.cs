using FluentAssertions.Equivalency;
using System.Collections;

namespace AoC.Common;

public record AoCRange : IEnumerable<long>
{
    private AoCRange(long from, long? to, long? length)
    {
        if (to == null && length == null) throw new ArgumentException("Både to and from kan inte vara null");
        From = from;

        if (to != null)
        {
            To = to.Value;
            Length = (To - From) + 1;
        }
        else
        {
            Length = length!.Value;
            To = From + Length - 1;
        }
    }

    public static AoCRange CreateFromTo(long from, long to)
    {
        return new(from, to, null);
    }
    public static AoCRange CreateFromLength(long from, long length)
    {
        return new(from, null, length);
    }

    public long From { get; set; }
    public long Length { get; set; }
    public long To { get; set; }

    public bool In(long num)
    {
        return num >= From && num <= To;
    }
    public bool Overlaps(AoCRange other)
    {
        if (other.From >= From && other.From <= To) return true;
        if (other.To >= From && other.To <= To) return true;
        if (other.From < From && other.To > To) return true;
        return false;
    }
    public bool ExpandIfOverlaps(AoCRange other)
    {
        if (other.From >= From && other.From <= To)
        {
            if (other.To > To)
            {
                To = other.To;
                return true;
            }
            return true;
        }
        if (other.To >= From && other.To <= To)
        {
            if (other.From < From)
            {
                From = other.From;
                return true;
            }
            return true;
        }
        if (other.From < From && other.To > To)
        {
            From = other.From;
            To = other.To;
            return true;
        }
        return false;
    }

    public IEnumerator<long> GetEnumerator()
    {
        return new AoCRangeEnum(From, To);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public class AoCRangeEnum : IEnumerator<long>
    {
        private readonly long _from;
        private readonly long _to;

        long position = -1;

        public AoCRangeEnum(long from, long to)
        {
            _from = from;
            _to = to;
            position = _from - 1;
        }

        public long Current
        {
            get
            {
                if (position < _from || position > _to) throw new InvalidOperationException();
                else return position;
            }
        }

        object IEnumerator.Current => Current;


        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            position++;
            return (position <= _to);
        }

        public void Reset()
        {
            position = _from - 1;
        }
    }
}
