using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace LINGYUN.Platform.Menus;

public interface IMenuRepository : IBasicRepository<Menu, Guid>
{
    Task<List<Menu>> GetListAsync(
        IEnumerable<Guid> idList,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get the last menu
    /// </summary>
    /// <param name="parentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Menu?> FindLastMenuAsync(
        Guid? parentId = null,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Query menu based on name
    /// </summary>
    /// <param name="menuName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Menu?> FindByNameAsync(
        string menuName,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Query the main menu. When each layout page is created, a main menu with the path / must be created.
    /// </summary>
    /// <param name="platformType"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Menu?> FindMainAsync(
        string? framework = null,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Get child nodes
    /// </summary>
    /// <param name="parentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Menu>> GetChildrenAsync(
        Guid? parentId,
        CancellationToken cancellationToken = default
    );
    /// <summary>
    /// Query the submenu through the parent menu encoding
    /// </summary>
    /// <param name="code"></param>
    /// <param name="parentId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Menu>> GetAllChildrenWithParentCodeAsync(
        string code,
        Guid? parentId,
        CancellationToken cancellationToken = default
    );
    /// <summary>
    /// Find user-accessible menus
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <param name="roles">Role list</param>
    /// <param name="platformType">Platform type</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Menu>> GetUserMenusAsync(
        Guid userId,
        string[] roles,
        string? framework = null,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Find the character access menu
    /// </summary>
    /// <param name="roles">Role list</param>
    /// <param name="platformType">Platform type</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Menu>> GetRoleMenusAsync(
        string[] roles,
        string? framework = null,
        CancellationToken cancellationToken = default);

    Task<int> GetCountAsync(
        string? filter =null,
        string? framework = null,
        Guid? parentId = null,
        Guid? layoutId = null,
        CancellationToken cancellationToken = default);

    Task<List<Menu>> GetListAsync(
        string? filter = null,
        string? framework = null,
        Guid? parentId = null,
        Guid? layoutId = null,
        string? sorting = nameof(Menu.Code),
        int skipCount = 0,
        int maxResultCount = 10,
        CancellationToken cancellationToken = default);

    Task<List<Menu>> GetAllAsync(
        string? filter = null,
        string? framework = null,
        Guid? parentId = null,
        Guid? layoutId = null,
        string? sorting = nameof(Menu.Code),
        CancellationToken cancellationToken = default);

    Task RemoveAllRolesAsync(
        Menu menu,
        CancellationToken cancellationToken = default
    );

    Task RemoveAllMembersAsync(
        Menu menu,
        CancellationToken cancellationToken = default
    );
}
