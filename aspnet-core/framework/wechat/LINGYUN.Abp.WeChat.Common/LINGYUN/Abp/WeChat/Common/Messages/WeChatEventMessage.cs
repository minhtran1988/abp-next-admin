using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Common.Messages;
/// <summary>
/// WeChat event message
/// </summary>
public abstract class WeChatEventMessage : WeChatMessage
{
    /// <summary>
    /// Event type
    /// </summary>
    [XmlElement("Event")]
    public string Event { get; set; } = default!;
}
