using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
/// <summary>
/// Customer group change event push
/// </summary>
public abstract class ExternalChatChangeEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// Change type
    /// </summary>
    [XmlElement("ChangeType")]
    public string ChangeType { get; set; } = default!;
    /// <summary>
    /// Group ID
    /// </summary>
    [XmlElement("ChatId")]
    public string ChatId { get; set; } = default!;
}

