using Microsoft.Extensions.DependencyInjection;
using PackageName.CompanyName.ProjectName.AIO.EntityFrameworkCore.DataSeeder;
using PackageName.CompanyName.ProjectName.Users;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Uow;
using Xunit;

namespace PackageName.CompanyName.ProjectName.DataSeeder
{
    /// <summary>
    /// Data seed initialization test
    /// </summary>
    [Collection("Database")]
    public class ProjectNameDataSeederTests : ProjectNameApplicationTestBase
    {
        private readonly IProjectNameDataSeeder _inspectionDataSeeder;
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IIdentityRoleRepository _identityRoleRepository;
        private readonly IIdentityUserRepository _identityUserRepository;

        public ProjectNameDataSeederTests()
        {
            _inspectionDataSeeder = GetRequiredService<IProjectNameDataSeeder>();
            _userRepository = GetRequiredService<IRepository<User, Guid>>();
            _identityRoleRepository = GetRequiredService<IIdentityRoleRepository>();
            _identityUserRepository = GetRequiredService<IIdentityUserRepository>();
        }

        [Fact]
        public async Task Should_Seed_Data_Successfully()
        {
            // Arrange
            var context = new DataSeedContext();

            // Act
            await _inspectionDataSeeder.SeedAsync(context);

            // Assert - wraps all database operations using unit work methods
            await WithUnitOfWorkAsync(async () =>
            {
                // test character
                var roles = await _identityRoleRepository.GetListAsync();
                roles.Count.ShouldBeGreaterThanOrEqualTo(7); // There should be at least 7 roles

                var superAdminRole = await _identityRoleRepository.FindByNormalizedNameAsync("Super Administrator".ToUpperInvariant());
                superAdminRole.ShouldNotBeNull();
                // test user
                var users = await _userRepository.GetListAsync();
                users.Count.ShouldBeGreaterThanOrEqualTo(10); // There should be at least 10 users

                foreach (var user in users)
                {
                    user.IdentityUserId.ShouldNotBe(Guid.Empty);

                    var identityUser = await _identityUserRepository.GetAsync(user.IdentityUserId);
                    identityUser.ShouldNotBeNull();
                }
                
                return true;
            });
        }

        [Theory]
        [InlineData("Super Administrator")]
        [InlineData("Normal User")]
        public async Task Should_Create_Roles(string roleName)
        {
            // Arrange
            var context = new DataSeedContext();
            await _inspectionDataSeeder.SeedAsync(context);

            // Act & Assert - wrapper using unit worker method
            await WithUnitOfWorkAsync(async () =>
            {
                var role = await _identityRoleRepository.FindByNormalizedNameAsync(roleName.ToUpperInvariant());
                role.ShouldNotBeNull();
                role.Name.ShouldBe(roleName);
                
                return true;
            });
        }

        [Theory]
        [InlineData("testuser1")]
        [InlineData("testuser2")]
        public async Task Should_Create_Users(string nickName)
        {
            // Arrange
            var context = new DataSeedContext();
            await _inspectionDataSeeder.SeedAsync(context);

            // Act & Assert - wrapper using unit worker method
            await WithUnitOfWorkAsync(async () =>
            {
                var users = await _userRepository.GetListAsync();
                var user = users.FirstOrDefault(u => u.NickName == nickName);
                user.ShouldNotBeNull();
                user.NickName.ShouldBe(nickName);
                user.IdentityUserId.ShouldNotBe(Guid.Empty);

                var identityUser = await _identityUserRepository.GetAsync(user.IdentityUserId);
                identityUser.ShouldNotBeNull();
                identityUser.Name.ShouldBe(nickName);
                
                return true;
            });
        }
        
        // Add unit work method
        protected override Task<TResult> WithUnitOfWorkAsync<TResult>(Func<Task<TResult>> func)
        {
            return WithUnitOfWorkAsync(new AbpUnitOfWorkOptions(), func);
        }
        
        // Optional: Add overloaded methods to support more scenarios
        protected async override Task<TResult> WithUnitOfWorkAsync<TResult>(AbpUnitOfWorkOptions options, Func<Task<TResult>> func)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var uowManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();

                using (var uow = uowManager.Begin(options))
                {
                    var result = await func();
                    await uow.CompleteAsync();
                    return result;
                }
            }
        }
    }
}
