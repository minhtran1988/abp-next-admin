using LINGYUN.Abp.WeChat.Common;
using Newtonsoft.Json;

namespace LINGYUN.Abp.WeChat.Token;

/// <summary>
/// WeChat access token return object
/// </summary>
public class WeChatTokenResponse
{
    /// <summary>
    /// Error code
    /// </summary>
    [JsonProperty("errcode")]
    public int ErrorCode { get; set; } = default!;
    /// <summary>
    /// Error message
    /// </summary>
    [JsonProperty("errmsg")]
    public string? ErrorMessage { get; set; }
    /// <summary>
    /// Access token
    /// </summary>
    [JsonProperty("access_token")]
    public string AccessToken { get; set; } = default!;
    /// <summary>
    /// Expiration time, unit (s)
    /// </summary>
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    public WeChatToken ToWeChatToken()
    {
        if(ErrorCode != 0)
        {
            throw new AbpWeChatException(ErrorCode.ToString(), ErrorMessage!);
        }
        return new WeChatToken(AccessToken, ExpiresIn);
    }
}
