namespace LINGYUN.Abp.WeChat.OpenId;

public class WeChatOpenId
{
    /// <summary>
    /// The user's unique identifier on the open platform will be returned if the UnionID issuance conditions are met.
    /// </summary>
    public string? UnionId { get; set; }
    /// <summary>
    /// User unique identifier
    /// </summary>
    public string OpenId { get; set; } = default!;
    /// <summary>
    /// Session key
    /// </summary>
    public string SessionKey { get; set; } = default!;

    public WeChatOpenId()
    {

    }

    public WeChatOpenId(string openId, string sessionKey, string? unionId = null)
    {
        OpenId = openId;
        SessionKey = sessionKey;
        UnionId = unionId;
    }
}
