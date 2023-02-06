using Newtonsoft.Json;

namespace TwitterApi.Models;

public class TwitterMessageWrapper
{
    [JsonProperty("data")]
    public TwitterMessage? Data;
}