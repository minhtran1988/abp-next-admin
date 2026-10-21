using LINGYUN.Abp.WeChat.Common.Messages;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Scan the QR code to push the event and pop up the "message receiving" prompt box.
/// </summary>
[EventName("scancode_waitmsg")]
public class ScanCodeWaitMsgPushEvent : ScanCodeEvent
{
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<ScanCodeWaitMsgPushEvent>(this);
    }
}
