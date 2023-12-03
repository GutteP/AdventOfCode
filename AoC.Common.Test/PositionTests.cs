using FluentAssertions;

namespace AoC.Common.Test;

public class PositionTests
{
    [Fact]
    public void PositionNeighbors()
    {
        Position<int> position = new(1, 1);
        List<Position<int>> neighbors = position.Neighbors(false);
        neighbors.Should().ContainEquivalentOf(new Position<int>(0, 1));
        neighbors.Should().ContainEquivalentOf(new Position<int>(1, 0));
        neighbors.Should().ContainEquivalentOf(new Position<int>(2, 1));
        neighbors.Should().ContainEquivalentOf(new Position<int>(1, 2));
    }
    [Fact]
    public void PositionNeighborsWithDiagonal()
    {
        Position<int> position = new(1, 1);
        List<Position<int>> neighbors = position.Neighbors(true);
        neighbors.Should().ContainEquivalentOf(new Position<int>(0, 1));
        neighbors.Should().ContainEquivalentOf(new Position<int>(1, 0));
        neighbors.Should().ContainEquivalentOf(new Position<int>(2, 1));
        neighbors.Should().ContainEquivalentOf(new Position<int>(1, 2));
        neighbors.Should().ContainEquivalentOf(new Position<int>(0, 0));
        neighbors.Should().ContainEquivalentOf(new Position<int>(0, 2));
        neighbors.Should().ContainEquivalentOf(new Position<int>(2, 0));
        neighbors.Should().ContainEquivalentOf(new Position<int>(2, 2));
    }
    [Fact]
    public void PositionsNeighborsWithDiagonal()
    {
        Positions<int> positions = new(new List<Position<int>> { new(1, 1), new(1, 2) });
        var neighbors = positions.Neighbors(true);
        neighbors.Should().ContainEquivalentOf(new Position<int>(0, 0));
        neighbors.Should().ContainEquivalentOf(new Position<int>(0, 1));
        neighbors.Should().ContainEquivalentOf(new Position<int>(0, 2));
        neighbors.Should().ContainEquivalentOf(new Position<int>(0, 3));
        neighbors.Should().ContainEquivalentOf(new Position<int>(2, 0));
        neighbors.Should().ContainEquivalentOf(new Position<int>(2, 1));
        neighbors.Should().ContainEquivalentOf(new Position<int>(2, 2));
        neighbors.Should().ContainEquivalentOf(new Position<int>(2, 3));
        neighbors.Should().ContainEquivalentOf(new Position<int>(1, 0));
        neighbors.Should().ContainEquivalentOf(new Position<int>(1, 3));
        neighbors.Count.Should().Be(10);
    }
}
