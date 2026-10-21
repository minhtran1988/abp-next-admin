namespace LINGYUN.Abp.Notifications.WeChat.Work;

/// <summary>
/// markdown message payload
/// </summary>
/// <remarks>
/// Does not participate in channel resolution: deduced from the <c>ContentType</c> defined by the notification,
/// The producer does not need to prepare any channel-specific data for it.
/// </remarks>
public class WeChatWorkMarkdownChannelData : WeChatWorkChannelData
{
    public override WeChatWorkMessageKind Kind => WeChatWorkMessageKind.Markdown;

    /// <summary>
    /// Message content, if empty, use notification standard data
    /// </summary>
    public virtual string? Content { get; set; }
}
