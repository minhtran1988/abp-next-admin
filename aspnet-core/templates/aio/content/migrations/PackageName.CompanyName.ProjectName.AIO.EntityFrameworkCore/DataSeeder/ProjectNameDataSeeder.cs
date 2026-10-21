using Microsoft.Extensions.Logging;
using PackageName.CompanyName.ProjectName.Users;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
using IdentityRole = Volo.Abp.Identity.IdentityRole;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace PackageName.CompanyName.ProjectName.AIO.EntityFrameworkCore.DataSeeder
{
    public class ProjectNameDataSeeder : IProjectNameDataSeeder, ITransientDependency
    {
        private readonly ICurrentTenant _currentTenant;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ILogger<ProjectNameDataSeeder> _logger;
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IdentityUserManager _identityUserManager;
        private readonly IdentityRoleManager _identityRoleManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// Constructor
        /// </summary>
        public ProjectNameDataSeeder(
            ICurrentTenant currentTenant,
            IGuidGenerator guidGenerator,
            ILogger<ProjectNameDataSeeder> logger,
            IRepository<User, Guid> userRepository,
            IdentityUserManager identityUserManager,
            IdentityRoleManager identityRoleManager,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _currentTenant = currentTenant;
            _guidGenerator = guidGenerator;
            _logger = logger;
            _userRepository = userRepository;
            _identityUserManager = identityUserManager;
            _identityRoleManager = identityRoleManager;
            _unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// Initialize data
        /// </summary>
        /// <param name="context">Data seed context</param>
        /// <returns>Task</returns>
        public async Task SeedAsync(DataSeedContext context)
        {
            using (_currentTenant.Change(context.TenantId))
            {
                _logger.LogInformation("Start initializing data...");

                // Initialize role
                using (var uow = _unitOfWorkManager.Begin(requiresNew: true))
                {
                    await SeedRolesAsync();
                    await uow.CompleteAsync();
                }

                // Initialize user data
                using (var uow = _unitOfWorkManager.Begin(requiresNew: true))
                {
                    await SeedUsersAsync();
                    await uow.CompleteAsync();
                }

                _logger.LogInformation("Data initialization completed");
            }
        }

        /// <summary>
        /// Initialize character data
        /// </summary>
        private async Task SeedRolesAsync()
        {
            // super administrator
            await CreateRoleIfNotExistsAsync(
                "Super Administrator",
                "System super administrator, has all permissions");
        }

        /// <summary>
        /// Create the role (if it does not exist)
        /// </summary>
        private async Task CreateRoleIfNotExistsAsync(string roleName, string description)
        {
            if (await _identityRoleManager.FindByNameAsync(roleName) == null)
            {
                var role = new IdentityRole(
                    _guidGenerator.Create(),
                    roleName,
                    _currentTenant.Id)
                {
                    IsStatic = true,
                    IsPublic = true
                };

                await _identityRoleManager.CreateAsync(role);

                _logger.LogInformation($"Create role: {roleName}");
            }
        }

        /// <summary>
        /// Initialize user data
        /// </summary>
        private async Task SeedUsersAsync()
        {

            // Find the super administrator role
            var superAdminRole = await _identityRoleManager.FindByNameAsync("Super Administrator");
            if (superAdminRole == null)
            {
                _logger.LogError("The super administrator role was not found and the role cannot be assigned to the user");
                return;
            }

            // Create user data (use fixed username to avoid generation problems)
            await CreateUserIfNotExistsAsync("user1", "user1", "Super Administrator");
            await CreateUserIfNotExistsAsync("user2", "user2", "Super Administrator");
            await CreateUserIfNotExistsAsync("user3", "user3", "Super Administrator");
            await CreateUserIfNotExistsAsync("user4", "user4", "Super Administrator");
        }

        /// <summary>
        /// Create user (if does not exist)
        /// </summary>
        private async Task CreateUserIfNotExistsAsync(
            string name,
            string userName,
            string roles)
        {
            // Check if the user already exists
            var existingUser = await _userRepository.FindAsync(u => u.NickName == name);
            if (existingUser != null)
            {
                _logger.LogInformation($"User [{name}] already exists, skip creation");
                return;
            }

            var identityUser = await _identityUserManager.FindByNameAsync(userName);
            if (identityUser == null)
            {
                // Create Identity user
                identityUser = new IdentityUser(
                    _guidGenerator.Create(),
                    userName,
                    $"{userName}@example.com",
                    _currentTenant.Id)
                {
                    Name = name,
                    Surname = ""
                };

                // Set default password 123456
                var identityResult = await _identityUserManager.CreateAsync(identityUser, "123456");
                if (!identityResult.Succeeded)
                {
                    _logger.LogError($"Failed to create Identity user [{name}]: {string.Join(", ", identityResult.Errors.Select(e => e.Description))}");
                    return;
                }

                // Assign roles
                if (!string.IsNullOrWhiteSpace(roles))
                {
                    var roleNames = roles.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach (var roleName in roleNames)
                    {
                        var trimmedRoleName = roleName.Trim();
                        var role = await _identityRoleManager.FindByNameAsync(trimmedRoleName);
                        if (role != null)
                        {
                            var roleResult = await _identityUserManager.AddToRoleAsync(identityUser, trimmedRoleName);
                            if (!roleResult.Succeeded)
                            {
                                _logger.LogWarning($"Assigning role [{trimmedRoleName}] to user [{name}] failed: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                            }
                        }
                        else
                        {
                            _logger.LogWarning($"The role [{trimmedRoleName}] does not exist and cannot be assigned to user [{name}]");
                        }
                    }
                }

                // Create system user
                var user = new User(
                    _guidGenerator.Create(),
                    name,
                    identityUser.Id);

                // Save user
                await _userRepository.InsertAsync(user);

                _logger.LogInformation($"Create user: {name}, user name: {userName}");
            }
            else
            {
                _logger.LogInformation($"Identity user [{userName}] already exists, check whether you need to create a business user");

                // Check whether a business user needs to be created
                var businessUser = await _userRepository.FindAsync(u => u.IdentityUserId == identityUser.Id);
                if (businessUser == null)
                {
                    // Create system user
                    var user = new User(
                        _guidGenerator.Create(),
                        name,
                        identityUser.Id);

                    // Save user
                    await _userRepository.InsertAsync(user);

                    _logger.LogInformation($"Create a business user for the existing Identity user: {name}");
                }
            }
        }
    }
}
