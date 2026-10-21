using System.ComponentModel;

namespace LINGYUN.Abp.WeChat.Work.OA.MeetingRooms.Models;
/// <summary>
/// Reservation status
/// </summary>
[Description("Reservation status")]
public enum MeetingRoomScheduleStatus
{
    /// <summary>
    /// Already booked
    /// </summary>
    [Description("Reserved")]
    Reserved = 0,
    /// <summary>
    /// Applying
    /// </summary>
    [Description("Applying")]
    Applying = 2,
    /// <summary>
    /// Under approval
    /// </summary>
    [Description("Under Approval")]
    Approvaling = 3,
}
