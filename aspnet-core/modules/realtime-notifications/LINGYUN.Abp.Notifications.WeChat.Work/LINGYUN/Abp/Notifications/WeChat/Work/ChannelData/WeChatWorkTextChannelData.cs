namespace LINGYUN.Abp.Notifications.WeChat.Work;

/// <summary>
/// Text message payload
/// </summary>
/// <remarks>
/// Does not participate in channel analysis: text messages are derived from notification standard data (title/message),
/// The producer does not need to prepare any channel-specific data for it.
/// </remarks>
public class WeChatWorkTextChannelData : WeChatWorkChannelData
{
    public override WeChatWorkMessageKind Kind => WeChatWorkMessageKind.Text;

    /// <summary>
    /// Message content, if empty, use notification standard data
    /// </summary>
    public virtual string? Content { get; set; }
}
