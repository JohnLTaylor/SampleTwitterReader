namespace TwitterReader;

public interface IHashTagStorage
{
    void IncrementTags(IEnumerable<HashTagCount> tags);
    IEnumerable<HashTagCount> Top(int count = 5);
}