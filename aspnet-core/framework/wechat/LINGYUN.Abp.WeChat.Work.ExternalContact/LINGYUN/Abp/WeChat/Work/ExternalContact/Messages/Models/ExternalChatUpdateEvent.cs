using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
/// <summary>
/// Customer group change event push
/// </summary>
public abstract class ExternalChatUpdateEvent : ExternalChatChangeEvent
{
    /// <summary>
    /// Change details. Currently there are the following types:<br />
    /// add_member: Add members to the group<br />
    /// del_member: member withdraws from the group<br />
    /// change_owner: group owner change<br />
    /// change_name: group name change<br />
    /// change_notice: Group announcement change
    /// </summary>
    [XmlElement("UpdateDetail")]
    public string UpdateDetail { get; set; } = default!;
}
