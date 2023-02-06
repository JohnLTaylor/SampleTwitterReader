using Microsoft.Extensions.DependencyInjection;
using TwitterAPI;
using TwitterAPI.Interfaces;

namespace TwitterReader;

public static class StartUp
{
    public static IServiceProvider Init()
    {
        var services = new ServiceCollection();

        services.AddSingleton<ITwitterApiConnectorSettings, TwitterApiConnectorSettings>();
        services.AddSingleton<ITwitterApiConnector,TwitterApiConnector>();
        services.AddSingleton<ITwitterReader, TwitterReader>();
        services.AddSingleton<IHashTagStorage, HashTagStorage>();
        services.AddSingleton<IHashTagScanner, HashTagScanner>();
        services.AddSingleton<IReporter, Reporter>();

        return services.BuildServiceProvider();
    }
}