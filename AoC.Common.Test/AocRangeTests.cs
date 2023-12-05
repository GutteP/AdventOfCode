using FluentAssertions;

namespace AoC.Common.Test;

public class AocRangeTests
{
    [Fact]
    public void Enumeration()
    {
        AoCRange range = AoCRange.CreateFromTo(1, 10);
        long i = 0;
        foreach (var item in range)
        {
            i++;
            item.Should().Be(i);
        }
        i.Should().Be(10);
    }

    [Theory]
    [InlineData(0, 10, 11, 20, false)]
    [InlineData(0, 10, 10, 20, true)]
    [InlineData(0, 1, 10, 11, false)]
    [InlineData(0, 30, 10, 20, true)]
    [InlineData(0, 10, -10, 20, true)]
    [InlineData(5, 10, 0, 7, true)]
    public void Overlap(long f1, long t1, long f2, long t2, bool overlaps)
    {
        AoCRange.CreateFromTo(f1, t1).Overlaps(AoCRange.CreateFromTo(f2, t2)).Should().Be(overlaps);
    }

    [Fact]
    public void ExpandIfOverlaps_1()
    {
        AoCRange range = AoCRange.CreateFromTo(5, 10);
        range.ExpandIfOverlaps(AoCRange.CreateFromTo(0, 5)).Should().Be(true);
        range.From.Should().Be(0);
        range.To.Should().Be(10);
    }
    [Fact]
    public void ExpandIfOverlaps_2()
    {
        AoCRange range = AoCRange.CreateFromTo(0, 5);
        range.ExpandIfOverlaps(AoCRange.CreateFromTo(5, 10)).Should().Be(true);
        range.From.Should().Be(0);
        range.To.Should().Be(10);
    }
    [Fact]
    public void ExpandIfOverlaps_3()
    {
        AoCRange range = AoCRange.CreateFromTo(5, 10);
        range.ExpandIfOverlaps(AoCRange.CreateFromTo(0, 15)).Should().Be(true);
        range.From.Should().Be(0);
        range.To.Should().Be(15);
    }
    [Fact]
    public void ExpandIfOverlaps_4()
    {
        AoCRange range = AoCRange.CreateFromTo(0, 15);
        range.ExpandIfOverlaps(AoCRange.CreateFromTo(5, 10)).Should().Be(true);
        range.From.Should().Be(0);
        range.To.Should().Be(15);
    }
    [Fact]
    public void ExpandIfOverlaps_False()
    {
        AoCRange range = AoCRange.CreateFromTo(0, 15);
        range.ExpandIfOverlaps(AoCRange.CreateFromTo(16, 20)).Should().Be(false);
        range.From.Should().Be(0);
        range.To.Should().Be(15);
    }

    [Fact]
    public void Intersects()
    {
        var intersection = AoCRange.CreateFromTo(0, 10).Intersect(AoCRange.CreateFromTo(6, 10));
        intersection.Count().Should().Be(5);
    }
}
