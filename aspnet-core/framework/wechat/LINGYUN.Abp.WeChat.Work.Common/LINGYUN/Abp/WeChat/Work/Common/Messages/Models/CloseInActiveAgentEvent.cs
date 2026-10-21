using LINGYUN.Abp.WeChat.Common.Messages;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Temporary deactivation event for applications that have not been used for a long time
/// </summary>
[EventName("close_inactive_agent")]
public class CloseInActiveAgentEvent : WeChatWorkEventMessage
{
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<CloseInActiveAgentEvent>(this);
    }
}
