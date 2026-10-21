using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
/// <summary>
/// Enterprise customer label change event push
/// </summary>
public abstract class ExternalTagChangeEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// Change type
    /// </summary>
    [XmlElement("ChangeType")]
    public string ChangeType { get; set; } = default!;
    /// <summary>
    /// The rule group id to which the label or label group belongs is only called back to the "Customer Contact" application
    /// </summary>
    [XmlElement("StrategyId")]
    public string StrategyId { get; set; } = default!;
}
