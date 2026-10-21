using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.Common.Members.Models;
/// <summary>
/// Property type
/// </summary>
[Description("Property Type")]
public enum ExternalAttributeType
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
    Web = 1,
    /// <summary>
    /// Mini program
    /// </summary>
    [Description("Mini Program")]
    MiniProgram = 2
}
