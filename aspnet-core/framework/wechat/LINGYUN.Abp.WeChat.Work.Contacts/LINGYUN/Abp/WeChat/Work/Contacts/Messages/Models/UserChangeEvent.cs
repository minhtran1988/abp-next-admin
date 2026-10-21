using LINGYUN.Abp.WeChat.Work.Common.Members.Models;
using LINGYUN.Abp.WeChat.Work.Common.Messages;
using System.Xml.Serialization;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Messages.Models;
/// <summary>
/// Member change event
/// </summary>
public abstract class UserChangeEvent : WeChatWorkEventMessage
{
    /// <summary>
    /// Change type
    /// </summary>
    [XmlElement("ChangeType")]
    public string ChangeType { get; set; } = default!;
    /// <summary>
    /// Member UserID
    /// </summary>
    [XmlElement("UserID")]
    public string UserId { get; set; } = default!;
    /// <summary>
    /// Member name; Self-built applications developed on behalf of others require administrator authorization before returning.
    /// </summary>
    [XmlElement("Name")]
    public string? Name { get; set; }
    /// <summary>
    /// Member department list, only returns the department IDs that the application has viewing permissions for
    /// </summary>
    [XmlElement("Department")]
    public string? Department { get; set; }
    /// <summary>
    /// Main department
    /// </summary>
    [XmlElement("MainDepartment")]
    public string MainDepartment { get; set; } = default!;
    /// <summary>
    /// Indicates whether the department is the head of the department,
    /// 0-no,
    /// 1-Yes,
    /// The order corresponds to the departments in the Department field one by one.
    /// Third-party address book applications or third-party applications and agent-developed applications authorized with the "Organizational Structure Information-Application can obtain the enterprise's departmental organizational structure information-Department Head" permission;
    /// For members not created by third parties, third-party address book applications cannot obtain them;
    /// Upstream companies cannot obtain this field of downstream company members.
    /// </summary>
    [XmlElement("IsLeaderInDept")]
    public string IsLeaderInDept { get; set; } = default!;
    /// <summary>
    /// Directly subordinate UserID, up to 1.
    /// It can be obtained by third-party address book applications or third-party applications and agent-developed applications authorized with the "Organizational Structure Information - Applications can obtain member organizational structure information within the visible range - Direct superiors" permission;
    /// For members not created by third parties, third-party address book applications cannot obtain them;
    /// Upstream companies cannot obtain this field of downstream company members.
    /// </summary>
    [XmlElement("DirectLeader")]
    public string DirectLeader { get; set; } = default!;
    /// <summary>
    /// Position information.
    /// The length is 0~64 bytes; self-developed applications require administrator authorization before returning.
    /// Upstream shared applications do not return this field
    /// </summary>
    [XmlElement("Position")]
    public string? Position { get; set; }
    /// <summary>
    /// Mobile phone number. To develop a self-built application requires administrator authorization and member oauth2 authorization;
    /// Third-party only address book applications can obtain it;
    /// For members not created by third parties, third-party address book applications are not available;
    /// Upstream companies cannot obtain this field of downstream company members.
    /// </summary>
    [XmlElement("Mobile")]
    public string? Mobile { get; set; }
    /// <summary>
    /// Gender.
    /// 0 means undefined,
    /// 1 means male,
    /// 2 means female.
    /// Developing self-built applications requires administrator authorization and member oauth2 authorization;
    /// Third-party only address book applications can obtain it;
    /// For members not created by third parties, third-party address book applications are not available;
    /// Upstream companies cannot obtain this field of downstream company members.
    /// Note: Unavailable means return value 0
    /// </summary>
    [XmlElement("Gender")]
    public byte Gender { get; set; }
    /// <summary>
    /// Email,
    /// Developing self-built applications requires administrator authorization and member oauth2 authorization;
    /// Third-party only address book applications can obtain it;
    /// For members not created by third parties, third-party address book applications are not available;
    /// Upstream companies cannot obtain this field of downstream company members.
    /// </summary>
    [XmlElement("Email")]
    public string? Email { get; set; }
    /// <summary>
    /// Corporate email,
    /// Developing self-built applications requires administrator authorization and member oauth2 authorization;
    /// Third-party only address book applications can obtain it;
    /// For members not created by third parties, third-party address book applications are not available;
    /// Upstream companies cannot obtain this field of downstream company members.
    /// </summary>
    [XmlElement("BizMail")]
    public string? BizMail { get; set; }
    /// <summary>
    /// Activation status:
    /// 1=Activated
    /// 2=Disabled
    /// 4=Inactive. Activated means that the enterprise WeChat has been activated or the WeChat plug-in has been followed (original enterprise account)
    /// 5=Member exits
    /// </summary>
    [XmlElement("Status")]
    public byte Status { get; set; }
    /// <summary>
    /// Avatar url.
    /// Note: If you want to get the thumbnail, just change the "/0" at the end of the URL to "/100".
    /// Developing self-built applications requires administrator authorization and member oauth2 authorization;
    /// Third-party only address book applications can obtain it;
    /// For members not created by third parties, third-party address book applications are not available;
    /// Upstream companies cannot obtain this field of downstream company members.
    /// </summary>
    [XmlElement("Avatar")]
    public string? Avatar { get; set; }
    /// <summary>
    /// Member alias.
    /// Upstream shared applications do not return this field
    /// </summary>
    [XmlElement("Alias")]
    public string? Alias { get; set; }
    /// <summary>
    /// Landline;
    /// Self-developed applications require administrator authorization before returning.
    /// Upstream shared applications do not return this field
    /// </summary>
    [XmlElement("Telephone")]
    public string? Telephone { get; set; }
    /// <summary>
    /// address.
    /// Developing self-built applications requires administrator authorization and member oauth2 authorization;
    /// Third-party only address book applications can obtain it;
    /// For members not created by third parties, third-party address book applications are not available;
    /// Upstream companies cannot obtain this field of downstream company members.
    /// </summary>
    [XmlElement("Address")]
    public string? Address { get; set; }
    /// <summary>
    /// Extended attributes;
    /// Self-developed applications require administrator authorization before returning.
    /// Upstream shared applications do not return this field
    /// </summary>
    [XmlArray("ExtAttr")]
    public MemberExtendAttribute? ExtendAttribute { get; set; }
}
