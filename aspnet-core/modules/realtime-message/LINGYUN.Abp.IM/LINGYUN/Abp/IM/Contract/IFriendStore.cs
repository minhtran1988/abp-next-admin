using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.IM.Contract;

public interface IFriendStore
{
    /// <summary>
    /// Whether it is a friend relationship
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userId"></param>
    /// <param name="friendId"></param>
    /// <returns></returns>
    Task<bool> IsFriendAsync(
        Guid? tenantId,
        Guid userId,
        Guid friendId,
        CancellationToken cancellationToken = default
        );
    /// <summary>
    /// Query the friend list
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userId"></param>
    /// <param name="sorting"></param>
    /// <returns></returns>
    Task<List<UserFriend>> GetListAsync(
        Guid? tenantId,
        Guid userId,
        string? sorting = nameof(UserFriend.UserId),
        CancellationToken cancellationToken = default
        );
    /// <summary>
    /// Get the number of friends
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userId"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<int> GetCountAsync(
        Guid? tenantId,
        Guid userId,
        string? filter = null,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the friend list
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userId"></param>
    /// <param name="filter"></param>
    /// <param name="sorting"></param>
    /// <param name="skipCount"></param>
    /// <param name="maxResultCount"></param>
    /// <returns></returns>
    Task<List<UserFriend>> GetPagedListAsync(
        Guid? tenantId,
        Guid userId,
        string? filter = null,
        string? sorting = nameof(UserFriend.UserId),
        int skipCount = 0,
        int maxResultCount = 10,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the list of recently contacted friends
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userId"></param>
    /// <param name="skipCount"></param>
    /// <param name="maxResultCount"></param>
    /// <returns></returns>
    Task<List<UserFriend>> GetLastContactListAsync(
        Guid? tenantId,
        Guid userId,
        int skipCount = 0,
        int maxResultCount = 10,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get friend information
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userId"></param>
    /// <param name="friendId"></param>
    /// <returns></returns>
    Task<UserFriend?> GetMemberAsync(
        Guid? tenantId,
        Guid userId,
        Guid friendId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Add friends
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userId"></param>
    /// <param name="friendId"></param>
    /// <returns></returns>
    Task AddMemberAsync(
        Guid? tenantId,
        Guid userId,
        Guid friendId,
        string? remarkName = null,
        bool isStatic = false,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Add friend request
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userId"></param>
    /// <param name="friendId"></param>
    /// <param name="remarkName"></param>
    /// <returns></returns>
    Task<UserAddFriendResult> AddRequestAsync(
        Guid? tenantId,
        Guid userId,
        Guid friendId,
        string? remarkName = null,
        string? description = null,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Remove friends
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userId"></param>
    /// <param name="friendId"></param>
    /// <returns></returns>
    Task RemoveMemberAsync(
        Guid? tenantId,
        Guid userId,
        Guid friendId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Add blacklist
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userId"></param>
    /// <param name="friendId"></param>
    /// <returns></returns>
    Task AddShieldMemberAsync(
        Guid? tenantId,
        Guid userId,
        Guid friendId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Remove blacklist
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userId"></param>
    /// <param name="friendId"></param>
    /// <returns></returns>
    Task RemoveShieldMemberAsync(
        Guid? tenantId,
        Guid userId,
        Guid friendId,
        CancellationToken cancellationToken = default);
}
