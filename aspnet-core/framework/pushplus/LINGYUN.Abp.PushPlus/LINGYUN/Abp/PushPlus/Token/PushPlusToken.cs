using Newtonsoft.Json;

namespace LINGYUN.Abp.PushPlus.Token;

public class PushPlusToken
{
    /// <summary>
    /// Access token, subsequent requests need to be added to the header
    /// </summary>
    [JsonProperty("accessKey")]
    public string AccessKey { get; set; } = default!;
    /// <summary>
    /// Expiration time, need to be obtained again after expiration
    /// </summary>
    [JsonProperty("expiresIn")]
    public int ExpiresIn { get; set; }

    public PushPlusToken()
    {

    }

    public PushPlusToken(string accessKey, int expiresIn)
    {
        AccessKey = accessKey;
        ExpiresIn = expiresIn;
    }
}
