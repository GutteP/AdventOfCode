using System.ComponentModel;

namespace AoC._2023._22;

public class SandSlabs : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<List<Slab>, int>(Transformer, PartOne), new Runner<List<Slab>, int>(Transformer, PartTwo));
    }

    private List<Slab> Transformer(string path)
    {
        List<Slab> slabs = new();
        int rowNumber = 0;
        foreach (string slabLine in InputReader.ReadLines(path))
        {
            var sp = slabLine.Split('~');
            var a = sp[0].Split(',').ToIntList();
            var b = sp[1].Split(',').ToIntList();

            List<Position3D<int>> kubes = new();
            int diffIndex = -1;
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] != b[i])
                {
                    diffIndex = i;
                    break;
                }
            }
            if (diffIndex == -1)
            {
                kubes.Add(new Position3D<int>(a[0], a[1], a[2]));
            }
            else
            {
                int diff = b[diffIndex] - a[diffIndex];
                bool neg = diff < 0;
                if (neg) diff--;
                else diff++;
                for (int i = 0; i != diff;)
                {
                    if (diffIndex == 0) kubes.Add(new(a[0] + i, a[1], a[2]));
                    else if (diffIndex == 1) kubes.Add(new(a[0], a[1] + i, a[2]));
                    else if (diffIndex == 2) kubes.Add(new(a[0], a[1], a[2] + i));

                    if (neg) i--;
                    else i++;
                }
            }
            slabs.Add(new Slab(rowNumber, kubes));
            rowNumber++;
        }
        return slabs.OrderBy(x => x.Kubes[0].Z).ToList();
    }

    private int PartOne(List<Slab> slabs)
    {
        Settle(slabs);
        int disintegratedSafely = 0;
        foreach (Slab slab in slabs)
        {
            if (slabs.Any(x => x.SupportedBy.Contains(slab.Id) && x.SupportedBy.Count == 1)) continue;
            disintegratedSafely++;
        }
        return disintegratedSafely;
    }
    private int PartTwo(List<Slab> slabs)
    {
        Settle(slabs);
        List<int> notSafe = new();
        foreach (Slab slab in slabs)
        {
            if (slabs.Any(x => x.SupportedBy.Contains(slab.Id) && x.SupportedBy.Count == 1)) notSafe.Add(slab.Id);
        }
        int sumOfFallingSlabs = 0;
        foreach (Slab slab in slabs.Where(x => notSafe.Contains(x.Id)))
        {
            List<int> falling = new List<int>() { slab.Id };
            while (falling.Count > 0)
            {
                var newFalling = slabs.Where(x => x.SupportedBy.Count > 0 && !x.SupportedBy.Except(falling).Any()).Select(x => x.Id).ToList();
                if (falling.Union(newFalling).Count() == falling.Count) break;
                falling = falling.Union(newFalling).ToList();
            }
            sumOfFallingSlabs += falling.Count - 1;
        }

        return sumOfFallingSlabs;
    }

    private void Settle(List<Slab> slabs)
    {
        foreach (Slab slab in slabs)
        {
            while (slab.MoveDown())
            {
                foreach (Slab other in slabs)
                {
                    if (other == slab) continue;
                    if (slab.Overlap(other))
                    {
                        slab.SupportedBy.Add(other.Id);
                    }
                }
                if (slab.SupportedBy.Count > 0)
                {
                    slab.MoveUp();
                    break;
                }
            }
        }
    }

    public record Slab
    {
        public Slab(int id, IEnumerable<Position3D<int>> kubes)
        {
            Kubes = kubes.OrderBy(x => x.Z).ThenBy(x => x.X).ThenBy(x => x.Y).ToList();
            if (Kubes[0].X != Kubes[Kubes.Count - 1].X) Direction = Direction3D.X;
            else if (Kubes[0].Y != Kubes[Kubes.Count - 1].Y) Direction = Direction3D.Y;
            else if (Kubes[0].Z != Kubes[Kubes.Count - 1].Z) Direction = Direction3D.Z;
            Id = id;
            SupportedBy = new();
            //Low = Kubes[0].Z;
            //High = Kubes[Kubes.Count - 1].Z;

        }
        public List<Position3D<int>> Kubes { get; set; }
        public Direction3D? Direction { get; set; }
        public int Id { get; set; }
        public List<int> SupportedBy { get; set; }
        //public int Low { get; set; }
        //public int High { get; set; }

        public override string ToString()
        {
            return $"{Id}: \t{Kubes[0]} ~ {Kubes[Kubes.Count - 1]} = {string.Join(", ", SupportedBy)}";
        }

        public bool MoveDown()
        {
            if (Kubes[0].Z <= 1) return false;
            for (int i = 0; i < Kubes.Count; i++)
            {
                Kubes[i].Move(Direction3D.Z, -1);
            }
            return true;
        }
        public void MoveUp()
        {
            for (int i = 0; i < Kubes.Count; i++)
            {
                Kubes[i].Move(Direction3D.Z, 1);
            }
        }
        public bool Overlap(Slab other)
        {
            if (!RangeOverlap(Kubes[0].Z, Kubes[Kubes.Count - 1].Z, other.Kubes[0].Z, other.Kubes[other.Kubes.Count - 1].Z)) return false;
            for (int i = 0; i < Kubes.Count; i++)
            {
                for (int j = 0; j < other.Kubes.Count; j++)
                {
                    if (Kubes[i].X != other.Kubes[j].X) continue;
                    if (Kubes[i].Y != other.Kubes[j].Y) continue;
                    if (Kubes[i].Z != other.Kubes[j].Z) continue;
                    return true;
                }
            }
            return false;
        }
        private bool RangeOverlap(int a1, int a2, int b1, int b2)
        {
            if (a1 > b1 && a1 > b2 && a2 > b1 && a2 > b2) return false;
            if (a1 < b1 && a1 < b2 && a2 < b1 && a2 < b2) return false;
            return true;
        }
    }
}
