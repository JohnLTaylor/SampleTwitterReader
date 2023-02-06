using System.Net.Http.Headers;
using TwitterAPI.Interfaces;

namespace TwitterAPI;

public class TwitterApiConnector : ITwitterApiConnector
{
    private readonly ITwitterApiConnectorSettings _settings;

    public TwitterApiConnector(ITwitterApiConnectorSettings settings)
    {
        _settings = settings;
    }

    public Task<Stream> Open()
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", this._settings.BearerToken);
        return httpClient.GetStreamAsync("https://api.twitter.com/2/tweets/sample/stream");
    }
}