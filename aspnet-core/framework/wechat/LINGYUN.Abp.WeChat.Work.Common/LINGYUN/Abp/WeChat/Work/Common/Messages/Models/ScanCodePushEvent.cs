using LINGYUN.Abp.WeChat.Common.Messages;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Event push of scan code push event
/// </summary>
[EventName("scancode_push")]
public class ScanCodePushEvent : ScanCodeEvent
{
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<ScanCodePushEvent>(this);
    }
}
