using LINGYUN.Abp.WeChat.Common.Messages;
using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Official.Messages;
public abstract class WeChatOfficialGeneralMessage : WeChatGeneralMessage
{
    /// <summary>
    /// The data ID of the message (only available if the message comes from an article)
    /// </summary>
    [XmlElement("MsgDataId")]
    public string? MsgDataId { get; set; }
    /// <summary>
    /// When there are multiple pictures and texts, the article number starts from 1 (the message is only available if it comes from an article)
    /// </summary>
    [XmlElement("Idx")]
    public string? Idx { get; set; }
}
