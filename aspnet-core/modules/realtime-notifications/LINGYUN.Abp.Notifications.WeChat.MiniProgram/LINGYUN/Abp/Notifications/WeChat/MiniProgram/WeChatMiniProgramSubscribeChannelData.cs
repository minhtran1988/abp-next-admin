using LINGYUN.Abp.Notifications;

namespace LINGYUN.Abp.Notifications.WeChat.MiniProgram;

/// <summary>
/// WeChat mini program channel data (load)
/// </summary>
/// <remarks>
/// <para>
/// The payload of the mini program subscription message is "broadcast": in addition to the template identifier and jump parameters,
/// Template data (<c>data</c>) comes directly from the attribute bag of notification data, so it is only declared here
/// Scalar fields that require typed reading.
/// </para>
/// <para>
/// The key name has the prefix of this channel (<see cref="WeChatMiniProgramNotificationDataKeys.Prefix"/>),
/// Reading is case-insensitive; existence is deduced from the template identification key, so the jump parameter is declared as
/// "Only read, do not participate in matching" - the producer may only write the template identifier.
/// </para>
/// <para>
/// Unprefixed keys left over from history are read by the channel with fallback during parsing (see
/// <c>WeChatMiniProgramNotificationPublishProvider.ResolveChannelData</c>)。
/// </para>
/// </remarks>
[NotificationChannelData(WeChatMiniProgramNotificationPublishProvider.ProviderName, Priority = 100)]
public class WeChatMiniProgramSubscribeChannelData : NotificationChannelData
{
    /// <summary>
    /// Mini program template identification (load identification key)
    /// </summary>
    [NotificationChannelProperty(WeChatMiniProgramNotificationDataKeys.TemplateId)]
    public virtual string? TemplateId { get; set; }

    /// <summary>
    /// Click to jump to the mini program page
    /// </summary>
    [NotificationChannelProperty(WeChatMiniProgramNotificationDataKeys.RedirectPage, IgnoreWhenMatching = true)]
    public virtual string? RedirectPage { get; set; }

    /// <summary>
    /// Jump applet type
    /// </summary>
    [NotificationChannelProperty(WeChatMiniProgramNotificationDataKeys.WeAppState, IgnoreWhenMatching = true)]
    public virtual string? WeAppState { get; set; }

    /// <summary>
    /// Mini program language
    /// </summary>
    [NotificationChannelProperty(WeChatMiniProgramNotificationDataKeys.WeAppLanguage, IgnoreWhenMatching = true)]
    public virtual string? WeAppLanguage { get; set; }
}
