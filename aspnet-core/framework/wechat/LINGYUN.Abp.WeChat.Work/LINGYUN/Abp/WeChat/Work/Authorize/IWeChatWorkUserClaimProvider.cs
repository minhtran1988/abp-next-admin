using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WeChat.Work.Authorize;
/// <summary>
/// Enterprise WeChat user identity provider
/// </summary>
public interface IWeChatWorkUserClaimProvider
{
    /// <summary>
    /// Query the enterprise WeChat user ID through user ID
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string?> FindUserIdentifierAsync(
        Guid userId, 
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Query the enterprise WeChat user ID list through the user ID list
    /// </summary>
    /// <param name="userIdList"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<string>> FindUserIdentifierListAsync(
        IEnumerable<Guid> userIdList,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Bind user enterprise WeChat
    /// </summary>
    /// <param name="userId">UserId</param>
    /// <param name="weChatUserId">Enterprise WeChat user ID</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task BindUserAsync(
        Guid userId,
        string weChatUserId,
        CancellationToken cancellationToken = default);
}
