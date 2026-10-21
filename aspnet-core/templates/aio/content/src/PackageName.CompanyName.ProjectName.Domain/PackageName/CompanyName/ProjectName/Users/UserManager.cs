using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Identity;

namespace PackageName.CompanyName.ProjectName.Users
{
    /// <summary>
    /// User management service, used to process users' CRUD operations
    /// </summary>
    public class UserManager : DomainService, IUserManager
    {
        private readonly IUserRepository _userRepository;
        private readonly IdentityUserManager _identityUserManager;
        private readonly ILogger<UserManager> _logger;

        public UserManager(
            IUserRepository userRepository,
            IdentityUserManager identityUserManager,
            ILogger<UserManager> logger)
        {
            _userRepository = userRepository;
            _identityUserManager = identityUserManager;
            _logger = logger;
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="nickName">User nickname</param>
        /// <param name="password">User password</param>
        /// <param name="contactInfo">Contact information</param>
        /// <param name="position">Position</param>
        /// <param name="isActive">Whether to enable</param>
        /// <returns>Created user entity</returns>
        public async Task<User> CreateAsync(
            string nickName,
            string password,
            string contactInfo = null,
            string position = null,
            bool isActive = true)
        {
            // Parameter validation
            if (string.IsNullOrWhiteSpace(nickName))
            {
                throw new UserFriendlyException("Username cannot be empty");
            }

            // Password verification
            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                throw new UserFriendlyException("The password cannot be empty and the length cannot be less than 6 characters");
            }

            // Check if the user nickname already exists
            var existingUser = await _userRepository.FindAsync(u => u.NickName == nickName);
            if (existingUser != null)
            {
                throw new UserFriendlyException($"A user with the nickname '{nickName}' already exists");
            }

            // Create Identity user
            var identityUser = new IdentityUser(GuidGenerator.Create(), nickName, $"{nickName}@inspection.com");
            var identityResult = await _identityUserManager.CreateAsync(identityUser, password);
            if (!identityResult.Succeeded)
            {
                throw new UserFriendlyException("Failed to create user: " +
                                                string.Join(", ", identityResult.Errors.Select(x => x.Description)));
            }

            // Set user status
            if (!isActive)
            {
                var lockoutResult = await _identityUserManager.SetLockoutEndDateAsync(identityUser, DateTimeOffset.MaxValue);
                if (!lockoutResult.Succeeded)
                {
                    throw new UserFriendlyException("Failed to set user status: " +
                        string.Join(", ", lockoutResult.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                // Make sure the user is active
                await _identityUserManager.SetLockoutEnabledAsync(identityUser, false);
                var lockoutResult = await _identityUserManager.SetLockoutEndDateAsync(identityUser, null);
                if (!lockoutResult.Succeeded)
                {
                    throw new UserFriendlyException("Failed to set user status: " +
                        string.Join(", ", lockoutResult.Errors.Select(e => e.Description)));
                }
            }

            // Create business user
            var user = new User(
                GuidGenerator.Create(),
                nickName,
                identityUser.Id,
                contactInfo,
                position
            );

            // Save user
            await _userRepository.InsertAsync(user, true);

            _logger.LogInformation($"New user created: {nickName}, ID: {user.Id}");

            return user;
        }

        /// <summary>
        /// Update user information
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="nickName">User nickname</param>
        /// <param name="password">User password (optional, pass in null if not modified)</param>
        /// <param name="contactInfo">Contact information</param>
        /// <param name="position">Position</param>
        /// <param name="isActive">Whether to enable</param>
        /// <returns>Updated user entity</returns>
        public async Task<User> UpdateAsync(
            Guid id,
            string nickName,
            string password,
            string contactInfo = null,
            string position = null,
            bool isActive = true)
        {
            // Maximum number of retries
            const int maxRetries = 3;
            int retryCount = 0;

            while (true)
            {
                try
                {
                    // Re-fetch the latest user data on each try
                    var user = await _userRepository.GetAsync(id, false);
                    if (user == null)
                    {
                        throw new UserFriendlyException("User does not exist");
                    }
                    
                    // Check if the user's nickname is already used by other users
                    var existingUser = await _userRepository.FindAsync(u => u.NickName == nickName && u.Id != id);
                    if (existingUser != null)
                    {
                        throw new UserFriendlyException($"A user with the nickname '{nickName}' already exists");
                    }

                    // Update Identity user
                    var identityUser = await _identityUserManager.FindByIdAsync(user.IdentityUserId.ToString());
                    if (identityUser == null)
                    {
                        throw new UserFriendlyException("Identity user does not exist");
                    }

                    // Update username
                    var usernameResult = await _identityUserManager.SetUserNameAsync(identityUser, nickName);
                    if (!usernameResult.Succeeded)
                    {
                        throw new UserFriendlyException("Failed to update username: " +
                            string.Join(", ", usernameResult.Errors.Select(e => e.Description)));
                    }

                    // update email
                    var emailResult = await _identityUserManager.SetEmailAsync(identityUser, $"{nickName}@inspection.com");
                    if (!emailResult.Succeeded)
                    {
                        throw new UserFriendlyException("Failed to update email: " +
                            string.Join(", ", emailResult.Errors.Select(e => e.Description)));
                    }

                    // If a new password is provided, update the password
                    if (!string.IsNullOrEmpty(password))
                    {
                        // Remove current password
                        await _identityUserManager.RemovePasswordAsync(identityUser);
                        // Set new password
                        var passwordResult = await _identityUserManager.AddPasswordAsync(identityUser, password);
                        if (!passwordResult.Succeeded)
                        {
                            throw new UserFriendlyException("Failed to update password: " +
                                string.Join(", ", passwordResult.Errors.Select(e => e.Description)));
                        }
                    }

                    // Set user status
                    await SetUserActiveStatusAsync(id, isActive);

                    // Update user information
                    user.NickName = nickName;
                    user.ContactInfo = contactInfo;
                    user.Position = position;

                    // save updates
                    await _userRepository.UpdateAsync(user, true);

                    _logger.LogInformation($"Updated user information: {nickName}, ID: {user.Id}");

                    return user;
                }
                catch (Volo.Abp.Data.AbpDbConcurrencyException ex)
                {
                    // Increase retry count
                    retryCount++;

                    // If the maximum number of retries is reached, throw a user-friendly exception
                    if (retryCount >= maxRetries)
                    {
                        throw new UserFriendlyException(
                            "Failed to update user information: the data has been modified by other users. Please refresh the page and try again.",
                            "409", ex.Message,
                            ex);
                    }

                    // Retry after a short delay
                    await Task.Delay(100 * retryCount); // Gradually increase the delay time

                    // Record retry information
                    _logger.LogWarning($"A concurrency conflict was detected when user [{id}] was updated, and the {retryCount}th retry is in progress...");
                }
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>Operation tasks</returns>
        public async Task DeleteAsync(Guid id)
        {
            // Maximum number of retries
            const int maxRetries = 3;
            int retryCount = 0;

            while (true)
            {
                try
                {
                    // Re-fetch the latest user data on each try
                    var user = await _userRepository.GetAsync(id);
                    if (user == null)
                    {
                        _logger.LogWarning($"Trying to delete a non-existent user, ID: {id}");
                        return; // If the user does not exist, no further processing will be performed
                    }

                    // Delete Identity user
                    var identityUser = await _identityUserManager.FindByIdAsync(user.IdentityUserId.ToString());
                    if (identityUser != null)
                    {
                        var result = await _identityUserManager.DeleteAsync(identityUser);
                        if (!result.Succeeded)
                        {
                            throw new UserFriendlyException("Failed to delete Identity user: " +
                                string.Join(", ", result.Errors.Select(e => e.Description)));
                        }
                    }

                    // delete user
                    await _userRepository.DeleteAsync(user);

                    _logger.LogInformation($"Deleted user, ID: {id}");
                    return;
                }
                catch (Volo.Abp.Data.AbpDbConcurrencyException ex)
                {
                    // Increase retry count
                    retryCount++;

                    // If the maximum number of retries is reached, throw a user-friendly exception
                    if (retryCount >= maxRetries)
                    {
                        throw new UserFriendlyException(
                            "Failed to delete user: data has been modified by other users. Please refresh the page and try again.",
                            "409", ex.Message,
                            ex);
                    }

                    // Retry after a short delay
                    await Task.Delay(100 * retryCount); // Gradually increase the delay time

                    // Record retry information
                    _logger.LogWarning($"A concurrency conflict was detected when user [{id}] was deleted, and the {retryCount}th retry is in progress...");
                }
            }
        }

        /// <summary>
        /// Modify user password
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="currentPassword">Current password</param>
        /// <param name="newPassword">New password</param>
        /// <returns>Operation results</returns>
        public async Task ChangePasswordAsync(Guid id, string currentPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 6)
            {
                throw new UserFriendlyException("The new password cannot be empty and the length cannot be less than 6 characters");
            }

            // Get user
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                throw new UserFriendlyException("User does not exist");
            }

            // Get the Identity user
            var identityUser = await _identityUserManager.FindByIdAsync(user.IdentityUserId.ToString());
            if (identityUser == null)
            {
                throw new UserFriendlyException("Identity user does not exist");
            }

            // Change password
            var result = await _identityUserManager.ChangePasswordAsync(identityUser, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                throw new UserFriendlyException("Failed to change password: " +
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            _logger.LogInformation($"User [{user.NickName}] successfully changed the password");
        }

        /// <summary>
        /// Reset user password (administrator operation)
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="newPassword">New password</param>
        /// <returns>Operation results</returns>
        public async Task ResetPasswordAsync(Guid id, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword) || newPassword.Length < 6)
            {
                throw new UserFriendlyException("The new password cannot be empty and the length cannot be less than 6 characters");
            }

            // Get user
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                throw new UserFriendlyException("User does not exist");
            }

            // Get the Identity user
            var identityUser = await _identityUserManager.FindByIdAsync(user.IdentityUserId.ToString());
            if (identityUser == null)
            {
                throw new UserFriendlyException("Identity user does not exist");
            }

            // Generate reset token
            var token = await _identityUserManager.GeneratePasswordResetTokenAsync(identityUser);

            // reset password
            var result = await _identityUserManager.ResetPasswordAsync(identityUser, token, newPassword);
            if (!result.Succeeded)
            {
                throw new UserFriendlyException("Password reset failed: " +
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            _logger.LogInformation($"The administrator reset the password of user [{user.NickName}]");
        }

        /// <summary>
        /// Get user information
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>User entity</returns>
        public async Task<User> GetAsync(Guid id)
        {
            return await _userRepository.GetAsync(id);
        }

        /// <summary>
        /// Get the user based on the Identity user ID
        /// </summary>
        /// <param name="identityUserId">Identity user ID</param>
        /// <returns>User entity, return null if it does not exist</returns>
        public async Task<User> FindByIdentityUserIdAsync(Guid identityUserId)
        {
            return await _userRepository.FindAsync(u => u.IdentityUserId == identityUserId);
        }

        /// <summary>
        /// Find users based on their nicknames
        /// </summary>
        /// <param name="nickName">User nickname</param>
        /// <returns>User entity, return null if it does not exist</returns>
        public async Task<User> FindByNickNameAsync(string nickName)
        {
            return await _userRepository.FindAsync(u => u.NickName == nickName);
        }

        /// <summary>
        /// Disable or enable user
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="isActive">Whether to enable</param>
        /// <returns>Operation tasks</returns>
        public async Task SetUserActiveStatusAsync(Guid id, bool isActive)
        {
            // Get user
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                throw new UserFriendlyException("User does not exist");
            }

            // Get the Identity user
            var identityUser = await _identityUserManager.FindByIdAsync(user.IdentityUserId.ToString());
            if (identityUser == null)
            {
                throw new UserFriendlyException("Identity user does not exist");
            }

            // Set user status
            if (isActive)
            {
                // enable user
                var result = await _identityUserManager.SetLockoutEndDateAsync(identityUser, null);
                if (!result.Succeeded)
                {
                    throw new UserFriendlyException($"Failed to enable user: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
            else
            {
                // Disable user (set permanent lock)
                var result = await _identityUserManager.SetLockoutEndDateAsync(identityUser, DateTimeOffset.MaxValue);
                if (!result.Succeeded)
                {
                    throw new UserFriendlyException($"Failed to disable user: " +
                        string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }

            _logger.LogInformation($"{(isActive ? "Enabled" : "Disabled")}user [{user.NickName}]");
        }
    }
}