using System.ComponentModel;

namespace LINGYUN.Abp.DataProtection;
/// <summary>
/// Data filtering connection method
/// </summary>
public enum DataAccessFilterLogic
{
    /// <summary>
    /// and
    /// </summary>
    [Description("And")]
    And,
    /// <summary>
    /// or
    /// </summary>
    [Description("or")]
    Or
}