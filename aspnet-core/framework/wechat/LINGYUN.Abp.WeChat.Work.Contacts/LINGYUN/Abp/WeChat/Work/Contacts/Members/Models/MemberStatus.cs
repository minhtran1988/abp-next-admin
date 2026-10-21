using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Models;
/// <summary>
/// Activation status
/// </summary>
[Description("activation status")]
public enum MemberStatus
{
    /// <summary>
    /// Activated
    /// </summary>
    [Description("Activated")]
    Activated = 1,
    /// <summary>
    /// Disabled
    /// </summary>
    [Description("Disabled")]
    Disabled = 2,
    /// <summary>
    /// Not activated
    /// </summary>
    [Description("not activated")]
    NotActivated = 4,
    /// <summary>
    /// Exit the enterprise
    /// </summary>
    [Description("Exit the enterprise")]
    Exited = 5,
}
