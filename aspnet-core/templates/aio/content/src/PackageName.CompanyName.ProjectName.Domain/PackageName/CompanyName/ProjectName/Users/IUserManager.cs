using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace PackageName.CompanyName.ProjectName.Users
{
    /// <summary>
    /// User management service interface
    /// </summary>
    public interface IUserManager : IDomainService
    {
        /// <summary>
        /// Create new user
        /// </summary>
        Task<User> CreateAsync(
            string nickName,
            string password,
            string contactInfo = null,
            string position = null,
            bool isActive = true
            );

        /// <summary>
        /// Update user information
        /// </summary>
        Task<User> UpdateAsync(
            Guid id,
            string nickName,
            string password,
            string contactInfo = null,
            string position = null,
            bool isActive = true);

        /// <summary>
        /// Delete user
        /// </summary>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Modify user password
        /// </summary>
        Task ChangePasswordAsync(Guid id, string currentPassword, string newPassword);

        /// <summary>
        /// Reset user password
        /// </summary>
        Task ResetPasswordAsync(Guid id, string newPassword);

        /// <summary>
        /// Get user information
        /// </summary>
        Task<User> GetAsync(Guid id);

        /// <summary>
        /// Get the user based on the Identity user ID
        /// </summary>
        Task<User> FindByIdentityUserIdAsync(Guid identityUserId);

        /// <summary>
        /// Find users based on their nicknames
        /// </summary>
        Task<User> FindByNickNameAsync(string nickName);

        /// <summary>
        /// Disable or enable user
        /// </summary>
        Task SetUserActiveStatusAsync(Guid id, bool isActive);
    }
}