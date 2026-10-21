using LINGYUN.Abp.Notifications;
using LINGYUN.Abp.WeChat.Work.Messages.Models;

namespace LINGYUN.Abp.Notifications.WeChat.Work;

/// <summary>
/// Graphic message (mpnews) payload
/// </summary>
[NotificationChannelData(WeChatWorkNotificationPublishProvider.ProviderName, Priority = 200)]
public class WeChatWorkMpNewChannelData : WeChatWorkChannelData
{
    public override WeChatWorkMessageKind Kind => WeChatWorkMessageKind.MpNews;

    [NotificationChannelProperty(WeChatWorkNotificationDataKeys.MpNewMessage)]
    public virtual MpNewMessagePayload? MpNewMessage { get; set; }
}
