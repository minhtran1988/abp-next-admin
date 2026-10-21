using LINGYUN.Abp.WeChat.Work.OA.Schedules.Request;
using LINGYUN.Abp.WeChat.Work.OA.Schedules.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules;
/// <summary>
/// Schedule management
/// </summary>
public interface IWeChatWorkScheduleProvider
{
    /// <summary>
    /// Create schedule
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/93648" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkCreateScheduleResponse> CreateScheduleAsync(
        WeChatWorkCreateScheduleRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Update schedule
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97720" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkUpdateScheduleResponse> UpdateScheduleAsync(
        WeChatWorkUpdateScheduleRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Add new schedule participants
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97721" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkResponse> AddAttendeesAsync(
        WeChatWorkScheduleAddAttendeesRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Delete schedule participants
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97722" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkResponse> DeleteAttendeesAsync(
        WeChatWorkScheduleDeleteAttendeesRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the schedule list under the calendar
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97723" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetScheduleListByCalendarResponse> GetScheduleListByCalendarAsync(
        WeChatWorkGetScheduleListByCalendarRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get schedule details
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97724" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetScheduleListResponse> GetScheduleListAsync(
        WeChatWorkGetScheduleListRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Cancel schedule
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97725" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkResponse> DeleteScheduleAsync(
        WeChatWorkDeleteScheduleRequest request,
        CancellationToken cancellationToken = default);
}
