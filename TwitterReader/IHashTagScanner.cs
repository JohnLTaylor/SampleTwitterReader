using TwitterApi.Models;

namespace TwitterReader;

public interface IHashTagScanner
{
    void Enqueue(TwitterMessage msg);
    Task Run(CancellationToken ctx);
}