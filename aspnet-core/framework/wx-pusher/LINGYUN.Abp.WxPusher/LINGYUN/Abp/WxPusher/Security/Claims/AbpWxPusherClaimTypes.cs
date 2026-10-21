namespace LINGYUN.Abp.WxPusher.Security.Claims;

public static class AbpWxPusherClaimTypes
{
    /// <summary>
    /// The user's unique identifier
    /// </summary>
    public static string Uid { get; set; } = "wxp-uid";
    /// <summary>
    /// User subscribes to topic
    /// </summary>
    public static string Topic { get; set; } = "wxp-topic";
}
