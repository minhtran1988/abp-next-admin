using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
/// <summary>
/// Customer group announcement change event push
/// </summary>
[EventName("external_chat_change_notice")]
public class ExternalChatChangeNoticeEvent : ExternalChatUpdateEvent
{
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<ExternalChatChangeNoticeEvent>(this);
    }
}
