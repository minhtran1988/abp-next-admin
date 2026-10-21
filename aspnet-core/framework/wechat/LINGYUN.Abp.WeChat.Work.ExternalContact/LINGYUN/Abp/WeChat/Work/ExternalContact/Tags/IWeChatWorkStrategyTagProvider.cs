using LINGYUN.Abp.WeChat.Work.ExternalContact.Tags.Request;
using LINGYUN.Abp.WeChat.Work.ExternalContact.Tags.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Tags;
/// <summary>
/// Rule group label management
/// </summary>
public interface IWeChatWorkStrategyTagProvider
{
    /// <summary>
    /// Get the enterprise customer tag under the specified rule group
    /// </summary>
    /// <remarks>
    /// For details, see: https://developer.work.weixin.qq.com/document/path/94882#%E8%8E%B7%E5%8F%96%E6%8C%87%E5%AE%9A%E8 %A7%84%E5%88%99%E7%BB%84%E4%B8%8B%E7%9A%84%E4%BC%81%E4%B8%9A%E5%AE%A2%E6%88%B7%E6%A0%87%E7%AD%BE
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetStrategyTagListResponse> GetStrategyTagListAsync(
        WeChatWorkGetStrategyTagListRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Create an enterprise customer label for the specified rule group
    /// </summary>
    /// <remarks>
    /// For details, see: https://developer.work.weixin.qq.com/document/path/94882#%E8%8E%B7%E5%8F%96%E6%8C%87%E5%AE%9A%E8 %A7%84%E5%88%99%E7%BB%84%E4%B8%8B%E7%9A%84%E4%BC%81%E4%B8%9A%E5%AE%A2%E6%88%B7%E6%A0%87%E7%AD%BE
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkCreateStrategyTagResponse> CreateStrategyTagAsync(
        WeChatWorkCreateStrategyTagRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Edit the enterprise customer label under the specified rule group
    /// </summary>
    /// <remarks>
    /// For details, see: https://developer.work.weixin.qq.com/document/path/94882#%E7%BC%96%E8%BE%91%E6%8C%87%E5%AE%9A%E8 %A7%84%E5%88%99%E7%BB%84%E4%B8%8B%E7%9A%84%E4%BC%81%E4%B8%9A%E5%AE%A2%E6%88%B7%E6%A0%87%E7%AD%BE
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkResponse> UpdateStrategyTagAsync(
        WeChatWorkUpdateStrategyTagRequest request,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Delete the enterprise customer label under the specified rule group
    /// </summary>
    /// <remarks>
    /// For details, see: https://developer.work.weixin.qq.com/document/path/94882#%E5%88%A0%E9%99%A4%E6%8C%87%E5%AE%9A%E8 %A7%84%E5%88%99%E7%BB%84%E4%B8%8B%E7%9A%84%E4%BC%81%E4%B8%9A%E5%AE%A2%E6%88%B7%E6%A0%87%E7%AD%BE
    /// </remarks>
    /// <param name="request">Request parameters</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkResponse> DeleteStrategyTagAsync(
        WeChatWorkDeleteStrategyTagRequest request,
        CancellationToken cancellationToken = default);
}
