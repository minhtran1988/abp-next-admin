namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Request;
/// <summary>
/// Delete schedule participants
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97722"/>
/// </remarks>
public class WeChatWorkScheduleDeleteAttendeesRequest : WeChatWorkScheduleChangeAttendeesRequest
{
    public WeChatWorkScheduleDeleteAttendeesRequest(string scheduleId) : base(scheduleId)
    {
    }
}
