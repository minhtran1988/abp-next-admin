using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
/// <summary>
/// Enterprise customer change event push
/// </summary>
public abstract class ExternalContactChangeEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// Change type
    /// </summary>
    [XmlElement("ChangeType")]
    public string ChangeType { get; set; } = default!;
    /// <summary>
    /// UserID of enterprise service personnel
    /// </summary>
    [XmlElement("UserID")]
    public string UserId { get; set; } = default!;
    /// <summary>
    /// The userid of the external contact, please note that it is not the account of a corporate member.
    /// </summary>
    [XmlElement("ExternalUserID")]
    public string ExternalUserId { get; set; } = default!;
}
