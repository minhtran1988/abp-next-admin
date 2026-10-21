using System.Collections.Generic;
using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Messages.Models;
/// <summary>
/// Customer group member change event push
/// </summary>
public abstract class ExternalChatChangeMemberEvent : ExternalChatUpdateEvent
{
    /// <summary>
    /// Number of member changes
    /// </summary>
    [XmlElement("MemChangeCnt")]
    public int MemChangeCnt { get; set; }
    /// <summary>
    /// Changed member list
    /// </summary>
    [XmlElement("MemChangeList")]
    public List<ExternalChatChangeMember> MemChangeList { get; set; } = new List<ExternalChatChangeMember>();
    /// <summary>
    /// The version number of group members before the change
    /// </summary>
    [XmlElement("LastMemVer")]
    public string LastMemVer { get; set; } = default!;
    /// <summary>
    /// The changed version number of group members
    /// </summary>
    [XmlElement("CurMemVer")]
    public string CurMemVer { get; set; } = default!;
}

public class ExternalChatChangeMember
{
    /// <summary>
    /// Member ID
    /// </summary>
    [XmlElement("Item")]
    public string UserId { get; set; } = default!;
}
