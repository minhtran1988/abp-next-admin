using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Repeat type
/// </summary>
[Description("Repeating Type")]
public enum ScheduleReminderRepeatType : uint
{
    /// <summary>
    /// Daily
    /// </summary>
    [Description("Daily")]
    Daily = 0,
    /// <summary>
    /// Weekly
    /// </summary>
    [Description("Weekly")]
    Weekly = 1,
    /// <summary>
    /// Monthly
    /// </summary>
    [Description("Monthly")]
    EveryMonth = 2,
    /// <summary>
    /// Every year
    /// </summary>
    [Description("Every year")]
    EveryYear = 5,
    /// <summary>
    /// working days
    /// </summary>
    [Description("working days")]
    WorkingDays = 7,
}
