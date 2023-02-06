// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using TwitterReader;

var sp = StartUp.Init();

var cts = new CancellationTokenSource();

Console.CancelKeyPress += delegate {
    cts.Cancel();
};

await Task.WhenAll(
    sp.GetRequiredService<ITwitterReader>().Run(cts.Token),
    sp.GetRequiredService<IHashTagScanner>().Run(cts.Token),
    sp.GetRequiredService<IHashTagScanner>().Run(cts.Token),
    sp.GetRequiredService<IHashTagScanner>().Run(cts.Token),
    sp.GetRequiredService<IHashTagScanner>().Run(cts.Token),
    sp.GetRequiredService<IReporter>().Run(cts.Token));
