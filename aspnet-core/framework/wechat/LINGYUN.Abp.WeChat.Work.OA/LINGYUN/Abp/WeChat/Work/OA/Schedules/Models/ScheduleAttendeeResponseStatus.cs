using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Acceptance status of schedule participants
/// </summary>
[Description("Schedule participant acceptance status")]
public enum ScheduleAttendeeResponseStatus : uint
{
    /// <summary>
    /// Not processed
    /// </summary>
    [Description("Unprocessed")]
    UnProcessed = 0,
    /// <summary>
    /// TBD
    /// </summary>
    [Description("TBD")]
    Determined = 1,
    /// <summary>
    /// Accept all
    /// </summary>
    [Description("Accept all")]
    AcceptAll = 2,
    /// <summary>
    /// Accept only once
    /// </summary>
    [Description("Accept only once")]
    AcceptedOnce = 3,
    /// <summary>
    /// reject
    /// </summary>
    [Description("deny")]
    Refuse = 4,
}
