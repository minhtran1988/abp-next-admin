using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Token.Models;

/// <summary>
/// WeChat access token return object
/// </summary>
public class WeChatWorkTokenResponse : WeChatWorkResponse
{
    /// <summary>
    /// Access token
    /// </summary>
    [JsonProperty("access_token")]
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = default!;
    /// <summary>
    /// Expiration time, unit (s)
    /// </summary>
    [JsonProperty("expires_in")]
    [JsonPropertyName("expires_in")]
    [System.Text.Json.Serialization.JsonConverter(typeof(NumberToStringConverter))]
    public int ExpiresIn { get; set; }

    public WeChatWorkToken ToWeChatWorkToken()
    {
        ThrowIfNotSuccess();
        return new WeChatWorkToken(AccessToken, ExpiresIn);
    }
}
