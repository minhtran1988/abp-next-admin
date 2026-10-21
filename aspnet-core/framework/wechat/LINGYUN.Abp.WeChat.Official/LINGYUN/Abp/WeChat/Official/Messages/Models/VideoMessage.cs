using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Official.Messages.Models;
/// <summary>
/// Video message
/// </summary>
[EventName("video")]
public class VideoMessage : WeChatOfficialGeneralMessage
{
    /// <summary>
    /// The media ID of the video message thumbnail, you can call the multimedia file download interface to pull the data.
    /// </summary>
    [XmlElement("ThumbMediaId")]
    public string ThumbMediaId { get; set; } = default!;
    /// <summary>
    /// Video message media id, you can call the temporary material interface to pull data.
    /// </summary>
    [XmlElement("MediaId")]
    public string MediaId { get; set; } = default!;
    public override WeChatMessageEto ToEto()
    {
        return new WeChatOfficialGeneralMessageEto<VideoMessage>(this);
    }
}
