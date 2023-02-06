using System.Diagnostics;

namespace TwitterReader;

class HashTagStorage : IHashTagStorage
{
    private readonly HashSet<HashTagCount> _values = new(new HashTagCountByNameComparer());
    
    public void IncrementTags(IEnumerable<HashTagCount> tags)
    {
        lock (_values)
        {
            foreach (var tag in tags)
            {
                if (_values.TryGetValue(tag, out var actualValue))
                {
                    actualValue.Count += tag.Count;
                }
                else
                {
                    _values.Add(tag);
                }                    
            }
        }
    }

    public IEnumerable<HashTagCount> Top(int count = 5)
    {
        List<HashTagCount> values;
        lock (_values)
        {
            values = _values.Select(v => v.Clone()).ToList();
        }

        values.Sort(new HashTagCountByCountDescComparer());

        return values.Take(count).ToList();
    }
}