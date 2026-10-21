using LINGYUN.Abp.WeChat.Work.Messages;

namespace LINGYUN.Abp.Notifications.WeChat.Work;

/// <summary>
/// Enterprise WeChat message construction result
/// </summary>
public class WeChatWorkMessageFactoryResult
{
    /// <summary>
    /// Message carrier
    /// </summary>
    public WeChatWorkMessage Message { get; }

    /// <summary>
    /// Message carrier type, used for logging and troubleshooting
    /// </summary>
    public WeChatWorkMessageKind Kind { get; }

    public WeChatWorkMessageFactoryResult(WeChatWorkMessage message, WeChatWorkMessageKind kind)
    {
        Message = message;
        Kind = kind;
    }

    public static WeChatWorkMessageFactoryResult Create(WeChatWorkMessage message, WeChatWorkMessageKind kind)
    {
        return new WeChatWorkMessageFactoryResult(message, kind);
    }
}
