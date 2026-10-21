using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Common.Messages;
/// <summary>
/// WeChat ordinary messages
/// </summary>
public abstract class WeChatGeneralMessage : WeChatMessage
{
    /// <summary>
    /// Message id, 64-bit integer
    /// </summary>
    [XmlElement("MsgId")]
    public long MsgId { get; set; }
}
