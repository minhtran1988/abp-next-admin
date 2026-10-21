using LINGYUN.Abp.WeChat.Common.Messages;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Pop up the event push of the WeChat photo album sender
/// </summary>
[EventName("pic_weixin")]
public class PictureWeixinPushEvent : PicturePushEvent
{
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<PictureWeixinPushEvent>(this);
    }
}