namespace AoC._2022.DayXIII;

public class PacketOrderChecker : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<List<(Packet A, Packet B)>, int>(PairTransformer, OrderChecker), new Runner<List<Packet>, int>(WithDividerTransformer, DecoderKey));
    }

    private List<(Packet A, Packet B)> PairTransformer(string path)
    {
        var input = InputReader.ReadLines(path);
        List<(Packet A, Packet B)> pairs = new();
        for (int i = 0; i < input.Count; i += 3)
        {
            pairs.Add((new Packet(input[i]), new Packet(input[i + 1])));
        }
        return pairs;
    }
    private List<Packet> WithDividerTransformer(string path)
    {
        List<Packet> packets = new();
        foreach (var p in InputReader.ReadLines(path))
        {
            if (string.IsNullOrEmpty(p)) continue;
            packets.Add(new Packet(p));
        }
        packets.Add(new Packet("[[2]]"));
        packets.Add(new Packet("[[6]]"));

        return packets;
    }

    private int OrderChecker(List<(Packet A, Packet B)> pairs)
    {
        int sum = 0;
        for (int i = 0; i < pairs.Count; i++)
        {
            if (pairs[i].A.CompareTo(pairs[i].B) == -1) sum += i + 1;
        }
        return sum;
    }
    private int DecoderKey(List<Packet> pairs)
    {
        pairs.Sort();
        var a = pairs.IndexOf(pairs.Where(x => !x.Value.IsIntValue && x.Value.ListValue.Count() == 1 && !x.Value.ListValue.First().IsIntValue && x.Value.ListValue.First().ListValue.Count() == 1 && x.Value.ListValue.First().ListValue.First().IsIntValue && x.Value.ListValue.First().ListValue.First().IntValue == 2).First());
        int b = pairs.IndexOf(pairs.Where(x => !x.Value.IsIntValue && x.Value.ListValue.Count() == 1 && !x.Value.ListValue.First().IsIntValue && x.Value.ListValue.First().ListValue.Count() == 1 && x.Value.ListValue.First().ListValue.First().IsIntValue && x.Value.ListValue.First().ListValue.First().IntValue == 6).First());
        return (a + 1) * (b + 1);
    }
}

public record Packet : IComparable<Packet>
{
    private List<IPacketValue> _packet;
    public Packet(string packet)
    {
        _packet = new List<IPacketValue>();

        List<PacketListValue> temp = new();
        while (true)
        {
            var i = packet.LastIndexOf('[');
            if (i == 0) break;
            var l = String.Concat(packet.Take(new Range(i, packet.IndexOf(']', i)))).Trim('[', ']');
            packet = packet.Remove(i, l.Count() + 2);
            packet = packet.Insert(i, $"L{temp.Count}");

            List<IPacketValue> values = new();
            if (l == "") temp.Add(new PacketListValue(new List<IPacketValue>()));
            else
            {
                foreach (string s in l.SplitOn(Seperator.Comma))
                {
                    if (s.StartsWith("L"))
                    {
                        values.Add(temp[int.Parse(s.Remove(0, 1))]);
                    }
                    else values.Add(new PacketIntValue(int.Parse(s)));
                }
                temp.Add(new(values));
            }

        }

        if (packet == "[]") _packet.Add(new PacketListValue(new List<IPacketValue>()));
        else
        {
            foreach (string s in packet.Trim('[', ']').SplitOn(Seperator.Comma))
            {
                if (s.StartsWith("L"))
                {
                    _packet.Add(temp[int.Parse(s.Remove(0, 1))]);
                }
                else _packet.Add(new PacketIntValue(int.Parse(s)));
            }
        }
        Value = new PacketListValue(_packet);
    }
    public IPacketValue Value { get; private set; }

    public int CompareTo(Packet? other)
    {
        return Value.CompareTo(other.Value);
    }
}

public interface IPacketValue : IComparable<IPacketValue>
{
    bool IsIntValue { get; }
    int IntValue { get; }
    IEnumerable<IPacketValue> ListValue { get; }
}

public record PacketIntValue : IPacketValue
{
    private int _value;
    public PacketIntValue(int value)
    {
        _value = value;
    }
    public bool IsIntValue => true;

    public int IntValue => _value;

    public IEnumerable<IPacketValue> ListValue => null;

    public int CompareTo(IPacketValue? other)
    {
        if (other is PacketIntValue v)
        {
            return _value.CompareTo(v.IntValue);
        }
        else
        {
            return new PacketListValue(new List<IPacketValue> { this }).CompareTo(other);
        }
    }
}

public record PacketListValue : IPacketValue
{
    private IEnumerable<IPacketValue> _value;

    public PacketListValue(IEnumerable<IPacketValue> value)
    {
        _value = value;
    }
    public bool IsIntValue => false;

    public int IntValue => -1;

    public IEnumerable<IPacketValue> ListValue => _value;

    public int CompareTo(IPacketValue? other)
    {
        PacketListValue otherList = default;
        if (other is PacketIntValue intValue)
        {
            otherList = new PacketListValue(new List<IPacketValue> { intValue });
        }
        else otherList = (PacketListValue)other;

        int i = 0;
        while (i < _value.Count() && i < otherList.ListValue.Count())
        {
            if (_value.ElementAt(i).CompareTo(otherList.ListValue.ElementAt(i)) != 0) return _value.ElementAt(i).CompareTo(otherList.ListValue.ElementAt(i));
            i++;
        }
        return _value.Count().CompareTo(otherList.ListValue.Count());
    }
}
