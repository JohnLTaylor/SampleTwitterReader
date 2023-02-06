using Newtonsoft.Json;
using TwitterAPI.Interfaces;

namespace TwitterApi.Utils;

public class TwitterApiStreamReader<T> where T:class
{
    private readonly ITwitterApiConnector _connector;

    public TwitterApiStreamReader(ITwitterApiConnector connector)
    {
        _connector = connector;
    }

    public async IAsyncEnumerable<T> ReadAync(CancellationToken ctx)
    {
        await using var s = await _connector.Open();
        using var sr = new StreamReader(s);
        {
            while (!sr.EndOfStream && !ctx.IsCancellationRequested)
            {
                var block = await sr.ReadLineAsync();

                if (block == null)
                {
                    continue;
                }

                var obj = JsonConvert.DeserializeObject<T>(block);

                if (obj == null)
                {
                    continue;
                }
                        
                yield return  obj;
            }
        }
    }
}