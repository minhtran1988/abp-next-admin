using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Models;
/// <summary>
/// Calendar notification range member permissions
/// </summary>
[Description("Calendar notification scope member permissions")]
public enum CalendarSharePermission
{
    /// <summary>
    /// Viewable
    /// </summary>
    [Description("Viewable")]
    View = 1,
    /// <summary>
    /// Only view busy status
    /// </summary>
    [Description("View only busy status")]
    OnlyBusy = 3
}
