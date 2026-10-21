using PackageName.CompanyName.ProjectName.Users.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace PackageName.CompanyName.ProjectName.Users
{
    /// <summary>
    /// User application service interface
    /// </summary>
    public interface IUserAppService : 
        IApplicationService
    {
        /// <summary>
        /// Create user
        /// </summary>
        Task<UserDto> CreateAsync(CreateUpdateUserDto input);

        /// <summary>
        /// Update user
        /// </summary>
        Task<UserDto> UpdateAsync(Guid id, CreateUpdateUserDto input);

        /// <summary>
        /// Delete user
        /// </summary>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Get user
        /// </summary>
        Task<UserDto> GetAsync(Guid id);

        /// <summary>
        /// Get user list
        /// </summary>
        Task<PagedResultDto<UserItemDto>> GetListAsync(UserPagedAndSortedResultRequestDto input);

        /// <summary>
        /// Modify user password
        /// </summary>
        Task ChangePasswordAsync(Guid id, string currentPassword, string newPassword);

        /// <summary>
        /// Reset user password (administrator operation)
        /// </summary>
        Task ResetPasswordAsync(Guid id, string newPassword);

        /// <summary>
        /// Enable or disable user
        /// </summary>
        Task SetUserActiveStatusAsync(Guid id, bool isActive);
    }
}