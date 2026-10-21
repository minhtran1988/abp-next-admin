using System;

namespace LINGYUN.Abp.Notifications.WeChat.Work;

/// <summary>
/// Enterprise WeChat message receiver
/// </summary>
/// <remarks>
/// Enterprise WeChat requires that touser / toparty / totag cannot be empty at the same time.
/// Therefore the receiver must be passed to the message construction process as a whole.
/// </remarks>
public class WeChatWorkNotificationContact
{
    /// <summary>
    /// Member ID list, multiple receivers are separated by "|"
    /// </summary>
    public string? ToUser { get; set; }

    /// <summary>
    /// Department ID list, multiple recipients are separated by "|"
    /// </summary>
    public string? ToParty { get; set; }

    /// <summary>
    /// Tag ID list, multiple receivers separated by "|"
    /// </summary>
    public string? ToTag { get; set; }

    public bool IsEmpty()
    {
        return ToUser.IsNullOrWhiteSpace()
            && ToParty.IsNullOrWhiteSpace()
            && ToTag.IsNullOrWhiteSpace();
    }

    public static WeChatWorkNotificationContact From(
        string? toUser = null,
        string? toParty = null,
        string? toTag = null)
    {
        return new WeChatWorkNotificationContact
        {
            ToUser = toUser,
            ToParty = toParty,
            ToTag = toTag
        };
    }
}
