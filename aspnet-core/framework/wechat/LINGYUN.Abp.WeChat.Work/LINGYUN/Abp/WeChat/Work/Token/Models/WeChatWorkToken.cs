namespace LINGYUN.Abp.WeChat.Work.Token.Models;

/// <summary>
/// Enterprise WeChat token
/// </summary>
public class WeChatWorkToken
{
    /// <summary>
    /// Access token
    /// </summary>
    public string AccessToken { get; set; } = default!;
    /// <summary>
    /// Expiration time, unit (s)
    /// </summary>
    public int ExpiresIn { get; set; }
    public WeChatWorkToken()
    {

    }
    public WeChatWorkToken(string token, int expiresIn)
    {
        AccessToken = token;
        ExpiresIn = expiresIn;
    }
}
