using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
/// <summary>
/// Generate session callback event push
/// </summary>
[EventName("external_contact_msgaudit_notify")]
public class ExternalContactMsgAuditNotifyEvent : WeChatWorkEventMessage
{
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<ExternalContactMsgAuditNotifyEvent>(this);
    }
}
