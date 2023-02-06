using TwitterAPI.Interfaces;
using TwitterApi.Models;
using TwitterApi.Utils;

namespace TwitterReader;

public class TwitterReader : ITwitterReader
{
    private readonly ITwitterApiConnector _connector;
    private readonly IHashTagScanner _scanner;

    public TwitterReader(ITwitterApiConnector connector, IHashTagScanner scanner)
    {
        _connector = connector;
        _scanner = scanner;
    }

    public async Task Run(CancellationToken ctx)
    {
        while (!ctx.IsCancellationRequested)
        {
            var reader = new TwitterApiStreamReader<TwitterMessageWrapper>(_connector);

            await foreach (var wrapper in reader.ReadAync(ctx))
            {
                var msg = wrapper.Data;

                if (msg == null)
                {
                    continue;
                }
                
                _scanner.Enqueue(msg);
            }
        }
    }
}