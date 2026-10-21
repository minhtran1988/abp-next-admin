using LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Request;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Transfers;
/// <summary>
/// On-the-job inheritance interface
/// </summary>
public interface IWeChatWorkEmployExtendProvider
{
    /// <summary>
    /// Assign clients to active members
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92124"/>
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkTransferCustomerResponse> TransferCustomerAsync(
        WeChatWorkTransferCustomerRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Query customer replacement status
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94088"/>
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetTransferResultResponse> GetTransferResultAsync(
        WeChatWorkGetTransferResultRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Assign customer groups to active members
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/95703"/>
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGroupChatOnjobTransferResponse> GroupChatOnjobTransferAsync(
        WeChatWorkGroupChatOnjobTransferRequest request,
        CancellationToken cancellationToken = default);
}
