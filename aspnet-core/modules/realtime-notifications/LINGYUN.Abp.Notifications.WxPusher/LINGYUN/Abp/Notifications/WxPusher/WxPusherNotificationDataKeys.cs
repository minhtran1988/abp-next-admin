namespace LINGYUN.Abp.Notifications.WxPusher;

/// <summary>
/// WxPusher notification data key
/// </summary>
public static class WxPusherNotificationDataKeys
{
    public const string Prefix = "wx-pusher:";

    /// <summary>
    /// The jump address after the message is clicked
    /// </summary>
    public const string Url = Prefix + "url";
}
