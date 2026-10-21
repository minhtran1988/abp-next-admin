using LINGYUN.Abp.WeChat.Work.OA.Approvals.Request;
using LINGYUN.Abp.WeChat.Work.OA.Approvals.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.OA.Approvals;
/// <summary>
/// Approval template interface
/// </summary>
/// <remarks>
/// For details, see: https://developer.work.weixin.qq.com/document/path/91854
/// </remarks>
public interface IWeChatWorkApprovalTemplateProvider
{
    /// <summary>
    /// Create approval template
    /// </summary>
    /// <remarks>
    /// For details, see: https://developer.work.weixin.qq.com/document/path/97437
    /// </remarks>
    /// <param name="request">Create approval template request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Create approval template response parameters</returns>
    Task<WeChatWorkCreateTemplateResponse> CreateTemplateAsync(
        WeChatWorkCreateTemplateRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Update approval template
    /// </summary>
    /// <remarks>
    /// For details, see: https://developer.work.weixin.qq.com/document/path/97438
    /// </remarks>
    /// <param name="request">Update approval template request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Update approval template response parameters</returns>
    Task<WeChatWorkResponse> UpdateTemplateAsync(
        WeChatWorkCreateTemplateRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get approval template details
    /// </summary>
    /// <remarks>
    /// For details, see: https://developer.work.weixin.qq.com/document/path/91982
    /// </remarks>
    /// <param name="request">Get approval template request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Approval template response parameters</returns>
    Task<WeChatWorkTemplateResponse> GetTemplateAsync(
        WeChatWorkGetTemplateRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get approval order numbers in batches
    /// </summary>
    /// <remarks>
    /// For details, see: https://developer.work.weixin.qq.com/document/path/91816
    /// </remarks>
    /// <param name="request">Batch acquisition of approval order number request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Get approval order number response parameters in batches</returns>
    Task<WeChatWorkGetApprovalInfoResponse> GetApprovalInfoAsync(
        WeChatWorkGetApprovalInfoRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get approval application details
    /// </summary>
    /// <remarks>
    /// For details, see: https://developer.work.weixin.qq.com/document/path/91983
    /// </remarks>
    /// <param name="request">Get approval application details request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Get approval application details response parameters</returns>
    Task<WeChatWorkGetApprovalDetailResponse> GetApprovalDetailAsync(
        WeChatWorkGetApprovalDetailRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Submit application for approval
    /// </summary>
    /// <remarks>
    /// For details, see: https://developer.work.weixin.qq.com/document/path/91853
    /// </remarks>
    /// <param name="request">Submit approval application request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Submit approval application response parameters</returns>
    Task<WeChatWorkApplyEventResponse> ApplyEventAsync(
        WeChatWorkApplyEventRequest request,
        CancellationToken cancellationToken = default);
}
