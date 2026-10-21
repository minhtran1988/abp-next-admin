using LINGYUN.Abp.WeChat.Common.Messages;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Long-term unused application re-activation event
/// </summary>
[EventName("reopen_inactive_agent")]
public class ReOpenInActiveAgentEevent : WeChatWorkEventMessage
{
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<ReOpenInActiveAgentEevent>(this);
    }
}