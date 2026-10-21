using LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Request;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers;
/// <summary>
/// Customer management interface
/// </summary>
public interface IWeChatWorkCustomerProvider
{
    /// <summary>
    /// Get the customer list
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92113" />
    /// </remarks>
    /// <param name="userId">The userid of the enterprise member</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetCustomerListResponse> GetCustomerListAsync(
        string userId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get customer details in batches
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92994" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkBulkGetCustomerResponse> BulkGetCustomerAsync(
        WeChatWorkBulkGetCustomerRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get customer details
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92114" />
    /// </remarks>
    /// <param name="externalUserid">The userid of the external contact, please note that it is not an account of a corporate member</param>
    /// <param name="cursor">next_cursor returned from the last request</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetCustomerResponse> GetCustomerAsync(
        string externalUserid, 
        string? cursor = null,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Modify customer remark information
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92115" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkResponse> UpdateCustomerRemarkAsync(
        WeChatWorkUpdateCustomerRemarkRequest request,
        CancellationToken cancellationToken = default);
}
