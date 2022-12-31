namespace AoC._2022.Day18
{
    public class LavaDroplet : IAoCDay<int>
    {
        public DayRunner<int> Runner()
        {
            return new DayRunner<int>(new Runner<bool[,,], int>(Transformer, SurfaceArea), new Runner<bool[,,], int>(Transformer, ExteriorSurfaceArea));
        }

        private bool[,,] Transformer(string path)
        {
            int max = 0;
            int min = int.MaxValue;
            List<string> input = InputReader.ReadLines(path);
            foreach (string row in input)
            {
                List<int> coordinates = row.SplitOn(Seperator.Comma).ToInt();
                foreach (var item in coordinates)
                {
                    if (item > max) max = item;
                    if (item < min) min = item;

                }
            }
            int range = max - min;
            int displace = 1 - min;
            bool[,,] droplet = new bool[range + 2, range + 2, range + 2];
            foreach (string row in input)
            {
                List<int> coordinates = row.SplitOn(Seperator.Comma).ToInt();
                droplet[coordinates[0] + displace, coordinates[1] + displace, coordinates[2] + displace] = true;
            }
            return droplet;
        }

        private int SurfaceArea(bool[,,] droplet)
        {
            int exposedSides = 0;
            for (int x = 0; x < droplet.GetLength(0); x++)
            {
                for (int y = 0; y < droplet.GetLength(1); y++)
                {
                    for (int z = 0; z < droplet.GetLength(2); z++)
                    {
                        if (droplet[x, y, z])
                        {
                            exposedSides += 6 - NumberOfNeighbors(droplet, (x, y, z));
                        }
                    }
                }
            }

            return exposedSides;
        }

        private int ExteriorSurfaceArea(bool[,,] droplet)
        {
            return SurfaceArea(droplet) - InteriorSurfaceArea(droplet);
        }

        private int InteriorSurfaceArea(bool[,,] droplet)
        {
            int internallyExposedSides = 0;
            for (int x = 0; x < droplet.GetLength(0); x++)
            {
                for (int y = 0; y < droplet.GetLength(1); y++)
                {
                    for (int z = 0; z < droplet.GetLength(2); z++)
                    {
                        if (!droplet[x, y, z])
                        {
                            if (OnTheInside(droplet, (x, y, z)))
                            {
                                internallyExposedSides += NumberOfNeighbors(droplet, (x, y, z));
                            }

                        }
                    }
                }
            }
            return internallyExposedSides;
        }

        private bool OnTheInside(bool[,,] droplet, (int x, int y, int z) p)
        {
            HashSet<(int x, int y, int z)> visited = new();
            List<(int x, int y, int z)> emptyNeighbors = EmptyNeighbors(droplet, p);
            visited.Add(p);
            while (emptyNeighbors.Any())
            {
                if (visited.Contains(emptyNeighbors.First()))
                {
                    emptyNeighbors.Remove(emptyNeighbors.First());
                    continue;
                }
                if (emptyNeighbors.First().x == 0 ||
                    emptyNeighbors.First().y == 0 ||
                    emptyNeighbors.First().z == 0 ||
                    emptyNeighbors.First().x == droplet.GetLength(1) - 1 ||
                    emptyNeighbors.First().y == droplet.GetLength(1) - 1 ||
                    emptyNeighbors.First().z == droplet.GetLength(1) - 1)
                {
                    return false;
                }

                emptyNeighbors.AddRange(EmptyNeighbors(droplet, emptyNeighbors.First()));
                visited.Add(emptyNeighbors.First());
                emptyNeighbors.Remove(emptyNeighbors.First());
            }
            return true;
        }
        public List<(int x, int y, int z)> EmptyNeighbors(bool[,,] droplet, (int x, int y, int z) p)
        {
            List<(int x, int y, int z)> emptyNeighbors = new();
            try
            {
                if (!droplet[p.x + 1, p.y, p.z]) emptyNeighbors.Add((p.x + 1, p.y, p.z));
            }
            catch (Exception) { }
            try
            {
                if (!droplet[p.x - 1, p.y, p.z]) emptyNeighbors.Add((p.x - 1, p.y, p.z));
            }
            catch (Exception) { }
            try
            {
                if (!droplet[p.x, p.y + 1, p.z]) emptyNeighbors.Add((p.x, p.y + 1, p.z));
            }
            catch (Exception) { }
            try
            {
                if (!droplet[p.x, p.y - 1, p.z]) emptyNeighbors.Add((p.x, p.y - 1, p.z));
            }
            catch (Exception) { }
            try
            {
                if (!droplet[p.x, p.y, p.z + 1]) emptyNeighbors.Add((p.x, p.y, p.z + 1));
            }
            catch (Exception) { }
            try
            {
                if (!droplet[p.x, p.y, p.z - 1]) emptyNeighbors.Add((p.x, p.y, p.z - 1));
            }
            catch (Exception) { }
            return emptyNeighbors;
        }
        public int NumberOfNeighbors(bool[,,] droplet, (int x, int y, int z) p)
        {
            int neightbors = 0;
            try
            {
                if (droplet[p.x + 1, p.y, p.z]) neightbors++;
            }
            catch (Exception) { }
            try
            {
                if (droplet[p.x - 1, p.y, p.z]) neightbors++;
            }
            catch (Exception) { }
            try
            {
                if (droplet[p.x, p.y + 1, p.z]) neightbors++;
            }
            catch (Exception) { }
            try
            {
                if (droplet[p.x, p.y - 1, p.z]) neightbors++;
            }
            catch (Exception) { }
            try
            {
                if (droplet[p.x, p.y, p.z + 1]) neightbors++;
            }
            catch (Exception) { }
            try
            {
                if (droplet[p.x, p.y, p.z - 1]) neightbors++;
            }
            catch (Exception) { }
            return neightbors;
        }
    }


}
