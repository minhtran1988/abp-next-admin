using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LINGYUN.Abp.IM;

/// <summary>
/// IM user information search interface
/// </summary>
public interface IUserCardFinder
{
    /// <summary>
    /// Query the number of IM users
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="findUserName">User name</param>
    /// <param name="startAge">Start and end ages</param>
    /// <param name="endAge">Starting and ending ages</param>
    /// <param name="sex">Gender</param>
    /// <returns></returns>
    Task<int> GetCountAsync(
        Guid? tenantId,
        string? findUserName = null,
        int? startAge = null,
        int? endAge = null,
        Sex? sex = null);
    /// <summary>
    /// Query IM user list
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="findUserName">User name</param>
    /// <param name="startAge">Start and end ages</param>
    /// <param name="endAge">Starting and ending ages</param>
    /// <param name="sex">Gender</param>
    /// <param name="sorting">Sort field</param>
    /// <param name="skipCount">Starting recording position</param>
    /// <param name="maxResultCount">Maximum number of returns</param>
    /// <returns></returns>
    Task<List<UserCard>> GetListAsync(
        Guid? tenantId,
        string? findUserName = null,
        int? startAge = null,
        int? endAge = null,
        Sex? sex = null,
        string sorting = nameof(UserCard.UserId),
        int skipCount = 0,
        int maxResultCount = 10);
    /// <summary>
    /// Get IM user information
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="findUserId"></param>
    /// <returns></returns>
    Task<UserCard?> GetMemberAsync(
        Guid? tenantId,
        Guid findUserId);
}
