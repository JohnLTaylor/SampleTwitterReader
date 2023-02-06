namespace TwitterReader;

class Reporter : IReporter
{
    private readonly IHashTagStorage _storage;

    public Reporter(IHashTagStorage storage)
    {
        _storage = storage;
    }
    
    public async Task Run(CancellationToken ctx)
    {
        while (!ctx.IsCancellationRequested)
        {
            foreach (var tag in _storage.Top())
            {
                Console.WriteLine($"{tag.Name}: {tag.Count}");
            }

            await Task.Delay(TimeSpan.FromSeconds(10), ctx);
            
            Console.WriteLine("");
        }
    }
}