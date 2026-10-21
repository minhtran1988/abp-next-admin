using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.Common.Members.Models;
/// <summary>
/// Gender
/// </summary>
[Description("Gender")]
public enum Gender
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