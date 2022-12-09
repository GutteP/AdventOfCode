namespace AoC._2022.Day8;

public class GroveEvaluator : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<int[,], int>(Transformer, NumberOfVisible), new Runner<int[,], int>(Transformer, ScenicScore));
    }

    private int[,] Transformer(string path)
    {
        return InputReader.ReadLines(path).ToNumericValue().Map2D();
    }

    private int NumberOfVisible(int[,] map)
    {
        int numberOfVisible = 0;
        for (int i = 1; i < map.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < map.GetLength(1) - 1; j++)
            {
                if (IsVisible((i, j), map)) numberOfVisible++;
            }
        }
        numberOfVisible += (map.GetLength(0) * 2) + (map.GetLength(1) * 2) - 4;

        return numberOfVisible;
    }

    #region Part 1 helper methods
    private bool IsVisible((int i, int j) tree, int[,] map)
    {
        if (VisibleFromNorth(tree, map[tree.i, tree.j], map)) return true;
        if (VisibleFromSouth(tree, map[tree.i, tree.j], map)) return true;
        if (VisibleFromWest(tree, map[tree.i, tree.j], map)) return true;
        if (VisibleFromEast(tree, map[tree.i, tree.j], map)) return true;
        return false;
    }

    private bool VisibleFromNorth((int i, int j) tree, int height, int[,] map)
    {
        bool visible = true;
        for (int i = 0; i < tree.Item1; i++)
        {
            if (map[i, tree.j] >= height)
            {
                visible = false;
                break;
            }
        }
        return visible;
    }
    private bool VisibleFromSouth((int i, int j) tree, int height, int[,] map)
    {
        bool visible = true;
        for (int i = tree.Item1 + 1; i < map.GetLength(0); i++)
        {
            if (map[i, tree.j] >= height)
            {
                visible = false;
                break;
            }
        }
        return visible;
    }
    private bool VisibleFromWest((int i, int j) tree, int height, int[,] map)
    {
        bool visible = true;
        for (int j = 0; j < tree.j; j++)
        {
            if (map[tree.i, j] >= height)
            {
                visible = false;
                break;
            }
        }
        return visible;
    }
    private bool VisibleFromEast((int i, int j) tree, int height, int[,] map)
    {
        bool visible = true;
        for (int j = tree.Item2 + 1; j < map.GetLength(1); j++)
        {
            if (map[tree.i, j] >= height)
            {
                visible = false;
                break;
            }
        }
        return visible;
    }
    #endregion

    private int ScenicScore(int[,] map)
    {
        int topScenicScore = 0;
        for (int i = 1; i < map.GetLength(0) - 1; i++)
        {
            for (int j = 1; j < map.GetLength(1) - 1; j++)
            {
                int score = ScenicScore((i, j), map);
                if (score > topScenicScore) topScenicScore = score;
            }
        }
        return topScenicScore;
    }

    #region Part 2 helper methods
    private int ScenicScore((int i, int j) tree, int[,] map)
    {
        int visibleNorth = VisibleToTheNorth(tree, map[tree.i, tree.j], map);
        int visibleSouth = VisibleToTheSouth(tree, map[tree.i, tree.j], map);
        int visibleWest = VisibleToTheWest(tree, map[tree.i, tree.j], map);
        int visibleEast = VisibleToTheEast(tree, map[tree.i, tree.j], map);
        return visibleNorth * visibleSouth * visibleWest * visibleEast;
    }
    private int VisibleToTheNorth((int i, int j) tree, int height, int[,] map)
    {
        int count = 0;
        for (int i = tree.i - 1; i >= 0; i--)
        {
            count++;
            if (map[i, tree.j] >= height) break;
        }
        return count;
    }
    private int VisibleToTheSouth((int i, int j) tree, int height, int[,] map)
    {
        int count = 0;
        for (int i = tree.i + 1; i < map.GetLength(0); i++)
        {
            count++;
            if (map[i, tree.j] >= height) break;
        }
        return count;
    }
    private int VisibleToTheWest((int i, int j) tree, int height, int[,] map)
    {
        int count = 0;
        for (int j = tree.j - 1; j >= 0; j--)
        {
            count++;
            if (map[tree.i, j] >= height) break;
        }
        return count;
    }
    private int VisibleToTheEast((int i, int j) tree, int height, int[,] map)
    {
        int count = 0;
        for (int j = tree.j + 1; j < map.GetLength(1); j++)
        {
            count++;
            if (map[tree.i, j] >= height) break;
        }
        return count;
    }

    #endregion
}
