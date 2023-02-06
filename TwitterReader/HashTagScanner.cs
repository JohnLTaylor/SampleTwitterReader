using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using TwitterApi.Models;

namespace TwitterReader;

class HashTagScanner : IHashTagScanner
{
    private readonly IHashTagStorage _storage;
    private static readonly Channel<TwitterMessage> Channel;

    static HashTagScanner()
    {
        var opts = new BoundedChannelOptions(100)
        {
            FullMode = BoundedChannelFullMode.DropWrite
        };
        Channel = System.Threading.Channels.Channel.CreateBounded<TwitterMessage>(opts);
    }

    public HashTagScanner(IHashTagStorage storage)
    {
        _storage = storage;
    }

    public void Enqueue(TwitterMessage msg)
    {
        Channel.Writer.TryWrite(msg);
    }

    public async Task Run(CancellationToken ctx)
    {
        while (!ctx.IsCancellationRequested)
        {
            var msg = await Channel.Reader.ReadAsync(ctx);

            if (ctx.IsCancellationRequested)
            {
                break;
            }
            
            _storage.IncrementTags(GetTags(msg.Text).Select(t => new HashTagCount(t)));
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private List<string> GetTags(string msgText)
    {
        // only count one tag per twit
        return Regex.Matches(msgText, @"(?:#)[^#\s]+").Select(p => p.Value.ToLowerInvariant()).Distinct().ToList();
    }
}