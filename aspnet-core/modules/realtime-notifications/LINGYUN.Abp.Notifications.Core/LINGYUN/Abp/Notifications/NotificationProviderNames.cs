namespace LINGYUN.Abp.Notifications;

/// <summary>
/// Built-in notification provider
/// </summary>
public static class NotificationProviderNames
{
    /// <summary>
    /// SignalR real-time notification
    /// </summary>
    public const string SignalR = "SignalR";
    /// <summary>
    /// SMS notification
    /// </summary>
    public const string Sms = "Sms";
    /// <summary>
    /// Email notification
    /// </summary>
    public const string Emailing = "Emailing";
    /// <summary>
    /// WeChat mini program template notification
    /// </summary>
    public const string WechatMiniProgram = "WeChat.MiniProgram";
    /// <summary>
    /// Enterprise WeChat application message
    /// </summary>
    public const string WechatWork = "WeChat.Work";
}
