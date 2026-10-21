using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Schedule update operation mode
/// </summary>
[Description("Schedule update operation mode")]
public enum ScheduleUpdateMode : uint
{
    /// <summary>
    /// Modify all
    /// </summary>
    [Description("Modify all")]
    ModifyAll = 0,
    /// <summary>
    /// Only modify this schedule
    /// </summary>
    [Description("Only modify this schedule")]
    ModifyThisSchedule = 1,
    /// <summary>
    /// Modify all future schedules
    /// </summary>
    [Description("Modify all future schedules")]
    ModifyAllFutureSchedules = 2,
}
