using System;
using Volo.Abp;

namespace LINGYUN.Abp.Notifications.WeChat.Work;

/// <summary>
/// Enterprise WeChat message public options
/// </summary>
public class WeChatWorkMessageOptions
{
    /// <summary>
    /// Message application ID, filled in by the channel when publishing
    /// </summary>
    public string AgentId { get; set; } = default!;

    /// <summary>
    /// Is it confidential information?
    /// </summary>
    public bool? Safe { get; set; }

    /// <summary>
    /// Whether to enable id translation
    /// </summary>
    public bool? EnableIdTrans { get; set; }

    /// <summary>
    /// Whether to enable duplicate message checking
    /// </summary>
    public bool? EnableDuplicateCheck { get; set; }

    /// <summary>
    /// Time interval for repeated message checking (seconds)
    /// </summary>
    public int? DuplicateCheckInterval { get; set; }

    /// <summary>
    /// Parse public options from notification data
    /// </summary>
    public static WeChatWorkMessageOptions From(
        INotificationChannelDataResolver resolver,
        NotificationData notificationData)
    {
        Check.NotNull(resolver, nameof(resolver));
        Check.NotNull(notificationData, nameof(notificationData));

        return new WeChatWorkMessageOptions
        {
            Safe = resolver.GetValue<bool>(notificationData, WeChatWorkNotificationDataKeys.Safe),
            EnableIdTrans = resolver.GetValue<bool>(notificationData, WeChatWorkNotificationDataKeys.EnableIdTrans),
            EnableDuplicateCheck = resolver.GetValue<bool>(notificationData, WeChatWorkNotificationDataKeys.EnableDuplicateCheck),
            DuplicateCheckInterval = resolver.GetValue<int>(notificationData, WeChatWorkNotificationDataKeys.DuplicateCheckInterval)
        };
    }

    /// <summary>
    /// Confidential message mark
    /// </summary>
    public byte SafeValue => Safe == true ? (byte)1 : (byte)0;

    /// <summary>
    /// id translation tag
    /// </summary>
    public byte EnableIdTransValue => EnableIdTrans == true ? (byte)1 : (byte)0;

    /// <summary>
    /// Duplicate message check mark
    /// </summary>
    public byte EnableDuplicateCheckValue => EnableDuplicateCheck == true ? (byte)1 : (byte)0;

    /// <summary>
    /// Repeat message check interval, if not set, use the default value of Enterprise WeChat
    /// </summary>
    public int DuplicateCheckIntervalValue => DuplicateCheckInterval ?? 1800;
}
