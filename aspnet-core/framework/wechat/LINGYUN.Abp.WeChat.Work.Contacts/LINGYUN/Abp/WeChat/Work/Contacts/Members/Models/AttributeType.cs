using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.Contacts.Members.Models;
/// <summary>
/// Property type
/// </summary>
[Description("Property Type")]
public enum AttributeType
{
    /// <summary>
    /// text
    /// </summary>
    [Description("text")]
    Text = 0,
    /// <summary>
    /// Web page
    /// </summary>
    [Description("Webpage")]
    Web = 1
}
