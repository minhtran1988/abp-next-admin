using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
/// <summary>
/// Customer group owner change event push
/// </summary>
[EventName("external_chat_change_owner")]
public class ExternalChatChangeOwnerEvent : ExternalChatUpdateEvent
{
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<ExternalChatChangeOwnerEvent>(this);
    }
}
