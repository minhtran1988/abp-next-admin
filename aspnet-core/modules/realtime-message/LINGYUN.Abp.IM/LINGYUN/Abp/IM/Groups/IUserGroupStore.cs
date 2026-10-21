using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LINGYUN.Abp.IM.Groups;

public interface IUserGroupStore
{
    /// <summary>
    /// Whether the member is in the group
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="groupId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<bool> MemberHasInGroupAsync(
        Guid? tenantId,
        long groupId,
        Guid userId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the group user identity
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="groupId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<GroupUserCard?> GetUserGroupCardAsync(
        Guid? tenantId,
        long groupId,
        Guid userId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the distribution list of the user's distribution group
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<IEnumerable<Group>> GetUserGroupsAsync(
        Guid? tenantId,
        Guid userId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the list of group members
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="groupId"></param>
    /// <returns></returns>
    Task<IEnumerable<GroupUserCard>> GetMembersAsync(
        Guid? tenantId,
        long groupId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the number of group members
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="groupId"></param>
    /// <returns></returns>
    Task<int> GetMembersCountAsync(
        Guid? tenantId,
        long groupId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get distribution group users
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="groupId"></param>
    /// <param name="sorting"></param>
    /// <param name="skipCount"></param>
    /// <param name="maxResultCount"></param>
    /// <returns></returns>
    Task<List<GroupUserCard>> GetMembersAsync(
        Guid? tenantId,
        long groupId,
        string? sorting = nameof(GroupUserCard.UserId),
        int skipCount = 0,
        int maxResultCount = 10,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// User joins distribution group
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userId"></param>
    /// <param name="groupId"></param>
    /// <returns></returns>
    Task AddUserToGroupAsync(
        Guid? tenantId,
        Guid userId,
        long groupId,
        Guid acceptUserId,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// User exits distribution group
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userId"></param>
    /// <param name="groupId"></param>
    /// <returns></returns>
    Task RemoveUserFormGroupAsync(
        Guid? tenantId,
        Guid userId,
        long groupId,
        CancellationToken cancellationToken = default);
}
