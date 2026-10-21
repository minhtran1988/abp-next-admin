using LINGYUN.Abp.WeChat.Common.Messages;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Official.Messages.Models;
/// <summary>
/// User unfollow event
/// </summary>
[EventName("user_un_subscribe")]
public class UserUnSubscribeEvent : WeChatEventMessage
{
    public override WeChatMessageEto ToEto()
    {
        return new WeChatOfficialEventMessageEto<UserUnSubscribeEvent>(this);
    }
}
