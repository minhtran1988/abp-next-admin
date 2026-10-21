using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Schedule deletion operation mode
/// </summary>
[Description("Schedule deletion operation mode")]
public enum ScheduleDeleteMode : uint
{
    /// <summary>
    /// Delete all schedules
    /// </summary>
    [Description("Delete all schedules")]
    DeleteAll = 0,
    /// <summary>
    /// Only delete this schedule
    /// </summary>
    [Description("Only delete this schedule")]
    DeleteThisSchedule = 1,
    /// <summary>
    /// Delete this and subsequent schedules
    /// </summary>
    [Description("Delete this and subsequent schedules")]
    DeleteAllFutureSchedules = 2,
}
