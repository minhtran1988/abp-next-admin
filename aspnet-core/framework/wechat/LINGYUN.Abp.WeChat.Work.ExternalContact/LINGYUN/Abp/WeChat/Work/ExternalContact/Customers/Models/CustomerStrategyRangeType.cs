using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Models;
/// <summary>
/// Node type
/// </summary>
[Description("NodeType")]
public enum CustomerStrategyRangeType
{
    /// <summary>
    /// members
    /// </summary>
    [Description("Member")]
    Member = 1,
    /// <summary>
    /// Department
    /// </summary>
    [Description("Department")]
    Part = 2
}
