using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Official.Messages.Models;
/// <summary>
/// Voice message
/// </summary>
[EventName("voice")]
public class VoiceMessage : WeChatOfficialGeneralMessage
{
    /// <summary>
    /// Voice format, such as amr, speex, etc.
    /// </summary>
    [XmlElement("Format")]
    public string Format { get; set; } = default!;
    /// <summary>
    /// Speech recognition results, UTF8 encoding
    /// </summary>
    /// <remarks>
    /// After enabling speech recognition, every time the user sends a voice to the official account, WeChat will add a Recognition field (
    /// Note: Due to client caching, developers can enable or disable the speech recognition function, which will take effect immediately for new followers and will take 24 hours to take effect for already followed users. Developers can re-follow this account for testing).
    /// </remarks>
    [XmlElement("Recognition")]
    public string Recognition { get; set; } = default!;
    /// <summary>
    /// Voice message media id, you can call the temporary material interface to pull the media
    /// </summary>
    [XmlElement("MediaId")]
    public string MediaId { get; set; } = default!;
    public override WeChatMessageEto ToEto()
    {
        return new WeChatOfficialGeneralMessageEto<VoiceMessage>(this);
    }
}
