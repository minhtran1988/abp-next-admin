namespace LINGYUN.Abp.WeChat.Work.OA.Schedules.Request;
/// <summary>
/// Add schedule participant request parameters
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97721"/>
/// </remarks>
public class WeChatWorkScheduleAddAttendeesRequest : WeChatWorkScheduleChangeAttendeesRequest
{
    public WeChatWorkScheduleAddAttendeesRequest(string scheduleId) : base(scheduleId)
    {
    }
}
