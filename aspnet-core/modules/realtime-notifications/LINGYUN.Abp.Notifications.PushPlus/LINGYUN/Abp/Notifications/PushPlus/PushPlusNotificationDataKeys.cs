namespace LINGYUN.Abp.Notifications.PushPlus;

/// <summary>
/// PushPlus notification data key
/// </summary>
public static class PushPlusNotificationDataKeys
{
    public const string Prefix = "push-plus:";

    /// <summary>
    /// webhook address
    /// </summary>
    public const string Webhook = Prefix + "webhook";
    /// <summary>
    /// Callback address
    /// </summary>
    public const string CallbackUrl = Prefix + "callback";
}
