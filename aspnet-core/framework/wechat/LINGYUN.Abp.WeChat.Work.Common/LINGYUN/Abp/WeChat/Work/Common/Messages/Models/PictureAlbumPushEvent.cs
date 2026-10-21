using LINGYUN.Abp.WeChat.Common.Messages;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Pop-up event push for taking pictures or posting pictures in album
/// </summary>
[EventName("pic_photo_or_album")]
public class PictureAlbumPushEvent : PicturePushEvent
{
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<PictureAlbumPushEvent>(this);
    }
}