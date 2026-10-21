using LINGYUN.Abp.IM.Groups;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace LINGYUN.Abp.MessageService.Groups;

public interface IUserGroupAppService : IApplicationService
{
    /// <summary>
    /// Apply to join the group
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task ApplyJoinGroupAsync(UserJoinGroupDto input);
    /// <summary>
    /// Get my group
    /// </summary>
    /// <returns></returns>
    Task<ListResultDto<Group>> GetMyGroupsAsync();
    /// <summary>
    /// Get group users
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<GroupUserCard>> GetGroupUsersAsync(GroupUserGetByPagedDto input);
    /// <summary>
    /// Process user group application
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task GroupAcceptUserAsync(GroupAcceptUserDto input);
    /// <summary>
    /// Remove users from group
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task GroupRemoveUserAsync(GroupRemoveUserDto input);
}
