using LINGYUN.Abp.WeChat.Common.Messages;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Pop up the event push of the system to take pictures and send pictures
/// </summary>
[EventName("pic_sysphoto")]
public class PictureSystemPushEvent : PicturePushEvent
{
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<PictureSystemPushEvent>(this);
    }
}
