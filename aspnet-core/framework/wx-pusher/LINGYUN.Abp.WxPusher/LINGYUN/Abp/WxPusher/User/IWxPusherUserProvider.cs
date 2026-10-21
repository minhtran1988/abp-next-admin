using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.WxPusher.User;
/// <summary>
/// User interface
/// </summary>
public interface IWxPusherUserProvider
{
    /// <summary>
    /// Query App’s following users V2
    /// </summary>
    /// <remarks>
    /// Obtain the user information of all WeChat users who follow the application/topic. It should be noted that if a WeChat user follows applications, topics, or even multiple topics at the same time, multiple records will be returned.
    /// </remarks>
    /// <param name="page">Page number of requested data</param>
    /// <param name="pageSize">Page size, cannot exceed 100</param>
    /// <param name="uid">User's uid, optional. If not passed, all users will be queried. If uid is passed, the information of a certain user will be queried. </param>
    /// <param name="isBlock">Query for blocked users, optional, query all users if not passed, true for blocked users, false for unblocked users</param>
    /// <param name="type">The type of attention, optional, not passed to query all users, 0 is application, 1 is topic</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<WxPusherPagedResult<UserProfile>> GetUserListAsync(
        int page = 1,
        int pageSize = 10,
        string? uid = null,
        bool? isBlock = null,
        FlowType? type = null,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Delete user
    /// </summary>
    /// <remarks>
    /// You can delete the user's attention to the application or topic. After deletion, the user can follow it again. If you want the user to follow it again, you can call the block interface to block the user.
    /// </remarks>
    /// <param name="id">User id, which can be obtained through the user query interface</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> DeleteUserAsync(
        int id,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Block users
    /// </summary>
    /// <remarks>
    /// After being blocked, no more messages can be sent, and the user cannot follow him again unless you cancel the block. Call the blacklist interface instead of calling the delete interface.
    /// </remarks>
    /// <param name="id">User id, which can be obtained through the user query interface</param>
    /// <param name="reject">Whether to block, true means block, false means cancel block</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> RejectUserAsync(
        int id,
        bool reject,
        CancellationToken cancellationToken = default);
}
