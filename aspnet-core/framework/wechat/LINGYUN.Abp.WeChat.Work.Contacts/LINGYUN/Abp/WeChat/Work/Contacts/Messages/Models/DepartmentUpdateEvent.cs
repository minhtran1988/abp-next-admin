using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Messages.Models;
/// <summary>
/// Department change event
/// </summary>
public abstract class DepartmentUpdateEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// DepartmentId
    /// </summary>
    [XmlElement("Id")]
    public int Id { get; set; }
    /// <summary>
    /// Department name
    /// </summary>
    [XmlElement("Name")]
    public string Name { get; set; } = default!;
    /// <summary>
    /// Parent department id
    /// </summary>
    [XmlElement("ParentId", IsNullable = true)]
    public int? ParentId { get; set; }
}
