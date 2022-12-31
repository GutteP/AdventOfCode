namespace AoC._2022.Day07;

public class CommandLineInterpreter : IAoCDay<int>
{
    public DayRunner<int> Runner()
    {
        return new DayRunner<int>(new Runner<List<string>, int>(Read, PartOne), new Runner<List<string>, int>(Read, PartTwo));
    }

    private List<string> Read(string path)
    {
        return InputReader.ReadLines(path);
    }

    private int PartOne(List<string> input)
    {
        var ls = GetFolderLs("/", input);
        DeviceFolder root = CreateFolder(ls.ContentRows, ls.RemainingInput);
        return CountAllFolderWithMaxSize(root, 100000);
    }

    private int PartTwo(List<string> input)
    {
        var r = GetFolderLs("/", input);
        DeviceFolder root = CreateFolder(r.ContentRows, r.RemainingInput);
        int toFreeUp = 30000000 - (70000000 - root.TotalSize);
        List<DeviceFolder> allFolders = new();
        allFolders.AddRange(ListFolders(root));
        allFolders.Add(root);
        return allFolders.OrderBy(x => x.TotalSize).ToList().Find(x => x.TotalSize >= toFreeUp).TotalSize;
    }
    private List<DeviceFolder> ListFolders(DeviceFolder f)
    {
        List<DeviceFolder> folders = new();
        foreach (var child in f.ChildFolders)
        {
            folders.AddRange(ListFolders(child));
            folders.Add(child);
        }
        return folders;
    }

    private int CountAllFolderWithMaxSize(DeviceFolder f, int size)
    {
        int total = 0;
        if (f.TotalSize <= size) total += f.TotalSize;
        foreach (var child in f.ChildFolders)
        {
            total += CountAllFolderWithMaxSize(child, size);
        }
        return total;
    }

    private DeviceFolder CreateFolder(List<string> contentRows, List<string> remainingInput)
    {
        DeviceFolder folder = new();
        foreach (var row in contentRows)
        {
            // Dir
            if (row.StartsWith("dir"))
            {
                var ls = GetFolderLs(row.SplitOn(Seperator.Space)[1], remainingInput);
                DeviceFolder child = CreateFolder(ls.ContentRows, ls.RemainingInput);
                folder.IndirectSize += child.TotalSize;
                folder.ChildFolders.Add(child);
            }
            // File
            else if (int.TryParse(row.SplitOn(Seperator.Space)[0], out int size))
            {
                folder.DirectSize += size;
            }
        }
        return folder;
    }

    private (List<string> ContentRows, List<string> RemainingInput) GetFolderLs(string folderName, List<string> input)
    {
        List<string> rows = new();
        bool go = false;
        int start = -1;
        int end = -1;
        for (int i = 0; i < input.Count; i++)
        {
            if (go)
            {
                if (input[i].StartsWith("$"))
                {
                    end = i;
                    break;
                }
                rows.Add(input[i]);
            }
            if (input[i].StartsWith("$"))
            {
                if (input[i] == $"$ cd {folderName}")
                {
                    if (input[i + 1] == "$ ls")
                    {
                        start = i;
                        go = true;
                        i++;
                        continue;
                    }
                }
            }
        }

        if (start != -1 && end != -1)
        {
            input.RemoveRange(start, end - start);
        }

        return (rows, input);
    }
}

public class DeviceFolder
{
    public DeviceFolder()
    {
        ChildFolders = new();
    }
    public int DirectSize { get; set; }
    public int IndirectSize { get; set; }
    public int TotalSize { get { return IndirectSize + DirectSize; } }
    public List<DeviceFolder> ChildFolders { get; set; }
}
