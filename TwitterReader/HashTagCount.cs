using System.Runtime.CompilerServices;

namespace TwitterReader;

public class HashTagCount
{
    public readonly string Name;
    public long Count;

    public HashTagCount(string name)
    {
        Name = name;
        Count = 1;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HashTagCount Clone()
    {
        return new HashTagCount(Name)
        {
            Count = Count
        };
    }
}

public class HashTagCountByNameComparer : IEqualityComparer<HashTagCount>
{
    public bool Equals(HashTagCount? x, HashTagCount? y)
    {
        return x?.Name == y?.Name;
    }

    public int GetHashCode(HashTagCount obj)
    {
        return obj.Name.GetHashCode();
    }
}

public class HashTagCountByCountDescComparer : IComparer<HashTagCount>
{
    public int Compare(HashTagCount? x, HashTagCount? y)
    {
        long diff = (y?.Count ?? 0) - (x?.Count ?? 0);

        if (diff == 0)
        {
            return 0;
        }
        else if (diff < 0)
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }
}