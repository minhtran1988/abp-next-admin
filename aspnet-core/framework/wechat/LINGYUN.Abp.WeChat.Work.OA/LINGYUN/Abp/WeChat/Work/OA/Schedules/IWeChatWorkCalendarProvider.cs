using LINGYUN.Abp.WeChat.Work.OA.Schedules.Request;
using LINGYUN.Abp.WeChat.Work.OA.Schedules.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.OA.Schedules;
/// <summary>
/// Calendar management
/// </summary>
public interface IWeChatWorkCalendarProvider
{
    /// <summary>
    /// Create calendar
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/93647" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkCreateCalendarResponse> CreateCalendarAsync(
        WeChatWorkCreateCalendarRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Update calendar
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97716" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkUpdateCalendarResponse> UpdateCalendarAsync(
        WeChatWorkUpdateCalendarRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get calendar details
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97717" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetCalendarListResponse> GetCalendarListAsync(
        WeChatWorkGetCalendarListRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Delete calendar
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/97718" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkResponse> DeleteCalendarAsync(
        WeChatWorkDeleteCalendarRequest request,
        CancellationToken cancellationToken = default);
}
