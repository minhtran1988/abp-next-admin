using LINGYUN.Abp.WeChat.Common.Messages;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Enterprise Internet sharing application events
/// </summary>
/// <remarks>
/// The triggering time of this event is:
/// 1. Higher-level enterprises share self-built applications with lower-level enterprises for use
/// 2. The superior enterprise removes the subordinate enterprise from the shared application
/// </remarks>
[EventName("share_agent_change")]
public class ShareAgentChangeEvent : WeChatWorkEventMessage
{
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<ShareAgentChangeEvent>(this);
    }
}
