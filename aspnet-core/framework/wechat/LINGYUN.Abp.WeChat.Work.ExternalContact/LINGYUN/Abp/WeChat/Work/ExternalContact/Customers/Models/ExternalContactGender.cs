using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
/// <summary>
/// External contact gender
/// </summary>
[Description("External contact gender")]
public enum ExternalContactGender
{
    /// <summary>
    /// Unknown
    /// </summary>
    [Description("Unknown")]
    None = 0,
    /// <summary>
    /// Male
    /// </summary>
    [Description("Male")]
    Male = 1,
    /// <summary>
    /// women
    /// </summary>
    [Description("Female")]
    FeMale = 2,
}
