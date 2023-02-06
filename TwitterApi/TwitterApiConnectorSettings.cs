using TwitterAPI.Interfaces;

namespace TwitterAPI;

public class TwitterApiConnectorSettings : ITwitterApiConnectorSettings
{
    public string BearerToken { get; }

    public TwitterApiConnectorSettings()
    {
        BearerToken = Environment.GetEnvironmentVariable("TWITTER_BEARER_TOKEN")
                      ?? throw new InvalidOperationException("Please set the TWITTER_BEARER_TOKEN environment variable");
    }
}