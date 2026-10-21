using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Common.Messages.Models;
/// <summary>
/// Voice message
/// </summary>
[EventName("voice")]
public class VoiceMessage : WeChatWorkGeneralMessage
{
    /// <summary>
    /// Voice format, such as amr, speex, etc.
    /// </summary>
    [XmlElement("Format")]
    public string Format { get; set; } = default!;
    /// <summary>
    /// Voice message media id, you can call the temporary material interface to pull the media
    /// </summary>
    [XmlElement("MediaId")]
    public string MediaId { get; set; } = default!;
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkGeneralMessageEto<VoiceMessage>(this);
    }
}
