using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Common;

public static class Finder
{
    public static List<int> AllIndexesOf(this string a, char c)
    {
        List<int> indexes = new();
        int from = 0;
        while (true)
        {
            try
            {
                int i = a.IndexOf(c, from);
                indexes.Add(i);
                from = i + 1;
            }
            catch (Exception)
            {
                break;
            }
        }
        return indexes;
    }
}
