using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Common;

public static class Indexes
{
    public static int LoopAround(this int index, int collectionLength)
    {
        if (index > collectionLength)
        {
            index -= collectionLength * (index / collectionLength);

        }
        else if (index < 0)
        {
            index += collectionLength * ((Math.Abs(index) / collectionLength) + 1);
        }
        return index;
    }

    public static int LoopAround(this long index, int collectionLength)
    {
        if (index > collectionLength)
        {
            index -= collectionLength * (index / collectionLength);

        }
        else if (index < 0)
        {
            index += collectionLength * ((Math.Abs(index) / collectionLength) + 1);
        }
        return (int)index;
    }
}
