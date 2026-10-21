using LINGYUN.Abp.WeChat.Work.ExternalContact.Follows.Response;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.ExternalContact.Follows;
/// <summary>
/// Enterprise service personnel management interface
/// </summary>
public interface IWeChatWorkFollowUserProvider
{
    /// <summary>
    /// Get the list of members configured with customer contact function
    /// </summary>
    /// <remarks>
    /// For details, see: <see href="https://developer.work.weixin.qq.com/document/path/92571" />
    /// </remarks>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WeChatWorkGetFollowUserListResponse> GetFollowUserListAsync(CancellationToken cancellationToken = default);
}
