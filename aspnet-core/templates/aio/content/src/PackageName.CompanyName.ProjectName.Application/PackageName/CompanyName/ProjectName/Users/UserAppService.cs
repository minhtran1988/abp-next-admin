using Microsoft.AspNetCore.Authorization;
using PackageName.CompanyName.ProjectName.Permissions;
using PackageName.CompanyName.ProjectName.Users.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace PackageName.CompanyName.ProjectName.Users
{
    /// <summary>
    /// User
    /// </summary>
    public class UserAppService : ApplicationService, IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserManager _userManager;
        private readonly IdentityUserManager _identityUserManager;

        public UserAppService(
            IUserRepository userRepository,
            IUserManager userManager,
            IdentityUserManager identityUserManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _identityUserManager = identityUserManager;
        }

        /// <summary>
        /// Create user
        /// </summary>
        [Authorize(ProjectNamePermissions.User.Create)]
        public async Task<UserDto> CreateAsync(CreateUpdateUserDto input)
        {
            // Parameter checking and validation logic can be added here
            if (string.IsNullOrEmpty(input.NickName))
            {
                throw new UserFriendlyException("nickname cannot be empty");
            }
            if (string.IsNullOrEmpty(input.Password))
            {
                throw new UserFriendlyException("Password cannot be empty");
            }

            // Use UserManager to create users
            var user = await _userManager.CreateAsync(
                input.NickName,
                input.Password,
                input.ContactInfo,
                input.Position,
                input.IsActive);

            // Return DTO object
            return await MapToUserDtoAsync(user);
        }

        /// <summary>
        /// Update user
        /// </summary>
        [Authorize(ProjectNamePermissions.User.Update)]
        public async Task<UserDto> UpdateAsync(Guid id, CreateUpdateUserDto input)
        {
            // Use UserManager to update basic user information
            var user = await _userManager.UpdateAsync(
                id,
                input.NickName,
                input.Password,
                input.ContactInfo,
                input.Position,
                input.IsActive);

            // Ensure that the updated user status is consistent with that in the DTO
            if (user.IdentityUser != null)
            {
                bool currentIsActive = user.IdentityUser.LockoutEnd == null || user.IdentityUser.LockoutEnd < DateTimeOffset.Now;
                if (currentIsActive != input.IsActive)
                {
                    await _userManager.SetUserActiveStatusAsync(id, input.IsActive);
                    user = await _userManager.GetAsync(id);
                }
            }

            // Return DTO object
            return await MapToUserDtoAsync(user);
        }

        /// <summary>
        /// Delete user
        /// </summary>
        [Authorize(ProjectNamePermissions.User.Delete)]
        public Task DeleteAsync(Guid id)
        {
            return _userManager.DeleteAsync(id);
        }

        /// <summary>
        /// Get user
        /// </summary>
        [Authorize(ProjectNamePermissions.User.Default)]
        public async Task<UserDto> GetAsync(Guid id)
        {
            var user = await _userManager.GetAsync(id);
            return await MapToUserDtoAsync(user);
        }

        /// <summary>
        /// Get user list
        /// </summary>
        [Authorize(ProjectNamePermissions.User.Default)]
        public async Task<PagedResultDto<UserItemDto>> GetListAsync(UserPagedAndSortedResultRequestDto input)
        {
            // Create query
            var query = await CreateFilteredQueryAsync(input);

            // Get the total number of records
            var totalCount = await AsyncExecuter.CountAsync(query);

            // Get sorted and paginated query results
            var users = await AsyncExecuter.ToListAsync(
                query.OrderBy(input.Sorting ?? nameof(User.NickName))
                    .Skip(input.SkipCount)
                    .Take(input.MaxResultCount));

            // Convert to DTO and return
            var userDtos = new List<UserItemDto>();
            foreach (var user in users)
            {
                var userDto = ObjectMapper.Map<User, UserItemDto>(user);

                // Fill in role information
                if (user.IdentityUser != null)
                {
                    var roles = await _identityUserManager.GetRolesAsync(user.IdentityUser);
                    userDto.RoleNames = string.Join("、", roles);
                    userDto.IsActive = user.IdentityUser.LockoutEnd == null || user.IdentityUser.LockoutEnd < DateTimeOffset.Now;
                }

                userDtos.Add(userDto);
            }

            return new PagedResultDto<UserItemDto>(totalCount, userDtos);
        }

        /// <summary>
        /// Modify user password
        /// </summary>
        [Authorize]
        public async Task ChangePasswordAsync(Guid id, string currentPassword, string newPassword)
        {
            await _userManager.ChangePasswordAsync(id, currentPassword, newPassword);
        }

        /// <summary>
        /// Reset user password (administrator operation)
        /// </summary>
        [Authorize(ProjectNamePermissions.User.Update)]
        public async Task ResetPasswordAsync(Guid id, string newPassword)
        {
            await _userManager.ResetPasswordAsync(id, newPassword);
        }

        /// <summary>
        /// Enable or disable user
        /// </summary>
        [Authorize(ProjectNamePermissions.User.Update)]
        public async Task SetUserActiveStatusAsync(Guid id, bool isActive)
        {
            await _userManager.SetUserActiveStatusAsync(id, isActive);
        }

        /// <summary>
        /// Create a basic query and apply filter conditions
        /// </summary>
        protected async virtual Task<IQueryable<User>> CreateFilteredQueryAsync(
            UserPagedAndSortedResultRequestDto input)
        {
            // Get the basic query and load related entities
            var query = await _userRepository.WithDetailsAsync(
                x => x.IdentityUser);

            // Apply filters
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(input.NickName),
                    x => x.NickName.Contains(input.NickName));
        }

        /// <summary>
        /// Map the User entity to UserDto and fill in the permission information
        /// </summary>
        private async Task<UserDto> MapToUserDtoAsync(User user)
        {
            var userDto = ObjectMapper.Map<User, UserDto>(user);

            // Set user status and role information
            if (user.IdentityUser != null)
            {
                var roles = await _identityUserManager.GetRolesAsync(user.IdentityUser);
                userDto.RoleNames = string.Join("、", roles);
                userDto.IsActive = user.IdentityUser.LockoutEnd == null || user.IdentityUser.LockoutEnd < DateTimeOffset.Now;
            }

            return userDto;
        }
    }
}