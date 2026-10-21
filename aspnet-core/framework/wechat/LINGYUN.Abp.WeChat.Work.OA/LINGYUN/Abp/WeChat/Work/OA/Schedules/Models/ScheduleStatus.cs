using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Schedule status
/// </summary>
[Description("Schedule Status")]
public enum ScheduleStatus : uint
{
    /// <summary>
    /// normal
    /// </summary>
    [Description("normal")]
    Normal = 0,
    /// <summary>
    /// Canceled
    /// </summary>
    [Description("Cancelled")]
    Cancelled = 1,
}
