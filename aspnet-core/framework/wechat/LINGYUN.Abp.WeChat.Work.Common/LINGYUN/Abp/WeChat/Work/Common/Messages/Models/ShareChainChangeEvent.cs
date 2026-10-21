using LINGYUN.Abp.WeChat.Common.Messages;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Share application events upstream and downstream
/// </summary>
/// <remarks>
/// The triggering time of this event is:
/// 1. Upstream companies share self-built applications with downstream companies for use
/// 2. The upstream company removes the downstream company from the shared application
/// </remarks>
[EventName("share_chain_change")]
public class ShareChainChangeEvent : WeChatWorkEventMessage
{
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<ShareChainChangeEvent>(this);
    }
}
