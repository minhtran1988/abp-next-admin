using Microsoft.AspNetCore.Mvc;
using PackageName.CompanyName.ProjectName.Users.Dtos;
using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace PackageName.CompanyName.ProjectName.Users
{
    /// <summary>
    /// User management controller
    /// </summary>
    [RemoteService]
    [Route("api/app/user")]
    public class UserController : ProjectNameControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// Create user
        /// </summary>
        [HttpPost]
        public async Task<UserDto> CreateAsync(CreateUpdateUserDto input)
        {
            return await _userAppService.CreateAsync(input);
        }

        /// <summary>
        /// Update user
        /// </summary>
        [HttpPut("{id}")]
        public async Task<UserDto> UpdateAsync(Guid id, CreateUpdateUserDto input)
        {
            return await _userAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// Delete user
        /// </summary>
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _userAppService.DeleteAsync(id);
        }

        /// <summary>
        /// Get user
        /// </summary>
        [HttpGet("{id}")]
        public async Task<UserDto> GetAsync(Guid id)
        {
            return await _userAppService.GetAsync(id);
        }

        /// <summary>
        /// Get user list
        /// </summary>
        [HttpGet]
        public async Task<PagedResultDto<UserItemDto>> GetListAsync(UserPagedAndSortedResultRequestDto input)
        {
            return await _userAppService.GetListAsync(input);
        }

        /// <summary>
        /// Modify user password
        /// </summary>
        [HttpPost("{id}/change-password")]
        public async Task ChangePasswordAsync(Guid id, [FromBody] ChangePasswordRequest request)
        {
            await _userAppService.ChangePasswordAsync(id, request.CurrentPassword, request.NewPassword);
        }

        /// <summary>
        /// Reset user password (administrator operation)
        /// </summary>
        [HttpPost("{id}/reset-password")]
        public async Task ResetPasswordAsync(Guid id, [FromBody] ResetPasswordRequest request)
        {
            await _userAppService.ResetPasswordAsync(id, request.NewPassword);
        }

        /// <summary>
        /// Enable or disable user
        /// </summary>
        [HttpPost("{id}/set-active")]
        public async Task SetUserActiveStatusAsync(Guid id, [FromBody] SetUserActiveRequest request)
        {
            await _userAppService.SetUserActiveStatusAsync(id, request.IsActive);
        }
    }

    /// <summary>
    /// Password change request
    /// </summary>
    public class ChangePasswordRequest
    {
        /// <summary>
        /// Current password
        /// </summary>
        public string CurrentPassword { get; set; }

        /// <summary>
        /// New password
        /// </summary>
        public string NewPassword { get; set; }
    }

    /// <summary>
    /// Reset password request
    /// </summary>
    public class ResetPasswordRequest
    {
        /// <summary>
        /// New password
        /// </summary>
        public string NewPassword { get; set; }
    }

    /// <summary>
    /// Set user status request
    /// </summary>
    public class SetUserActiveRequest
    {
        /// <summary>
        /// Whether to enable
        /// </summary>
        public bool IsActive { get; set; }
    }
}
