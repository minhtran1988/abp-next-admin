using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
/// <summary>
/// Delete follow-up member events
/// </summary>
[EventName("external_contact_del_follow_user")]
public class ExternalContactDeleteFollowUserEvent : ExternalContactChangeEvent
{
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<ExternalContactDeleteFollowUserEvent>(this);
    }
}
