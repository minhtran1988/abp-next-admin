using System.ComponentModel;

namespace LINGYUN.Abp.DataProtection;
/// <summary>
/// Data filtering operation
/// </summary>
public enum DataAccessFilterOperate
{
    /// <summary>
    /// equal to
    /// </summary>
    [Description("equal to")]
    Equal = 1,
    /// <summary>
    /// Not equal to
    /// </summary>
    [Description("not equal to")]
    NotEqual = 2,
    /// <summary>
    /// Less than
    /// </summary>
    [Description("less than")]
    Less = 3,
    /// <summary>
    /// Less than or equal to
    /// </summary>
    [Description("Less than or equal to")]
    LessOrEqual = 4,
    /// <summary>
    /// Greater than
    /// </summary>
    [Description("greater than")]
    Greater = 5,
    /// <summary>
    /// Greater than or equal to
    /// </summary>
    [Description("Greater than or equal to")]
    GreaterOrEqual = 6,
    /// <summary>
    /// Left contains
    /// </summary>
    [Description("left contains")]
    StartsWith = 7,
    /// <summary>
    /// right contains
    /// </summary>
    [Description("right contains")]
    EndsWith = 8,
    /// <summary>
    /// Contains
    /// </summary>
    [Description("includes")]
    Contains = 9,
    /// <summary>
    /// Does not contain
    /// </summary>
    [Description("does not contain")]
    NotContains = 10,
}