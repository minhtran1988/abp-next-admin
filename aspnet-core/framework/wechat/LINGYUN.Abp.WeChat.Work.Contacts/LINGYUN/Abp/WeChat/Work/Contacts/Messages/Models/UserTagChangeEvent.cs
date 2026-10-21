using LINGYUN.Abp.WeChat.Common.Messages;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;
using Volo.Abp.EventBus;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Messages.Models;
/// <summary>
/// Tag member change event
/// </summary>
[EventName("update_tag")]
public class UserTagChangeEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// TagId
    /// </summary>
    [XmlElement("TagId")]
    public string TagId { get; set; } = default!;
    /// <summary>
    /// List of new member userids in the tag, separated by commas
    /// </summary>
    [XmlElement("AddUserItems", IsNullable = true)]
    public string? AddUserItems { get; set; }
    /// <summary>
    /// List of member userids to be deleted in the tag, separated by commas
    /// </summary>
    [XmlElement("DelUserItems", IsNullable = true)]
    public string? DelUserItems { get; set; }
    /// <summary>
    /// List of new department IDs in the tag, separated by commas
    /// </summary>
    [XmlElement("AddPartyItems", IsNullable = true)]
    public string? AddPartyItems { get; set; }
    /// <summary>
    /// List of department IDs to be deleted in the tag, separated by commas
    /// </summary>
    [XmlElement("DelPartyItems", IsNullable = true)]
    public string? DelPartyItems { get; set; }
    public override WeChatMessageEto ToEto()
    {
        return new WeChatWorkEventMessageEto<UserTagChangeEvent>(this);
    }
}
