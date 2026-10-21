namespace LINGYUN.Abp.WeChat.Token;

/// <summary>
/// WeChat token
/// </summary>
public class WeChatToken
{
    /// <summary>
    /// Access token
    /// </summary>
    public string AccessToken { get; set; } = default!;
    /// <summary>
    /// Expiration time, unit (s)
    /// </summary>
    public int ExpiresIn { get; set; }
    public WeChatToken()
    {

    }
    public WeChatToken(string token, int expiresIn)
    {
        AccessToken = token;
        ExpiresIn = expiresIn;
    }
}
