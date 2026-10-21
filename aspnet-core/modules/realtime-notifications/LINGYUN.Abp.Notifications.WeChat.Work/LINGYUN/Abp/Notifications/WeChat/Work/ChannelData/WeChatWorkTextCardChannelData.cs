using LINGYUN.Abp.Notifications;
using LINGYUN.Abp.WeChat.Work.Messages.Models;

namespace LINGYUN.Abp.Notifications.WeChat.Work;

/// <summary>
/// Text card message payload
/// </summary>
[NotificationChannelData(WeChatWorkNotificationPublishProvider.ProviderName, Priority = 400)]
public class WeChatWorkTextCardChannelData : WeChatWorkChannelData
{
    public override WeChatWorkMessageKind Kind => WeChatWorkMessageKind.TextCard;

    [NotificationChannelProperty(WeChatWorkNotificationDataKeys.TextCardMessage)]
    public virtual TextCardMessage? TextCard { get; set; }
}
