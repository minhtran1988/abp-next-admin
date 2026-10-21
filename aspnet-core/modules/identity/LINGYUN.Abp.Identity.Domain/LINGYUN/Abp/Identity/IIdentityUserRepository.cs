using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace LINGYUN.Abp.Identity;

public interface IIdentityUserRepository : Volo.Abp.Identity.IIdentityUserRepository
{
    /// <summary>
    /// Is the mobile phone number already in use?
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> IsPhoneNumberUedAsync(
        string phoneNumber,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Whether the mobile phone number has been confirmed (bound)
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> IsPhoneNumberConfirmedAsync(
        string phoneNumber,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Whether the email address has been confirmed (binding)
    /// </summary>
    /// <param name="normalizedEmail"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> IsNormalizedEmailConfirmedAsync(
       string normalizedEmail,
       CancellationToken cancellationToken = default);
    /// <summary>
    /// Query users by mobile phone number
    /// </summary>
    /// <param name="phoneNumber">Mobile phone number</param>
    /// <param name="isConfirmed">Has it been confirmed</param>
    /// <param name="includeDetails"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IdentityUser?> FindByPhoneNumberAsync(
        string phoneNumber,
        bool isConfirmed = true,
        bool includeDetails = false,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get users through user primary key list
    /// </summary>
    /// <param name="userIds"></param>
    /// <param name="includeDetails"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<IdentityUser>> GetListByIdListAsync(
        List<Guid> userIds,
        bool includeDetails = false,
        CancellationToken cancellationToken = default
    );
    /// <summary>
    /// Get the list of all organizations of the user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="filter"></param>
    /// <param name="includeDetails"></param>
    /// <param name="skipCount"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<OrganizationUnit>> GetOrganizationUnitsAsync(
        Guid userId, 
        string? filter = null,
        bool includeDetails = false, 
        int skipCount = 1, 
        int maxResultCount = 10, 
        CancellationToken cancellationToken = default
    );
    /// <summary>
    /// 
    /// </summary>
    /// <param name="organizationUnitId"></param>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<long> GetUsersInOrganizationUnitCountAsync(
        Guid organizationUnitId,
        string? filter = null,
        CancellationToken cancellationToken = default
    );

    Task<List<IdentityUser>> GetUsersInOrganizationUnitAsync(
        Guid organizationUnitId,
        string? filter = null,
        int skipCount = 1,
        int maxResultCount = 10,
        CancellationToken cancellationToken = default
    );

    Task<long> GetUsersInOrganizationsListCountAsync(
        List<Guid> organizationUnitIds,
        string? filter = null,
        CancellationToken cancellationToken = default
    );

    Task<List<IdentityUser>> GetUsersInOrganizationsListAsync(
        List<Guid> organizationUnitIds,
        string? filter = null,
        int skipCount = 1,
        int maxResultCount = 10,
        CancellationToken cancellationToken = default
    );

    Task<long> GetUsersInOrganizationUnitWithChildrenCountAsync(
        string code,
        string? filter = null,
        CancellationToken cancellationToken = default
    );

    Task<List<IdentityUser>> GetUsersInOrganizationUnitWithChildrenAsync(
        string code,
        string? filter = null,
        int skipCount = 1,
        int maxResultCount = 10,
        CancellationToken cancellationToken = default
    );
}
