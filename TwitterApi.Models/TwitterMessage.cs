using Newtonsoft.Json;

namespace TwitterApi.Models;

public class TwitterMessage
{
    [JsonProperty("id")]
    public string Id = "";
    
    [JsonProperty("text")]
    public string Text = "";
    
    [JsonProperty("created_at")]
    public DateTimeOffset CreatedAt;
}