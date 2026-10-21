using LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Request;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Customers.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Customers;
/// <summary>
/// Customer contact rule group management
/// </summary>
/// <remarks>
/// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94883" />
/// </remarks>
public interface IWeChatWorkCustomerStrategyProvider
{
    /// <summary>
    /// Get the rule group list
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94883#%E8%8E%B7%E5%8F%96%E8%A7%84%E5%88%99%E7%BB%84%E5%88%97%E8%A1%A8" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetCustomerStrategyListResponse> GetCustomerStrategyListAsync(
        WeChatWorkGetCustomerStrategyListRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the rule group
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94883#%E8%8E%B7%E5%8F%96%E8%A7%84%E5%88%99%E7%BB%84%E8%AF%A6%E6%83%85" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetCustomerStrategyResponse> GetCustomerStrategyAsync(
        WeChatWorkGetCustomerStrategyRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the rule group management scope
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94883#%E8%8E%B7%E5%8F%96%E8%A7%84%E5%88%99%E7%BB%84%E7%AE%A1%E7%90%86%E8%8C%83%E5%9B%B4" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetCustomerStrategyRangeResponse> GetCustomerStrategyRangeAsync(
        WeChatWorkGetCustomerStrategyRangeRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Create a new rule group
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94883#%E5%88%9B%E5%BB%BA%E6%96%B0%E7%9A%84%E8%A7%84%E5%88%99%E7%BB%84" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkCreateCustomerStrategyResponse> CreateCustomerStrategyAsync(
        WeChatWorkCreateCustomerStrategyRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Edit the rule group and its management scope
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94883#%E7%BC%96%E8%BE%91 %E8%A7%84%E5%88%99%E7%BB%84%E5%8F%8A%E5%85%B6%E7%AE%A1%E7%90%86%E8%8C%83%E5%9B%B4" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkResponse> UpdateCustomerStrategyAsync(
        WeChatWorkUpdateCustomerStrategyRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Delete rule group
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/94883#%E5%88%A0%E9%99%A4%E8%A7%84%E5%88%99%E7%BB%84" />
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkResponse> DeleteCustomerStrategyAsync(
        WeChatWorkDeleteCustomerStrategyRequest request,
        CancellationToken cancellationToken = default);
}
